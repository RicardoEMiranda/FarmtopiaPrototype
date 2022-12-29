using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectBarnClicked : MonoBehaviour {

    //[SerializeField] private AudioSource audioMagicWand;
    //[SerializeField] private AudioSource audioPoof;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip clipMagicWand;
    [SerializeField] private AudioClip clipPoof;
    [SerializeField] private GameObject vfxGO;
    private GameObject fireworks;

    private void Start()  {
        //audioMagicWand = GetComponent<AudioSource>();
        //audioPoof = GetComponent<AudioSource>();
        audioSource = GetComponent<AudioSource>();
        fireworks = vfxGO.transform.GetChild(0).gameObject;
           

    }

    private void OnMouseDown()  {
        Debug.Log("Barn Clicked");
        //audioMagicWand.Play();
        //audioPoof.Play();
        audioSource.PlayOneShot(clipMagicWand);
        audioSource.PlayOneShot(clipPoof);
        fireworks.SetActive(true);

        StartCoroutine(StopFireworks());
    }

    IEnumerator StopFireworks()  {
        yield return new WaitForSeconds(2.5f);
        fireworks.SetActive(false);
    }

}
