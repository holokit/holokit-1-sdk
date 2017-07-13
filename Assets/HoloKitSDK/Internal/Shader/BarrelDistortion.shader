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
		float2 center = float2(0.5, 0.5);
		center.x += _HorizontalOffsetFactor;
		center.y += _VerticalOffsetFactor;

		float2 uv = i.uv - center;
		float z = sqrt(1.0 - uv.x * uv.x - uv.y * uv.y);
		float a = 1.0 / (z * tan(_BarrelDistortionFactor * 0.5));
		float2 uv2 = (uv * a) + center;
		float2 red_uv = (uv * a * (1 + _RedDistortionFactor)) + center;
		float2 green_uv = (uv * a * (1 + _GreenDistortionFactor)) + center;
		float2 blue_uv = (uv * a * (1 + _BlueDistortionFactor)) + center;
		
		// if(uv2.x>=1 || uv2.y>=1 || uv2.x<=0 || uv2.y<=0) 
		// 	c = float4(0,0,0,1);
		// else 
		// 	c = tex2D(_MainTex, uv2);

		float4 c = float4(0.0, 0.0, 0.0, 1.0);

		if(red_uv.x > 1 || red_uv.y > 1 || red_uv.x < 0 || red_uv.y < 0) {
			c.r = 0;
		} else {
			c.r = tex2D(_MainTex, red_uv).r;
		}

		if(green_uv.x > 1 || green_uv.y > 1 || green_uv.x < 0 || green_uv.y <0) {
			c.g = 0;
		} else {
			c.g = tex2D(_MainTex, green_uv).g;
		}

		if(blue_uv.x > 1 || blue_uv.y > 1 || blue_uv.x < 0 || blue_uv.y < 0) {
			c.b = 0;
		} else {
			c.b = tex2D(_MainTex, blue_uv).b;
		}

		// float2 uv = i.uv - center;
		// float r2 = 1.0 - uv.x * uv.x - uv.y * uv.y;
		// float a = 1.0 + _BarrelDistortionFactor * r2;
		// float2 uv2 = uv * a;
		
		// float2 red_uv = uv2 * (1.0 + _RedDistortionFactor) + center;
		// float2 green_uv = uv2 * (1.0 + _GreenDistortionFactor) + center;
		// float2 blue_uv = uv2 * (1.0 + _BlueDistortionFactor) + center;
		
		// half4 c = half4(0.0, 0.0, 0.0, 1.0);

		// if(red_uv.x > 1 || red_uv.y > 1 || red_uv.x < 0 || red_uv.y < 0) {
		// 	c.r = 0;
		// } else {
		// 	c.r = tex2D(_MainTex, red_uv).r;
		// }

		// if(green_uv.x > 1 || green_uv.y > 1 || green_uv.x < 0 || green_uv.y <0) {
		// 	c.g = 0;
		// } else {
		// 	c.g = tex2D(_MainTex, green_uv).g;
		// }

		// if(blue_uv.x > 1 || blue_uv.y > 1 || blue_uv.x < 0 || blue_uv.y < 0) {
		// 	c.b = 0;
		// } else {
		// 	c.b = tex2D(_MainTex, blue_uv).b;
		// }

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