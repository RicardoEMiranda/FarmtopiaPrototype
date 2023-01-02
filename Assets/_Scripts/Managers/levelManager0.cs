using System.Collections;
using System.Collections.Generic;
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

    [Header("NPC Character Properties")]
    [SerializeField] private GameObject npcCanvas;
    [SerializeField] private GameObject npcMan;
    [SerializeField] private GameObject npcGirl;
    [SerializeField] private GameObject npcDialogueBinGO;
    [SerializeField] private GameObject npcDialogueCanvasGO;
    [SerializeField] private GameObject npcClickManagerGO;
    private AudioSource npcAudioSource;
    [SerializeField] private TextMeshProUGUI npcDialogueTMP;
    [SerializeField] private Transform waypoint1;
    private FarmerAI farmerAI;


    [Header ("Select Host Panel Properties")]
    [SerializeField] private GameObject selectFarmerHostPanel;
    [SerializeField] private GameObject manFarmerButton;
    [SerializeField] private GameObject girlFarmerButton;
    private bool farmerHostSelected;

    [Header ("Big Host Character Properties")]
    [SerializeField] private GameObject bigHostCanvas;
    [SerializeField] private GameObject bigHostMan;
    [SerializeField] private GameObject bigHostGirl;
    private GameObject activeHost;
    private bool hostSelected;
    private bool hostPreferencesSet;

    [Header("Cameras")]
    [SerializeField] private GameObject CameraRig;
    private CinemachineManager cinemachineManager;
 


    [SerializeField] private GameObject functionsGO;
    private OnClickEvents onClickEvents;

    private int Step = 1;
    private int characterIndex;
    private float typeDelay = .05f;
    private int count;
    private bool npcActivated;

    private bool introDialogueStarted;
    private bool npcIntroDialogueStarted;

    // Start is called before the first frame update
    void Start()  {
        selectFarmerHostPanel.SetActive(false);
        farmerHostSelected = false;
        onClickEvents = functionsGO.GetComponent<OnClickEvents>();
        hostPreferencesSet = false;

        hostDialogueL1 = dialogueBinGO.GetComponent<HostDialogueL1>();
        audioSource = dialogueBinGO.GetComponent<AudioSource>();
        npcAudioSource = npcDialogueCanvasGO.GetComponent<AudioSource>();
        onNextClicked = clickManagerGO.GetComponent<OnNextClicked>();
        npcOnNextClicked = npcClickManagerGO.GetComponent<OnNextClicked>();
        farmerAI = npcCanvas.GetComponent<FarmerAI>();
        cinemachineManager = CameraRig.GetComponent<CinemachineManager>();
    }

    // Update is called once per frame
    void Update() {
        if(Step==1)  {
            //turn on Select Farmer Host Panel. Do it once and condition out of Update loop
            if(!hostSelected)  {
                selectFarmerHostPanel.SetActive(true);
                //make sure hostSelected = true; in next step when host is selected
            }

            //Check when and which button is selected (Farmer Man or Farmer Girl)
            if(onClickEvents.hostNextClicked)  {
                //Debug.Log("Button clicked");
                if (onClickEvents.hostClickedString == "LittleMissSunshine")  {
                    activeHost = bigHostGirl;
                }
                if (onClickEvents.hostClickedString == "YoungMan")  {
                    activeHost = bigHostMan;
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
                    dialogueBinGO.SetActive(false);
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
                }
                else  {
                    string output = hostDialogueL1.ReturnDialogue(hostDialogueL1.NPC, count);
                    characterIndex = 0;
                    StartCoroutine(Type(output, npcDialogueTMP));
                    npcAudioSource.Play();
                    //string output = hostDialogueL1.ReturnDialogue(hostDialogueL1.NPC, count);
                    //characterIndex = 0;
                    //StartCoroutine(Type(output, npcDialogueTMP));
                    //npcAudioSource.Play();
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
    }

    private void SetHostPreferences(GameObject hostSelected) {

        if(activeHost == bigHostGirl)  {
            bigHostCanvas.SetActive(true);
            bigHostGirl.SetActive(true);
            bigHostMan.SetActive(false);
            bigHostCanvas.SetActive(false);

            npcCanvas.SetActive(true);
            npcGirl.SetActive(true);
            npcMan.SetActive(false);
            npcCanvas.SetActive(false);
        } 
        if(activeHost == bigHostMan) {
            bigHostCanvas.SetActive(true);
            bigHostGirl.SetActive(false);
            bigHostMan.SetActive(true);
            bigHostCanvas.SetActive(false);

            npcCanvas.SetActive(true);
            npcGirl.SetActive(false);
            npcMan.SetActive(true);
            npcCanvas.SetActive(false);
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

    

}

/*
 
public class LevelManager_0 : MonoBehaviour {

    private int step;

    //Select Farmer Host Variables
    [SerializeField] private GameObject selectFarmerHostPanel;
    private bool farmerHostSelected;

    [SerializeField] private GameObject hostOverlay;
    [SerializeField] private TextMeshProUGUI hostOverlayText;
    [SerializeField] private GameObject functionsGO;
    private OnClickEvents onClickEvents;
    public string hostSelected;

    private Step currentStep;

    [SerializeField] private GameObject farmerHostOverlay;
    [SerializeField] private GameObject farmerHostBoy;
    [SerializeField] private GameObject farmerHostGirl;
    [SerializeField] private GameObject sacagawea;
    [SerializeField] private GameObject farmerNPC;
    [SerializeField] private GameObject farmerNPCBoy;
    [SerializeField] private GameObject farmerNPCGirl;

    [SerializeField] private GameObject managers;
    [SerializeField] private GameObject dialogueGO;
    private HostDialogueManager dialogueManager;
    private DialogueLevel1 dialogueLevel1;
    [SerializeField] private GameObject CameraRig;
    private CinemachineManager cinemachineManager;
    [SerializeField] private GameObject npcCanvasGO;
    [SerializeField] private TextMeshProUGUI npcDialogueTMP;
    [SerializeField] private GameObject barnWaypoint;
    [SerializeField] private GameObject hostDialogueGroup;
    private bool arrivedAtBarn;

    [SerializeField] private GameObject NPCManagersGO;
    private NPCDialogueManager npcDialogueManager;
    public bool barnIsClickable;
    [SerializeField] private GameObject barn;
    private DetectBarnClicked detectBarnClicked;
    [SerializeField] private GameObject barnInventoryButton;

    [Header ("Dialogue Bubble Properties")]
    //New Dialogue Group Properties
    [SerializeField] private GameObject dialogueCanvasGO;
    [SerializeField] private GameObject onClickManagerGO;
    [SerializeField] private TextMeshProUGUI dialogueTMP;
    [SerializeField] public int fontSize = 28;
    private OnNextClicked onNextClicked;
    private HostDialogueL1 hostDialogueL1;
    private AudioSource audioSource;
    private int count;
    private int characterIndex;
    private float typeDelay = .05f;
 
    
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

