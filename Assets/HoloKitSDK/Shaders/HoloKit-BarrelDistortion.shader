Shader "HoloKit/BarrelDistortion" {
	Properties{
		_MainTex("Base (RGB)", 2D) = "" {}
	}

		CGINCLUDE

#include "UnityCG.cginc"

#pragma fragmentoption ARB_precision_hint_fastest 

	uniform float _BarrelDistortionFactor;
	uniform float _HorizontalOffsetFactor;
	uniform float _VerticalOffsetFactor;

	sampler2D _MainTex;

	struct v2f {
		float4 pos : SV_POSITION;
		float2 uv : TEXCOORD0;
	};

	v2f vert(appdata_img v)
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
		float r2 = 1.0 - uv.x * uv.x - uv.y * uv.y;
		float a = 1.0 + r2 * _BarrelDistortionFactor;
//        float a = 1.0 / (sqrt(z) * tan(_BarrelDistortionFactor * 0.5));

		float2 uv2 = (uv * a) + center;
        
		float4 c = float4(0.0, 0.0, 0.0, 1.0);
		if(uv2.x>=1 || uv2.y>=1 || uv2.x<=0 || uv2.y<=0) 
			c = float4(0,0,0,1);
		else 
			c = tex2D(_MainTex, uv2);

		return c;
	}

		ENDCG

		Subshader {
		Pass{
			ZTest Always Cull Off ZWrite Off
			Fog{ Mode off }

			CGPROGRAM
#pragma fragmentoption ARB_precision_hint_fastest 
#pragma vertex vert
#pragma fragment frag
			ENDCG
		}

	}

	Fallback off

} // shader