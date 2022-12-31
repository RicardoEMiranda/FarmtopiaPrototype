using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelManager : MonoBehaviour {

    [SerializeField] private GameObject dialogueBinGO;

    private int count;

    [SerializeField] private TextMeshProUGUI dialogueTMP;
    [SerializeField] public int fontSize = 28;
    [SerializeField] private GameObject onClickManagersGO;
    private OnNextClicked onNextClicked;
    private HostDialogueL1 hostDialogueL1;
    private string testCase;

    // Start is called before the first frame update
    void Start() {
        //Debug.Log("Start running");
        hostDialogueL1 = dialogueBinGO.GetComponent<HostDialogueL1>();

        int count = 0;
        dialogueTMP.fontSize = fontSize;
        dialogueTMP.text = "";
        onNextClicked = onClickManagersGO.GetComponent<OnNextClicked>();

        //Activate BigHost Dialogue Bin Start dialogue output for line 1 from the start
        //string returnString = hostDialogueL1.ReturnDialogue(hostDialogueL1.BigHost, 0);
        //dialogueTMP.text = returnString;

    }

    // Update is called once per frame
    void Update() {

        if (Input.GetKeyDown(KeyCode.A)) { //Initialize Big Host Dialogue event
            Debug.Log("A");
            testCase = "A";
            //Initialize shared dialogue parameters
            //Initialize hostDialogueL1.BigHost, return first string in hostDialogueL1.BigHost to TMP
            dialogueBinGO.SetActive(true);
            InitializeDialogueParameters(hostDialogueL1.BigHost);
        }
        if(onNextClicked.clicked && testCase == "A")  {
            onNextClicked.clicked = false;
            count += 1;
            //Continue Big Host Dialogue
            if (count >= hostDialogueL1.BigHost.Length)  {
                count = 0;
                dialogueBinGO.SetActive(false);
            } else  {
                string output = hostDialogueL1.ReturnDialogue(hostDialogueL1.BigHost, count);
                dialogueTMP.text = output;
            }
        }

        if (Input.GetKeyDown(KeyCode.S)) {
            Debug.Log("S");
            testCase = "B";
            //Initialize shared dialogue parameters
            //Initialize hostDialogueL1.NPC, return first string in hostDialogueL1.NPC to TMP
            dialogueBinGO.SetActive(true);
            InitializeDialogueParameters(hostDialogueL1.NPC);
        }
        if (onNextClicked.clicked && testCase == "B")  {
            onNextClicked.clicked = false;
            count += 1;
            //Continue Big Host Dialogue
            if (count >= hostDialogueL1.NPC.Length)  {
                count = 0;
                dialogueBinGO.SetActive(false);
            } else {
                string output = hostDialogueL1.ReturnDialogue(hostDialogueL1.NPC, count);
                dialogueTMP.text = output;
            }
        }


        if (Input.GetKeyDown(KeyCode.D)) {
            Debug.Log("D");
            testCase = "D";
            //Initialize hostDialogueL1.Sacagawea
            //Initialize hostDialogueL1.Sacagawea, return first string in hostDialogueL1.Sacagawea to TMP
            dialogueBinGO.SetActive(true);
            InitializeDialogueParameters(hostDialogueL1.Sacagawea);  
        }
        if (onNextClicked.clicked && testCase == "D") {
            onNextClicked.clicked = false;
            count += 1;
            //Continue Big Host Dialogue
            if (count >= hostDialogueL1.Sacagawea.Length) {
                count = 0;
                dialogueBinGO.SetActive(false);
            }  else   {
                string output = hostDialogueL1.ReturnDialogue(hostDialogueL1.Sacagawea, count);
                dialogueTMP.text = output;
            }
        }


        //Debug.Log(count);
        /*if (Input.GetKeyDown(KeyCode.DownArrow) || onNextClicked.clicked)  {
            onNextClicked.clicked = false;
            Debug.Log("Clicked: " + onNextClicked.clicked);
            //Debug.Log("Key Down");
            count += 1;
            if(count >= hostDialogueL1.BigHost.Length)  {
                //Debug.Log("No more text, reset Count.");
                count = 0;
                dialogueBinGO.SetActive(false);
            } else  {
                string output = hostDialogueL1.ReturnDialogue(hostDialogueL1.BigHost, count);
                dialogueTMP.text = output;
            } 
        }*/

    }

    private void InitializeDialogueParameters(string[,] character) {
        //hostDialogueL1 = dialogueBinGO.GetComponent<HostDialogueL1>();
        int count = 0;
        dialogueTMP.fontSize = fontSize;
        dialogueTMP.text = "";
        //onNextClicked = onClickManagersGO.GetComponent<OnNextClicked>();
        string returnString = hostDialogueL1.ReturnDialogue(character, 0);
        dialogueTMP.text = returnString;
    }

    private void PrintNextLine(string[,] character) {
        onNextClicked.clicked = false;
        //Debug.Log("Clicked: " + onNextClicked.clicked);
        //Debug.Log("Key Down");
        count += 1;
        string output = hostDialogueL1.ReturnDialogue(hostDialogueL1.BigHost, count);
        dialogueTMP.text = output;
    }
  

}

