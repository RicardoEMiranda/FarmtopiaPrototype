using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TypeDialogue : MonoBehaviour
{
    //Purpose: TypeDialogue will take in the integer level from GameManager (field input for testing) and the corresponding dialogue line
    //from HostDialogueLevelx and type it out onto the HostFarmer's text mesh pro text field.

    //Integer Level from Input Field: need reference to the integer input in the Level Input field in the game (used for testing only)
    //Use GameManager.GetLevel() (GetLevel() is public) to obtain the current Level input
    [SerializeField] public GameObject gameManagerGO;
    private GameManager gameManager;
    private int level;
    private bool startedTyping;

    //HostDialogueLevelx Reference: Need a refernce to the HostDialogueLevelx script to access the appropriate Dialogue array


    private void Start()
    {
        gameManager = gameManagerGO.GetComponent<GameManager>();
    }

    private void Update()
    {
        //gameManager = gameManagerGO.GetComponent<GameManager>();
        //int level = gameManager.GetLevel();
        //Debug.Log(level);
    }

    public string Type(string dialogue, TextMeshProUGUI txt)  {

         //StartCoroutine(TypeText(dialogue, txt));
         //audioSource.Play();
         return dialogue;
         //return dialogue;
         //return "Test string";
      
    }

    IEnumerator TypeText(string line, TextMeshProUGUI txt)  {
        foreach (char letter in line.ToCharArray())  {
            txt.text +=  letter;
            yield return new WaitForSeconds(.2f);
        }
        //audioSource.Stop();

    }
}

/*
 * void Update()  {

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
 */
