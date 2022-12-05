using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionManager : MonoBehaviour {

    [SerializeField] public GameObject wayPoint01;
    public int level = 0;
    public int nextMission = 0;
    public int dialogueQueue = 0;
    public bool playMissionDialogue;

    private CollisionDetector collisionDetector;
   

    // Start is called before the first frame update
    void Start() {
        collisionDetector = wayPoint01.GetComponent<CollisionDetector>();
    }

    // Update is called once per frame
    void Update() {

        DetectMission0();

    }

    private void DetectMission0() {
        if (collisionDetector.hitFarmerAntonio) {
            Debug.Log("Hit Farmer Antonio (MissionManager");
            collisionDetector.hitFarmerAntonio = false;
            playMissionDialogue = true;
            dialogueQueue = 0;
        }
    }
}
