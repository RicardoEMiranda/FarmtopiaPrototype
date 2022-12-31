using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue_Level1_Sacagawea : MonoBehaviour {

    public string[,] dialogue = new string[1, 3]  {
        {"Hello. I'm Sacagawea. You just earned 100 hemp seed! You can start planting the field with the new seed.",
          "Once fully grown, you can harvest the plant stalks and use it make fiber and other products.",
          "Click on the Barn Inventory incon on the lower right to check your inventory. Try it out!"
        }
    };

    public string ReturnDialogue(string[,] hostDialogue, int line)
    {
        return hostDialogue[0, line];

    }

}
