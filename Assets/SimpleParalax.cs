using System.Collections; 
using System.Collections.Generic; 
using UnityEngine; 

public class SimpleParalax : MonoBehaviour { 

public float paralaxFactor = 0.0f; 

// Use this for initialization 
void Start () { 
} 

// Update is called once per frame 
void FixedUpdate () { 
float theCameraPosition = Camera.main.transform.position.x; 

Vector3 thePosition = transform.position; 
thePosition.x = theCameraPosition * paralaxFactor; 
transform.position = thePosition; 
} 
}