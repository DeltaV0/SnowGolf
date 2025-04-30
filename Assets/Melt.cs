using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melt : MonoBehaviour
{
	public Transform trans;

	//public List<Collider2D> Colliding = new List<Collider2D>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		transform.position = trans.position; 

    }

	private void FixedUpdate(){
		Collider2D[] col = Physics2D.OverlapCircleAll(new Vector2(transform.position.x,transform.position.y), 0.8f);
		if(col.Length > 0){
			for(int i = 0; i < col.Length; i++){
				if(col[i].name.Equals("Platform")){
					Platform collided = col[i].gameObject.GetComponent<Platform>();
					collided.health -= 1;
				}
			}
		}
	}

	/*void OnTriggerStay2D(Collider2D col){
		if(col.name.Equals("Platform")){
			//Colliding.
			Platform collided = col.gameObject.GetComponent<Platform>();
			collided.health -= 1;
		}
	}*/
}
