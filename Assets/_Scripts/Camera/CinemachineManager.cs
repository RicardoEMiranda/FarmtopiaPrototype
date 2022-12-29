using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Cinemachine;

public class CinemachineManager : MonoBehaviour {

   [SerializeField] private GameObject overWorldCam;
   [SerializeField] private GameObject NPCCam;

   public void ActivateWorldCam() {
        overWorldCam.SetActive(true);
        NPCCam.SetActive(false);
    }

    public void ActivateNPCCam() {
        overWorldCam.SetActive(false);
        NPCCam.SetActive(true);
    }

}
