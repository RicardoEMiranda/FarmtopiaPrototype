using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldController : MonoBehaviour {

    public GameObject seed;
    public GameObject seedling;
    public GameObject medium;
    public GameObject large;
    [SerializeField] public float cycleTimer = 5f;
    [SerializeField] public GameObject plantSeedCanvas;



    public bool fieldSelected = false;
    public bool canPlant = false;
    public bool startedSeedTimer = false;
    public bool startSeedlingTimer = false;
    public bool startMediumTimer = false;

    //NOTE: Need to add the routine where the first time player clicks the field, it actually tills the soil
    //currently it goes right into picking the seed and harvesting

    private void Update() {

        if(startSeedlingTimer)  {
            StartCoroutine(StartSeedlingTimer(cycleTimer));
        }

        if (startMediumTimer) {
            StartCoroutine(StartMediumTimer(cycleTimer));
        }

    }

    private void OnMouseDown() {
      
        if(canPlant)  {
            fieldSelected = true;
            plantSeedCanvas.SetActive(true);
        }
        
        
    }

    public void PlantSeedButtonClicked() {

        canPlant = false;
        //turn off plantSeed Canvas
        plantSeedCanvas.SetActive(false);

        //turn on seed GameObject, but check firt that SeedTimer hasn't started yet, if it has, don't set active
        if(!startedSeedTimer)  {
            seed.SetActive(true);
        }

        //Start seedTimer coroutine
        StartCoroutine(StartSeedTimer(cycleTimer));

    }

    IEnumerator StartSeedTimer(float delay) {
        startedSeedTimer = true;
        Debug.Log("Start Seed Timer");
        yield return new WaitForSeconds(delay);
        Debug.Log("Timer done");
        seed.SetActive(false);
        seedling.SetActive(true);
        startSeedlingTimer = true;
    }

    IEnumerator StartSeedlingTimer(float delay)  {
        yield return new WaitForSeconds(delay);
        seedling.SetActive(false);
        medium.SetActive(true);
        startSeedlingTimer = false;
        startMediumTimer = true;
    }

    IEnumerator StartMediumTimer(float delay)  {
        yield return new WaitForSeconds(delay);
        medium.SetActive(false);
        large.SetActive(true);
        startMediumTimer = false;
    }

}

//Detect when field is clicked 
//When clicked, bring up hemp seed menu


