using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager_v2 : MonoBehaviour {

    [SerializeField] private TextMeshProUGUI tmpDisplay;

    [TextArea(2,4)]
    [SerializeField] public string[] level1Dialogue;
    [SerializeField] private Transform level1Waypoints;
    [SerializeField] private GameObject farmHandAntonio;
    [SerializeField] private GameObject canvasSpeechBubble;
    [SerializeField] public float typingSpeed = .04f;
    [SerializeField] public GameObject bubbleImage;

    private MissionManager missionManager;
    private int mission;
    private string[,] dialogueArray;
    public bool stop;

    private float delta;
    private int index = 0;

    [SerializeField] public GameObject waypoint;
    private CollisionDetector collisionDetector;

    [SerializeField] public AudioSource audioSource;

    // Start is called before the first frame update
    void Start() {        

        collisionDetector = waypoint.GetComponent<CollisionDetector>();
        //string[,] dialogueMatrix = new string[0,L0M1Dialogue.Length + L0M2Dialogue.Length];
        //L0M1Dialogue.CopyTo(dialogueMatrix,0);
        //L0M2Dialogue.CopyTo(dialogueMatrix, L0M1Dialogue.Length);
        
    }

    // Update is called once per frame
    void Update()  {

        if (missionManager.playMissionDialogue) {
            mission = missionManager.currentMission;
            Debug.Log("Play Mission Dialogue: " + missionManager.currentMission);
            
            GetDialogue();
            StartCoroutine(TypeText(dialogueArray[0, index]));
            audioSource.Play();
            missionManager.playMissionDialogue = false;
        } 
   
    }

    IEnumerator TypeText(string array) {
        foreach (char letter in dialogueArray[0, index].ToCharArray()) {
            tmpDisplay.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
        audioSource.Stop();
        //StopCoroutine(TypeText(dialogueArray[0,index]));
    }

    public void OnNextButtonDown() {
        //Debug.Log("Next");

        if(index < dialogueArray.Length -1)  {
            index++;
            tmpDisplay.text = ""; //reset the text before printing more
            StartCoroutine(TypeText(dialogueArray[0, index]));
            audioSource.Play();
        }
        else  {
            tmpDisplay.text = "";
            //Debug.Log("else reached");
            //canvasSpeechBubble.SetActive(false);
            bubbleImage.SetActive(false);
            dialogueArray = null;
            index = 0;
        }
        
    }

    private void GetDialogue()  {

        if (missionManager.level==0 && missionManager.currentMission==1)  {
            //Debug.Log("Level: " + missionManager.level + "  Mission: " + missionManager.nextMission);
            dialogueArray = new string[1, 3] { 
                {"Hello. I'm Farmhand Antonio!",
                "You can click on the field next to me to select the hemp seed varietal to plant a new crop.",
                "Go ahead, try it!",
                },
             };
        } else if (missionManager.level == 0 && missionManager.currentMission == 2)  {
            dialogueArray = null;
            Debug.Log("Level: " + missionManager.level + "  Mission: " + missionManager.currentMission);
            dialogueArray = new string[1, 5]   {
                {"Great job! You've planted your first batch of hemp seed.",
                "Now, we just wait until the crop is ready to harvest.",
                "In the mean time, check out your crop and NFT inventories.",
                "You can click on the inventory icons on the lower right to check them.",
                "You can purchase NFTs and trade them in for discounts and free CBD products in our store.",
                },
            };
        }
        else {
            Debug.Log("Exception.");
        }

    }


}
