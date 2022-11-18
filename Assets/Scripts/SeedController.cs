using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedController : MonoBehaviour {

    public GameObject seedCorn;
    public GameObject seedSquash;

    private void OnMouseDown() {
        Debug.Log(gameObject.name);
        seedCorn.SetActive(false);
        seedSquash.SetActive(false);
    }

}
