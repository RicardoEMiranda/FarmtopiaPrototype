using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueLevel1 : MonoBehaviour {

    private int stringCount;
    public bool barnIntroComplete;

    public string[,] dialogueArray = new string[1, 3]  {
        {"Hello. I'm glad you're here. We can really use your help around the farm.",
          "Our barn needs some repairs. Once fixed, we can start storing harvest and other materials.",
          "Let's go check out the barn. Come on!"
        }
    };

    public string[,] NPCDialogue = new string[1, 2] {
        {"This barn is really run down! Let's fix it so we can start collecting barn inventory items.",
          "Just click on the barn to fix it up. Go ahead, try it!"

        }
    };



    // Start is called before the first frame update
    private void Start() {
        stringCount = 0;
    }

    private void Update()  {
        
    }

    public string ReturnString(int line) {

        string returnString = dialogueArray[0, line];
        stringCount = line;
        return returnString;

    }

    
}
