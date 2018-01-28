using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class GameController : MonoBehaviour {

	public List<WaterLine> waterLineList;
	public float[] xCameraTriggerFlow;
	public float[] flowList;
	public Transform targetCamera;

	public MusicRules musicRules;





	int i =0;





	// Use this for initialization
	void Start () {
		Invoke ("SearchWaterLine", 1f);
		musicRules.audioList [0].Play();
		
	}
	
	// Update is called once per frame
	void Update () {
		if (i <= xCameraTriggerFlow.Length - 1) {
			if (targetCamera.position.x >= xCameraTriggerFlow [i]) {
				foreach (WaterLine wt in waterLineList) {
					wt.setDefaultFlow(flowList[i]) ;
				}
				i++;
			}
		}


		}

	void  SearchWaterLine (){
		GameObject[] waterBlockGameObjects;
		waterBlockGameObjects = GameObject.FindGameObjectsWithTag ("waterBlock");
		foreach (GameObject wBlockGM in waterBlockGameObjects){
			WaterLine wb = wBlockGM.GetComponent<WaterLine> ();
			waterLineList.Add (wb);
		}
	}
	
	public void ForceSetGlobalFlow(float inFlow) {
		foreach (WaterLine wt in waterLineList) {
			wt.setDefaultFlow(inFlow) ;

		}	   	
	}
}
