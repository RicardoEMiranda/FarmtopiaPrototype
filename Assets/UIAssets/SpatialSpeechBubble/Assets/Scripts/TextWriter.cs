using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextWriter : MonoBehaviour {
  public Transform target = null;
  public Transform origin = null;

  public float activateOnDistance = 3.0f;
  public float clearOnDistance = 5.0f;
  public float timeBetweenLetters = 0.05f;
  private float timePassed = 0.0f;

  public string text = "POP UP!\nHello World.";
  public TextMesh targetTextMesh;

  private string writedText = "";
  private int letterIndex = 0;

  private float fDistance;
  private bool bInside = false;
  private bool bOutside = false;
  private bool bWrite = false;

  private void
  Reset() { 
    origin = transform;
    target = Camera.main.transform;
  }

  private void
  Update () {
    fDistance = (target.position - origin.position).magnitude;

    bInside = fDistance < activateOnDistance;
    bOutside = fDistance > clearOnDistance;

    if (bOutside) {
      bWrite = false;
    }
    if (bInside) {
      bWrite = true;
    }

    if (bWrite) {
      timePassed += Time.deltaTime;
      while (timePassed > timeBetweenLetters) {
        timePassed -= timeBetweenLetters;
        addCharacter();
      }
    }
    else {
      writedText = "";
      timePassed = 0.0f;
      letterIndex = 0;
    }

    targetTextMesh.text = writedText;
  }

  private void
  addCharacter() {
    //Add a character only if there are characters left to write.
    if (letterIndex < text.Length) {
      //Check if there is a new line character.
      if (isNewLineCharacter()) {
        writedText += '\n';
        ++letterIndex;
      }
      //Write the current character.
      else {
        writedText += text[letterIndex];
      }
      ++letterIndex;
    }
  }

  private bool
  isNewLineCharacter() {
    //Check if there are 2 charactes
    if (letterIndex + 1 < text.Length) {
      //Check if is the '\n' character
      return (text[letterIndex] == '\\' && text[letterIndex + 1] == 'n');
    }
    return false;
  }

  private void
  OnDrawGizmosSelected() {
    Color prevCol = Gizmos.color;

    Gizmos.color = new Color(1.0f, 1.0f, 1.0f, 0.5f);
    Gizmos.DrawWireSphere(origin.position, activateOnDistance);

    Gizmos.color = new Color(0.0f, 0.0f, 0.0f, 0.5f);
    Gizmos.DrawWireSphere(origin.position, clearOnDistance);

    Gizmos.color = prevCol;
  }

  public void
  setText(string newText) {
    text = newText;
  }

  private void
  OnValidate() {
    if (origin == null) {
      Debug.LogError("origin can't be null!");
    }
    if (target == null) {
      Debug.LogError("target can't be null!");
    }
    if (clearOnDistance < 0.0f) {
      clearOnDistance = float.MaxValue;
    }
    else if (clearOnDistance < activateOnDistance) {
      Debug.LogError("clearOnDistance can't be lower than activateOnDistance");
      clearOnDistance = activateOnDistance + 1.0f;
    }
  }
}