using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
	public static bool Paused = false;

	public GameObject UI;

	public AudioSource[] audio;

	public float[] voliumes;

	public AudioSource[] audio2;

	void Start(){
		Resume ();
	}

    // Update is called once per frame
    void Update()
    {
      if(Input.GetKeyDown(KeyCode.Escape))
		{
			if(Paused){
				Resume();
			} else {
				Pause();
			}
		}
    }

	public void Resume(){
		audio2[1].Play();
		for(int i = 0; i < audio.Length; i++){
			audio[i].UnPause();
		}
		UI.SetActive(false);
		Time.timeScale = 1f;
		Paused = false;
	}

	public void Pause(){
		audio2[0].Play();
		for(int i = 0; i < audio.Length; i++){
			audio[i].Pause();
		}
		UI.SetActive(true);
		Time.timeScale = 0f;
		Paused = true;
	}

	public void menu(){
		SceneManager.LoadSceneAsync("Title");
	}
}
