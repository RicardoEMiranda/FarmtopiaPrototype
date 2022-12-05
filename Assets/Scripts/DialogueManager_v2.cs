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

    [SerializeField] public GameObject MissionManager;
    private MissionManager missionManager;

    private string[,] dialogueMatrix = new string[1, 3] { 
        //[1,3] is for 1 column, 3 rows
        //when ready to implement level specific dialogue, can use the column dimension for each level and row dimension for the current mission#
        //For example, if have 10 levels, would declare dialogueMatrix = new string[10, mission#]
        //The level# and mission# can be public integers that come from MissionManager inside of the GameManager Object in hierarchy

        //Sample Level 1 (index 0) dialogue.
        {"Hello. I'm Farmhand Antonio!",
         "You can click on the field next to me to select the hemp seed varietal to plant a new crop.",
         "Go ahead, try it!",
        }
    };

    private float delta;
    private int index = 0;

    [SerializeField] public GameObject waypoint;
    private CollisionDetector collisionDetector;

    [SerializeField] public AudioSource audioSource;

    // Start is called before the first frame update
    void Start() {        

        collisionDetector = waypoint.GetComponent<CollisionDetector>();
        missionManager = MissionManager.GetComponent<MissionManager>();
    }

    // Update is called once per frame
    void Update()  {

        if(missionManager.playMissionDialogue) {
            //if(collisionDetector.hitFarmerAntonio) {
            //Debug.Log("Farmer Antonio at Waypoint");
            //collisionDetector.hitFarmerAntonio = false;
            Debug.Log("Play Mission Dialogue true");
            missionManager.playMissionDialogue = false;
            StartCoroutine(TypeText());
            audioSource.Play();
        }
    }

    IEnumerator TypeText() {
        foreach (char letter in dialogueMatrix[0, index].ToCharArray()) {
            tmpDisplay.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
        audioSource.Stop();
    }

    public void OnNextButtonDown() {
        //Debug.Log("Next");

        if(index < dialogueMatrix.Length -1)  {
            index++;
            tmpDisplay.text = ""; //reset the text before printing more
            StartCoroutine(TypeText());
            audioSource.Play();
        }
        else  {
            tmpDisplay.text = "";
            //Debug.Log("else reached");
            //canvasSpeechBubble.SetActive(false);
            bubbleImage.SetActive(false);
        }
        
    }


}
