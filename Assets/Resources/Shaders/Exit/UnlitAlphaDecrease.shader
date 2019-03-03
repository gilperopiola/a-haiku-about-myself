﻿// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

Shader "Custom/UnlitAlphaDecrease" {
	Properties {
		_MainTex ("Texture", 2D) = "white" {}
		_Color("Color", Color) = (0, 0, 0, 0)
		_AlphaY ("Alpha Y", Range (-20 , 20)) = 6.5
	}
	SubShader {
		Tags { "RenderType"="Transparent" "Queue"="Transparent" }
		
		Blend SrcAlpha OneMinusSrcAlpha
		LOD 100

		Pass {
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

			struct appdata {
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f {
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
				float y : TEXCOORD1;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			float _AlphaY;
			float4 _Color;
			
			v2f vert (appdata v) {
				v2f o;
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				float4 vPos = mul(unity_ObjectToWorld, v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				o.y = (vPos.y + _AlphaY);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target {
				fixed4 col = _Color;
				col.a = 0.7f - saturate(i.y);
				return col;
			}

			ENDCG
		}
	}
}
