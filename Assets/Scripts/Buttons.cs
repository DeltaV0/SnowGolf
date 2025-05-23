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
		float v1 = PlayerPrefs.GetFloat("Volume");
		int v2 = PlayerPrefs.GetInt("INIT");
		PlayerPrefs.DeleteAll();
		PlayerPrefs.SetFloat("Volume", v1);
		PlayerPrefs.SetInt("INIT", v2);
		PlayerPrefs.Save();
		SceneManager.LoadSceneAsync("SampleScene");
	}

	public void menu(){
		SceneManager.LoadSceneAsync("Title");
	}
}
