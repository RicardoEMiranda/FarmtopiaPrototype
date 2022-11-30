Shader "GUI/3D Text Shader" {
  Properties {
    _MainTex("Font Texture", 2D) = "white" {}  //This needs to be the font texture.
    _Color("Text Color", Color) = (1, 1, 1, 1) //The font color.
  }

  SubShader {
    Tags { "Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent" }
    Lighting Off Cull Off ZWrite Off Fog { Mode Off }
    Blend SrcAlpha OneMinusSrcAlpha
    Pass {
      Color[_Color]
      SetTexture[_MainTex] {
        combine primary, texture * primary
      }
    }
  }
}