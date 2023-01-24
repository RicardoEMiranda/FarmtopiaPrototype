using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NFTInventoryManager : MonoBehaviour {

    [SerializeField] public GameObject NFTInventoryPanel;
    [SerializeField] public GameObject characterNFTTabButton;
    [SerializeField] public GameObject landNFTTabButton;
    [SerializeField] public GameObject characterNFTPanel;
    //[SerializeField] public GameObject landNFTPanel;
    [SerializeField] public AudioSource audioSource;

    public void OnNFTButtonDown() {
        NFTInventoryPanel.SetActive(true);
        //Debug.Log("True");
        audioSource.Play();
    }

    public void OnCharacterNFTButtonDown()  {
        Debug.Log("Character NFT Button Down");
        audioSource.Play();

    }

    public void OnLandNFTButtonDown() {
        Debug.Log("Land NFT Button Down");
        characterNFTPanel.SetActive(false);
        audioSource.Play();

    }

    public void OnExitButtonDown()  {
        NFTInventoryPanel.SetActive(false);
        audioSource.Play();
    }
}
