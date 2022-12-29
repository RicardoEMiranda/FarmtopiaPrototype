using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Level2Manager : MonoBehaviour {
        //Copy All this into all LevelXManager.cs scripts
        private CommonReferences commonReferences;
        private GameObject farmerHost;
        private GameObject farmerHostDialogueBubble;
        private TextMeshProUGUI farmerHostDialogueText;
        private GameObject farmerNPC;
        private GameObject farmerNPCDialogueBubble;
        private TextMeshProUGUI farmerNPCText;
        //End Copy
        //Don't forget to declare using TMPro;

    // Start is called before the first frame update
    void Start()  {
        //Copy All this into all LevelXManager.cs scripts
        commonReferences = GetComponent<CommonReferences>();
        farmerHost = commonReferences.farmerHost;
        farmerHostDialogueBubble = commonReferences.farmerHostDialogueBubble;
        farmerHostDialogueText = commonReferences.farmerHostDialogueText;
        farmerNPC = commonReferences.farmerNPC;
        farmerNPCDialogueBubble = commonReferences.farmerNPCDialogueBubble;
        farmerNPCText = commonReferences.farmerNPCText;
        //End Copy
        //Don't forget to declare using TMPro;
    }

    // Update is called once per frame
    void Update()  {
        
    }

    public string GetLevelMessage() {

        string message = "Level 2 Message.";
        return message;
    }

}
