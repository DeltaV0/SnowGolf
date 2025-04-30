using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
		Time.timeScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	public void load(){
		SceneManager.LoadSceneAsync("SampleScene");
	}

	public void loadwiped(){
		PlayerPrefs.DeleteAll();
		PlayerPrefs.Save();
		SceneManager.LoadSceneAsync("SampleScene");
	}

	public void menu(){
		SceneManager.LoadSceneAsync("Title");
	}
}
