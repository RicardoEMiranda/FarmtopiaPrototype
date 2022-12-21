using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour {

    [SerializeField] private TMP_InputField levelInput;
    [SerializeField] private GameObject levelContainer;
    private Level1Manager level1Manager;
    private Level2Manager level2Manager;
    private int currentLevel;
    private CommonReferences commonReferences;
    private HostDialogueLevel1 hostDialogue;
    private DissolveOnActivate dissolveOnActivate;
    private TypeDialogue typeDialogue;

    private TextMeshProUGUI hostText;
    private TextMeshProUGUI npcText;
    public bool type;
    private OnClickEvents hostOnClick;
    int dialogueLine = 0;

    // Start is called before the first frame update
    void Start()  {
        currentLevel = GetLevel();
        level1Manager = levelContainer.GetComponent<Level1Manager>();
        level2Manager = levelContainer.GetComponent<Level2Manager>();
        commonReferences = levelContainer.GetComponent<CommonReferences>();
        hostDialogue = commonReferences.dialogueBin.GetComponent<HostDialogueLevel1>();
        typeDialogue = commonReferences.dialogueBin.GetComponent<TypeDialogue>();
        dissolveOnActivate = commonReferences.functions.GetComponent<DissolveOnActivate>();
        hostText = commonReferences.farmerHostDialogueText;
        npcText = commonReferences.farmerNPCText;
        hostOnClick = commonReferences.functions.GetComponent<OnClickEvents>();
    }

    // Update is called once per frame
    void Update() {
        currentLevel = GetLevel();
        //Debug.Log(currentLevel);

        RunLevelManager(currentLevel);
    }

    public int GetLevel() {
        //need to get the player's current level in the game from Firebase
        //For now using a TMP input field for testing
        string inputText = levelInput.text; 
        //gets the entered level for testing purposes
        //This will come external to this script and will be updated when player has met the level objectives

        int level;
        if (int.TryParse(inputText, out level))  {
            // The input string was successfully parsed as an integer
            // You can now use the "level" variable to get the parsed integer value
            return level;
        } 
        else   {
            // The input string could not be parsed as an integer
            // You can handle this error however you want
            // For example, you could return a default value of 1
            return 1;
        }
    }

    private void RunLevelManager(int level) {
        //Debug.Log("Running Level: " + level);

        if(level == 1)  {
           
            //use Level1Manager
            //Debug.Log(level1Manager.GetLevelMessage());
            level1Manager.HandleNarrativeEvent("start_Level1_Intro");

            //Check if Host Farmer's Dialogue bubble is ready DissolveOnActivate.ReadyToType()
            //If ReadyToType, type first line of text
            //Debug.Log(dissolveOnActivate.ReadyToType());
            if(dissolveOnActivate.ReadyToType() && dialogueLine == 0)  {
                //Debug.Log("Ready to Type");
                //Debug.Log(typeDialogue.Type(hostDialogue.ReturnString(0)));
                //hostText.text = typeDialogue.Type(hostDialogue.ReturnString(0));
                string d = hostDialogue.ReturnString(dialogueLine);
                hostText.text = "";
                type = true;
                hostText.text = typeDialogue.Type(d, hostText);
                //dialogueLine = 1;
            }

            if (hostOnClick.hostNextClicked)  {
                //Debug.Log("Host Clicked");
                hostOnClick.hostNextClicked = false;

                if (dialogueLine <= hostDialogue.line.Length - 1)  {
                    Debug.Log(dialogueLine);
                    hostText.text = typeDialogue.Type(hostDialogue.ReturnString(dialogueLine), hostText);
                    dialogueLine += 1;
                }
            }

        }

        if(level == 2)  {
            //use Level2Manager
            //Debug.Log(level2Manager.GetLevelMessage());
        }

    } 

    

}

