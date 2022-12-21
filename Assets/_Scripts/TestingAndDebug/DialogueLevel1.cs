using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueLevel1 : MonoBehaviour {

    public string[,] dialogueArray = new string[1, 2]  {
        {"Level 1, line 1 text.",
          "Level 1, line 2 text."
        }
    };

    

    // Start is called before the first frame update
    

    public string ReturnString(int line) {
        string returnString = dialogueArray[0, line];
        return returnString;
    }

    
}
