using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnNextClicked : MonoBehaviour {

    public bool clicked;
    private bool loopStarted;
    public int count;

    private void Start() {
        count = 0;
    }

    public void Clicked()  {
        clicked = true;
        count += 1;
        //clicked = true;
        //StartCoroutine(TurnOffClicked());
    }

    IEnumerator TurnOffClicked()  {
        yield return new WaitForSeconds(0);
        clicked = false;
    }
   
}
