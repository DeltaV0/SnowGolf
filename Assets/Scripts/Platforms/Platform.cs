using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
	public int health;
	public int maxHealth;
	public SpriteRenderer spr;

	public GameObject breakparts;
    // Start is called before the first frame update
    void Start()
    {
		health = maxHealth;
		spr.color = new Color(0.3f, 0.8f, 0.9f, 0.6f);
    }

    // Update is called once per frame
    void Update()
    {
		if(health <= 0){
			GameObject bullet = (GameObject)Instantiate (breakparts, transform.position, transform.rotation);
			Globals.me.shatter();
			Destroy(gameObject);
		}
		spr.color = new Color(0.3f, 0.8f, 0.9f, (0.5f * health / maxHealth) + 0.1f);
    }
}
