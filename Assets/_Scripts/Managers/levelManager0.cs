using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;
using Cinemachine;

public class levelManager0 : MonoBehaviour {

    [Header("Big Host Dialogue Bin Properties")]
    [SerializeField] private GameObject dialogueBinGO;
    [SerializeField] private GameObject clickManagerGO;
    [SerializeField] private TextMeshProUGUI dialogueTMP;
    [SerializeField] public int fontSize = 28;
    private HostDialogueL1 hostDialogueL1;
    private AudioSource audioSource;
    private OnNextClicked onNextClicked;
    private OnNextClicked npcOnNextClicked;
    private OnNextClicked npc2OnNextClicked;

    [Header("NPC1 Character Properties")]
    [SerializeField] private GameObject npcCanvas;
    [SerializeField] private GameObject npcMan;
    [SerializeField] private GameObject npcGirl;
    [SerializeField] private GameObject npcSacagawea;
    [SerializeField] private GameObject npcDialogueBinGO;
    [SerializeField] private GameObject npcDialogueCanvasGO;
    [SerializeField] private GameObject npcClickManagerGO;
    private AudioSource npcAudioSource;
    private AudioSource npc2AudioSource;
    [SerializeField] private TextMeshProUGUI npcDialogueTMP;
    private FarmerAI farmerAI;
    private farmerAI_ farmerAI_;

    [Header ("NPC2 Character Properties")]
    [SerializeField] private GameObject npc2Canvas;
    [SerializeField] private GameObject npc2Man;
    [SerializeField] private GameObject npc2Girl;
    [SerializeField] private GameObject npc2Sacagawea;
    [SerializeField] private GameObject npc2DialogueBinGO;
    [SerializeField] private GameObject npc2DialogueCanvasGO;
    [SerializeField] private GameObject npc2ClickManagerGO;
    [SerializeField] private TextMeshProUGUI npc2DialogueTMP;

    [Header ("Select Host Panel Properties")]
    [SerializeField] private GameObject selectFarmerHostPanel;
    [SerializeField] private GameObject manFarmerButton;
    [SerializeField] private GameObject girlFarmerButton;
    private bool farmerHostSelected;

    [Header ("Big Host Character Properties")]
    [SerializeField] private GameObject bigHostCanvas;
    [SerializeField] private GameObject bigHostMan;
    [SerializeField] private GameObject bigHostGirl;
    [SerializeField] private GameObject bigHostSacagawea;
    [SerializeField] private GameObject bigHostMan2;
    [SerializeField] private GameObject bigHostGirl2;
    [SerializeField] private GameObject bigHostSacagawea2;
    private GameObject activeHost;
    private GameObject activeHost2;
    private GameObject tempHost;
    private bool hostSelected;
    private bool hostPreferencesSet;

    [Header("Cameras")]
    [SerializeField] private GameObject CameraRig;
    private CinemachineManager cinemachineManager;

    [Header ("Barn Properties")]
    [SerializeField] private GameObject barn;
    [SerializeField] private GameObject barnInventoryButton;
    private DetectBarnClicked detectBarnClicked;
    public bool barnIsClickable;
    private bool barnRepaired;

    [Header("Barn Inventory Properties")]
    [SerializeField] private TextMeshProUGUI hempSeedTMP;
    [SerializeField] private GameObject inventoryManagerGO;
    private int hempSeedCount;

    [Header("Field & Crop Properties")]
    [SerializeField] private GameObject fieldGO;
    private FieldController fieldController;
    public List<GameObject> fieldObjects;


    [SerializeField] private GameObject functionsGO;
    private OnClickEvents onClickEvents;

    [Header ("Waypoints")]
    [SerializeField] private Transform waypoint1;
    [SerializeField] private Transform waypoint2;
    [SerializeField] private Transform waypointStart;

    private int Step = 1;
    private int characterIndex;
    private float typeDelay = .05f;
    private int count;
    private bool npcActivated;

    private bool introDialogueStarted;
    private bool npcIntroDialogueStarted;
    private bool hostStarted;
    private bool hostFinished;

    // Start is called before the first frame update
    void Start()  {
        selectFarmerHostPanel.SetActive(false);
        farmerHostSelected = false;
        onClickEvents = functionsGO.GetComponent<OnClickEvents>();
        hostPreferencesSet = false;

        hostDialogueL1 = dialogueBinGO.GetComponent<HostDialogueL1>();
        audioSource = dialogueBinGO.GetComponent<AudioSource>();
        npcAudioSource = npcDialogueCanvasGO.GetComponent<AudioSource>();
        npc2AudioSource = npc2DialogueCanvasGO.GetComponent<AudioSource>();
        onNextClicked = clickManagerGO.GetComponent<OnNextClicked>();
        npcOnNextClicked = npcClickManagerGO.GetComponent<OnNextClicked>();
        npc2OnNextClicked = npc2ClickManagerGO.GetComponent<OnNextClicked>();
        farmerAI = npcCanvas.GetComponent<FarmerAI>();
        farmerAI_ = npc2Canvas.GetComponent<farmerAI_>();
        cinemachineManager = CameraRig.GetComponent<CinemachineManager>();
        fieldController = fieldGO.GetComponent<FieldController>();
        fieldController.canPlant = false;
        //Script attached to the Barn Transform. DetectBarnClicked uses the 
        //box collider attached to the Barn Transform to detect when the collider is clicked on
        detectBarnClicked = barn.GetComponent<DetectBarnClicked>();
        //Set Barn Inventory to inactive. This will activate later after the barn has been repaired
        barnInventoryButton.SetActive(false);
        barnRepaired = false;

        fieldObjects = FindObjectsOfType<FieldController>().Select(f => f.gameObject).ToList();
    }

    // Update is called once per frame
    void Update() {
        //Step Overrides for testing
        if(Input.GetKeyDown(KeyCode.Space))  {
            selectFarmerHostPanel.SetActive(false);
            activeHost2 = bigHostMan2;
            hostSelected = true;
            npcAudioSource = npc2AudioSource;
            Step = 6;
            fieldController.canPlant = false;

            foreach (GameObject field in fieldObjects) {
                FieldController fieldController = field.GetComponent<FieldController>();
                fieldController.canPlant = false;
            }
        }

        if(Step==1)  {
            //FarmerAI needs refactoring. Use FarmerAI for Steps 1 through 5 but for Step 6, start using farmerAI_. 
            //Deactivate FarmerAI and activate farmerAI_
            npcCanvas.GetComponent<FarmerAI>().enabled = true;
            npcCanvas.GetComponent<farmerAI_>().enabled = false;

            //turn on Select Farmer Host Panel. Do it once and condition out of Update loop
            if (!hostSelected)  {
                selectFarmerHostPanel.SetActive(true);
                //make sure hostSelected = true; in next step when host is selected
            }

            //Check when and which button is selected (Farmer Man or Farmer Girl)
            if(onClickEvents.hostNextClicked)  {
                //Debug.Log("Button clicked");
                if (onClickEvents.hostClickedString == "LittleMissSunshine")  {
                    activeHost = bigHostGirl;
                    activeHost2 = bigHostGirl2;
                }
                if (onClickEvents.hostClickedString == "YoungMan")  {
                    activeHost = bigHostMan;
                    activeHost2 = bigHostMan2;
                }
                hostSelected = true;
                StartCoroutine(DeactivateWithDelay(selectFarmerHostPanel, 0));
            }

            //Once one is selected, set Big Host and NPC Characters based on user selection
            if(!hostPreferencesSet && hostSelected)  {
                SetHostPreferences(activeHost);
                hostPreferencesSet = true;
            }

            //Start Big Host intro Dialogue: Welcome to the farm and let's check out the barn
            //Activate Big Host Canvas & Dialogue
            if(hostPreferencesSet && !introDialogueStarted)  {
                introDialogueStarted = true;
                
                StartCoroutine(ActivateWithDelay(bigHostCanvas, 1.25f));
                dialogueTMP.text = "";
                StartCoroutine(InitializeDialogueParameters(hostDialogueL1.BigHost, 1.5f, dialogueTMP));
            }

            if(onNextClicked.clicked && activeHost == bigHostCanvas.activeInHierarchy)  {
                onNextClicked.clicked = false;
                count += 1;
                //Debug.Log("On Next Clicked");

                if(count >= hostDialogueL1.BigHost.Length)  {
                    //Once finished with the intro, turn off panel and set Step = 2;
                    count = 0;
                    //dialogueBinGO.SetActive(false);
                    bigHostCanvas.SetActive(false);
                    Step = 2;
                }
                else  {
                    string output = hostDialogueL1.ReturnDialogue(hostDialogueL1.BigHost, count);
                    characterIndex = 0;
                    StartCoroutine(Type(output, dialogueTMP));
                    audioSource.Play();
                }

            }
            

        }

        if(Step == 2)  {
            //Step 2 begins after Big Host gives his intro dialogue: Welcome to the farm and let's check out the barn
            //Debug.Log("Step 2");

            //Activate NPC character and NPC camera
            if (!npcActivated)  {
                npcCanvas.SetActive(true);
                npcActivated = true;
                cinemachineManager.ActivateNPCCam();
                farmerAI.start = true;
                farmerAI.GoToWaypoint(waypoint1);
                npcActivated = true;
            }

            //Check NPC is at waypoint, once at waypoint, activate dialogue
            Vector3 delta = npcCanvas.transform.position - waypoint1.position;
            float deltaMagnitude = delta.magnitude;
           
            if(deltaMagnitude <= .05 && !npcIntroDialogueStarted)  {
                //npcIntroDialogueStarted = true;
                npcDialogueBinGO.SetActive(true);
                npcDialogueTMP.text = "";
                StartCoroutine(InitializeDialogueParameters(hostDialogueL1.NPC, .5f, npcDialogueTMP));
                npcIntroDialogueStarted = true;
            }
         
            if (npcOnNextClicked.clicked && npcCanvas.activeInHierarchy)  {
                npcOnNextClicked.clicked = false;
                count += 1;

                if (count >= hostDialogueL1.NPC.Length)  {
                    //Once finished with the intro, turn off panel and set Step = 2;
                    count = 0;
                    npcDialogueCanvasGO.SetActive(false);
                    npcCanvas.SetActive(false);
                    npcActivated = false;
                    barnIsClickable = true;
                }
                else  {
                    string output = hostDialogueL1.ReturnDialogue(hostDialogueL1.NPC, count);
                    characterIndex = 0;
                    StartCoroutine(Type(output, npcDialogueTMP));
                    npcAudioSource.Play();
                    
                }
            }

            CheckBarnClicked();
            if(barnRepaired)  {
                Step = 3;
                //Reset host selection bools for step 3
                hostPreferencesSet = false;
                bool hostStarted = false;
                introDialogueStarted = false;
            }
        }

        if(Step == 3) {

            //The barn fixed VFX needs a couple of seconds to play. After a couple of seconds,
            //Switch to Main Camera and Activate Sacagawea BigHost
            StartCoroutine(SwitchCamAfterDelay(cinemachineManager, "ActivateWorldCam", 2f));

            //Set Sacagawea character as the active host, this is the parameter that needs to be
            //passed to SetHostPreferences
            tempHost = bigHostSacagawea;

            if(!hostPreferencesSet)  {
                SetHostPreferences(tempHost);
                //Debug.Log("Setting Host Preferences");
                hostPreferencesSet = true;
            }

            if (hostPreferencesSet && !hostStarted)  {
                hostStarted = true;

                //Activate big host Canvas (assumes that Dialogue Bubble (bigHostCanvas) is also active bydefault)
                StartCoroutine(ActivateWithDelay(bigHostCanvas, 4f));
                StartCoroutine(ActivateWithDelay(dialogueBinGO, 4f));
                dialogueTMP.text = "";
                //Start Sacagawea Big Host Dialogue: You have earned 100 seed and check the Barn Inventory
                StartCoroutine(InitializeDialogueParameters(hostDialogueL1.Sacagawea, 4.05f, dialogueTMP));
            }

            if (onNextClicked.clicked && activeHost == bigHostCanvas.activeInHierarchy)  {
                onNextClicked.clicked = false;
                count += 1;
                //Debug.Log("On Next Clicked");

                if (count >= hostDialogueL1.Sacagawea.Length)  {
                    //Once finished with the intro, turn off panel and set Step = 2;
                    count = 0;
                    //dialogueBinGO.SetActive(false);
                    bigHostCanvas.SetActive(false);
                    hostFinished = true;
                    
                }
                else   {
                    string output = hostDialogueL1.ReturnDialogue(hostDialogueL1.Sacagawea, count);
                    characterIndex = 0;
                    StartCoroutine(Type(output, dialogueTMP));
                    audioSource.Play();
                }

            }

            if(hostFinished)  {
                hempSeedTMP.text = "100";
                hempSeedCount = 100;
                hostPreferencesSet = false;

                //Need to make sure that Barn Inventory button is clicked before initiating NPC character
                //in Step 6, otherwise if they decide to click on the button after the NPC starts walking, they player will likely
                //miss the NPC character walking into position. Also the NPC will start dialogue when in position and want to avoid
                //missing dialogue
                if(inventoryManagerGO.GetComponent<InventoryManager>().inventoryChecked)  {
                    Debug.Log("InventoryChecked");
                    //NOTE: Steps 3, 4 and 5 include Sacagawea gifting 100 hemp seed AND activating Barn Inventory Icon
                    //The next line of code will set Step = 6. Later, we can consolidate these. Keeping both 
                    //Steps here just so we remember to update the external script document
                    Step = 6;
                    npcIntroDialogueStarted = false;
                    npc2AudioSource = npc2DialogueCanvasGO.GetComponent<AudioSource>();
                }
                
            }

        }

        if(Step == 6)  {
            //See note above

            //FarmerAI needs refactoring. Use FarmerAI for Steps 1 through 5 but for Step 6, start using farmerAI_. 
            //Deactivate FarmerAI and activate farmerAI_
            npc2Canvas.GetComponent<FarmerAI>().enabled = false;
            npc2Canvas.GetComponent<farmerAI_>().enabled = true;
            //npcCanvas.SetActive(true);
            //npcDialogueBinGO.SetActive(true);
            
            
            //set activeHost to NPC. Note that Step 3 uses a tempHost for Sacagawea
            if (!hostPreferencesSet) {
                SetHostPreferences(activeHost2);
                //npcCanvas.transform.position = waypointStart.position;
                npc2Canvas.SetActive(true);
                farmerAI_.start = true;
                hostPreferencesSet = true;

                //farmerAI_.SetStartPosition(waypointStart);
                farmerAI_.SetDestination(waypoint2);
            }

            //Check NPC character is in position then start dialogue
            if(((npc2Canvas.transform.position - waypoint2.position).magnitude <= .05f) && !npcIntroDialogueStarted)  {
                //activate npcDialogue bubble and initiate dialogue text
                npc2DialogueBinGO.SetActive(true);
                npc2DialogueTMP.text = "";
                //string output = hostDialogueL1.ReturnDialogue(hostDialogueL1.NPC2, 0);
                //npcDialogueTMP.text = output;
                StartCoroutine(InitializeDialogueParameters(hostDialogueL1.NPC1, .5f, npc2DialogueTMP));
                npcIntroDialogueStarted = true;
            }

            //Continue Dialogue through next button clicks
            if (npc2OnNextClicked.clicked && npc2Canvas.activeInHierarchy)  {
                npc2OnNextClicked.clicked = false;
                count += 1;
              
                if (count >= hostDialogueL1.NPC1.Length)  {
                    //Once finished with the intro, turn off panel and...
                    
                    count = 0;

                    //Introduce player to the field and that it needs to be tilled
                    //Turn off Dialogue canvas but leave NPC canvas on to implement loitering
                    npc2DialogueCanvasGO.SetActive(false); 

                    //Start Loitering until fields are tilled

                    //npc2Canvas.SetActive(false);
                    //npcActivated = false;
                    
                    
                    //barnIsClickable = true;
                    //fieldController.canPlant = true;
                    foreach (GameObject field in fieldObjects)   {
                        FieldController fieldController = field.GetComponent<FieldController>();
                        fieldController.canPlant = true;
                    }
                }
                else  {
             
                    string output = hostDialogueL1.ReturnDialogue(hostDialogueL1.NPC1, count);
                    characterIndex = 0;
                    StartCoroutine(Type(output, npc2DialogueTMP));
                    npc2AudioSource.Play();

                }
            }


        }

        
    }

    IEnumerator ActivateWithDelay(GameObject obj, float delay) {
        yield return new WaitForSeconds(delay);
        obj.SetActive(true);
    }

    IEnumerator DeactivateWithDelay(GameObject obj, float delay)  {
        yield return new WaitForSeconds(delay);
        obj.SetActive(false);
        if(obj = npcCanvas)  {
            npcActivated = false;
        }

    }

    private void SetHostPreferences(GameObject hostSelected) {

        if(hostSelected == bigHostGirl)  {
            bigHostCanvas.SetActive(true);
            bigHostGirl.SetActive(true);
            bigHostMan.SetActive(false);
            bigHostSacagawea.SetActive(false);
            bigHostCanvas.SetActive(false);

            npcCanvas.SetActive(true);
            npcGirl.SetActive(true);
            npcMan.SetActive(false);
            npcSacagawea.SetActive(false);
            npcCanvas.SetActive(false);
        } 
        if(hostSelected == bigHostMan) {
            bigHostCanvas.SetActive(true);
            bigHostGirl.SetActive(false);
            bigHostMan.SetActive(true);
            bigHostSacagawea.SetActive(false);
            bigHostCanvas.SetActive(false);

            npcCanvas.SetActive(true);
            npcGirl.SetActive(false);
            npcMan.SetActive(true);
            npcSacagawea.SetActive(false);
            npcCanvas.SetActive(false);
        }

        if (hostSelected == bigHostSacagawea)  {
            bigHostCanvas.SetActive(true);
            bigHostGirl.SetActive(false);
            bigHostMan.SetActive(false);
            bigHostSacagawea.SetActive(true);
            bigHostCanvas.SetActive(false);

            npcCanvas.SetActive(true);
            npcGirl.SetActive(false);
            npcMan.SetActive(false);
            npcSacagawea.SetActive(true);
            npcCanvas.SetActive(false);
        }

        if(hostSelected == bigHostMan && Step >= 6) {
            npc2Canvas.SetActive(true);
            npc2Girl.SetActive(false);
            npc2Man.SetActive(false);
            npc2Man.SetActive(true);
            npc2Sacagawea.SetActive(false);
            npc2Canvas.SetActive(false);
        } 

        if (hostSelected == bigHostGirl && Step >= 6) {
            npc2Canvas.SetActive(true);
            npc2Girl.SetActive(true);
            npc2Man.SetActive(false);
            npc2Man.SetActive(false);
            npc2Sacagawea.SetActive(false);
            npc2Canvas.SetActive(false);
        }

    }

    IEnumerator InitializeDialogueParameters(string[,] character, float delay, TextMeshProUGUI tmp)  {

        yield return new WaitForSeconds(delay);
        int count = 0;
        dialogueTMP.fontSize = fontSize;
        dialogueTMP.text = "";
        string returnString = hostDialogueL1.ReturnDialogue(character, 0);

        characterIndex = 0;
        StartCoroutine(Type(returnString, tmp));
        if(tmp == npcDialogueTMP) {
            npcAudioSource.Play();
        }
        else  {
            audioSource.Play();
        }
        //audioSource.Play();

        if (tmp == npc2DialogueTMP) {
            npc2AudioSource.Play();
        }
        else {
            audioSource.Play();
        }

    }

    IEnumerator Type (string str, TextMeshProUGUI TMP) {
        TMP.text = "";
        foreach(char c in str)  {
            TMP.text += c;
            characterIndex++;
            yield return new WaitForSeconds(typeDelay);
        }
        audioSource.Stop();

        if(characterIndex == str.Length) {
            audioSource.Stop();
        }
    }

    private void CheckBarnClicked()  {
        if(detectBarnClicked.barnClicked && barnIsClickable && !barnRepaired)  {
            //Debug.Log("Barn Clicked & Not Repaired");
            barnIsClickable = false;
            detectBarnClicked.barnClicked = false;
            barnRepaired = true;
            barnInventoryButton.SetActive(true);
        }

    }

    IEnumerator SwitchCamAfterDelay(CinemachineManager cinemachineManager, string method, float delay) {
        yield return new WaitForSeconds(delay);
        if(method == "ActivateWorldCam") {
            cinemachineManager.ActivateWorldCam();
        } 
        if(method == "ActivateNPCCam")  {
            cinemachineManager.ActivateNPCCam();
        }
    }

}

/*
    
    // Start is called before the first frame update
    /*Initial States: 
    * Farmer Host Game Object inactive
    * Barn is not clickable (inactive)
    * Field objects are flat and untilled (maybe can click on it but...)
    * Field menu is empty (female hemp seed icon on field menu is inactive)
    * Barn Inventory icon inactive
    * No Barn Inventory items in the Barn Inventory panel
    */

/*Step 1: Select default NPC and Host Character
 * Happens as soon as the level loads from the splash page. Player is presented with a panel and prompted to select 
 * from either Little Miss Sunshine or Farmer Boy for the main hosting character.
 * 1. Bring up panel with text instructions and clickable characters (Little Miss Sunshine or Farmer Boy)
 * 2. When desired character is clicked, the apporpriate farmer NPC is activated in the NPC prefab and for the farmer host
 * 3. After a short delay, have the selected farmer host give an intro dialogue and then continue with Step 2.
 * State Change(s): Farmer host with selected character is activated
 * */

/*Step 2: Activate Barn on lot
 * This step prompts the player to activate the inactive barn, which is already on the lot, but inactive
 * 1. The Farmer NPC Host (selected from step 1) comes up with the dialogue system and dialogue prompting player to click on the barn to 
 *    repair it. 
 *    a. Barn becomes clickable at this point
 * 2. The barn needs to be clickable, when clicked triggers particle and SFX to indicate barn is active and ready
 */

/*Step 3: Barn inventory button on lower right is activated after Step 2 completed

/*Step 4: Sakagawea gifts 100 female hemp seed
After the barn has been activated in Step3, Sakagawea (Apron Lady) comes on as the Farmer Host with Dialogue and:
 * 1. Tells the player that they are getting 100 Female Hemp Seeds they can now plant. 
 * 2. Prompts the player to check the Barn inventory to check their new Female Hemp Seed Inventory.
 * State Change(s): Female Hemp Seed inventory (icon and 100 count) are available to view in the Barn Inventory panel

/*Step 5: 100 female hemp seed available in inventory
Player clicks on the Barn Inventory icon and verifies they have 100 female hemp seeds

/*Step 6: Player tills the field
 * 1. Farmer host comes on and tells the player that he can now plant his seed.  
 * 2. But first they have to till the field to get it ready. Go ahead and click on the field to get it ready.
 * 3. Player clicks on the field
 *  a. Shovel animation and SFX
 *  b. 1 minute timer after field is clicked. I think the shovel SFX should play during that 1 minute.
 *  State Change(s): After 1 minute, the flat field object is replaced with the tilled field object
 **/

/*Step 7: 1 minute timer runs for each segment of field that is being tilled
 * 
/*Step 8: Plant female hemp seed on field
 * After 1 minute timer runs out:
 * 1. Farmer Host comes on and lets player know they can now plant the female hemp seeds. To go ahead and click the field again
 *    and select the hemp seed on the menu to plant it.
 * 2. After player clicks the tilled field, the tilled field object is replaced with the seedlings object (seed in dirt)
 * State Change(s): From tilled field to seedling object, crop growth timer activated for each field section
 */

/*Step 9: Crop Growth Cycle (automatic)
 * 1. 1 minute timer from seed --> 6" tall seedling
 * 2. 1 minute timer for 6" seedling --> 3.2' tall plant
 * 3. 1 minute timer for 3.2' tall plant --> Full plant (Bush for female)
 */

/*Step 10: Crop Harvesting
 * 1. Host lets player know first crop is ready to harvest. 
 * 2. Can click on the field to harvest the female hemp flowers
 * 3. Once harvested, the flowers will need to dry in the barn
 * Player clicks on the field:
 * 4. Show scisors animation or scissors icon over field
 * 4. Start 1 minute timer, activate the drying incon and the timer countdown by the field
 * 5. Once 1 minute is up, add ____ # of female hemp flowers to inventory
 * State Change(s): Field changes from full crop (bushes) to flat field again, ______# of flowers added to inventory
 */




// Start is called before the first frame update
/*void Start()
{
    step = 0;
    SetStep(Step.Step1);
    selectFarmerHostPanel.SetActive(false);
    onClickEvents = functionsGO.GetComponent<OnClickEvents>();
    dialogueManager = managers.GetComponent<HostDialogueManager>();
    dialogueLevel1 = dialogueGO.GetComponent<DialogueLevel1>();
    cinemachineManager = CameraRig.GetComponent<CinemachineManager>();
    npcCanvasGO.SetActive(false);
    npcDialogueManager = NPCManagersGO.GetComponent<NPCDialogueManager>();
    npcDialogueTMP.text = "";
    detectBarnClicked = barn.GetComponent<DetectBarnClicked>();
    barnInventoryButton.SetActive(false);

    //Start New Dialogue Group properties
    hostDialogueL1 = dialogueCanvasGO.GetComponent<HostDialogueL1>();
    onNextClicked = onClickManagerGO.GetComponent<OnNextClicked>();
    audioSource = dialogueCanvasGO.GetComponent<AudioSource>();
    int count = 0;
    dialogueTMP.fontSize = fontSize;
    dialogueTMP.text = "";

}*/

