// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "HoloKit/PlaneDetect"
{
	Properties
	{
		_Texture("Texture", 2D) = "white" {}
		_OverColor("OverColor", Color) = (0,0,0,0)
		_Fader("Fader", 2D) = "white" {}
		_WaveIntensity("Wave Intensity", Float) = 0
		_TileCount("TileCount", Float) = 0
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Transparent"  "Queue" = "Transparent+0" "IgnoreProjector" = "True" "IsEmissive" = "true"  }
		Cull Off
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#pragma target 3.0
		#pragma surface surf Unlit alpha:fade keepalpha noshadow 
		struct Input
		{
			float3 worldPos;
			float2 uv_texcoord;
		};

		uniform sampler2D _Texture;
		uniform float _TileCount;
		uniform float4 _OverColor;
		uniform sampler2D _Fader;
		uniform float _WaveIntensity;

		inline fixed4 LightingUnlit( SurfaceOutput s, half3 lightDir, half atten )
		{
			return fixed4 ( 0, 0, 0, s.Alpha );
		}

		void surf( Input i , inout SurfaceOutput o )
		{
			float3 ase_worldPos = i.worldPos;
			float4 appendResult3 = (float4(ase_worldPos.x , ase_worldPos.z , 0.0 , 0.0));
			float4 tex2DNode2 = tex2D( _Texture, ( appendResult3 * _TileCount ).xy );
			o.Emission = ( tex2DNode2 * _OverColor ).rgb;
			float2 temp_cast_2 = (( _WaveIntensity * _Time.y )).xx;
			float2 uv_TexCoord19 = i.uv_texcoord * float2( 1,1 ) + temp_cast_2;
			o.Alpha = ( tex2DNode2.a * _OverColor.a * tex2D( _Fader, uv_TexCoord19 ).a );
		}

		ENDCG
	}
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=13801
0;92;1126;606;1682.447;222.0856;1.859503;True;False
Node;AmplifyShaderEditor.WorldPosInputsNode;5;-1113.436,-67.84444;Float;False;0;4;FLOAT3;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.RangedFloatNode;20;-1085.422,523.922;Float;False;Property;_WaveIntensity;Wave Intensity;3;0;0;0;0;0;1;FLOAT
Node;AmplifyShaderEditor.TimeNode;27;-1106.001,335.7656;Float;False;0;5;FLOAT4;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.RangedFloatNode;29;-1037.199,166.5506;Float;False;Property;_TileCount;TileCount;4;0;0;0;0;0;1;FLOAT
Node;AmplifyShaderEditor.DynamicAppendNode;3;-913.1738,-72.0509;Float;False;FLOAT4;4;0;FLOAT;0.0;False;1;FLOAT;0.0;False;2;FLOAT;0.0;False;3;FLOAT;0.0;False;1;FLOAT4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;21;-836.0274,460.2502;Float;False;2;2;0;FLOAT;0.0;False;1;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;28;-841.9517,153.5341;Float;False;2;2;0;FLOAT4;0,0,0,0;False;1;FLOAT;0.0,0,0,0;False;1;FLOAT4
Node;AmplifyShaderEditor.TextureCoordinatesNode;19;-708.2022,609.7074;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.SamplerNode;17;-406.6678,416.1584;Float;True;Property;_Fader;Fader;2;0;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;5;COLOR;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.ColorNode;13;-666.8091,118.7937;Float;False;Property;_OverColor;OverColor;1;0;0,0,0,0;0;5;COLOR;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.SamplerNode;2;-712.8246,-98.84341;Float;True;Property;_Texture;Texture;0;0;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;5;COLOR;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;16;-71.86975,248.21;Float;False;3;3;0;FLOAT;0.0;False;1;FLOAT;0.0;False;2;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;15;-250.7071,63.69364;Float;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;33;135.9154,-0.242697;Float;False;True;2;Float;ASEMaterialInspector;0;0;Unlit;HoloKit/PlaneDetect;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;False;False;Off;0;0;False;0;0;Transparent;0.5;True;False;0;False;Transparent;Transparent;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;False;0;255;255;0;0;0;0;0;0;0;0;False;2;15;10;25;False;0.5;False;2;SrcAlpha;OneMinusSrcAlpha;0;Zero;Zero;OFF;OFF;0;False;0;0,0,0,0;VertexOffset;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;0;0;False;14;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0.0;False;4;FLOAT;0.0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0.0;False;9;FLOAT;0.0;False;10;FLOAT;0.0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;3;0;5;1
WireConnection;3;1;5;3
WireConnection;21;0;20;0
WireConnection;21;1;27;2
WireConnection;28;0;3;0
WireConnection;28;1;29;0
WireConnection;19;1;21;0
WireConnection;17;1;19;0
WireConnection;2;1;28;0
WireConnection;16;0;2;4
WireConnection;16;1;13;4
WireConnection;16;2;17;4
WireConnection;15;0;2;0
WireConnection;15;1;13;0
WireConnection;33;2;15;0
WireConnection;33;9;16;0
ASEEND*/
//CHKSM=4210F07AB79EB2ED989772EB38D7E4F4E8F296EC