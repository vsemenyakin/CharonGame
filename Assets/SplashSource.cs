using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashSource : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	}

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag.Equals("waterPart")) {
            var theRigid = GetComponent<Rigidbody2D>();
            float theSpeed = theRigid.velocity.magnitude;

            if (theSpeed > 0.5f) {
                var theWaterScript = other.gameObject.transform.parent.gameObject.GetComponent<WaterLine>();
                theWaterScript.Splash(transform.position.x, theSpeed / 10.0f);
            }
        }
    }		
}
