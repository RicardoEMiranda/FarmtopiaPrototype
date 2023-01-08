using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HostDialogueL1 : MonoBehaviour, IDialogueL1 {

    public string[,] BigHost { get; } = new string[1, 3]  {
        {"Hello. I'm glad you're here. We can really use your help around the farm.",
          "Our barn needs some repairs. Once fixed, we can start storing harvest and other materials.",
          "Let's go check out the barn. Come on!"
        }
    };

    public string[,] NPC { get; } = new string[1, 2] {
            {"This barn is really run down! Let's fix it so we can start collecting barn inventory items.",
             "Just click on the barn to fix it up. Go ahead, try it!"
            }
    };

    public string[,] NPC1 { get; } = new string[1, 2]  {
        {"Now that you have the hemp seed, we should prep the planting fields.",
         "Click on the field to select the plot you want to till.",
        }
    };

    public string[,] NPC2 { get; } = new string[1, 3]  {
        {"Now that you have the hemp seed, let's plant our first hemp crop.",
         "Click on the field to select the plot you want to plant in.",
         "Once you select the plot, you can pick from the seed varieties available."
        }
    };


    public string[,] Sacagawea { get; } = new string[1, 3]  {
        {"Hello. I'm Sacagawea. You just earned 100 hemp seed! You can start planting the field with the new seed.",
          "Once fully grown, you can harvest the plant stalks and use them to make fiber and other products.",
          "Click on the Barn Inventory icon on the lower right to check your inventory. Try it out!"
        }
    };


    public string ReturnDialogue(string[,] character, int line)  {
        if(character == BigHost)  {
            return BigHost[0, line];
        } else if(character == NPC)  {
            return NPC[0, line];
        } else if(character == Sacagawea) {
            return Sacagawea[0, line];
        } else if (character == NPC1)  {
            return NPC1[0, line];
        } else if (character == NPC2)  {
            return NPC2[0, line];
        }
        else  {
            return null;
        }
       
    }

}
