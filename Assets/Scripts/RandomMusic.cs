using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMusic : MonoBehaviour
{
	public AudioClip[] music;

    public AudioClip[] music2;

    public AudioSource src;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		if(!src.isPlaying && !PauseMenu.Paused){
			src.clip = music[Random.Range(0, music.Length)];
			src.Play();
		}
    }
}
