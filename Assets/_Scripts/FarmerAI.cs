using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Cinemachine;

public class FarmerAI : MonoBehaviour {

    [SerializeField] public Transform[] waypoints;
    [SerializeField] public Vector3 currentPos;
    [SerializeField] public Vector3 endPos;
    [SerializeField] public float speed = 1.75f;
    [SerializeField] public GameObject speechBubble;
    [SerializeField] public TextMeshProUGUI textComponent;
    [SerializeField] private CinemachineVirtualCamera overWorldCam;
    [SerializeField] private CinemachineVirtualCamera NPCCam;
    public Vector3 navigationVector;
    public float delta;

    public float theta;
    private float rotAngle;
    private Vector3 dir = new Vector3(0f, 0f, -1f);

    private Animator animator;

    public bool start;

    // Start is called before the first frame update
    void Start() {
        //currentPos = gameObject.transform.position;
        //endPos = waypoints[1].position;
        //navigationVector = endPos - currentPos;
        //navigationVector = navigationVector.normalized;

        //theta = (Mathf.Atan2(navigationVector.z, navigationVector.x) * Mathf.Rad2Deg - 90)*-1;

        animator = GetComponent<Animator>();

        NPCCam = NPCCam.GetComponent<CinemachineVirtualCamera>();
    }

    // Update is called once per frame
    void Update() {

        currentPos = gameObject.transform.position;
        delta = (currentPos - endPos).magnitude;
        navigationVector = endPos - currentPos;
        navigationVector = navigationVector.normalized;
        //Debug.Log(delta);

        if(start)  {
            if (delta >= .05) {
                transform.forward = new Vector3(-navigationVector.x, 0f, -navigationVector.z) * -1;
                transform.Translate(new Vector3(navigationVector.x, 0f, navigationVector.z) * Time.deltaTime * speed);
                //transform.Translate(-Vector3.forward * Time.deltaTime * speed);
                animator.SetInteger("AnimState", 1);
            }
            else  {
                animator.SetInteger("AnimState", 0);

                // Get the position of the object
                Vector3 objectPosition = transform.position;

                // Get the camera's position
                Vector3 cameraPosition = NPCCam.transform.position;

                // Calculate the direction from the object to the camera
                Vector3 direction = cameraPosition - objectPosition;


                Quaternion rotation = Quaternion.LookRotation(direction);
                // Apply the rotation to the object
                transform.rotation = rotation;

                //speechBubble.SetActive(true);
            }
        }
        

    }

    public void GoToWaypoint(Transform waypoint) {
        currentPos = gameObject.transform.position;
        endPos = waypoint.position;
        navigationVector = endPos - currentPos;
        navigationVector = navigationVector.normalized;
    }
}
