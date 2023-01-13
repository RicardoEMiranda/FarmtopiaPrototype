using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.Rendering.PostProcessing;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class GlowIntensityController : MonoBehaviour {

    //[SerializeField] public GameObject volume;
    private Volume volume;
    private Bloom bloom;
    private float minVal = 15f;
    private float maxVal = 55f;
    private float freq = .1f;
    private float val;

    private void Start()  {
        
    //Start is called before the first frame update
    volume = gameObject.GetComponent<Volume>();
        volume.profile.TryGet<Bloom>(out bloom);
        bloom.intensity.value = minVal;
        
    }

    // Update is called once per frame
    void Update() {
        bloom.intensity.value = Mathf.Sin(20f*Time.time)+15f;
        val = bloom.intensity.value = Mathf.Sin(3.14159f*.75f*Time.time)*25f + 30f;
        
    }
}
