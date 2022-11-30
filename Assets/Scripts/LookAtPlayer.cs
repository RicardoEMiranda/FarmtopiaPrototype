using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayer : MonoBehaviour {

    private Transform _camera;

    // Start is called before the first frame update
    void Start() {
        //transform.Rotate(camera.transform.rotation.x, -180f, camera.transform.rotation.z, Space.World);
        _camera = Camera.main.transform;
        transform.LookAt(_camera);
        //transform.Rotate(camera.transform.rotation.x, -180f, camera.transform.rotation.z, Space.World);
    }

    // Update is called once per frame
    void LateUpdate() {
        transform.LookAt(_camera);
        //transform.Rotate(camera.transform.rotation.x, -180f, camera.transform.rotation.z, Space.World);
    }
}
