using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldController : MonoBehaviour {

    public GameObject tilledSoil;
    public GameObject seed;
    public GameObject seedling;
    public GameObject medium;
    public GameObject large;

    [SerializeField] public float cycleTimer = 5f;
    [SerializeField] public GameObject plantSeedCanvas;
    [SerializeField] public GameObject harvesterCanvas;
    [SerializeField] public AudioSource audioSource1;
    [SerializeField] public AudioSource audioSource2;
    [SerializeField] public AudioClip clipShoveling;
    [SerializeField] public AudioClip clipCropPop;
    [SerializeField] public AudioClip clipDingPop;
    [SerializeField] public GameObject largeCropGO;
    private OnCropClicked onCropClicked;



    public bool fieldTilled = false;
    private bool tillRequested = false;
    public bool fieldSelected = false;
    public bool canPlant = false;
    public bool startedSeedTimer = false;
    public bool startSeedlingTimer = false;
    public bool startMediumTimer = false;
    private bool seedlingTimerAudioStarted = false;
    private bool mediumTimerAudioStarted = false;
    public bool readyToHarvest = false;
    public bool cropHarvested = false;

    //NOTE: Need to add the routine where the first time player clicks the field, it actually tills the soil
    //currently it goes right into picking the seed and harvesting

    private void Start()  {
        audioSource1.clip = clipShoveling;
        audioSource2.clip = clipCropPop;
        onCropClicked = largeCropGO.GetComponent<OnCropClicked>();
    }
    private void Update() {

        if(startSeedlingTimer && !seedlingTimerAudioStarted)  {
            StartCoroutine(StartSeedlingTimer(cycleTimer));
            seedlingTimerAudioStarted = true;
            audioSource2.Play();
        }

        if (startMediumTimer && !mediumTimerAudioStarted) {
            StartCoroutine(StartMediumTimer(cycleTimer));
            mediumTimerAudioStarted = true;
            audioSource2.Play();
        }

        if (onCropClicked.cropClicked)  {
            Debug.Log("Crop Clicked...");
            harvesterCanvas.SetActive(true);
        }

    }

    private void OnMouseDown() {

        if(!fieldTilled && !tillRequested  && canPlant)  {
            //Start TillField Coroutine, time for 60 seconds then turn on tilledSoil GameObject
            tillRequested = true;
            StartCoroutine(TillField(cycleTimer));
        }
      
        if(canPlant && fieldTilled)  {
            fieldSelected = true;
            plantSeedCanvas.SetActive(true);
        }

        if(readyToHarvest && onCropClicked.cropClicked)  {
            //Debug.Log("Clicked after Ready to Harvest");
            harvesterCanvas.SetActive(true);
        }
        
        
    }

    IEnumerator TillField(float delay) {
        audioSource1.Play();
        tilledSoil.SetActive(true);
        yield return new WaitForSeconds(delay);
        fieldTilled = true;
    }

    public void PlantSeedButtonClicked() {
        canPlant = false;
        //turn off plantSeed Canvas
        plantSeedCanvas.SetActive(false);

        //turn on seed GameObject, but check firt that SeedTimer hasn't started yet, if it has, don't set active
        if(!startedSeedTimer)  {
            seed.SetActive(true);
            audioSource2.Play();
        }

        //Start seedTimer coroutine
        StartCoroutine(StartSeedTimer(cycleTimer));
        audioSource2.Play();

    }


    public void HarvesterClicked() {
        Debug.Log("Harvester Clicked");
        large.SetActive(false);
        harvesterCanvas.SetActive(false);
        tilledSoil.SetActive(false);
        audioSource1.clip = clipDingPop;
        audioSource1.Play();
        cropHarvested = true;
    }

    

    IEnumerator StartSeedTimer(float delay) {
        startedSeedTimer = true;
        yield return new WaitForSeconds(delay);
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
        audioSource2.Play();
        startMediumTimer = false;
        readyToHarvest = true;
    }

}

//Detect when field is clicked 
//When clicked, bring up hemp seed menu


