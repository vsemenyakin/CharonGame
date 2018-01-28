using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow2D : MonoBehaviour {
    public float boundsTop = 0.0f;
    public float boundsBottom = 0.0f;
    public float boundsLeft = 0.0f;
    public float boundsRight = 0.0f;

    public Vector2 movementFromBase = new Vector2(0.0f, 0.0f);
    public Vector2 baseLocation = new Vector2(0.0f, 0.0f);

    public float[] layerSpeeds;
    public GameObject[] layers;

    private Transform target;

	// Use this for initialization
	void Start () {
        target = GameObject.FindWithTag("Player").transform;
        baseLocation = new Vector2(transform.position.x, transform.position.y);
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = new Vector3(Mathf.Clamp(target.position.x,boundsLeft, boundsRight),Mathf.Clamp(target.position.y, boundsBottom, boundsTop), transform.position.z);

        movementFromBase = new Vector2(transform.position.x - baseLocation.x, transform.position.y - baseLocation.y);

        if (layers.Length > 0)
        {
            int i = 0;
            foreach (GameObject layer in layers)
            {
                var material = layer.GetComponent<Renderer>().material;
                material.SetTextureOffset("_MainTex", new Vector2((movementFromBase.x * 0.015f) * layerSpeeds[i], 0.0f));
                i++;
            }
        }
	}
}
