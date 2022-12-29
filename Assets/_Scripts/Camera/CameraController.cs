using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    private float axisRotationNormal;
    public Vector3 camPosition;
    public float camSpeed =.5f;

    // Start is called before the first frame update
    void Start() {
        axisRotationNormal = Mathf.Cos(45 * Mathf.PI / 180);
        camPosition = transform.position;
        //Debug.Log(camPosition);
    }

    // Update is called once per frame
    void Update() {

        GetInput(); 
        transform.position = Vector3.Lerp(transform.position, camPosition, Time.deltaTime*.1f);
    }


    private void GetInput() {
        if (Input.GetKey(KeyCode.A)) {
            camPosition = camPosition + new Vector3(-axisRotationNormal, 0, axisRotationNormal) * camSpeed;
            //Debug.Log(camPosition);
        } else if (Input.GetKey(KeyCode.D)) {
            //Debug.Log("Right");
            camPosition = camPosition + new Vector3(axisRotationNormal, 0, -axisRotationNormal) * camSpeed;
        } else if (Input.GetKey(KeyCode.W)) {
            //Debug.Log("Up");
            camPosition = camPosition + new Vector3(axisRotationNormal, 0, axisRotationNormal) * camSpeed;
        } else if (Input.GetKey(KeyCode.S)) {
            //Debug.Log("Down");
            camPosition = camPosition + new Vector3(-axisRotationNormal, 0, -axisRotationNormal) * camSpeed;
        } else  {
            camPosition = transform.position;
        }
    }
}
