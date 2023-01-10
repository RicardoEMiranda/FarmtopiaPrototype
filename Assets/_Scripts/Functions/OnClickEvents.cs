using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnClickEvents : MonoBehaviour {

    public bool hostNextClicked;
    public int noOfClicks;
    public string hostClickedString;
    public bool dialogueNextButtonClicked;
    public bool doneWithDemo;
    [SerializeField] private AudioSource audioPanelPop;
    [SerializeField] private AudioClip clip_click;
    [SerializeField] private GameObject levelUpPanel;
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

    public void OnPanelExit()  {
        //Debug.Log("Exit");
        audioPanelPop.clip = clip_click;
        audioPanelPop.Play();
        levelUpPanel.SetActive(false);
        doneWithDemo = true; //used for demo purposes only
    }

   

   
    
}


