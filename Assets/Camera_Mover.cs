using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Mover : MonoBehaviour
{
	public Transform trans;

	private float DT;

	public Camera cam;
    // Start is called before the first frame update
    void Start()
    {
		DT = Time.fixedDeltaTime;
    }

    // Update is called once per frame
    void Update()
    {
		if(!PauseMenu.Paused){
		Time.fixedDeltaTime = DT * Time.timeScale;
		}

    }

	private void FixedUpdate(){
		if(!Globals.me.stop){
			if(!PauseMenu.Paused){
		transform.position = Vector3.Lerp(transform.position, new Vector3(trans.position.x, trans.position.y, transform.position.z), 0.04f);
			if(Vector2.Distance(new Vector2(transform.position.x, transform.position.y), new Vector2(trans.position.x, trans.position.y)) > 5){
				cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, Vector2.Distance(new Vector2(transform.position.x, transform.position.y), new Vector2(trans.position.x, trans.position.y)), 0.04f);
			} else {
				cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, 5f, 0.04f);;
			}

			}
		}
	}
}
