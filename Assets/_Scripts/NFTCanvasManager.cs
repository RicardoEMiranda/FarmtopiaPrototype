using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NFTCanvasManager : MonoBehaviour {

    [SerializeField] public GameObject NFTInventoryPanel;
    [SerializeField] public GameObject button1;
    [SerializeField] public GameObject button2;
    [SerializeField] public GameObject content1;
    [SerializeField] public GameObject content2;
    [SerializeField] public AudioSource audioSource;

    void Start() {
        NFTInventoryPanel.SetActive(false);
    }

    public void OnNFTButtonDown() {
        NFTInventoryPanel.SetActive(true);
        audioSource.Play();
    }
    
    public void OnButton2Down() {
        content1.SetActive(false);
        content2.SetActive(true);
        audioSource.Play();
    }

    public void OnButton1Down() {
        content1.SetActive(true);
        content2.SetActive(false);
        audioSource.Play();
    }

    public void OnExitButtonDown()  {
        NFTInventoryPanel.SetActive(false);
        audioSource.Play();
    }
}
