using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicBlock : MonoBehaviour
{

	public button[] Buttons;

	public int[] Functions;

	public int[] UnpressedFunctions;

	public float[] Vars;

	public bool[] boolvars;

	public Vector3 origPos;

	public float speed = 1;


	//public bool resetting;

    // Start is called before the first frame update
    void Start()
    {
		origPos = transform.position;
    }

	public void Reset() { 
	StartCoroutine(Reset2());
	}

	IEnumerator Reset2()
	{
		Vector3 endpos = transform.position;

    //Color c = renderer.material.color;
    for (float i = 0; i < 1; i += 0.01f)
    {
		transform.position = Vector3.Lerp(endpos, origPos, i);
        yield return new WaitForSeconds(.01f);
    }
	}

    // Update is called once per frame
    void FixedUpdate()
    {
		if(!PauseMenu.Paused){
		for(int i = 0; i<Buttons.Length; i++)
		if(Buttons[i].Pressed){
			Run (Functions[i]);
		} else {
			Run (UnpressedFunctions[i]);
		}
		}
    }

	void Run(int num){
		switch(num){
		case 0:
			if(transform.position.x >= Vars[0]){
			transform.Translate(new Vector3(0.05f * Globals.me.WarpSpeed * speed, 0, 0));
			}
			break;
		case 1:
			if(transform.position.x <= Vars[1]){
				transform.Translate(new Vector3(-0.05f * Globals.me.WarpSpeed * speed, 0, 0));
			}
			//transform.Translate(new Vector3(-0.025f, 0, 0));
			break;
		case 2:
			if(!boolvars[0]){
				if(transform.position.y <= Vars[0]){
					transform.Translate(new Vector3(0, 0.1f * Globals.me.WarpSpeed * speed, 0));
				}
			} else {
				if(transform.position.y >= Vars[0]){
					transform.Translate(new Vector3(0, 0.1f * Globals.me.WarpSpeed * speed, 0));
				}
			}
			break;
		case 3:
			if(!boolvars[0]){
				if(transform.position.y >= Vars[1]){
					transform.Translate(new Vector3(0, -0.1f * Globals.me.WarpSpeed * speed, 0));
				}
			} else {
				if(transform.position.y <= Vars[1]){
					transform.Translate(new Vector3(0, -0.1f * Globals.me.WarpSpeed * speed, 0));
				}
			}
			break;
		case 4:
			if(!boolvars[0]){
				if(transform.position.x <= Vars[0]){
					transform.Translate(new Vector3(0, 0.1f * Globals.me.WarpSpeed * speed, 0));
				}
			} else {
				if(transform.position.x >= Vars[0]){
					transform.Translate(new Vector3(0, 0.1f * Globals.me.WarpSpeed * speed, 0));
				}
			}
			break;

		}

	}
}
