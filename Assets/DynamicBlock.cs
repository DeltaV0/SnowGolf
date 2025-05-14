using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicBlock : MonoBehaviour
{

	public button[] Buttons;

	public int[] Functions;

	public float[] Vars;

	public Vector3 origPos;

    // Start is called before the first frame update
    void Start()
    {
		origPos = transform.position;
    }

	public void Reset() { 
	transform.position = origPos;
	}

    // Update is called once per frame
    void FixedUpdate()
    {
		for(int i = 0; i<Buttons.Length; i++)
		if(Buttons[i].Pressed){
			Run (Functions[i]);
		}
    }

	void Run(int num){
		switch(num){
		case 0:
			if(transform.position.x >= Vars[0]){
			transform.Translate(new Vector3(0.025f, 0, 0));
			}
			break;
		case 1:
			if(transform.position.x <= Vars[1]){
				transform.Translate(new Vector3(-0.025f, 0, 0));
			}
			//transform.Translate(new Vector3(-0.025f, 0, 0));
			break;
		}

	}
}
