using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TractorAI : MonoBehaviour {

    private float speed = .5f;
    private int direction = -1;
    private float orientation;

    // Start is called before the first frame update
    void Start()  {
        orientation = gameObject.transform.localRotation.y;
    }

    // Update is called once per frame
    void Update() {
        //Debug.Log(orientation);
        transform.Translate(new Vector3(0,0,1)*Time.deltaTime*speed*direction);

        if(gameObject.transform.position.z <=-1.5f)  {
            //Debug.Log("Position Hit");
            orientation = 180;
            transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
        }

        if(gameObject.transform.position.z >= 5.3)  {
            orientation = 0;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        }
    }
}
