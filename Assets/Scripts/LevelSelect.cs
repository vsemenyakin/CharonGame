﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelSelect : MonoBehaviour {





	public void loadLevel(string levelID)
	{
		SceneManager.LoadScene(levelID);
	}

}
