using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelManager : MonoBehaviour {

    [SerializeField] private GameObject dialogueBinBigHost;
    [SerializeField] private GameObject dialogueBinNPC;
    [SerializeField] private GameObject dialogueBinSacagawea;
    private Dialogue_Level1_BigHost dialogueLevel1_BigHost;
    private Dialogue_Level1_NPC dialogueLevel1_NPC;
    private Dialogue_Level1_Sacagawea dialogueLevel1_Sacagawea;

    private int count;

    [SerializeField] private TextMeshProUGUI dialogueTMP;
    [SerializeField] public int fontSize = 28;
    [SerializeField] private GameObject onClickManagersGO;
    private OnNextClicked onNextClicked;
    private HostDialogueL1 hostDialogueL1;

    // Start is called before the first frame update
    void Start() {
        dialogueLevel1_BigHost = dialogueBinBigHost.GetComponent<Dialogue_Level1_BigHost>();
        dialogueLevel1_NPC = dialogueBinNPC.GetComponent<Dialogue_Level1_NPC>();
        dialogueLevel1_Sacagawea = dialogueBinSacagawea.GetComponent<Dialogue_Level1_Sacagawea>();

        int count = 0;
        dialogueTMP.fontSize = fontSize;
        dialogueTMP.text = "";
        onNextClicked = onClickManagersGO.GetComponent<OnNextClicked>();

        //Start dialogue output for line 1 from the start
        string returnString = dialogueLevel1_BigHost.ReturnDialogue(dialogueLevel1_BigHost.dialogue, 0);
        dialogueTMP.text = returnString;
        hostDialogueL1 = dialogueBinBigHost.GetComponent<HostDialogueL1>();
       
        //string str = BigHost.ReturnDialogue(BigHost.dialogue, 0);

    }

    // Update is called once per frame
    void Update() {

        //Start the dialogue bubble before running the check for Next Button Clicked
        //Start sequence goes here......

        //Then check if onNextClick.clicked
        if(onNextClicked.clicked) {
            if (onNextClicked.count < hostDialogueL1.dialogue.Length)  {
                //Debug.Log("Clicked : " + onNextClicked.clicked + " Count: " + onNextClicked.count);
                string returnString = hostDialogueL1.ReturnDialogue(dialogueLevel1_BigHost.dialogue, onNextClicked.count);
                dialogueTMP.text = returnString;
                onNextClicked.clicked = false;
            } else   {
                //this means that the dialogue for this character has run out and need to turn off the caption bubble at this point
            }
           
        }

        //select A, S or D keys
        //A for Big Host dialogue, S for NPC and D for Sacagawea
        if(Input.GetKeyDown(KeyCode.A))  {
            //Debug.Log("A");
            //count = 0;
            //if (count < dialogueLevel1_BigHost.dialogueBigHost.Length)  {
            //string returnString = dialogueLevel1.dialogueNPC[0, count]; //make sure the reference matches the one in the if statement
            //    string returnString = dialogueLevel1_BigHost.ReturnDialogue(dialogueLevel1_BigHost.dialogueBigHost, count);
            //    dialogueTMP.text = returnString;
            //Debug.Log(returnString);
            //}

            //string returnString = dialogueLevel1_BigHost.ReturnDialogue(dialogueLevel1_BigHost.dialogueBigHost, 0);
            //dialogueTMP.text = returnString;

            string output = CheckNextButtonClicked(onNextClicked.clicked, onNextClicked.count, hostDialogueL1);
            dialogueTMP.text = output;
            onNextClicked.clicked = false;

        } 

        string CheckNextButtonClicked(bool nextClicked, int clickCount, IDialogueL1 characterDialogue)  { 

            if (nextClicked)  {
                if (clickCount < characterDialogue.dialogue.Length)  {
                    //Debug.Log("Clicked : " + onNextClicked.clicked + " Count: " + onNextClicked.count);
                    string returnString = characterDialogue.ReturnDialogue(characterDialogue.dialogue, onNextClicked.count);
                    //dialogueTMP.text = returnString;
                    //onNextClicked.clicked = false;
                    return returnString;
                }
                else {
                    //this means that the dialogue for this character has run out and need to turn off the caption bubble at this point
                    return "";
                }
            } else  {
                return "";
            }

        }

    }

}

/*public void CheckNextButtonClicked(object obj)
{
    if (obj is MyNamespace.Dialogue_BigHost)
    {
        MyNamespace.Dialogue_BigHost bigHost = (MyNamespace.Dialogue_BigHost)obj;
        // Do something with the bigHost object
    }
    else if (obj is MyNamespace.Dialogue_NPC)
    {
        MyNamespace.Dialogue_NPC npc = (MyNamespace.Dialogue_NPC)obj;
        // Do something with the npc object
    }
}*/
