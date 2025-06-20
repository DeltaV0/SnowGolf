using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Globals : MonoBehaviour
{
	public Transform[] points;

	public Checkpoint[] checkpoints;

	public DynamicBlock[] dynamicstuff;
	public button[] buttons;

	public float WarpSpeed;

	public static Globals me;

	public int checkpoint;

	public bool stop;

	public GameObject bg1;
	public GameObject bg2;

	public Transform Player;

	public AudioSource Shatter;

    public AudioSource[] beeps;

    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
		WarpSpeed = 1f;
		stop = false;
		if (PlayerPrefs.GetInt ("Checkpoint") == 0) {
			PlayerPrefs.SetInt("Checkpoint", 1);
		}
		checkpoint = PlayerPrefs.GetInt ("Checkpoint");
		me = this;

    }

	public void ResetAll() {
		for (int i = 0; i < dynamicstuff.Length; i++) {
			dynamicstuff[i].Reset();

        }
		for (int i = 0; i < buttons.Length; i++) {
			buttons[i].Pressed = false;
			buttons[i].a = true;

        }

    }

    // Update is called once per frame
    void Update()
    {
		bg1.transform.position = new Vector3((Player.position.x * 0.1f),(Player.position.y * 0.1f), 0);
		bg2.transform.position = new Vector3((Player.position.x * 0.3f),(Player.position.y * 0.3f), 0);
    }

	public void shatter(){
		Shatter.pitch = Random.Range(0.8f, 1.2f);
			Shatter.Play();
	}

    public void beep(int i)
    {
		beeps[i].Play();
    }
}
