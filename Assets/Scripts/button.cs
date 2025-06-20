using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class button : MonoBehaviour
{
	public Transform buttonTransform;
	public bool Pressed;

  public bool Toggle;

  public bool Logic;
  public int type;

  public button[] inputs;

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
    if (!Logic)
    {
      if (!Toggle)
      {
        Pressed = buttonTransform.localPosition.y > -0.05f;
      }
      else
      {
        if (!Pressed && buttonTransform.localPosition.y > -0.05f && a && timer <= 0)
        {
          timer = 50;
          Pressed = true;
          Globals.me.beep(0);
          a = false;
        }
        if (buttonTransform.localPosition.y <= -0.05f)
        {
          a = true;
        }
        if (Pressed && buttonTransform.localPosition.y > -0.05f && a && timer <= 0)
        {
          timer = 50;
          Pressed = false;
          Globals.me.beep(1);
          a = false;
        }
      }
    }
    else
    {
      if (type == 0)
      {
        Pressed = !inputs[0].Pressed;
      } else if (type == 1)
      {
        Pressed = true;
                for (int i = 0; i < inputs.Length; i++) {
                    if (!inputs[i].Pressed) {
                        Pressed = false;
                    }
                }
      }
    }
    }

    private void FixedUpdate(){
      if(timer > 0){
        timer--;
      }


    }
}
