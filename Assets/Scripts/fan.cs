using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fan : MonoBehaviour
{
	public bool reverse;

	public float angle;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	void OnTriggerStay2D(Collider2D col)
	{
		if (!reverse) {
			col.gameObject.GetComponent<Rigidbody2D> ().AddForce (new Vector2 (Mathf.Sin (angle * Mathf.Deg2Rad) * 20, Mathf.Cos (angle * Mathf.Deg2Rad) * 20));
		} else {
			col.gameObject.GetComponent<Rigidbody2D> ().AddForce (new Vector2 (Mathf.Sin (angle * Mathf.Deg2Rad) * -20, Mathf.Cos (angle * Mathf.Deg2Rad) * -20));
		}

	}
}
