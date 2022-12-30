using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    public class DialogueLevel1_Sacagawea : MonoBehaviour
    {

        private int stringCount;

        public string[,] sacagaweaDialogue = new string[1, 3]  {
        {"Hello. I'm Sacagawea. You just earned 100 hemp seed! You can start planting the field with the new seed.",
          "Once fully grown, you can harvest the plant stalks and use it make fiber and other products.",
          "Click on the Barn Inventory incon on the lower right to check your inventory. Try it out!"
        }
    };

        // Start is called before the first frame update
        void Start()
        {
            stringCount = 0;
        }

        public string ReturnString(int line)
        {

            string returnString = sacagaweaDialogue[0, line];
            stringCount = line;
            return returnString;

        }

    }
