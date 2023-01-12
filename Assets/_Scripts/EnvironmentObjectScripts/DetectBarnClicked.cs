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
    [SerializeField] private GameObject managersGO;
    private levelManager0 levelManager0;
    private GameObject vortex;
    public bool barnClicked;


    private void Start()  {
        //audioMagicWand = GetComponent<AudioSource>();
        //audioPoof = GetComponent<AudioSource>();
        audioSource = GetComponent<AudioSource>();
        vortex = vfxGO.transform.GetChild(0).gameObject;
        levelManager0 = managersGO.GetComponent<levelManager0>();
    }

    private void OnMouseDown() {

        if (levelManager0.barnIsClickable) { 
        //Debug.Log("Barn Clicked");

        audioSource.PlayOneShot(clipMagicWand);
        audioSource.PlayOneShot(clipPoof);
        vortex.SetActive(true);

        StartCoroutine(StopFireworks());
        barnClicked = true;
        }

    }

    IEnumerator StopFireworks()  {
        yield return new WaitForSeconds(2.5f);
        vortex.SetActive(false);
    }

}
