using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue_Level1_BigHost: MonoBehaviour {

    public string[,] dialogue = new string[1,3]  {
        {"Hello. I'm glad you're here. We can really use your help around the farm.",
          "Our barn needs some repairs. Once fixed, we can start storing harvest and other materials.",
          "Let's go check out the barn. Come on!"
        }
    };

    //We want a function that we can call like this: ReturnDialogue(dialogueLevel1.dialogueBigHost, line)
    //dialogueLevel1.dialogueBigHost is a reference to the string array that is part of this script and
    //line is the line number to return

    public string ReturnDialogue(string[,] hostDialogue, int line) {
        return hostDialogue[0, line];

    }
}
