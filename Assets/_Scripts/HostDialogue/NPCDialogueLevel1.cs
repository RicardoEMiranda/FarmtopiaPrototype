using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDialogueLevel1 : MonoBehaviour {

    private int stringCount;
    public bool barnIntroComplete;

    
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

        string returnString = NPCDialogue[0, line];
        stringCount = line;
        return returnString;

    }

    
}
