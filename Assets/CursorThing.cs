using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorThing : MonoBehaviour
{
    public float mouseCursorSpeed;

    //public float target;
    // Start is called before the first frame update
    void Start()
    {
       // target = 0f;
    }

    private void FixedUpdate()
    {
        mouseCursorSpeed = (Input.GetAxis("Mouse X") / Time.deltaTime) * Globals.me.WarpSpeed;
        //target += mouseCursorSpeed * 0.2f;
        transform.position = Vector3.Lerp(transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition), 0.75f);
        //transform.rotation = Quaternion.Euler(0, 0, Mathf.Lerp(transform.rotation.z, target, 0.1f));
        transform.Rotate(0, 0, mouseCursorSpeed * 0.2f);
    }

    }
