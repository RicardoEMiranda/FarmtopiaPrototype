using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Cinemachine;

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
    private bool arrivedAtBarn;

    [SerializeField] private GameObject NPCManagersGO;
    private NPCDialogueManager npcDialogueManager;
    public bool barnIsClickable;
    [SerializeField] private GameObject barn;
    private DetectBarnClicked detectBarnClicked;



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
     * 1. The Farmer Host (selected from step 1) comes up with the dialogue system and dialogue prompting player to click on the barn to 
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
    void Start() {
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
    }

    private void Update() {
        CheckHostSelected();

        CheckDialogueSequenceFinished();

        if (dialogueLevel1.barnIntroComplete)  {
            //Debug.Log("Barn Intro Complete");
        }

        CheckNPCHostAtBarn();
        CheckNPCDialogueSequenceFinished();
        CheckBarnClickedStatus();
    }

    private void SetStep(Step currentStep) {
        switch(currentStep)   {
            case Step.Step1:
                RunStep1();
                break;

            case Step.Step2:
                //reset number of OnClickEvents.numberOfClicks

                RunStep2();
                break;

        }
        
    }

    
    public void CheckGotLM1() {
        Debug.Log("Got LM1");
    }

    IEnumerator PanelDelay(float delay) {
        yield return new WaitForSeconds(delay);
        selectFarmerHostPanel.SetActive(true);
    }

    IEnumerator OverlayDelay(GameObject panelObject, float delay)  {
        yield return new WaitForSeconds(delay);

        if(panelObject == selectFarmerHostPanel)  {
            selectFarmerHostPanel.SetActive(true);
        }
        if(panelObject == hostOverlay) {
            hostOverlay.SetActive(true);
        }

    
    }

    void IncrementStep()  {
        step += 1;
    }

    private enum Step   {
        Step1,
        Step2,
        Step3,
        Step4,
        Step5, 
        Step6,
        Step7, 
        Step8, 
        Step9,
        Step10,
        Step11
    }

    private void RunStep1() {
        //Debug.Log("Step 1 case");
        StartCoroutine(OverlayDelay(selectFarmerHostPanel, 2f));
   
    }

    private void RunStep2()  {
        //Debug.Log("Run Step2");
        StartCoroutine(OverlayDelay(farmerHostOverlay, 1.5f));

        //Check that NPC character is at the barn waypoint then, 
        //activate the dialogue box (NPCDialogueBuble.SetActive(true))
        Vector3 difference = farmerNPC.transform.position - barnWaypoint.transform.position;
        if(difference.magnitude <=.5)  {
            //Debug.Log("Arrived at barn waypoint");
        }
        
    }

    private void CheckHostSelected()  {
        if (onClickEvents.hostNextClicked)  {
            //Debug.Log("Host selected: " + onClickEvents.hostClickedString);
            hostSelected = onClickEvents.hostClickedString;
            onClickEvents.hostNextClicked = false;
            selectFarmerHostPanel.SetActive(false);
            
            if(hostSelected == "LittleMissSunshine")  {
                //Set overlay host
                farmerHostOverlay.SetActive(true);
                farmerHostBoy.SetActive(false);
                farmerHostGirl.SetActive(true);
                farmerHostOverlay.SetActive(false);

                //set NPC host
                farmerNPC.SetActive(true);
                farmerNPCBoy.SetActive(false);
                farmerNPCGirl.SetActive(true);
                farmerNPC.SetActive(false);
            }

            if(hostSelected == "YoungMan")  {
                //set overlay host
                farmerHostOverlay.SetActive(true);
                farmerHostBoy.SetActive(true);
                farmerHostGirl.SetActive(false);
                farmerHostOverlay.SetActive(false);

                //set NPC host
                farmerNPC.SetActive(true);
                farmerNPCBoy.SetActive(true);
                farmerNPCGirl.SetActive(false);
                farmerNPC.SetActive(false);
            }
            SetStep(Step.Step2);
            IncrementStep();
            //Debug.Log(step);
        }
    }

    private void ActivateNPC()  {
        //Debug.Log(hostSelected);
        farmerNPC.SetActive(true);
        
        if(hostSelected == "LittleMissSunshine")  {
            //Debug.Log("if entered");
            farmerNPCGirl.SetActive(true);
            farmerNPCBoy.SetActive(false);
        }

        if(hostSelected == "YoungMan") {
            farmerNPCGirl.SetActive(false);
            farmerNPCBoy.SetActive(true);
        }
        
    }

    private void CheckDialogueSequenceFinished()  {
        if(dialogueManager.dialogueSequenceFinished) {
            //Debug.Log("Dialogue Sequence Finished");
            dialogueManager.dialogueSequenceFinished = false;
            hostOverlay.SetActive(false);
            cinemachineManager.ActivateNPCCam();

            ActivateNPC(); 
        }
    }

    private void CheckNPCDialogueSequenceFinished() {
        if(npcDialogueManager.dialogueSequenceFinished)   {
            //Debug.Log("Sequence Finished");
            npcCanvasGO.SetActive(false);
            barnIsClickable = true;
        }
    }

    private void CheckNPCHostAtBarn()   {

        if (!arrivedAtBarn)  {
            //Check that NPC character is at the barn waypoint then, 
            //activate the dialogue box (NPCDialogueBuble.SetActive(true))
            Vector3 difference = farmerNPC.transform.position - barnWaypoint.transform.position;
            if (difference.magnitude <= .1)   {
                Debug.Log("Arrived at barn waypoint");
                arrivedAtBarn = true;
                npcCanvasGO.SetActive(true);
                npcDialogueManager.StartDialogue();
 
            }
        }
    }

    private void CheckBarnClickedStatus()  {
        if(detectBarnClicked.barnClicked) {
            Debug.Log("Barn Clicked");
            barnIsClickable = false;
        }
    }



}
