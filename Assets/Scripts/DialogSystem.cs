using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogSystem : MonoBehaviour {

    public TextAsset asset;

    int i = 0;

    public Text dialogue_Text;


    DialogSettings dialogue;



	
	void Start () {
        dialogue = DialogSettings.Load(asset);

        //character_image.GetComponent<Animator>().SetTrigger("go");
	}
	
	// Update is called once per frame
	void Update () {
        dialogue_Text.text = dialogue.nodes[i].text;
        
	}

    public void Next()
    {
        if(i< dialogue.nodes.Length-1)
            
                i++;
		else if (i == dialogue.nodes.Length - 1)
            {
			i = dialogue.nodes.Length - 1;
            }
        
        

    }

}
