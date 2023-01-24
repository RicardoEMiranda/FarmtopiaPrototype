using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CropController : MonoBehaviour {

    public GameObject cropRow;
    public AudioSource audioSource;

    //public GameObject collider;

    // Start is called before the first frame update
    void Start() {
      
    }

    // Update is called once per frame
    void Update() {
        
    }

    private void OnTriggerEnter(Collider other)  {

        //Debug.Log("Hit");
        if(other.gameObject.tag == "tractor") {
            Destroy(cropRow.gameObject, .5f);
            //cropRow.SetActive(false);
            audioSource.Play();

        }
        
    }

}
