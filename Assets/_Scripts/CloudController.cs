using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudController : MonoBehaviour {

    private float speed;
    private Vector3 speedVector;

    // Start is called before the first frame update
    void Start()  {
        speed = Random.Range(1, 5);
        speedVector = new Vector3(speed, 0, 0);
    }

    // Update is called once per frame
    void Update() {
        transform.Translate(speedVector * Time.deltaTime);
    }
}
