// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Hidden/BarrelDistortion" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "" {}
	}
	
	CGINCLUDE
	
	#include "UnityCG.cginc"

	#pragma fragmentoption ARB_precision_hint_fastest 
	
	uniform float _RedDistortionFactor;
	uniform float _GreenDistortionFactor;
	uniform float _BlueDistortionFactor;
	uniform float _BarrelDistortionFactor;
	uniform float _HorizontalOffsetFactor;
	uniform float _VerticalOffsetFactor;

	sampler2D _MainTex;
	
	struct v2f {
		float4 pos : SV_POSITION;
		float2 uv : TEXCOORD0;
	};

	v2f vert( appdata_img v ) 
	{
		v2f o;
		o.pos = UnityObjectToClipPos(v.vertex);
		o.uv = v.texcoord.xy;
		return o;
	} 

	half4 frag(v2f i) : SV_Target 
	{
        return tex2D(_MainTex, i.uv);
	}

	ENDCG 
	
Subshader {
 Pass {
	  ZTest Always Cull Off ZWrite Off
	  Fog { Mode off }      

      CGPROGRAM
      #pragma fragmentoption ARB_precision_hint_fastest 
      #pragma vertex vert
      #pragma fragment frag
      ENDCG
  }
  
}

Fallback off
	
} // shader