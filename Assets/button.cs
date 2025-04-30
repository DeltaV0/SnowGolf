using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class button : MonoBehaviour
{
	public Transform buttonTransform;
	public bool Pressed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		Pressed = buttonTransform.localPosition.y > -0.05f;
    }
}
