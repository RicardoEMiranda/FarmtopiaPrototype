using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnCropClicked : MonoBehaviour {

    public bool cropClicked;

    // Start is called before the first frame update
    void Start() {
        cropClicked = false;
    }

    // Update is called once per frame
    void Update()  {
        //Debug.Log(cropClicked);
    }

    public void OnMouseDown()  {
        cropClicked = true;
        //Debug.Log("Crop Clicked: " + cropClicked);
    }
}
