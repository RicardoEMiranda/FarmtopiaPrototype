using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectBarnClicked : MonoBehaviour {

    //[SerializeField] private AudioSource audioMagicWand;
    //[SerializeField] private AudioSource audioPoof;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip clipMagicWand;
    [SerializeField] private AudioClip clipPoof;

    private void Start()  {
        //audioMagicWand = GetComponent<AudioSource>();
        //audioPoof = GetComponent<AudioSource>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnMouseDown()  {
        Debug.Log("Barn Clicked");
        //audioMagicWand.Play();
        //audioPoof.Play();
        audioSource.PlayOneShot(clipMagicWand);
        audioSource.PlayOneShot(clipPoof);

    }

}
