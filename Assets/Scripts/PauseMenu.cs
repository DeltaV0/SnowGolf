using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class PauseMenu : MonoBehaviour
{
	public TMP_Text voltext;
    public TMP_Text vol2text;

    public Slider vol;
    public Slider vol2;

    public static bool Paused = false;

	public GameObject UI;

	public AudioSource[] audio;
    public AudioSource[] Music;

    public float[] voliumes;
    public float[] voliumes2;

    public AudioSource[] audio2;

	//public float[] 

	void Start(){
		Startup ();
		if(PlayerPrefs.GetInt("INIT") == 0){
			PlayerPrefs.SetInt("INIT", 1);
			PlayerPrefs.SetFloat("Volume", 0.75f);
            PlayerPrefs.SetFloat("VolumeMus", 0.75f);
            vol.value = 0.75f;
		}
		vol.value = PlayerPrefs.GetFloat("Volume");
        vol2.value = PlayerPrefs.GetFloat("VolumeMus");
    }

	public void ChangeVol(){
		PlayerPrefs.SetFloat("Volume", vol.value);
	}
    public void ChangeVolMus()
    {
        PlayerPrefs.SetFloat("VolumeMus", vol2.value);
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
        for (int i = 0; i < Music.Length; i++)
        {
            Music[i].volume = voliumes2[i] * PlayerPrefs.GetFloat("VolumeMus");
        }
        voltext.text = (int)(PlayerPrefs.GetFloat("Volume") * 100) + "%";
        vol2text.text = (int)(PlayerPrefs.GetFloat("VolumeMus") * 100) + "%";
    }
	public void Startup(){
		//audio2[1].Play();
		for(int i = 0; i < audio.Length; i++){
			audio[i].UnPause();
		}
        for (int i = 0; i < Music.Length; i++)
        {
            Music[i].UnPause();
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
        for (int i = 0; i < Music.Length; i++)
        {
            Music[i].UnPause();
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
        for (int i = 0; i < Music.Length; i++)
        {
            Music[i].Pause();
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
