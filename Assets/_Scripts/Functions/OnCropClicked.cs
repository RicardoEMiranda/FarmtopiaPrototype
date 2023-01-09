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
    void Update()
    {
        
    }

    public void OnMouseDown()  {
        cropClicked = true;
    }
}
