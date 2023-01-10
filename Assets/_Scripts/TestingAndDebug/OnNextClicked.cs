using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnNextClicked : MonoBehaviour {

    [SerializeField] public GameObject levelManagerGO;
    private levelManager0 levelManager0;
    public bool clicked;

    private void Start() {
        clicked = false;
        levelManager0 = levelManagerGO.GetComponent<levelManager0>();
    }

    public void Clicked()  {
        if(levelManager0.stoppedTyping)  {
            clicked = true;
            Debug.Log(clicked);
        }
        //clicked = true;
        //Debug.Log(clicked);
    }
   
}
