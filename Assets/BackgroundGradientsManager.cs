using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundGradientsManager : MonoBehaviour {

	public float[] places; 
	public Sprite[] sprites; 

	public SpriteRenderer prev; 
	public SpriteRenderer next; 

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		float theCameraX = Camera.main.transform.position.x;

		int theNextIndex = 0; 
		for (int i = 0, size = places.Length; i < size; ++i) { 
			if (places[i] > theCameraX) { 
				theNextIndex = i; break; 
			} 
		} 

		float theBackFrontDistant = places[theNextIndex] - places[theNextIndex - 1]; 
		float theCameraOnRangeX = theCameraX - places[theNextIndex - 1];

		prev.sprite = sprites[theNextIndex - 1];
		next.sprite = sprites[theNextIndex];

		Color theColor;

		theColor = next.color;
		theColor.a = theCameraOnRangeX/theBackFrontDistant;
		next.color = theColor;

		theColor = prev.color;
		theColor.a = 1 - next.color.a;
		prev.color = theColor;
	}
}
