using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionManager : MonoBehaviour {

    [SerializeField] public GameObject wayPoint01;
    [SerializeField] public GameObject[] field;
    private FieldController[] fieldController = new FieldController[6];
    public int level = 0;
    public int nextMission = 0;
    public int dialogueQueue = 0;
    public bool playMissionDialogue;

    private CollisionDetector collisionDetector;
   

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

        DetectMission0();
        DetectMission1();
    }

    private void DetectMission0() {
        if (collisionDetector.hitFarmerAntonio) {
            Debug.Log("Hit Farmer Antonio (MissionManager");
            collisionDetector.hitFarmerAntonio = false;
            playMissionDialogue = true;
            dialogueQueue = 0;
        }
    }

    private void DetectMission1()  {

        for(int i=0; i<fieldController.Length-1; i++)  {
            if (fieldController[i].transform.GetChild(0).gameObject.activeSelf) {
                //Debug.Log("Crop is active");
                dialogueQueue = 1;
            }
        }
    }
}
