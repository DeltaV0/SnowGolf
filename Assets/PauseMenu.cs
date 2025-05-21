using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class PauseMenu : MonoBehaviour
{
	public TMP_Text voltext;

	public Slider vol;

	public static bool Paused = false;

	public GameObject UI;

	public AudioSource[] audio;

	public float[] voliumes;

	public AudioSource[] audio2;

	//public float[] 

	void Start(){
		Startup ();
		if(PlayerPrefs.GetInt("INIT") == 0){
			PlayerPrefs.SetInt("INIT", 1);
			PlayerPrefs.SetFloat("Volume", 0.75f);
			vol.value = 0.75f;
		}
		vol.value = PlayerPrefs.GetFloat("Volume");
	}

	public void ChangeVol(){
		PlayerPrefs.SetFloat("Volume", vol.value);
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
		for(int i = 0; i < audio.Length; i++){
			audio[i].volume = voliumes[i] * PlayerPrefs.GetFloat("Volume");
		}
		voltext.text = (int)(PlayerPrefs.GetFloat("Volume") * 100) + "%";
    }
	public void Startup(){
		//audio2[1].Play();
		for(int i = 0; i < audio.Length; i++){
			audio[i].UnPause();
		}
		UI.SetActive(false);
		Time.timeScale = 1f;
		Paused = false;
		PlayerPrefs.Save();
	}
	public void Resume(){
		audio2[1].Play();
		for(int i = 0; i < audio.Length; i++){
			audio[i].UnPause();
		}
		UI.SetActive(false);
		Time.timeScale = 1f;
		Paused = false;
		PlayerPrefs.Save();
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
		PlayerPrefs.Save();
		SceneManager.LoadSceneAsync("Title");
	}
}
