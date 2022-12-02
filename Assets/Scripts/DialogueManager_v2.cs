using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager_v2 : MonoBehaviour {

    [SerializeField] private TextMeshProUGUI tmpDropText;
    //public string[] level1Dialogue = new string[1];
    private string[] level1Dialogue = new string[2];
    [SerializeField] private Transform level1Waypoints;
    [SerializeField] private GameObject farmHandAntonio;
    [SerializeField] private GameObject canvasSpeechBubble;
    private float delta;

    // Start is called before the first frame update
    void Start() {
        //tmpDropText.text = "Yo";
        //gameObject.SetActive(false);
        //GenerateLevel1Dialogue();
        level1Dialogue[0] = "Hello, I'm Farmhand Antonio!";
        level1Dialogue[1] = "Line 2.";
    }

    // Update is called once per frame
    void Update()  {

        delta = (farmHandAntonio.transform.position - level1Waypoints.transform.position).magnitude;
        //Debug.Log(delta);
        if(delta <= .1) {
            //Debug.Log("At Waypoint");
            canvasSpeechBubble.SetActive(true);
            tmpDropText.text = level1Dialogue[0];
        }
    }

    private void GenerateLevel1Dialogue()  {
        //Debug.Log("Generate Dialogue");
        //level1Dialogue[0] = "Hello, I'm Farmhand Antonio!";
        //level1Dialogue[1] = "You can click on the field <br> next to me to select what <br> hemp seed varietal you want to <br>" +
        //    "plant. Go ahead, try it!";
    }
}
