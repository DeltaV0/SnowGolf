using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Ball : MonoBehaviour
{
	public Rigidbody2D rb;
	public LineRenderer lr;

	//public Rigidbody2D platform;

	//public FrictionJoint2D thing;

	public float maxPower = 12f;
	public float power = 4f;

	public bool dragging;

	public bool grounded;

	public float fuel;

	public float maxfuel;

	public Image fuelLeft;

	public ParticleSystem parts;

	public ParticleSystem chargepart;
	public ParticleSystem die;

	public AudioLowPassFilter filter;

	public AudioSource[] music;

	public AudioSource jump;

    public AudioSource deathsfx;

    public AudioSource[] landsfx;

    public AudioSource WindFast;

    public int invtimer;

	public bool njk;

	public GameObject currentParent;

	public Animator DeathCounter;

	public TMP_Text deathtext;
	//public List<Platform> Colliding = new List<Platform>();
    // Start is called before the first frame update
    void Start()
    {
		lr.positionCount = 0;
		Time.timeScale = 1f;
		fuel = 8000;
		maxfuel = 8000;
		filter.cutoffFrequency = 22000f;
		music[0].pitch = 1f;
		music[1].pitch = 1f;
		grounded = false;
		njk = true;
    }

	void Die() {
		DeathCounter.SetTrigger("Die");
        PlayerPrefs.SetInt("Deaths", PlayerPrefs.GetInt("Deaths") + 1);
        deathtext.text = PlayerPrefs.GetInt("Deaths") + "";
        deathsfx.Play();
        Globals.me.ResetAll();
        die.Play();
        this.transform.parent = null;
        invtimer = 15;
        transform.position = Globals.me.points[Globals.me.checkpoint - 1].position;
        Globals.me.checkpoints[Globals.me.checkpoint - 1].replaceLevel();
        Globals.me.checkpoints[Globals.me.checkpoint - 1].refuel(this);
        rb.velocity = new Vector2(0, 0);
    }

    // Update is called once per frame
    void Update()
    {
		WindFast.volume = Mathf.Clamp(rb.velocity.magnitude * 0.01f * PlayerPrefs.GetFloat("Volume"), 0f, 1f * PlayerPrefs.GetFloat("Volume"));
        //thing.connectedBody = platform;
        transform.localRotation  = Quaternion.identity;
		//transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);
		if (njk) {
			Debug.Log (PlayerPrefs.GetInt ("Checkpoint") - 1);
			transform.position = Globals.me.points[PlayerPrefs.GetInt ("Checkpoint") - 1].position;
			njk = false;
		}
		if(invtimer > 0){
			invtimer--;
		}
		if(Input.GetKey("r") && invtimer <= 0){
			Die();
		}
		if(Input.GetKey("p") && invtimer <= 0){
			//undo this when testing             ^^^^^
			if(Globals.me.checkpoint < Globals.me.checkpoints.Length ){
			Globals.me.checkpoint++;
			} else {
				Globals.me.checkpoint = 1;
			}
			Die();
		}
		PlInput();
		if(dragging){
				if(!PauseMenu.Paused){
			Time.timeScale = Mathf.Lerp(Time.timeScale, 0.05f, 0.03f);
			Globals.me.WarpSpeed = Mathf.Lerp(Globals.me.WarpSpeed, 0.05f, 0.03f);
				}
			//filter.cutoffFrequency = Mathf.Lerp(filter.cutoffFrequency, 200f, 0.03f);
				music[0].pitch = Mathf.Lerp(music[0].pitch, 0.8f, 0.03f);
				music[1].pitch = Mathf.Lerp(music[1].pitch, 0.8f, 0.03f);
		} else {
				if(!PauseMenu.Paused){
			Time.timeScale = Mathf.Lerp(Time.timeScale, 1f, 0.1f);
			Globals.me.WarpSpeed = Mathf.Lerp(Globals.me.WarpSpeed, 1f, 0.03f);
				}
			//filter.cutoffFrequency = Mathf.Lerp(filter.cutoffFrequency, 22000f, 0.1f);
				music[0].pitch = Mathf.Lerp(music[0].pitch, 1f, 0.1f);
				music[1].pitch = Mathf.Lerp(music[1].pitch, 1f, 0.1f);
		}
			if(fuel <= 0 && invtimer <= 0){
                Die();

            }
		fuelLeft.fillAmount = (float)fuel / maxfuel;
    }

	public void PlInput(){
		if(!PauseMenu.Paused){
		if(!grounded){ return;} 
		

		Vector2 inputPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		float dist = Vector2.Distance(transform.position, inputPos);

		if(Input.GetMouseButtonDown(0)){
			DragStart();
		}
		if(Input.GetMouseButton(0) && dragging){
			DragChange(inputPos);
		}
		if(Input.GetMouseButtonUp(0) && dragging){
			DragEnd(inputPos);
		}
		}
	}


	public void DragStart(){
		lr.positionCount = 2;
		dragging = true;

	}
	public void DragChange(Vector2 pos){
		Vector2 dir = (Vector2)transform.position - pos;

		lr.SetPosition(0, transform.position);
		lr.SetPosition(1, (Vector2)transform.position + Vector2.ClampMagnitude((dir * power) / 2, maxPower / 2));
	}
	public void DragEnd(Vector2 pos){

		lr.positionCount = 0;
		float distance = Vector2.Distance((Vector2)transform.position, pos);
		dragging = false;


		if(distance < 0.25f){
			return;
		}
		jump.pitch = Random.Range(0.7f, 1.3f);
		jump.Play();
		parts.Play();
		grounded = false;
		Vector2 dir = (Vector2)transform.position - pos;
		rb.velocity = Vector2.ClampMagnitude(dir * power, maxPower);
	}

	void OnCollisionEnter2D(Collision2D collision){
		if(collision.gameObject.name.Equals("Platform") && rb.velocity.magnitude > 0.5f){
			Platform collided = collision.gameObject.GetComponent<Platform>();
			collided.health -= (int)rb.velocity.magnitude * 25;
            landsfx[0].pitch = Random.Range(0.5f, 0.9f);
            landsfx[0].Play();

        } else if(collision.gameObject.name.Equals("Death") && invtimer <= 0){
            Die();
        }
        else if ((collision.gameObject.name.Equals("Steel") || collision.gameObject.name.Equals("Dynamic")) && invtimer <= 0)
        {
            landsfx[1].pitch = Random.Range(0.5f, 0.9f);
            landsfx[1].Play();
        }
    }

	public void FindParent(string name) {
		while (this.transform.parent.name != name) {
			this.transform.parent = this.transform.parent.parent;

        }
	}

	private void GroundAndParent(Collider2D col){
		if(currentParent == null || Vector3.Distance(transform.position, col.transform.position) < Vector3.Distance(transform.position, currentParent.transform.position)){
						currentParent = col.transform.gameObject;
						this.transform.parent = col.transform.root;
					}
					grounded = true;
	}

	private void FixedUpdate(){

		if(grounded){
					chargepart.Play();
			}
		//???
		//if (DeathCounter.active) {
			//DeathCounter.SetActive(false);

       // }
		fuel -= Time.timeScale;
		LayerMask layermask;
		layermask = LayerMask.GetMask("Default");
		Collider2D[] col = Physics2D.OverlapCircleAll(new Vector2(transform.position.x,transform.position.y), 0.5f, layermask);
	if(col.Length > 0){
		for(int i = 0; i < col.Length; i++){
			if(col[i].name.Equals("Platform")){
					GroundAndParent(col[i]);
				}  else if(col[i].name.Equals("Steel")){
					GroundAndParent(col[i]);
				} else if(col[i].name.Equals("Checkpoint")){
					GroundAndParent(col[i]);
				} else if(col[i].name.Equals("Boost")){
					GroundAndParent(col[i]);
				} else if(col[i].name.Equals("Dynamic")){
					//special
					if(currentParent == null || Vector3.Distance(transform.position, col[i].transform.position) < Vector3.Distance(transform.position, currentParent.transform.position)){
						currentParent = col[i].transform.gameObject;
						this.transform.parent = col[i].transform;
					}
					grounded = true;
				} else if(col[i].name.Equals("Button")){
					//no buttons on rotating platform because scaling issues
					GroundAndParent(col[i]);
				} else if(col[i].name.Equals("Bouncy")){
					GroundAndParent(col[i]);
				}
		}
	}
		if(col.Length == 0){
			currentParent = null;
			this.transform.parent = null;
			transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);
		}
	}
}
