using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour {

    public TextMeshProUGUI textComponent;
    [TextArea(2, 10)]
    public string[] dialogue;
    public float textSpeed = .05f;
    private int index = 0;
    public AudioSource audioSource;
    public GameObject dialogueBox;
    private int dialogueCount;

    
    //public AudioClip typingAudio;

    // Start is called before the first frame update
    void Start()  {

        dialogueCount = dialogue.Length; 
        Debug.Log("Started");
        textComponent.text = "";
        StartDialogue();
    }

    // Update is called once per frame
    void Update()  {
       
        
        if(Input.GetMouseButtonDown(0))  {
            if(index <= dialogueCount-2)   {
                NextLine();
            }
            else  {
                StopAllCoroutines();
                textComponent.text = dialogue[index];
                audioSource.Stop();
                dialogueBox.SetActive(false);
            }
        }

    }

    private void StartDialogue() {
        StartCoroutine(TypeDialogue());
    }

    IEnumerator TypeDialogue()  {
        audioSource.Play();
        foreach (char c in dialogue[index].ToCharArray())  {
            textComponent.text = textComponent.text + c;
            yield return new WaitForSeconds(textSpeed);   
        }

        //Debug.Log("Stopped Typing");
        audioSource.Stop();
    }

    void NextLine() {
        if(index <= dialogue.Length-1)  {
            audioSource.Play();
            index++;
            //Debug.Log(index);
            textComponent.text = "";
            StartCoroutine(TypeDialogue());
        }
        if (index >=4)  {
            dialogueBox.SetActive(false);
            audioSource.Stop();
        }
    }
}
