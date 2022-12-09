// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Custom/Vegetation"
{
	Properties
	{
		_Color("Color", Color) = (1,1,1,0)
		[Enum(No,2,Yes,0)] _TwoSided ("Two Sided", Int) = 2
		_ShadowStrength("Shadow Strength", Range( 0 , 1)) = 0
		_Shadowcolor("Shadow color", Color) = (0,0,0,0)
		_Albedo("Albedo", 2D) = "white" {}
		_NormalMap("NormalMap", 2D) = "bump" {}
		_NormalMapStrength("Normal Map Strength", Range( 0 , 3)) = 1
		_Metallic("Metallic", Range( 0 , 1)) = 0
		_Smoothness("Smoothness", Range( 0 , 1)) = 0
		_Cutoff( "Mask Clip Value", Float ) = 0.35
		_WindStrength1(" Occlusion Strength", Range( 0 , 1)) = 0.5
		[Toggle(_WIND)] _Wind("Wind", Float) = 1
		WindSpeedFloat1("Wind Speed", Range( 0 , 1)) = 0.5
		WindTurbulence("Wind Turbulence", Range( 0 , 1)) = 0.5
		_WindDirection("WindDirection", Vector) = (0,0,0,0)
		_WindStrength("Wind Strength", Range( 0 , 1)) = 0.5
		[Header(Main Bending)][Space]_DefaultBending("Default Bending", Float) = 0
		[Space]_Amplitude("Amplitude", Float) = 1.5
		_AmplitudeOffset("Amplitude Offset", Float) = 2
		[Space]_Frequency("Frequency", Float) = 1.11
		_FrequencyOffset("Frequency Offset", Float) = 0
		[Space]_BendSpeed("Bend Speed", Float) = 1
		[Space]_float("float", Range( 0 , 360)) = 0
		_WindOffset("Wind Offset", Range( 0 , 180)) = 20
		[Space]_MaxHeight("Max Height", Float) = 10
		_NoiseTextureTilling2("Noise Tilling - Static (XY), Animated (ZW)", Vector) = (1,1,1,1)
		_NoisePannerSpeed("Noise Panner Speed", Vector) = (0.05,0.03,0,0)
		NoiseTextureFloat1("Foliage Noise Texture", 2D) = "white" {}
		[NoScaleOffset]_BendingNoise("Bending Noise", 2D) = "white" {}
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
		[Header(Forward Rendering Options)]
		[ToggleOff] _SpecularHighlights("Specular Highlights", Float) = 1.0
	}

	SubShader
	{
		Tags{ "RenderType" = "TreeOpaque"  "Queue" = "Geometry+0" "DisableBatching" = "LODFading" "IsEmissive" = "true"  }
		Cull [_TwoSided]
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#include "UnityCG.cginc"
		#pragma target 3.0
		#pragma shader_feature _SPECULARHIGHLIGHTS_OFF
		#pragma shader_feature _WIND
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows exclude_path:deferred vertex:vertexDataFunc 
		struct Input
		{
			float3 worldPos;
			float2 uv_texcoord;
			float3 worldNormal;
			INTERNAL_DATA
		};

		uniform float _float;
		uniform float _WindOffset;
		uniform sampler2D _BendingNoise;
		uniform float4 _NoiseTextureTilling2;
		uniform float2 _NoisePannerSpeed;
		uniform float _Amplitude;
		uniform float _AmplitudeOffset;
		uniform float _Frequency;
		uniform float _FrequencyOffset;
		uniform float _BendSpeed;
		uniform float _DefaultBending;
		uniform float _MaxHeight;
		uniform float3 _WindDirection;
		uniform sampler2D NoiseTextureFloat1;
		uniform float WindSpeedFloat1;
		uniform float WindTurbulence;
		uniform float _WindStrength;
		uniform sampler2D _NormalMap;
		uniform float4 _NormalMap_ST;
		uniform float _NormalMapStrength;
		uniform sampler2D _Albedo;
		uniform float4 _Color;
		uniform float _ShadowStrength;
		uniform float4 _Shadowcolor;
		uniform float _Metallic;
		uniform float _Smoothness;
		uniform float _WindStrength1;
		uniform float _Cutoff = 0.35;


		float3 RotateAroundAxis( float3 center, float3 original, float3 u, float angle )
		{
			original -= center;
			float C = cos( angle );
			float S = sin( angle );
			float t = 1 - C;
			float m00 = t * u.x * u.x + C;
			float m01 = t * u.x * u.y - S * u.z;
			float m02 = t * u.x * u.z + S * u.y;
			float m10 = t * u.x * u.y + S * u.z;
			float m11 = t * u.y * u.y + C;
			float m12 = t * u.y * u.z - S * u.x;
			float m20 = t * u.x * u.z - S * u.y;
			float m21 = t * u.y * u.z + S * u.x;
			float m22 = t * u.z * u.z + C;
			float3x3 finalMatrix = float3x3( m00, m01, m02, m10, m11, m12, m20, m21, m22 );
			return mul( finalMatrix, original ) + center;
		}


		void vertexDataFunc( inout appdata_full v, out Input o )
		{
			UNITY_INITIALIZE_OUTPUT( Input, o );
			float WindDirection232 = _float;
			float WindDirectionOffset229 = _WindOffset;
			float3 objToWorld205 = mul( unity_ObjectToWorld, float4( float3( 0,0,0 ), 1 ) ).xyz;
			float2 appendResult207 = (float2(objToWorld205.x , objToWorld205.z));
			float2 WorldSpaceUVs210 = appendResult207;
			float2 AnimatedNoiseTilling209 = (_NoiseTextureTilling2).zw;
			float2 panner214 = ( 0.1 * _Time.y * _NoisePannerSpeed + float2( 0,0 ));
			float4 AnimatedWorldNoise218 = tex2Dlod( _BendingNoise, float4( ( ( WorldSpaceUVs210 * AnimatedNoiseTilling209 ) + panner214 ), 0, 0.0) );
			float temp_output_258_0 = radians( ( ( WindDirection232 + ( WindDirectionOffset229 * (-1.0 + ((AnimatedWorldNoise218).r - 0.0) * (1.0 - -1.0) / (1.0 - 0.0)) ) ) * -1.0 ) );
			float3 appendResult270 = (float3(cos( temp_output_258_0 ) , 0.0 , sin( temp_output_258_0 )));
			float3 worldToObj273 = mul( unity_WorldToObject, float4( appendResult270, 1 ) ).xyz;
			float3 worldToObj272 = mul( unity_WorldToObject, float4( float3( 0,0,0 ), 1 ) ).xyz;
			float3 normalizeResult278 = normalize( ( worldToObj273 - worldToObj272 ) );
			float3 RotationAxis282 = normalizeResult278;
			float Amplitude253 = _Amplitude;
			float AmplitudeOffset247 = _AmplitudeOffset;
			float3 objToWorld237 = mul( unity_ObjectToWorld, float4( float3( 0,0,0 ), 1 ) ).xyz;
			float Frequency227 = _Frequency;
			float FrequencyOffset223 = _FrequencyOffset;
			float Phase246 = _BendSpeed;
			float DefaultBending262 = _DefaultBending;
			float3 ase_vertex3Pos = v.vertex.xyz;
			float MaxHeight263 = _MaxHeight;
			float MB_RotationAngle281 = radians( ( ( ( ( Amplitude253 + ( AmplitudeOffset247 * (float4( 0,0,0,0 )).r ) ) * sin( ( ( ( objToWorld237.x + objToWorld237.z ) + ( _Time.y * ( Frequency227 + ( FrequencyOffset223 * (float4( 0,0,0,0 )).r ) ) ) ) * Phase246 ) ) ) + DefaultBending262 ) * ( ase_vertex3Pos.y / MaxHeight263 ) ) );
			float3 appendResult283 = (float3(0.0 , ase_vertex3Pos.y , 0.0));
			float3 rotatedValue289 = RotateAroundAxis( appendResult283, ase_vertex3Pos, RotationAxis282, MB_RotationAngle281 );
			float3 rotatedValue293 = RotateAroundAxis( float3( 0,0,0 ), rotatedValue289, RotationAxis282, MB_RotationAngle281 );
			float3 LocalVertexOffset297 = ( ( rotatedValue293 - ase_vertex3Pos ) * step( 0.01 , ase_vertex3Pos.y ) );
			float3 temp_output_153_0 = float3( (_WindDirection).xz ,  0.0 );
			float3 ase_worldPos = mul( unity_ObjectToWorld, v.vertex );
			float2 panner158 = ( 1.0 * _Time.y * ( temp_output_153_0 * WindSpeedFloat1 * 10.0 ).xy + (ase_worldPos).xy);
			float4 Noise166 = ( tex2Dlod( NoiseTextureFloat1, float4( ( ( panner158 * WindTurbulence ) / float2( 10,10 ) ), 0, 0.0) ) * _WindStrength * 0.5 );
			#ifdef _WIND
				float4 staticSwitch176 = ( float4( _WindDirection , 0.0 ) * ( ( v.color * Noise166 ) + ( Noise166 * v.color ) ) );
			#else
				float4 staticSwitch176 = float4( 0,0,0,0 );
			#endif
			v.vertex.xyz += ( float4( LocalVertexOffset297 , 0.0 ) + staticSwitch176 ).rgb;
			v.vertex.w = 1;
		}

		void surf( Input i , inout SurfaceOutputStandard o )
		{
		    #ifdef LOD_FADE_CROSSFADE
            float2 vpos = IN.screenPos.xy / IN.screenPos.w * _ScreenParams.xy;
            UnityApplyDitherCrossFade(vpos);
            #endif
			float2 uv_NormalMap = i.uv_texcoord * _NormalMap_ST.xy + _NormalMap_ST.zw;
			float3 lerpResult189 = lerp( float3(0,0,1) , UnpackNormal( tex2D( _NormalMap, uv_NormalMap ) ) , _NormalMapStrength);
			o.Normal = lerpResult189;
			float3 ase_worldPos = i.worldPos;
			#if defined(LIGHTMAP_ON) && UNITY_VERSION < 560 //aseld
			float3 ase_worldlightDir = 0;
			#else //aseld
			float3 ase_worldlightDir = Unity_SafeNormalize( UnityWorldSpaceLightDir( ase_worldPos ) );
			#endif //aseld
			float dotResult68 = dot( float3(0,1,0) , ase_worldlightDir );
			float clampResult72 = clamp( dotResult68 , 0.0 , 1.0 );
			float3 temp_cast_0 = (clampResult72).xxx;
			float temp_output_2_0_g3 = 0.0;
			float temp_output_3_0_g3 = ( 1.0 - temp_output_2_0_g3 );
			float3 appendResult7_g3 = (float3(temp_output_3_0_g3 , temp_output_3_0_g3 , temp_output_3_0_g3));
			float3 temp_output_78_0 = ( ( temp_cast_0 * temp_output_2_0_g3 ) + appendResult7_g3 );
			float3 temp_output_153_0 = float3( (_WindDirection).xz ,  0.0 );
			float2 panner158 = ( 1.0 * _Time.y * ( temp_output_153_0 * WindSpeedFloat1 * 10.0 ).xy + (ase_worldPos).xy);
			float4 Noise166 = ( tex2D( NoiseTextureFloat1, ( ( panner158 * WindTurbulence ) / float2( 10,10 ) ) ) * _WindStrength * 0.5 );
			float cos184 = cos( ( tex2D( NoiseTextureFloat1, Noise166.rg ) * float4( 0,0,0,0 ) ).r );
			float sin184 = sin( ( tex2D( NoiseTextureFloat1, Noise166.rg ) * float4( 0,0,0,0 ) ).r );
			float2 rotator184 = mul( i.uv_texcoord - float2( 0.5,0.5 ) , float2x2( cos184 , -sin184 , sin184 , cos184 )) + float2( 0.5,0.5 );
			float4 tex2DNode25 = tex2D( _Albedo, rotator184 );
			float4 temp_output_75_0 = ( float4( temp_output_78_0 , 0.0 ) * tex2DNode25 * _Color );
			float3 desaturateInitialColor83 = temp_output_75_0.rgb;
			float desaturateDot83 = dot( desaturateInitialColor83, float3( 0.299, 0.587, 0.114 ));
			float3 desaturateVar83 = lerp( desaturateInitialColor83, desaturateDot83.xxx, 1.0 );
			float3 lerpResult84 = lerp( float3( 0,0,0 ) , float3( 0,0,0 ) , desaturateVar83);
			float4 lerpResult87 = lerp( temp_output_75_0 , float4( lerpResult84 , 0.0 ) , float4( 0,0,0,0 ));
			o.Albedo = lerpResult87.rgb;
			float3 ase_worldNormal = WorldNormalVector( i, float3( 0, 0, 1 ) );
			float dotResult58 = dot( ase_worldlightDir , ase_worldNormal );
			float4 lerpResult62 = lerp( ( dotResult58 * tex2DNode25 ) , tex2DNode25 , ( 1.0 - _ShadowStrength ));
			float4 temp_output_74_0 = ( float4( temp_output_78_0 , 0.0 ) * lerpResult62 * _Shadowcolor );
			float4 lerpResult91 = lerp( temp_output_74_0 , float4( 0,0,0,0 ) , float4( 0,0,0,0 ));
			o.Emission = lerpResult91.rgb;
			o.Metallic = _Metallic;
			o.Smoothness = _Smoothness;
			o.Occlusion = ( 1.0 - _WindStrength1 );
			o.Alpha = 1;
			clip( tex2DNode25.a - _Cutoff );
		}

		ENDCG
	}
	Fallback "Diffuse"
}
/*ASEBEGIN
Version=18900
6.4;128;1523.2;675;-6563.014;-285.5272;1;True;False
Node;AmplifyShaderEditor.CommentaryNode;200;150.1488,1666.159;Inherit;False;2302.227;639.3289;;16;299;298;218;217;216;215;214;213;212;211;210;209;208;207;206;205;World Space Noise;1,1,1,1;0;0
Node;AmplifyShaderEditor.Vector4Node;206;203.179,2094.624;Inherit;False;Property;_NoiseTextureTilling2;Noise Tilling - Static (XY), Animated (ZW);24;0;Create;False;0;0;0;False;0;False;1,1,1,1;1,1,1,1;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TransformPositionNode;205;175.8817,1741.378;Inherit;False;Object;World;False;Fast;True;1;0;FLOAT3;0,0,0;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.SwizzleNode;208;554.3047,2170.004;Inherit;False;FLOAT2;2;3;2;3;1;0;FLOAT4;0,0,0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.DynamicAppendNode;207;409.2078,1780.416;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;210;568.0308,1775.742;Inherit;False;WorldSpaceUVs;-1;True;1;0;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;209;732.4908,2168.566;Inherit;False;AnimatedNoiseTilling;-1;True;1;0;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.Vector2Node;211;1163.247,2148.16;Float;False;Property;_NoisePannerSpeed;Noise Panner Speed;25;0;Create;True;0;0;0;False;0;False;0.05,0.03;0.05,0.03;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.GetLocalVarNode;213;1184.268,1937.152;Inherit;False;210;WorldSpaceUVs;1;0;OBJECT;;False;1;FLOAT2;0
Node;AmplifyShaderEditor.GetLocalVarNode;212;1136.267,2041.151;Inherit;False;209;AnimatedNoiseTilling;1;0;OBJECT;;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;215;1447.268,1979.152;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.PannerNode;214;1412.275,2129.262;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;0.1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleAddOpNode;216;1637.125,2046.241;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SamplerNode;217;1820.38,2017.925;Inherit;True;Property;_BendingNoise;Bending Noise;29;1;[NoScaleOffset];Create;True;0;0;0;False;0;False;-1;None;242e784a7670a0940bcdf7e1cfdbdf5d;True;0;False;white;Auto;False;Instance;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;3;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.CommentaryNode;201;-1384.88,2179.231;Inherit;False;763.7373;893.087;;18;263;262;257;256;253;247;246;243;239;238;232;229;227;224;223;222;221;219;Material Properties;1,1,1,1;0;0
Node;AmplifyShaderEditor.RangedFloatNode;219;-1348.596,2631.895;Inherit;False;Property;_FrequencyOffset;Frequency Offset;19;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;202;-358.5543,2567.2;Inherit;False;2813.804;508.2881;;18;282;278;276;273;272;270;266;261;258;250;248;242;241;235;233;230;228;220;Rotation Axis;1,1,1,1;0;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;218;2146.722,2018.622;Inherit;False;AnimatedWorldNoise;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;221;-1351.98,2540.284;Float;False;Property;_Frequency;Frequency;18;0;Create;True;0;0;0;False;1;Space;False;1.11;3;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;220;-316.6119,2836.118;Inherit;False;218;AnimatedWorldNoise;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;223;-887.7032,2631.053;Inherit;False;FrequencyOffset;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;203;-359.3351,3334.183;Inherit;False;2812.515;1025.047;;27;281;279;277;275;274;271;269;268;267;265;264;260;259;255;254;252;251;249;245;244;240;237;236;234;231;226;225;Rotation Angle;1,1,1,1;0;0
Node;AmplifyShaderEditor.RangedFloatNode;222;-1356.218,2899.585;Float;False;Property;_WindOffset;Wind Offset;22;0;Create;True;0;0;0;False;0;False;20;180;0;180;0;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;225;-151.5552,4130.821;Inherit;False;223;FrequencyOffset;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;227;-848.9801,2537.284;Float;False;Frequency;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;224;-1356.061,2814.9;Float;False;Property;_float;float;21;0;Create;True;0;0;0;False;1;Space;False;0;0;0;360;0;1;FLOAT;0
Node;AmplifyShaderEditor.SwizzleNode;226;-51.55425,4226.821;Inherit;False;FLOAT;0;1;2;3;1;0;COLOR;0,0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;229;-910.9283,2898.394;Inherit;False;WindDirectionOffset;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SwizzleNode;228;-53.43804,2835.061;Inherit;False;FLOAT;0;1;2;3;1;0;COLOR;0,0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;234;171.4447,4172.821;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;150;2331.299,807.0836;Inherit;False;1616.924;639.8218;Noise;15;165;164;163;162;161;160;159;158;157;156;155;154;153;152;151;Noise;1,0,0.02020931,1;0;0
Node;AmplifyShaderEditor.GetLocalVarNode;231;102.4457,4074.821;Inherit;False;227;Frequency;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;230;89.39005,2733.118;Inherit;False;229;WindDirectionOffset;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.Vector3Node;149;1964.769,721.8765;Float;False;Property;_WindDirection;WindDirection;13;0;Create;True;0;0;0;False;0;False;0,0,0;0.5,0.2,0.5;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.TFHCRemapNode;233;176.4061,2839.891;Inherit;False;5;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;-1;False;4;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;232;-875.0612,2811.35;Float;False;WindDirection;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TransformPositionNode;237;294.5169,3706.605;Inherit;False;Object;World;False;Fast;True;1;0;FLOAT3;0,0,0;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.SimpleAddOpNode;240;355.4446,4120.821;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;238;-1348.679,2720.984;Float;False;Property;_BendSpeed;Bend Speed;20;0;Create;True;0;0;0;False;1;Space;False;1;-1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.TimeNode;236;278.4446,3914.821;Inherit;False;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SwizzleNode;151;2360.935,1082.42;Inherit;False;FLOAT2;0;2;1;2;1;0;FLOAT3;0,0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;239;-1351.98,2443.284;Float;False;Property;_AmplitudeOffset;Amplitude Offset;17;0;Create;True;0;0;0;False;0;False;2;2;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;241;414.4387,2780.344;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;235;320.389,2650.117;Inherit;False;232;WindDirection;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;245;528.1968,3743.71;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;242;595.4958,2837.772;Float;False;Constant;_Float2;Float 2;23;0;Create;True;0;0;0;False;0;False;-1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;246;-843.6793,2718.984;Inherit;False;Phase;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;243;-1350.98,2348.284;Float;False;Property;_Amplitude;Amplitude;16;0;Create;True;0;0;0;False;1;Space;False;1.5;2;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;247;-889.9791,2442.284;Inherit;False;AmplitudeOffset;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;244;518.4429,4026.821;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;152;2641.254,1351.187;Inherit;False;Constant;_Float0;Float 0;10;0;Create;True;0;0;0;False;0;False;10;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.WorldPosInputsNode;154;2357.402,861.4975;Float;True;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.TransformDirectionNode;153;2568.936,1082.42;Inherit;False;World;World;True;Fast;False;1;0;FLOAT3;0,0,0;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.RangedFloatNode;155;2527.541,1258.372;Float;False;Property;WindSpeedFloat1;Wind Speed;11;0;Create;False;0;0;0;False;0;False;0.5;0.5;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;248;620.4617,2719.345;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;249;698.5548,4061.306;Inherit;False;246;Phase;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;251;613.8958,3502.058;Inherit;False;247;AmplitudeOffset;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.SwizzleNode;156;2652.624,861.9125;Inherit;False;FLOAT2;0;1;2;2;1;0;FLOAT3;0,0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;250;805.0878,2766.425;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;157;2818.668,1238.531;Inherit;False;3;3;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;253;-842.9801,2346.284;Float;False;Amplitude;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;254;763.422,3876.457;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SwizzleNode;252;717.652,3603.932;Inherit;False;FLOAT;0;1;2;3;1;0;COLOR;0,0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;159;2894.813,1040.245;Float;False;Property;WindTurbulence;Wind Turbulence;12;0;Create;False;0;0;0;False;0;False;0.5;0.5;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;260;933.5029,3954.501;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;256;-1352.98,2252.285;Float;False;Property;_DefaultBending;Default Bending;15;0;Create;True;0;0;0;False;2;Header(Main Bending);Space;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RadiansOpNode;258;982.8568,2766.15;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;257;-1352.928,2983.39;Inherit;False;Property;_MaxHeight;Max Height;23;0;Create;True;0;0;0;False;1;Space;False;10;10;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;259;861.8969,3441.058;Inherit;False;253;Amplitude;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.PannerNode;158;2982.562,869.9765;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;255;932.5949,3554.978;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;263;-853.9283,2983.394;Inherit;False;MaxHeight;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;262;-874.9791,2250.285;Float;False;DefaultBending;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SinOpNode;264;1111.492,3954.729;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SinOpNode;266;1204.838,2823.091;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.CosOpNode;261;1200.608,2713.428;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;160;3185.645,870.0845;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleAddOpNode;265;1114.633,3492.437;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleDivideOpNode;161;3351.234,867.5106;Inherit;False;2;0;FLOAT2;0,0;False;1;FLOAT2;10,10;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;269;1345.774,3700.002;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;270;1392.83,2742.241;Inherit;False;FLOAT3;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.GetLocalVarNode;271;1318.301,4129.795;Inherit;False;263;MaxHeight;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.PosVertexDataNode;267;1332.337,3964.827;Inherit;False;0;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.GetLocalVarNode;268;1302.152,3854.108;Inherit;False;262;DefaultBending;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;163;3426.242,1137.183;Float;False;Property;_WindStrength;Wind Strength;14;0;Create;False;0;0;0;False;0;False;0.5;0.5;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;274;1582.2,3751.025;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TransformPositionNode;273;1548.79,2735.094;Inherit;False;World;Object;False;Fast;True;1;0;FLOAT3;0,0,0;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.SamplerNode;164;3497.073,862.8026;Inherit;True;Property;NoiseTextureFloat1;Foliage Noise Texture;26;0;Create;False;0;0;0;False;0;False;-1;None;242e784a7670a0940bcdf7e1cfdbdf5d;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleDivideOpNode;275;1576.184,4056.015;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TransformPositionNode;272;1551.79,2904.094;Inherit;False;World;Object;False;Fast;True;1;0;FLOAT3;0,0,0;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.RangedFloatNode;162;3422.596,1243.464;Float;False;Constant;_WindStrenght;Wind Strength 2;7;0;Create;False;0;0;0;False;0;False;0.5;1;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;165;3799.39,1118.498;Inherit;False;3;3;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;276;1803.79,2823.094;Inherit;False;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;277;1786.96,3899.497;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.NormalizeNode;278;1975.221,2822.177;Inherit;False;False;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.CommentaryNode;204;2709.568,2821.745;Inherit;False;1920.748;759.7495;;16;297;296;295;294;293;292;291;290;289;288;287;286;285;284;283;280;Main Bending Vertex Offset;1,1,1,1;0;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;166;4020.04,1112.363;Float;False;Noise;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RadiansOpNode;279;1956.723,3899.861;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;178;101.9353,-347.7656;Inherit;False;1012.714;535.89;UV Animation;4;184;183;182;181;UV Animation;0.7678117,1,0,1;0;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;281;2134.18,3895.176;Float;False;MB_RotationAngle;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;177;-117.1507,-265.5867;Inherit;False;166;Noise;1;0;OBJECT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.CommentaryNode;80;2017.333,-848.7575;Inherit;False;1509.034;413.6017;shadow;5;56;67;68;72;78;;1,1,1,1;0;0
Node;AmplifyShaderEditor.PosVertexDataNode;280;2777.248,3109.817;Inherit;False;0;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RegisterLocalVarNode;282;2153.581,2816.14;Inherit;False;RotationAxis;-1;True;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.DynamicAppendNode;283;2983.085,3132.295;Inherit;False;FLOAT3;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.GetLocalVarNode;285;2900.168,2940.864;Inherit;False;282;RotationAxis;1;0;OBJECT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.PosVertexDataNode;286;2927.555,3265.536;Inherit;False;0;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.GetLocalVarNode;284;2884.742,3030.895;Inherit;False;281;MB_RotationAngle;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.Vector3Node;67;2137.882,-790.8442;Float;False;Constant;_Vector0;Vector 0;15;0;Create;True;0;0;0;False;0;False;0,1,0;0,0,0;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.SamplerNode;181;180.4262,-290.6177;Inherit;True;Property;_TextureSample0;Texture Sample 0;26;0;Create;True;0;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Instance;164;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.WorldSpaceLightDirHlpNode;56;2067.333,-614.1558;Inherit;False;True;1;0;FLOAT;0;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.TextureCoordinatesNode;182;608.7854,-278.9448;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.DotProductOpNode;68;2334.979,-690.7558;Inherit;False;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;287;3284.792,2902.206;Inherit;False;282;RotationAxis;1;0;OBJECT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.CommentaryNode;167;3346.79,1480.203;Inherit;False;608.7889;673.9627;Vertex Animation;5;173;172;171;170;168;Vertex Animation;0,1,0.8708036,1;0;0
Node;AmplifyShaderEditor.GetLocalVarNode;288;3275.392,2993.488;Inherit;False;281;MB_RotationAngle;1;0;OBJECT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RotateAboutAxisNode;289;3213.454,3085.579;Inherit;False;False;4;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;2;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;183;685.9471,3.075871;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.PosVertexDataNode;292;3712.087,3120.182;Inherit;False;0;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.PosVertexDataNode;291;3721.774,3380.401;Inherit;False;0;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.VertexColorNode;170;3399.739,1554.974;Inherit;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RotateAboutAxisNode;293;3601.069,2972.374;Inherit;False;False;4;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;2;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.GetLocalVarNode;169;3084.258,1796.022;Inherit;False;166;Noise;1;0;OBJECT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.RotatorNode;184;853.8431,-144.6956;Inherit;True;3;0;FLOAT2;0,0;False;1;FLOAT2;0.5,0.5;False;2;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.VertexColorNode;168;3404.437,1968.039;Inherit;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ClampOpNode;72;2862.277,-643.1644;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.WorldNormalVector;60;2053.868,-341.5965;Inherit;False;False;1;0;FLOAT3;0,0,1;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.RangedFloatNode;290;3752.728,3295.496;Float;False;Constant;_Float3;Float 3;8;0;Create;True;0;0;0;False;0;False;0.01;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;191;3577.846,211.9993;Float;False;Property;_Color;Color;0;0;Create;True;0;0;0;False;0;False;1,1,1,0;1,1,1,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;171;3670.176,1714.214;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;63;3104.109,77.06786;Float;False;Property;_ShadowStrength;Shadow Strength;1;0;Create;True;0;0;0;False;0;False;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;78;3315.367,-672.3344;Inherit;False;Lerp White To;-1;;3;047d7c189c36a62438973bad9d37b1c2;0;2;1;FLOAT3;0,0,0;False;2;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.DotProductOpNode;58;3030.342,-129.7472;Inherit;False;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;172;3673.177,1851.776;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SamplerNode;25;1441.149,-220.403;Inherit;True;Property;_Albedo;Albedo;3;0;Create;True;0;0;0;False;0;False;-1;None;d07d42f4718b15a4ca58a34c24bb3351;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.StepOpNode;295;3980.903,3355.844;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;294;3970.754,3052.991;Inherit;False;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;59;3222.843,-86.60425;Inherit;False;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;296;4196.583,3205.441;Inherit;False;2;2;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleAddOpNode;173;3821.617,1775.415;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.OneMinusNode;193;3488.781,-49.03879;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;75;3935.24,-5.773822;Inherit;False;3;3;0;FLOAT3;0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;297;4390.618,3199.458;Float;False;LocalVertexOffset;-1;True;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.ColorNode;192;3553.586,-849.2154;Float;False;Property;_Shadowcolor;Shadow color;2;0;Create;True;0;0;0;False;0;False;0,0,0,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.DesaturateOpNode;83;4169.456,-139.8251;Inherit;False;2;0;FLOAT3;0,0,0;False;1;FLOAT;1;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;175;4206.657,718.9796;Inherit;False;2;2;0;FLOAT3;0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;62;3551.484,-281.9076;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.StaticSwitch;176;5340.508,667.3892;Float;False;Property;_Wind;Wind;10;0;Create;True;0;0;0;False;0;False;0;1;1;True;;Toggle;2;Key0;Key1;Create;False;False;9;1;COLOR;0,0,0,0;False;0;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;3;COLOR;0,0,0,0;False;4;COLOR;0,0,0,0;False;5;COLOR;0,0,0,0;False;6;COLOR;0,0,0,0;False;7;COLOR;0,0,0,0;False;8;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.Vector3Node;188;1871.464,76.01755;Inherit;False;Constant;_Vector1;Vector 1;10;0;Create;True;0;0;0;False;0;False;0,0,1;0,0,0;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.LerpOp;84;4609.042,-248.831;Inherit;False;3;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RangedFloatNode;198;6693.781,707.9612;Float;False;Property;_WindStrength1; Occlusion Strength;9;0;Create;False;0;0;0;False;0;False;0.5;0.5;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;186;1430.449,195.0842;Inherit;True;Property;_NormalMap;NormalMap;4;0;Create;True;0;0;0;False;0;False;-1;None;2c270364e6c51b74ba82587967081656;True;0;True;bump;Auto;True;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;74;3904.265,-698.4842;Inherit;False;3;3;0;FLOAT3;0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;187;1827.721,252.9455;Inherit;False;Property;_NormalMapStrength;Normal Map Strength;5;0;Create;True;0;0;0;False;0;False;1;1;0;3;0;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;300;5322.72,535.427;Inherit;False;297;LocalVertexOffset;1;0;OBJECT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.LerpOp;89;4584.438,-713.7209;Inherit;False;3;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.LerpOp;91;4813.838,-503.8902;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;299;722.4908,2041.566;Inherit;False;StaticNoileTilling;-1;True;1;0;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.LerpOp;189;2200.06,82.30655;Inherit;False;3;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.LerpOp;87;4900.854,-99.6487;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;303;6357.97,596.9122;Inherit;False;2;2;0;FLOAT3;0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.DesaturateOpNode;90;4141.227,-707.0986;Inherit;False;2;0;FLOAT3;0,0,0;False;1;FLOAT;1;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SwizzleNode;298;552.3047,2042.005;Inherit;False;FLOAT2;0;1;2;3;1;0;FLOAT4;0,0,0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;185;5279.456,247.8524;Float;False;Property;_Smoothness;Smoothness;7;0;Create;True;0;0;0;False;0;False;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;199;6996.33,717.7987;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;190;5239.927,384.273;Float;False;Property;_Metallic;Metallic;6;0;Create;True;0;0;0;False;0;False;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;7536.564,246.6706;Float;False;True;-1;2;ASEMaterialInspector;0;0;Standard;Custom/Vegetation;False;False;False;False;False;False;False;False;False;False;False;False;False;LODFading;False;False;False;False;True;False;True;Off;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Custom;0.35;True;True;0;True;TreeOpaque;;Geometry;ForwardOnly;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;8;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;False;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;208;0;206;0
WireConnection;207;0;205;1
WireConnection;207;1;205;3
WireConnection;210;0;207;0
WireConnection;209;0;208;0
WireConnection;215;0;213;0
WireConnection;215;1;212;0
WireConnection;214;2;211;0
WireConnection;216;0;215;0
WireConnection;216;1;214;0
WireConnection;217;1;216;0
WireConnection;218;0;217;0
WireConnection;223;0;219;0
WireConnection;227;0;221;0
WireConnection;229;0;222;0
WireConnection;228;0;220;0
WireConnection;234;0;225;0
WireConnection;234;1;226;0
WireConnection;233;0;228;0
WireConnection;232;0;224;0
WireConnection;240;0;231;0
WireConnection;240;1;234;0
WireConnection;151;0;149;0
WireConnection;241;0;230;0
WireConnection;241;1;233;0
WireConnection;245;0;237;1
WireConnection;245;1;237;3
WireConnection;246;0;238;0
WireConnection;247;0;239;0
WireConnection;244;0;236;2
WireConnection;244;1;240;0
WireConnection;153;0;151;0
WireConnection;248;0;235;0
WireConnection;248;1;241;0
WireConnection;156;0;154;0
WireConnection;250;0;248;0
WireConnection;250;1;242;0
WireConnection;157;0;153;0
WireConnection;157;1;155;0
WireConnection;157;2;152;0
WireConnection;253;0;243;0
WireConnection;254;0;245;0
WireConnection;254;1;244;0
WireConnection;260;0;254;0
WireConnection;260;1;249;0
WireConnection;258;0;250;0
WireConnection;158;0;156;0
WireConnection;158;2;157;0
WireConnection;255;0;251;0
WireConnection;255;1;252;0
WireConnection;263;0;257;0
WireConnection;262;0;256;0
WireConnection;264;0;260;0
WireConnection;266;0;258;0
WireConnection;261;0;258;0
WireConnection;160;0;158;0
WireConnection;160;1;159;0
WireConnection;265;0;259;0
WireConnection;265;1;255;0
WireConnection;161;0;160;0
WireConnection;269;0;265;0
WireConnection;269;1;264;0
WireConnection;270;0;261;0
WireConnection;270;2;266;0
WireConnection;274;0;269;0
WireConnection;274;1;268;0
WireConnection;273;0;270;0
WireConnection;164;1;161;0
WireConnection;275;0;267;2
WireConnection;275;1;271;0
WireConnection;165;0;164;0
WireConnection;165;1;163;0
WireConnection;165;2;162;0
WireConnection;276;0;273;0
WireConnection;276;1;272;0
WireConnection;277;0;274;0
WireConnection;277;1;275;0
WireConnection;278;0;276;0
WireConnection;166;0;165;0
WireConnection;279;0;277;0
WireConnection;281;0;279;0
WireConnection;282;0;278;0
WireConnection;283;1;280;2
WireConnection;181;1;177;0
WireConnection;68;0;67;0
WireConnection;68;1;56;0
WireConnection;289;0;285;0
WireConnection;289;1;284;0
WireConnection;289;2;283;0
WireConnection;289;3;286;0
WireConnection;183;0;181;0
WireConnection;293;0;287;0
WireConnection;293;1;288;0
WireConnection;293;3;289;0
WireConnection;184;0;182;0
WireConnection;184;2;183;0
WireConnection;72;0;68;0
WireConnection;171;0;170;0
WireConnection;171;1;169;0
WireConnection;78;1;72;0
WireConnection;58;0;56;0
WireConnection;58;1;60;0
WireConnection;172;0;169;0
WireConnection;172;1;168;0
WireConnection;25;1;184;0
WireConnection;295;0;290;0
WireConnection;295;1;291;2
WireConnection;294;0;293;0
WireConnection;294;1;292;0
WireConnection;59;0;58;0
WireConnection;59;1;25;0
WireConnection;296;0;294;0
WireConnection;296;1;295;0
WireConnection;173;0;171;0
WireConnection;173;1;172;0
WireConnection;193;0;63;0
WireConnection;75;0;78;0
WireConnection;75;1;25;0
WireConnection;75;2;191;0
WireConnection;297;0;296;0
WireConnection;83;0;75;0
WireConnection;175;0;149;0
WireConnection;175;1;173;0
WireConnection;62;0;59;0
WireConnection;62;1;25;0
WireConnection;62;2;193;0
WireConnection;176;0;175;0
WireConnection;84;2;83;0
WireConnection;74;0;78;0
WireConnection;74;1;62;0
WireConnection;74;2;192;0
WireConnection;89;2;90;0
WireConnection;91;0;74;0
WireConnection;299;0;298;0
WireConnection;189;0;188;0
WireConnection;189;1;186;0
WireConnection;189;2;187;0
WireConnection;87;0;75;0
WireConnection;87;1;84;0
WireConnection;303;0;300;0
WireConnection;303;1;176;0
WireConnection;90;0;74;0
WireConnection;298;0;206;0
WireConnection;199;0;198;0
WireConnection;0;0;87;0
WireConnection;0;1;189;0
WireConnection;0;2;91;0
WireConnection;0;3;190;0
WireConnection;0;4;185;0
WireConnection;0;5;199;0
WireConnection;0;10;25;4
WireConnection;0;11;303;0
ASEEND*/
//CHKSM=12119197CDC2D95067BC0249E129F8ABEA90BF19