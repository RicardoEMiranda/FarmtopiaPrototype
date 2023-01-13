using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedController : MonoBehaviour {

    public GameObject seedCorn;
    public GameObject seedCarrot;
    public GameObject seedMaze;
    public GameObject carrotSeedlings;

    private void OnMouseDown() {
        //Debug.Log(gameObject.name);
        seedCorn.SetActive(false);
        seedCarrot.SetActive(false);
        seedMaze.SetActive(false);
        carrotSeedlings.SetActive(true);
    }

}
