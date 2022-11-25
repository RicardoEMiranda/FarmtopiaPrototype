using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeechBubbleShowPerDistance : MonoBehaviour {
  public Transform target = null;
  public Transform origin = null;

  private Vector3 vec3InitScale = Vector3.one;

  public float AppearOnDistance = 3.0f;
  public AnimationCurve xEnterSizeScale;
  public AnimationCurve yEnterSizeScale;
  private float xEnterSizeTimePassed = 0.0f;
  private float yEnterSizeTimePassed = 0.0f;

  public float DisappearOnDistance = 5.0f;
  public AnimationCurve xExitSizeScale;
  public AnimationCurve yExitSizeScale;
  private float xExitSizeTimePassed = float.MaxValue;
  private float yExitSizeTimePassed = float.MaxValue;

  private bool bInside = false;
  private bool bOutside = false;
  private bool bshow = false;

  private float fDistance;
  private Vector3 vec3DialogScale;

  private void
  Reset() {
    origin = transform;
    target = Camera.main.transform;

    xEnterSizeScale = AnimationCurve.EaseInOut(0.0f, 0.0f, 0.25f, 1.0f);
    //yEnterSizeScale = AnimationCurve.EaseInOut(0.0f, 0.0f, 0.25f, 1.0f);
    yEnterSizeScale.AddKey(0.0f, 0.0f);
    yEnterSizeScale.AddKey(0.2f, 1.5f);
    yEnterSizeScale.AddKey(0.25f, 1.0f);

    xExitSizeScale = AnimationCurve.Linear(0.0f, 1.0f, 0.1f, 0.0f);
    yExitSizeScale = AnimationCurve.Linear(0.0f, 1.0f, 0.1f, 0.0f);
  }

  private void
  Start() {vec3InitScale = transform.localScale;
    transform.localScale = Vector3.zero;
  }

  private void
  LateUpdate () {
    fDistance = (target.position - origin.position).magnitude;

    bOutside = fDistance > DisappearOnDistance;
    bInside = fDistance < AppearOnDistance;

    if (bOutside) {
      bshow = false;
    }
    if (bInside) {
      bshow = true;
    }

    vec3DialogScale = vec3InitScale;

    if (bshow) {
      xExitSizeTimePassed = yExitSizeTimePassed = 0.0f;

      xEnterSizeTimePassed += Time.deltaTime;
      yEnterSizeTimePassed += Time.deltaTime;

      vec3DialogScale.x = vec3InitScale.x * xEnterSizeScale.Evaluate(xEnterSizeTimePassed);
      vec3DialogScale.y = vec3InitScale.y * yEnterSizeScale.Evaluate(yEnterSizeTimePassed);
    }
    else {
      xEnterSizeTimePassed = yEnterSizeTimePassed = 0.0f;

      xExitSizeTimePassed += Time.deltaTime;
      yExitSizeTimePassed += Time.deltaTime;

      vec3DialogScale.x = vec3InitScale.x * xExitSizeScale.Evaluate(xExitSizeTimePassed);
      vec3DialogScale.y = vec3InitScale.y * yExitSizeScale.Evaluate(yExitSizeTimePassed);
    }

    transform.localScale = vec3DialogScale;
	}

  private void
  OnDrawGizmosSelected() {
    Color prevCol = Gizmos.color;
    
    Gizmos.color = new Color(0.0f, 1.0f, 0.0f, 0.5f);
    Gizmos.DrawWireSphere(origin.position, AppearOnDistance);

    Gizmos.color = new Color(1.0f, 0.0f, 0.0f, 0.5f);
    Gizmos.DrawWireSphere(origin.position, DisappearOnDistance);

    Gizmos.color = prevCol;
  }

  private void
  OnValidate() {
    if (origin == null) {
      Debug.LogError("origin can't be null!");
    }
    if (target == null) {
      Debug.LogError("target can't be null!");
    }
    if (DisappearOnDistance < 0.0f) {
      DisappearOnDistance = float.MaxValue;
    }
    else if (DisappearOnDistance < AppearOnDistance) {
      Debug.LogError("DisappearOnDistance can't be lower than AppearOnDistance");
      DisappearOnDistance = AppearOnDistance + 1.0f;
    }
  }
}