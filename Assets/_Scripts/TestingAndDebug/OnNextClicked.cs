using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnNextClicked : MonoBehaviour {

    public bool clicked;

    private void Start() {
        clicked = false;
    }

    public void Clicked()  {
        clicked = true;
     
    }
   
}
