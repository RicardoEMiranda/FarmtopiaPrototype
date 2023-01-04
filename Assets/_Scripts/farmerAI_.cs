using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Cinemachine;

public class farmerAI_ : MonoBehaviour {

    [SerializeField] public Vector3 currentPos;
    [SerializeField] public Vector3 endPos;
    [SerializeField] public float speed = 1.75f;
    //[SerializeField] public GameObject speechBubble;
    //[SerializeField] public TextMeshProUGUI textComponent;
    [SerializeField] private CinemachineVirtualCamera overWorldCam;
    [SerializeField] private CinemachineVirtualCamera NPCCam;
    private Transform waypointTransform;
    public Vector3 navigationVector;
    public float deltaMagnitude;

    private float rotAngle;
    private Vector3 dir = new Vector3(0f, 0f, -1f);

    private Animator animator;

    public bool start;


    // Start is called before the first frame update
    void Start() {

        animator = GetComponent<Animator>();
        overWorldCam = overWorldCam.GetComponent<CinemachineVirtualCamera>();

    }

    // Update is called once per frame
    void Update() {

        deltaMagnitude = (endPos - transform.position).magnitude;
        if (start) {

            if (deltaMagnitude >= .05) { 
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
            Vector3 direction = endPos - transform.position;
            transform.rotation = Quaternion.LookRotation(direction);
            animator.SetInteger("AnimState", 1);
            } else {
                animator.SetInteger("AnimState", 0);
                // Get the position of the object
                Vector3 objectPosition = transform.position;
                // Get the camera's position
                Vector3 cameraPosition = overWorldCam.transform.position;

                // Calculate the direction from the object to the camera
                Vector3 direction = cameraPosition - objectPosition;


                Quaternion rotation = Quaternion.LookRotation(direction);
                // Apply the rotation to the object
                transform.rotation = rotation;
            }
        } 
       
    }

    public void SetStartPosition(Transform waypointTransform) {
        transform.position = waypointTransform.position;
        transform.rotation = Quaternion.Euler(0, 180, 0);
        //Vector3 direction = transform.position - waypointTransform.position;
        //transform.rotation = Quaternion.LookRotation(direction);
    }

    public void SetDestination(Transform destinationTransform)  {

        endPos = destinationTransform.position;
    }


}



