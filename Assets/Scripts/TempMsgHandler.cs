using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempMsgHandler : MonoBehaviour {
    [SerializeField] public GameObject speechCanvas;
    [SerializeField] public GameObject dialogueBox;
    [SerializeField] public GameObject dialogueBox2;

    public void OnMsg1ButtonDown()  {
        //Debug.Log("Msg1 Button Down");
        speechCanvas.SetActive(true);
        dialogueBox.SetActive(true);
    }

    public void OnMsg2ButtonDown()     {
        //Debug.Log("Msg1 Button Down");
        speechCanvas.SetActive(true);
        dialogueBox2.SetActive(true);
    }

    public void OnDialogueNextButtonDown() {
        Debug.Log("Next >");
    }

}
