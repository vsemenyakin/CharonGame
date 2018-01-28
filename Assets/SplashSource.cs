using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashSource : MonoBehaviour {

	public float limitSpeed = 3.0f;
	public float impulseK = 0.2f;

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

			if (theSpeed > limitSpeed) {
                var theWaterPart = other.gameObject.transform.parent.gameObject;
                var theWaterScript = theWaterPart.GetComponent<WaterLine>();
				theWaterScript.Splash(transform.position.x - theWaterPart.transform.position.x, theSpeed * impulseK);
            }
        }
    }		
}
