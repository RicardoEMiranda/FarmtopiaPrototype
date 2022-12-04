using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager_v2 : MonoBehaviour {

    [SerializeField] private TextMeshProUGUI tmpDisplay;

    [TextArea(2,4)]
    [SerializeField] public string[] level1Dialogue;
    [SerializeField] private Transform level1Waypoints;
    [SerializeField] private GameObject farmHandAntonio;
    [SerializeField] private GameObject canvasSpeechBubble;
    [SerializeField] public float typingSpeed = .04f;
    private float delta;
    private int index = 0;

    [SerializeField] public GameObject waypoint;
    private CollisionDetector collisionDetector;

    [SerializeField] public AudioSource audioSource;

    // Start is called before the first frame update
    void Start() {        

        collisionDetector = waypoint.GetComponent<CollisionDetector>();
    }

    // Update is called once per frame
    void Update()  {
        //delta = (farmHandAntonio.transform.position - level1Waypoints.transform.position).magnitude;
        //Debug.Log(delta);
        //if(delta <= .1) {
            //Debug.Log("At Waypoint");
        //    canvasSpeechBubble.SetActive(true);
        //}

        if(collisionDetector.hitFarmerAntonio) {
            ////Debug.Log("Farmer Antonio at Waypoint");
            collisionDetector.hitFarmerAntonio = false;
            StartCoroutine(TypeText());
            audioSource.Play();
        }
    }

    IEnumerator TypeText() {
        foreach (char letter in level1Dialogue[index].ToCharArray()) {
            tmpDisplay.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
        audioSource.Stop();
    }

    public void OnNextButtonDown() {
        //Debug.Log("Next");

        if(index < level1Dialogue.Length -1)  {
            index++;
            tmpDisplay.text = ""; //reset the text before printing more
            StartCoroutine(TypeText());
            audioSource.Play();
        }
        else  {
            tmpDisplay.text = "";
            canvasSpeechBubble.SetActive(false);
        }
        
    }
}
