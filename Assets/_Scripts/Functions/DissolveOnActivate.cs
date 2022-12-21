using UnityEngine;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DissolveOnActivate : MonoBehaviour {
    [SerializeField] public GameObject hostFarmer;
    public RawImage rawImage;
    [SerializeField] public GameObject dialogueBubble;
    [SerializeField] public float fadeTime = 2f;
    private AudioSource audioSource;
    [SerializeField] private AudioClip audioClip;
    private bool playedClip;

    //[Range(0, 1)]
    //public float val;
    private void Start()  {
        rawImage = hostFarmer.GetComponentInChildren<RawImage>();
        rawImage.CrossFadeAlpha(0, 0, true);

        dialogueBubble.SetActive(false);
        audioSource = dialogueBubble.GetComponent<AudioSource>();
    }

    private void Update() {
        
    }

    public void OnActivate() {
        rawImage.CrossFadeAlpha(1, fadeTime, false);
        StartCoroutine(ActivateDialogueBubble());
        
    }

    IEnumerator ActivateDialogueBubble()  {
        
        yield return new WaitForSeconds(fadeTime);
        dialogueBubble.SetActive(true);
        //audioSource.PlayOneShot(audioClip);
        PlayClip();
    }

    private void PlayClip() {
        if(!playedClip)  {
            audioSource.PlayOneShot(audioClip);
            playedClip = true; 
        }
    }

    public bool ReadyToType() {

        if(playedClip)  {
            return true;
        } else  {
            return false;
        }
       
    }

}

