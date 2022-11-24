using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmerAI : MonoBehaviour {

    private float speed = .25f;
    private int direction = 1;
    private float orientation;
    private float pos;

    // Start is called before the first frame update
    void Start()  {
        orientation = gameObject.transform.localRotation.y;
        pos = gameObject.transform.position.z;
    }

    // Update is called once per frame
    void Update() {
        //Debug.Log(orientation);
        transform.Translate(new Vector3(0,0,1)*Time.deltaTime*speed*direction);

        if(gameObject.transform.position.z <=.5f)  {
            //Debug.Log("Position Hit");
            orientation = 0;
            pos = .5f;
            StartCoroutine(FarmerWaitCoroutine(orientation, pos));
            //transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        }

        if (gameObject.transform.position.z >= 4.2f) {
            //Debug.Log("Position Hit");
            orientation = 180;
            pos = 4.2f;
            StartCoroutine(FarmerWaitCoroutine(orientation, pos));
            //transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
        }

        IEnumerator FarmerWaitCoroutine(float val, float posZ)  {
            transform.position = new Vector3(transform.position.x, transform.position.y, posZ);
            yield return new WaitForSeconds(4);
            transform.rotation = Quaternion.Euler(new Vector3(0, val, 0));
        }
    }
}
