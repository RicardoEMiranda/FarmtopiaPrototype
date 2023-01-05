using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldController : MonoBehaviour {

    public GameObject seed;
    public GameObject seedling;
    public GameObject medium;
    public GameObject large;
    [SerializeField] public float cycleTimer = 60f;

    public bool fieldSelected = false;


    private void Update() {

      
    }

    private void OnMouseDown() {
      
        fieldSelected = true;
        
    }

}
