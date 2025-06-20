using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Checkpoint : MonoBehaviour
{
	public int ID;

	public ParticleSystem part;

	public bool Active;

	public GameObject PrefabSection;

	public string name;

	public int fuelrefill;

	public bool winn;
    // Start is called before the first frame update
    void Start()
    {
		name = PrefabSection.name;
		if(ID != 1){
		part.Stop();
		} else {
			part.Play();
			//Globals.me.checkpoint = 1;
		}
    }

	public void replaceLevel(){
		//Debug.Log("death");

		if(GameObject.Find(name) == null){
			Debug.Log(GameObject.Find(name + "(Clone)").name);
			Destroy(GameObject.Find(name + "(Clone)"));
		} else {
			Debug.Log(GameObject.Find(name).name);
		Destroy(GameObject.Find(name));
		}

		if(ID == 1){
			GameObject bullet = (GameObject)Instantiate (PrefabSection, new Vector3(5, 6, 0), Quaternion.identity);
			//Debug.Log("create");
		} else {
			GameObject bullet = (GameObject)Instantiate (PrefabSection, new Vector3(0, 0, 0), Quaternion.identity);
			//Debug.Log("create 2");
		}
	
	}

    // Update is called once per frame
    void Update()
    {
        
    }

	public void refuel(Ball thing){
		thing.fuel = (float)fuelrefill;
		thing.maxfuel = (float)fuelrefill;
	}

	void OnCollisionEnter2D(Collision2D collision){
		if(collision.gameObject.name.Equals("Player")){
			if(!winn){
			collision.gameObject.GetComponent<Ball>().fuel = (float)fuelrefill;
			collision.gameObject.GetComponent<Ball>().maxfuel = (float)fuelrefill;
			part.Play();
				if (Globals.me.checkpoint != ID) {
					Globals.me.beep(2);

                }
			Globals.me.checkpoint = ID;
			PlayerPrefs.SetInt("Checkpoint", ID);
			PlayerPrefs.Save();
			} else {
				Globals.me.anim.SetTrigger("Victory");
				Invoke("win", 0f);
				part.Play();
				Globals.me.checkpoint = ID;
				PlayerPrefs.SetInt("Checkpoint", ID);
				PlayerPrefs.Save();
				collision.gameObject.GetComponent<Ball>().fuel = (float)fuelrefill;
				collision.gameObject.GetComponent<Ball>().maxfuel = (float)fuelrefill;
			}
		}
	}

	void win(){
		SceneManager.LoadSceneAsync("Victory");
	}

}
