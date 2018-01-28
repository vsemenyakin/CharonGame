using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogSystem : MonoBehaviour {

    public TextAsset asset;

    int i = 0;

    public Text dialogue_Text;

	public List<float> xCameraTrigger;

	private DialogSettings dialogue;

	private GameObject mainCamera;


	
	void Start () {
        dialogue = DialogSettings.Load(asset);
		mainCamera = GameObject.FindWithTag ("MainCamera");

        //character_image.GetComponent<Animator>().SetTrigger("go");
	}
	
	// Update is called once per frame
	void Update () {
		if (dialogue.nodes.Length-1 >= i)
			Next ();
        
	}

    public void Next()
    {
		if (mainCamera.transform.position.x >= xCameraTrigger [i]  ) {
			dialogue_Text.text = dialogue.nodes [i].text;
			i++;
		}
		else if (i == dialogue.nodes.Length - 1)
            {
			i = dialogue.nodes.Length - 1;
            }
        
        

    }

}
