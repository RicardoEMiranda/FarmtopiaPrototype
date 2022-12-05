using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionManager : MonoBehaviour {

    [SerializeField] public GameObject wayPoint01;
    [SerializeField] public GameObject[] field;
    private FieldController[] fieldController = new FieldController[6];
    public int level = 0;
    public int currentMission = 1;
    public int dialogueQueue = 0;
    public bool playMissionDialogue;
    public bool noFieldsSelected = true;

    private CollisionDetector collisionDetector;
    [SerializeField] public GameObject dialogueBubble;
   

    // Start is called before the first frame update
    void Start() {

        collisionDetector = wayPoint01.GetComponent<CollisionDetector>();
        for(int i=0; i<field.Length -1; i++)  {
            fieldController[i] = field[i].GetComponent<FieldController>();
        }
        //Debug.Log(fieldController);
    }

    // Update is called once per frame
    void Update() {

        DetectMission1();
        DetectMission2();
        
    }

    private void DetectMission1() {
        if (collisionDetector.hitFarmerAntonio) {
            //Debug.Log("Hit Farmer Antonio (MissionManager");
            collisionDetector.hitFarmerAntonio = false;
            playMissionDialogue = true;
            dialogueQueue = 0;
            currentMission = 1;
        }  
    }

    private void DetectMission2()  {

        for(int i=0; i<fieldController.Length-1; i++)  {
            if (fieldController[i].transform.GetChild(0).gameObject.activeSelf && noFieldsSelected) {
                dialogueBubble.SetActive(true);
                //Debug.Log("Crop is active: " + fieldController[i].transform.GetChild(0).gameObject.name);
                noFieldsSelected = false;
                dialogueQueue = 1;
                currentMission = 2;
                playMissionDialogue = true;
                //Debug.Log("Crop go is active: " + fieldController[i].transform.GetChild(0).gameObject.activeSelf);
                //Debug.Log("Play mission 2 dialogue: " + playMissionDialogue);
                //Debug.Log("Next Mission: " + nextMission);
            } 
        }
    }
}
