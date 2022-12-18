using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HostDialogueLevel1 : MonoBehaviour {

    public string[,] line = new string[1,2];
    //index i is the number of farmer hosts, one row for each farmer host that will be providing dialogue
    //Initially, will only be one host for Level 1
    //index j is for the number of lines that will be delivered by host i in level 1

    [SerializeField] public TextMeshProUGUI textArea; 


    // Start is called before the first frame update
    void Start() {
        textArea.text = "";

        line[0, 0] = "We're glad you're here to help us run the farm. We can really use your help.";  
        line[0,1] = "Our new farm needs a name. Go ahead and type in a name.";
    }

    // Update is called once per frame
    void Update() {

        
    }

    public string ReturnString(int dialogueLine)  {
        //return "We're glad you're here.";
        return line[0, dialogueLine];
        //return line[0, row];
    }
}
