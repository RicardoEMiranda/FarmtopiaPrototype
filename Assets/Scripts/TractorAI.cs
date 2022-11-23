using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TractorAI : MonoBehaviour {

    public float speed = .5f;
    private int direction = 1;
    private float orientation;

    // Start is called before the first frame update
    void Start()  {
        
        //orientation = gameObject.transform.localRotation.y;
    }

    // Update is called once per frame
    void Update() {
        //Debug.Log(orientation);
        //transform.Translate(new Vector3(0,0,1)*Time.deltaTime*speed*direction);

       

        if(gameObject.transform.position.x >=-234.0f && gameObject.transform.position.x <= -207.0f )  {
            transform.Translate(new Vector3(0, 0, 1) * Time.deltaTime * speed * direction);
        }

        if(gameObject.transform.position.z >= 5.3)  {
            //orientation = 0;
            //transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        }
    }
}
