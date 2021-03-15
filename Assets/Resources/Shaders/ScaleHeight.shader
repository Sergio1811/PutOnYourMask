// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "ScaleHeight"
{
	Properties
	{
		_Cutoff( "Mask Clip Value", Float ) = 0.5
		_ScaleHeight("ScaleHeight", Float) = 1
		_EmissionPower("EmissionPower", Float) = 1
		_MainColor("MainColor", Color) = (0,0,0,0)
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "TransparentCutout"  "Queue" = "Geometry+0" "IgnoreProjector" = "True" "IsEmissive" = "true"  }
		Cull Off
		Blend SrcAlpha OneMinusSrcAlpha
		CGPROGRAM
		#pragma target 3.0
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows exclude_path:deferred 
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform float4 _MainColor;
		uniform float _EmissionPower;
		uniform float _ScaleHeight;
		uniform float _Cutoff = 0.5;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float4 temp_output_12_0 = _MainColor;
			o.Albedo = temp_output_12_0.rgb;
			o.Emission = ( _MainColor * _EmissionPower ).rgb;
			o.Alpha = 1;
			float lerpResult8 = lerp( ( _MainColor.a * ( ( 1.0 - i.uv_texcoord.x ) * _ScaleHeight ) ) , 1.0 , ( _ScaleHeight / 2.0 ));
			clip( lerpResult8 - _Cutoff );
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=15401
338.4;94.4;939;628;984.9248;741.7421;1.895664;True;False
Node;AmplifyShaderEditor.TextureCoordinatesNode;1;-717.7443,-110.2049;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.OneMinusNode;2;-396.2323,-69.10741;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;6;-342.1809,215.1773;Float;False;Property;_ScaleHeight;ScaleHeight;1;0;Create;True;0;0;False;0;1;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;12;-271.8755,-451.0155;Float;False;Property;_MainColor;MainColor;3;0;Create;True;0;0;False;0;0,0,0,0;0.9652156,1,0,0;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;3;-173.2322,0.8925819;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleDivideOpNode;9;139.8464,178.0835;Float;False;2;0;FLOAT;0;False;1;FLOAT;2;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;11;-147.4368,-175.2233;Float;False;Property;_EmissionPower;EmissionPower;2;0;Create;True;0;0;False;0;1;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;7;30.9164,-77.43165;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;10;189.3466,-256.2809;Float;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;8;242.9389,-96.79713;Float;False;3;0;FLOAT;0;False;1;FLOAT;1;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;524.1281,-353.2987;Float;False;True;2;Float;ASEMaterialInspector;0;0;Standard;ScaleHeight;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;False;False;False;False;False;Off;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Custom;0.5;True;True;0;True;TransparentCutout;;Geometry;ForwardOnly;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;2;5;False;-1;10;False;-1;0;0;False;-1;0;False;-1;-1;False;-1;-1;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;0;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;2;0;1;1
WireConnection;3;0;2;0
WireConnection;3;1;6;0
WireConnection;9;0;6;0
WireConnection;7;0;12;4
WireConnection;7;1;3;0
WireConnection;10;0;12;0
WireConnection;10;1;11;0
WireConnection;8;0;7;0
WireConnection;8;2;9;0
WireConnection;0;0;12;0
WireConnection;0;2;10;0
WireConnection;0;10;8;0
ASEEND*/
//CHKSM=54841C37060A173EC11D39C73E478F285C567679