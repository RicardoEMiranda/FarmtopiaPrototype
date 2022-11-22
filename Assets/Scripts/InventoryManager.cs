using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour {

    public GameObject panel;
    public AudioSource click;

    // Start is called before the first frame update
    void Start()  {
        
    }

    // Update is called once per frame
    void Update()  {
        
    }

    public void OnInventoryButtonDown() {
        Debug.Log("Bring up Inventory.");
        panel.SetActive(true);
        click.Play();
    }

    public void OnInventoryExitButtonDown() {
        panel.SetActive(false);
        click.Play();
    }

    public void OnNFTButtonDown() {
        Debug.Log("Bring up NFT Menu");
    }
}
