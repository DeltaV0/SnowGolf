using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

	public AudioLowPassFilter filter;

	public AudioSource[] music;

	public AudioSource jump;

	public int invtimer;

	public bool njk;
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
		//Debug.Log (PlayerPrefs.GetInt ("Checkpoint") - 1);
		//transform.position = Globals.me.points[1].position;
		//Vector2 dir = (Vector2)transform.position - new Vector2(-13, 5);
		//rb.velocity = Vector2.ClampMagnitude(dir * 5, maxPower);
    }

    // Update is called once per frame
    void Update()
    {
		//thing.connectedBody = platform;
		if (njk) {
			Debug.Log (PlayerPrefs.GetInt ("Checkpoint") - 1);
			transform.position = Globals.me.points[PlayerPrefs.GetInt ("Checkpoint") - 1].position;
			njk = false;
		}
		if(invtimer > 0){
			invtimer--;
		}
		if(Input.GetKey("r") && invtimer <= 0){
			invtimer = 15;
			transform.position = Globals.me.points[Globals.me.checkpoint - 1].position;
			Globals.me.checkpoints[Globals.me.checkpoint - 1].replaceLevel();
			Globals.me.checkpoints[Globals.me.checkpoint - 1].refuel(this);
			rb.velocity = new Vector2(0, 0);
		}
		if(!Globals.me.stop){
		PlInput();
		if(dragging){
				if(!PauseMenu.Paused){
			Time.timeScale = Mathf.Lerp(Time.timeScale, 0.05f, 0.03f);
				}
			//filter.cutoffFrequency = Mathf.Lerp(filter.cutoffFrequency, 200f, 0.03f);
				music[0].pitch = Mathf.Lerp(music[0].pitch, 0.8f, 0.03f);
				music[1].pitch = Mathf.Lerp(music[1].pitch, 0.8f, 0.03f);
		} else {
				if(!PauseMenu.Paused){
			Time.timeScale = Mathf.Lerp(Time.timeScale, 1f, 0.1f);
				}
			//filter.cutoffFrequency = Mathf.Lerp(filter.cutoffFrequency, 22000f, 0.1f);
				music[0].pitch = Mathf.Lerp(music[0].pitch, 1f, 0.1f);
				music[1].pitch = Mathf.Lerp(music[1].pitch, 1f, 0.1f);
		}
			/*if(grounded){
				if(!chargepart.isPlaying){
					chargepart.Play();
				}
			} else {
				chargepart.Stop();
			}*/
			if(fuel <= 0 && invtimer <= 0){
				invtimer = 5;
				transform.position = Globals.me.points[Globals.me.checkpoint - 1].position;
				Globals.me.checkpoints[Globals.me.checkpoint - 1].replaceLevel();
				Globals.me.checkpoints[Globals.me.checkpoint - 1].refuel(this);
				rb.velocity = new Vector2(0, 0);

			}
		}
		fuelLeft.fillAmount = (float)fuel / maxfuel;
    }

	public void PlInput(){
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

	//public bool IsReady(){
		//return rb.velocity.magnitude <= 0.2f;
	//}

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
		} else if(collision.gameObject.name.Equals("Death") && invtimer <= 0){
			invtimer = 5;
			transform.position = Globals.me.points[Globals.me.checkpoint - 1].position;
			Globals.me.checkpoints[Globals.me.checkpoint - 1].replaceLevel();
			Globals.me.checkpoints[Globals.me.checkpoint - 1].refuel(this);
			rb.velocity = new Vector2(0, 0);
		}// else if(collision.gameObject.name.Equals("Dynamic")){
			//this.transform.parent = collision.transform.root;
		//}
	}

	void OnCollisionExit2D(Collision2D collision){
		//if(collision.gameObject.name.Equals("Dynamic")){
			//this.transform.parent = null;
		//}
	}



	private void FixedUpdate(){

		if(grounded){
					chargepart.Play();
			}


		fuel -= Time.timeScale;
		LayerMask layermask;
		layermask = LayerMask.GetMask("Default");
		Collider2D[] col = Physics2D.OverlapCircleAll(new Vector2(transform.position.x,transform.position.y), 0.5f, layermask);
	if(col.Length > 0){
		for(int i = 0; i < col.Length; i++){
			if(col[i].name.Equals("Platform")){
					this.transform.parent = col[i].transform.root;
					grounded = true;
				}  else if(col[i].name.Equals("Steel")){
					this.transform.parent = col[i].transform.root;
					grounded = true;
				} else if(col[i].name.Equals("Checkpoint")){
					this.transform.parent = col[i].transform.root;
					grounded = true;
				} else if(col[i].name.Equals("Boost")){
					this.transform.parent = col[i].transform.root;
					grounded = true;
				} else if(col[i].name.Equals("Dynamic")){
					this.transform.parent = col[i].transform.root;
					grounded = true;
				} else if(col[i].name.Equals("Button")){
					this.transform.parent = col[i].transform.root;
					grounded = true;
				}
		}
	}
		if(col.Length == 0){
			this.transform.parent = null;
		}
	}
}
