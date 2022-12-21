using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DummyManager : MonoBehaviour {

    
    [SerializeField] private GameObject functionsGO;
    [SerializeField] private GameObject dialogueGO;
    [SerializeField] public TextMeshProUGUI textArea;
    private TypeWriter typeWriter;
    private DialogueLevel1 hostDialogueL1;
    private OnClickEvents onClickEvents;
    private int line = 0;
    
    // Start is called before the first frame update
    void Start()  {
        typeWriter = functionsGO.GetComponent<TypeWriter>();
        hostDialogueL1 = dialogueGO.GetComponent<DialogueLevel1>();
        //Debug.Log("Inside Dummy Manager, string returned is: " + returnDialogue);
        onClickEvents = functionsGO.GetComponent<OnClickEvents>();


        string returnDialogue = hostDialogueL1.ReturnString(line);
        //Debug.Log(returnDialogue);
        typeWriter.Type(returnDialogue, textArea);
        
        


    }

    // Update is called once per frame
    void Update()  {
        //Debug.Log(typeWriter.finishedTyping);

    }
}
