using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue_Level1_NPC : MonoBehaviour {

    public string[,] dialogue = new string[1, 2] {
        {"This barn is really run down! Let's fix it so we can start collecting barn inventory items.",
          "Just click on the barn to fix it up. Go ahead, try it!"

        }
    };


    public string ReturnDialogue(string[,] hostDialogue, int line)
    {
        return hostDialogue[0, line];

    }
}
