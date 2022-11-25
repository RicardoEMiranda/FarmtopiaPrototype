using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmerAI_Test : MonoBehaviour {

    [SerializeField] public Transform[] waypoints;
    [SerializeField] public Vector3 currentPos;
    [SerializeField] public Vector3 endPos;
    public Vector3 navigationVector;
    public float delta;

    // Start is called before the first frame update
    void Start() {
        currentPos = gameObject.transform.position;
        endPos = waypoints[1].position;
        navigationVector = endPos - currentPos;
        navigationVector = navigationVector.normalized;
    }

    // Update is called once per frame
    void Update() {

        currentPos = gameObject.transform.position;
        delta = (currentPos - endPos).magnitude;

        if (delta >= .01)  {
            transform.Translate(new Vector3(navigationVector.x, transform.position.y, navigationVector.z) * Time.deltaTime * 1);
        }
        
    }
}
