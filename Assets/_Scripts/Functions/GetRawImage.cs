using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetRawImage : MonoBehaviour {
    public RawImage rawImage;
    [SerializeField] public float fadeTime = 2f;

    //[Range(0, 1)]
    //public float val;

    private void Start() {
        rawImage = GetComponent<RawImage>();
        
        rawImage.CrossFadeAlpha(0, 0, true);
        rawImage.CrossFadeAlpha(1, fadeTime, false);
   
    }

    
}


