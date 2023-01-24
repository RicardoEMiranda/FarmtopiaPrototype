using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetector : MonoBehaviour {

    public bool hitFarmerAntonio;

    public void Start() {
        hitFarmerAntonio = false;
    }

    public void OnTriggerEnter(Collider other)  {
        if(other.transform.tag == "FarmerAntonio")  {
            //Debug.Log("Waypoint detected collision with Farmer Antonio");
            hitFarmerAntonio = true;
        }
    }

}
