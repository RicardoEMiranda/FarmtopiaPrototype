using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmerAI_Test : MonoBehaviour {

    [SerializeField] public Transform[] waypoints;
    [SerializeField] public Vector3 currentPos;
    [SerializeField] public Vector3 endPos;
    [SerializeField] public float speed = 2f;
    public Vector3 navigationVector;
    public float delta;

    public float theta;
    private float rotAngle;

    private Animator animator;

    // Start is called before the first frame update
    void Start() {
        currentPos = gameObject.transform.position;
        endPos = waypoints[1].position;
        navigationVector = endPos - currentPos;
        navigationVector = navigationVector.normalized;

        //theta = (Mathf.Atan2(navigationVector.z, navigationVector.x) * Mathf.Rad2Deg - 90)*-1;

        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update() {

        currentPos = gameObject.transform.position;
        delta = (currentPos - endPos).magnitude;
        navigationVector = endPos - currentPos;
        navigationVector = navigationVector.normalized;
        //Debug.Log(delta);



        if (delta >= .6)  {
            transform.Translate(new Vector3(navigationVector.x, -.4999998f, -navigationVector.z) * Time.deltaTime * speed);
            animator.SetInteger("AnimState", 1);
        }  else  {
            animator.SetInteger("AnimState", 0);
        }
        
    }
}
