using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldController : MonoBehaviour {

    public GameObject seedCorn;
    public GameObject seedSquash;
    

    private void OnMouseDown() {
        Debug.Log("Click");
        seedCorn.SetActive(true);
        seedSquash.SetActive(true);
    }

}
