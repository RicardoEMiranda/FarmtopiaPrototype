using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NPCDialogueManager : MonoBehaviour {

    [SerializeField] private GameObject functionsGO;
    [SerializeField] private GameObject dialogueGO;
    [SerializeField] public TextMeshProUGUI textArea;
    private TypeWriter typeWriter;
    private NPCDialogueLevel1 NPCDialogueL1;
    private OnClickEvents onClickEvents;
    private int line = 0;
    private bool messagePrinted;
    public bool dialogueSequenceFinished;
    
    // Start is called before the first frame update
    void Start()  {
        onClickEvents = functionsGO.GetComponent<OnClickEvents>();
        line = onClickEvents.noOfClicks;
        typeWriter = functionsGO.GetComponent<TypeWriter>();
        NPCDialogueL1 = dialogueGO.GetComponent<NPCDialogueLevel1>();
        //Debug.Log("Inside Dummy Manager, string returned is: " + returnDialogue);
        onClickEvents = functionsGO.GetComponent<OnClickEvents>();


        //string returnDialogue = NPCDialogueL1.ReturnString(line);
        //Debug.Log(returnDialogue);
        //typeWriter.Type(returnDialogue, textArea);
    }

    // Update is called once per frame
    void Update()  {
        //Debug.Log(typeWriter.finishedTyping);

        if(onClickEvents.dialogueNextButtonClicked && onClickEvents.noOfClicks <= NPCDialogueL1.NPCDialogue.Length-1  )  {
            //Debug.Log("Next button clicked: ");
            int line = onClickEvents.noOfClicks;
            onClickEvents.dialogueNextButtonClicked = false;
            string returnDialogue = NPCDialogueL1.ReturnString(line);
            typeWriter.Type(returnDialogue, textArea);
        }

        if(onClickEvents.noOfClicks == NPCDialogueL1.NPCDialogue.Length)  {
            
            if(!messagePrinted)  {
                //Debug.Log("Turn off host");
                messagePrinted = true;
            }
           
            onClickEvents.dialogueNextButtonClicked = false;
            //onClickEvents.noOfClicks = 0;
            dialogueSequenceFinished = true;
        }

    }

    public void StartDialogue() {
        string returnDialogue = NPCDialogueL1.ReturnString(line);
        Debug.Log(returnDialogue);
        typeWriter.Type(returnDialogue, textArea);
    }

}
