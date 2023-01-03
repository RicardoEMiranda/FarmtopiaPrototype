using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnClickEvents : MonoBehaviour {

    public bool hostNextClicked;
    public int noOfClicks;
    public string hostClickedString;
    public bool dialogueNextButtonClicked;
    [SerializeField] private AudioSource audioPanelPop;
    public bool resetClicks;

    private void Start()  {
        hostClickedString = "";
        noOfClicks = 0;
        hostNextClicked = false;
        //Debug.Log(noOfClicks);
    }

    public void OnPickLittleMissHost() {
        //Debug.Log("LMS");
        hostNextClicked = true;
        hostClickedString = "LittleMissSunshine";
        audioPanelPop.Play();
    }

    public void OnPickYoungManHost()  {
        //Debug.Log("Young Man Host");
        hostNextClicked = true;
        hostClickedString = "YoungMan";
        audioPanelPop.Play();
    }

    public void OnNextArrowClicked() {
        noOfClicks += 1;
        dialogueNextButtonClicked = true;
        Debug.Log(noOfClicks);
    }


    
}


