using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IDialogueL1 {
    string[,] dialogue { get; }
    string ReturnDialogue(string[,] hostDialogue, int line);

}

public class DialogueL1 : MonoBehaviour{
    public virtual string ReturnDialogue(string[,] hostDialogue, int line)  {
        return hostDialogue[0, line];

    }
}

