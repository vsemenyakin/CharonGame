using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(AudioSource))]
public class AudioMaker : MonoBehaviour {

	public AudioClip impact;
	public AudioSource audioSource;
	public float volume = 0.7F;

	void Start()
	{
		//audioSource = GetComponent<AudioSource>();

		//Debug.Log (audioSource);
		//audioSource.clip = impact;
		//audioSource.Play ();
		//Play();


	}

	public void Play()
	{
		audioSource.PlayOneShot(impact, volume);

	}
	public void Pause(){
		audioSource.Pause ();
	}
	public void unPause(){
		audioSource.UnPause ();
	}
}
