using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayer : MonoBehaviour {

    private Transform _camera;
    void Start() {
        _camera = Camera.main.transform;
        transform.LookAt(_camera);
    }
    void LateUpdate() {
        transform.LookAt(_camera);
    }
}
