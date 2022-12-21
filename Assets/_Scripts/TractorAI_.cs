using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TractorAI_ : MonoBehaviour {

    public Vector3 startPos;
    public float speed = 1.25f;
    private int direction = 1;
    public float orientation;
    public Vector3 currentPos;

    // Start is called before the first frame update
    void Start()  {
        //gameObject.transform.position = new Vector3(-4.73f, -.45f, -5.44f);
        startPos = gameObject.transform.position;
        orientation = gameObject.transform.localRotation.y;
    }

    // Update is called once per frame
    void Update() {
       
        currentPos = transform.position;
        transform.Translate(new Vector3(0, 0, 1) * Time.deltaTime * speed * direction);

        
        if(gameObject.transform.position.z >= 12.5) { 
            transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
        }

        if (gameObject.transform.position.z <= -5.465)  {
            
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
           
        }


    }
}
