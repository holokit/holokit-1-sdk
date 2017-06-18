// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Hidden/BarrelDistortion" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "" {}
		_FOV ("FOV", float) = 1.48
		_Offset ("Offset", float) = 0
	}
	
	CGINCLUDE
	
	#include "UnityCG.cginc"
	#pragma fragmentoption ARB_precision_hint_fastest 
	struct v2f {
		float4 pos : SV_POSITION;
		float2 uv : TEXCOORD0;
	};
	
	sampler2D _MainTex;
	
	float _FOV;
	float _Offset;
	
	v2f vert( appdata_img v ) 
	{
		v2f o;
		o.pos = UnityObjectToClipPos(v.vertex);
		o.uv = v.texcoord.xy;
		return o;
	} 
	
	half4 frag(v2f i) : SV_Target 
	{
		float2 uv = i.uv - 0.5;
		float z = sqrt(1.0 - uv.x * uv.x - uv.y * uv.y);
		float a = 1.0 / (z * tan(_FOV * 0.5));
		float4 c;
		float2 uv2 = (uv * a) + 0.5;
		uv2.x -= _Offset;
		
		if(uv2.x>=1 || uv2.y>=1 || uv2.x<=0 || uv2.y<=0) 
		c = float4(0,0,0,1);
		else 
		c = tex2D(_MainTex, uv2);
		return c;
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