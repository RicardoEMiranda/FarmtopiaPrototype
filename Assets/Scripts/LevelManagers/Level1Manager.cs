using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Level1Manager : MonoBehaviour {
    //Copy All this into all LevelXManager.cs scripts
    private CommonReferences commonReferences;
    private GameObject farmerHost;
    private GameObject farmerHostDialogueBubble;
    private TextMeshProUGUI farmerHostDialogueText;
    private GameObject farmerNPC;
    private GameObject farmerNPCDialogueBubble;
    private TextMeshProUGUI farmerNPCText;
    private GameObject mainCamera;
    private HostDialogueLevel1 hostDialogue;
    private DissolveOnActivate dissolveOnActivate;
    //End Copy
    //Don't forget to declare using TMPro;

    private int currentMission;

    // Start is called before the first frame update
    void Start() {
        //Copy All this into all LevelXManager.cs scripts
        commonReferences = GetComponent<CommonReferences>();
        farmerHost = commonReferences.farmerHost;
        farmerHostDialogueBubble = commonReferences.farmerHostDialogueBubble;
        farmerHostDialogueText = commonReferences.farmerHostDialogueText;
        farmerNPC = commonReferences.farmerNPC;
        farmerNPCDialogueBubble = commonReferences.farmerNPCDialogueBubble;
        farmerNPCText = commonReferences.farmerNPCText;
        mainCamera = GameObject.Find("MainCamera");
        hostDialogue = commonReferences.dialogueBin.GetComponent<HostDialogueLevel1>();
        dissolveOnActivate = commonReferences.functions.GetComponent<DissolveOnActivate>();
        //End Copy
        //Don't forget to declare using TMPro;

        currentMission = 1;
       
    }

    // Update is called once per frame
    void Update()  {
        
    }

    public void HandleNarrativeEvent(string eventName) {
        if(eventName == "start_Level1_Intro")  {
            farmerHost.SetActive(true);
            dissolveOnActivate.OnActivate();
            //farmerHostDialogueBubble.SetActive(true);

            //Debug.Log("Farmer host Level 1 Intro");
        }
    }

    public string GetLevelMessage() {
        string message = "Level 1 Message.";
        return message;
    }



}
