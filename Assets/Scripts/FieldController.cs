using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldController : MonoBehaviour {

    public GameObject seedCorn;
    public GameObject seedCarrot;
    public GameObject seedMaze;
    public GameObject carrotSeedlings;
    public bool fieldSelected = false;

    private void Update() {

      
    }

    private void OnMouseDown() {
        //Debug.Log("Click");
        seedCorn.SetActive(true);
        seedCarrot.SetActive(true);
        seedMaze.SetActive(true);

        if (gameObject.tag == "LastField")  {
            //Debug.Log("Last field selected: " + gameObject.name);
            fieldSelected = true;
        }
    }

}
