using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicRules : MonoBehaviour {

	public AudioMaker[] audioList = new AudioMaker[3];
	private AudioMaker audioMaker;
	void Start () {
		audioList[0] = GameObject.FindWithTag ("music1").GetComponent<AudioMaker>();
		audioList [0].volume = 0.7F;
		audioList[1] = GameObject.FindWithTag ("music2").GetComponent<AudioMaker>();
		audioList [1].volume = 0.7F;
		audioList[2] = GameObject.FindWithTag ("music3").GetComponent<AudioMaker>();
		audioList [2].volume = 0.5F;

		audioList[0].Play ();
	}
}
