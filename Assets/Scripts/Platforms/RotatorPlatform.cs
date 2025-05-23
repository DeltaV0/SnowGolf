using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatorPlatform : MonoBehaviour
{
    public float speed;
    public Transform otherObj;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        otherObj.Rotate(0, 0, -1 * speed * Globals.me.WarpSpeed);
        transform.Rotate(0, 0, 1 * speed * Globals.me.WarpSpeed);
    }
}
