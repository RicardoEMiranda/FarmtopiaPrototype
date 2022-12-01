using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour {

    [SerializeField] public GameObject panelCropInventory;
    [SerializeField] public GameObject panelNFTInventory;
    public AudioSource click;

    public bool nextClicked;

    // Start is called before the first frame update
    void Start()  {
        panelCropInventory.SetActive(false);
    }

    // Update is called once per frame
    void Update()  {
        
    }

    public void OnCropInventoryButtonDown() {
        //Debug.Log("Bring up Inventory.");
        panelCropInventory.SetActive(true);
        click.Play();
    }

    public void OnCropInventoryExitButtonDown() {
        panelCropInventory.SetActive(false);
        click.Play();
    }

    public void OnNFTButtonDown() {
        //Debug.Log("Bring up NFT Menu");
        panelNFTInventory.SetActive(true);
        click.Play();
    }

    public void OnNFTInventoryExitButtonDown() {
        panelNFTInventory.SetActive(false);
        click.Play();
    }

    public void OnDialogueNextButtonDown()  {
        Debug.Log("Next >");
        nextClicked = true;
    }
}
