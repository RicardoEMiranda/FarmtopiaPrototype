using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IDialogueL1 {
    string[,] BigHost { get; }
    string[,] NPC { get; }
    string[,] NPC1 { get; }
    string[,] NPC2 { get; }
    string[,] Sacagawea { get; }
    string ReturnDialogue(string[,] character, int line);
}

//public class DialogueL1 : MonoBehaviour{
 //   public virtual string ReturnDialogue(string[,] hostDialogue, int line)  {
 //       return hostDialogue[0, line];

 //   }
    
//}

