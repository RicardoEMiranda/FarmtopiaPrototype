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

    private 

    // Start is called before the first frame update
    void Start()  {
        currentLevel = GetLevel();
        level1Manager = levelContainer.GetComponent<Level1Manager>();
        level2Manager = levelContainer.GetComponent<Level2Manager>();
    }

    // Update is called once per frame
    void Update() {
        currentLevel = GetLevel();
        //Debug.Log(currentLevel);

        RunLevelManager(currentLevel);
    }

    int GetLevel() {
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

        }

        if(level == 2)  {
            //use Level2Manager
            //Debug.Log(level2Manager.GetLevelMessage());
        }

    } 

    

}

