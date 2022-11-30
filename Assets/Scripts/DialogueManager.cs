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
    private int dialogueCount;
    [Space]
    public GameObject dialogueBox;
    private SpriteRenderer spriteRenderer;

    //public AudioClip typingAudio;

    // Start is called before the first frame update
    private void Awake()
    {
        spriteRenderer = dialogueBox.GetComponent<SpriteRenderer>();
    }
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
            SetBoarderSize();
            yield return new WaitForSeconds(textSpeed);   
        }

        //Debug.Log("Stopped Typing");
        audioSource.Stop();
    }
    
    private void SetBoarderSize()
    {
        //textComponent.text = dialogue[index];
        Vector2 background = textComponent.textInfo.meshInfo[0].mesh.bounds.size;
        Vector2 back = textComponent.bounds.size;
        back *= transform.localScale;
        spriteRenderer.size = new Vector2(back.x + 0.25f, back.y + 0.25f);
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
