using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogSystem : MonoBehaviour {

    public TextAsset asset;

    int i = 0;

    public Text curDialogue_Text;
	public Text moveDialogue_Text = null;
	public Text prevDialogue_Text = null;
	private Animator curDialogueTextController;
	private Animator moveDialogueTextController;
	private Animator prevDialogueTextController;

	public List<float> xCameraTrigger;

	private DialogSettings dialogue;

	private GameObject mainCamera;


	
	void Start () {
        dialogue = DialogSettings.Load(asset);
		mainCamera = GameObject.FindWithTag ("MainCamera");

		// get animator from text
		curDialogueTextController = curDialogue_Text.GetComponent<Animator>();
		moveDialogueTextController = moveDialogue_Text.GetComponent<Animator>();
		prevDialogueTextController = prevDialogue_Text.GetComponent<Animator> ();

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
			PlayTextAnimation ();
			curDialogue_Text.text = dialogue.nodes [i].text;
			if (dialogue.nodes [i].color == "player") {
				curDialogue_Text.color = Color.white;
			}
			if (dialogue.nodes [i].color == "choron") {
				curDialogue_Text.color = Color.black;
			}
			i++;
		}
		else if (i == dialogue.nodes.Length - 1)
            {
			i = dialogue.nodes.Length - 1;
            }
        
        

    }

	void PlayTextAnimation(){
		// change text and color using current text
		prevDialogue_Text.text = moveDialogue_Text.text;
		prevDialogue_Text.color = moveDialogue_Text.color;
		moveDialogue_Text.text = curDialogue_Text.text;
		moveDialogue_Text.color = curDialogue_Text.color;
		curDialogueTextController.SetTrigger ("go");
		moveDialogueTextController.SetTrigger ("go");
		prevDialogueTextController.SetTrigger ("go");
	}

}
