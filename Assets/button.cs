using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class button : MonoBehaviour
{
	public Transform buttonTransform;
	public bool Pressed;

  public bool Toggle;

  public bool a;

  private int timer;
    // Start is called before the first frame update
    void Start()
    {
      timer = 50;
      a = true;
    }

    // Update is called once per frame
    void Update()
    {
      if(!Toggle){
		      Pressed = buttonTransform.localPosition.y > -0.05f;
      } else {
          if(!Pressed && buttonTransform.localPosition.y > -0.05f && a && timer <= 0){
            timer = 50;
            Pressed = true;
            a = false;
          }
          if(buttonTransform.localPosition.y <= -0.05f){
            a = true;
          }
          if(Pressed && buttonTransform.localPosition.y > -0.05f && a && timer <= 0){
            timer = 50;
            Pressed = false;
            a = false;
          }
      }
    }

    private void FixedUpdate(){
      if(timer > 0){
        timer--;
      }


    }
}
