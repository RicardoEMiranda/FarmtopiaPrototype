using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpechBubbleSize : MonoBehaviour {
  public TextMesh textMesh;
  public Font font;

  public float textOffset = 0.1f;
  public Vector3 SpeechBubbleOffset = Vector3.zero;
  public Vector2 minSpeechBubbleSize = Vector2.zero;
  public Transform Center;
  public Transform SideUp, SideDown, SideLeft, SideRight;
  public Transform CornerUpLeft, CornerUpRight, CornerDownLeft, CornerDownRight;

  public float variableline = 0.86f;
  private float proportion;

  private Vector2 centerPSize;
  private Vector2 sideDownPSize;
  private Vector2 sideUpPSize;
  private Vector2 sideLeftPSize;
  private Vector2 sideRightPSize;

  private Material ownMat = null;

  private void
  Start() {
    ownMat = textMesh.GetComponent<Renderer>().material;
    textMesh.GetComponent<Renderer>().material = ownMat;
    if (!font) {
      font = Font.CreateDynamicFontFromOSFont("DefaultDynamicFont", 12);
    }
    if (font) {
      textMesh.font = font;
      ownMat.mainTexture = font.material.mainTexture;
    }
    ownMat.color = textMesh.color;

    CalculateProportion();

    centerPSize    = Center.GetComponent<SpriteRenderer>().sprite.rect.size * 0.01f;
    sideDownPSize  = SideDown.GetComponent<SpriteRenderer>().sprite.rect.size * 0.01f;
    sideUpPSize    = SideUp.GetComponent<SpriteRenderer>().sprite.rect.size * 0.01f;
    sideLeftPSize  = SideLeft.GetComponent<SpriteRenderer>().sprite.rect.size * 0.01f;
    sideRightPSize = SideRight.GetComponent<SpriteRenderer>().sprite.rect.size * 0.01f;
  }

	private void
  Update () {
    float sizeX = Mathf.Max(minSpeechBubbleSize.x,
                            GetMaxWidth(textMesh));
    float sizeY = Mathf.Max(minSpeechBubbleSize.y,
                            GetMaxHeight(textMesh, variableline, proportion) * 2.0f);

    Center.localScale = new Vector3(sizeX / centerPSize.x,
                                    sizeY / centerPSize.y,
                                    1.0f);
    SideDown.localScale = new Vector3(sizeX / sideDownPSize.x,
                                      1.0f,
                                      1.0f);
    SideUp.localScale = new Vector3(sizeX / sideUpPSize.x,
                                    1.0f,
                                    1.0f);
    SideLeft.localScale = new Vector3(1.0f,
                                      sizeY / sideLeftPSize.y,
                                      1.0f);
    SideRight.localScale = new Vector3(1.0f,
                                       sizeY / sideRightPSize.y,
                                       1.0f);

    SideDown.localPosition = SpeechBubbleOffset;
    CornerDownLeft.localPosition = SideDown.localPosition -
                                   (Vector3.right * sideDownPSize.x * 0.5f) *
                                   SideDown.localScale.x;
    CornerDownRight.localPosition = SideDown.localPosition +
                                    (Vector3.right * sideDownPSize.x * 0.5f) *
                                    SideDown.localScale.x;

    Center.localPosition = SideDown.localPosition +
                           (Vector3.up * sideDownPSize.y);
    textMesh.transform.localPosition = Center.localPosition -
                                       Vector3.forward * textOffset +
                                       (Vector3.up * centerPSize.y) *
                                       Center.localScale.y * 0.5f;
    SideLeft.localPosition = Center.localPosition -
                            (Vector3.right * centerPSize.x * 0.5f) *
                            Center.localScale.x;
    SideRight.localPosition = Center.localPosition +
                              (Vector3.right * centerPSize.x * 0.5f) *
                              Center.localScale.x;
    
    SideUp.localPosition = Center.localPosition +
                           (Vector3.up * centerPSize.y) *
                           Center.localScale.y;
    CornerUpLeft.localPosition = SideUp.localPosition -
                                 (Vector3.right * sideUpPSize.x * 0.5f) *
                                 SideUp.localScale.x;
    CornerUpRight.localPosition = SideUp.localPosition +
                                  (Vector3.right * sideUpPSize.x * 0.5f) *
                                  SideUp.localScale.x;
  }

  public static float
  GetMaxHeight(TextMesh mesh, float variableline, float proportion) {
    float numberOfEnters = 1.0f;
    
    foreach (char symbol in mesh.text) {
      if (symbol == '\n') {
        numberOfEnters += 1.0f;
      }
    }

    float pos = numberOfEnters * 0.5f;
    pos += pos * 0.5f * variableline;
    pos *= proportion * mesh.characterSize * mesh.lineSpacing;

    pos *= (mesh.fontSize == 0) ? mesh.font.fontSize : mesh.fontSize;
    pos /= 13.0f;

    return pos;
  }

  public static float
  GetMaxWidth(TextMesh mesh) {
    float actualWidth = 0.0f, maxWidth = -1.0f;

    CharacterInfo info;
    foreach (char symbol in mesh.text) {
      if (symbol == '\n') {
        if (maxWidth < actualWidth) {
          maxWidth = actualWidth;
        }
        actualWidth = 0.0f;
      }
      else {
        if (mesh.font.GetCharacterInfo(symbol,
                                       out info,
                                       mesh.fontSize,
                                       mesh.fontStyle)) {
          actualWidth += info.advance;
        }
      }
    }

    if (maxWidth < actualWidth) {
      maxWidth = actualWidth;
    }

    return maxWidth * mesh.characterSize * 0.1f;
  }

  private void
  CalculateProportion () {
    //In font = 13, lineSpacing = 14 is an unit.

    proportion = (13.0f * textMesh.font.lineHeight) / (textMesh.font.fontSize * 14.0f);
  }
}