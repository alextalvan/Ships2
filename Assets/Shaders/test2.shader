Shader "Custom/test2" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0

		_WaveScale("Custom Wave Scale", Vector) = (0.1,1.0,0.1,0.0)
		_WaveSpeed("Custom Wave Speed", Range(0.0,10.0)) = 1.0
		_Offset("Custom offset",Vector) = (0.0,0.0,0.0,0.0)
		_TimeScale("Custom time scale", Float) = 1.0
		_SyncTime("Custom time", Float) = 0.0
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard fullforwardshadows

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		//#pragma surface surf Lambert vertex:vert
		#pragma vertex vert

		sampler2D _MainTex;

		struct Input {
			float2 uv_MainTex;
		};

		half _Glossiness;
		half _Metallic;
		fixed4 _Color;

		//custom
		float4 _WaveScale;
		float _WaveSpeed;
		float4 _Offset;
		float _TimeScale;
		float _SyncTime;


		void vert(inout appdata_full v, out Input o) {
			v.vertex.y += sin(_SyncTime * _TimeScale * _WaveSpeed  + (v.vertex.z + _Offset.z) * _WaveScale.z) * _WaveScale.y;
			v.vertex.y += sin(_SyncTime * _TimeScale *_WaveSpeed + (v.vertex.x + _Offset.x) * _WaveScale.x )* _WaveScale.y;
			UNITY_INITIALIZE_OUTPUT(Input, o);
		}

		void surf (Input IN, inout SurfaceOutputStandard o) {
			// Albedo comes from a texture tinted by color
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
			o.Albedo = c.rgb;
			// Metallic and smoothness come from slider variables
			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;
			o.Alpha = c.a;
		}
		ENDCG
	} 
	FallBack "Diffuse"
}
