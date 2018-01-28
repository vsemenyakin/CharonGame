using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowActivateSplashListener : ISplashListener {

	public GameController gameController = null;
	public float flow = 50.0f;

	override public void OnSplashed() {
		//Debug.Log("!!!!! - 1");
		gameController.ForceSetGlobalFlow(flow);
	}
}
