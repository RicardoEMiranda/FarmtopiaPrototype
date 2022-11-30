using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayer : MonoBehaviour {

    public Transform _camera;

    // Start is called before the first frame update
    void Start() {
        _camera = Camera.main.transform;
        transform.LookAt(_camera);
    }

    // Update is called once per frame
    void LateUpdate() {
        transform.LookAt(_camera);
    }
}
