using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TypeWriter : MonoBehaviour {

    //[SerializeField] private GameObject audioGO;
    [SerializeField] private AudioSource audioSource;
    //private AudioClip clip;
    
    //index of character in string that will be typed out
    private int characterIndex;

    private Coroutine typeCoroutine;
    private float typeDelay = .05f;
    public bool finishedTyping;


    public void Type(string line, TMP_Text textArea)  {

        //Stop any current typing
        //StopCoroutine(typeCoroutine);

        textArea.text = "";
        characterIndex = 0;

        //Start the typeCoroutine
        typeCoroutine = StartCoroutine(TypeText(line, textArea));
        audioSource.Play();

    }

    IEnumerator TypeText(string line, TMP_Text textArea)  {

        foreach (char c in line)  {
            textArea.text += c;
            characterIndex++;
            yield return new WaitForSeconds(typeDelay);
        }
        audioSource.Stop();

        if (characterIndex == line.Length) {
            //Debug.Log("Finished typing!");
            finishedTyping = true;
            audioSource.Stop();
        }
    }

}


