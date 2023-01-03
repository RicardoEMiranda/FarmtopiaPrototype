using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICinemachineManager {

    void ActivateWorldCam();
    void ActivateNPCCam();

}

/*
 * public interface IDialogueL1 {
    string[,] BigHost { get; }
    string[,] NPC { get; }
    //string ReturnDialogue(string[,] hostDialogue, int line);
    string[,] Sacagawea { get; }
    string ReturnDialogue(string[,] character, int line);
}
 * */
