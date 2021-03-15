Shader "Custom/CullOffShader" {
	Properties
	{
		_IlluminCol("Self-Illumination color (RGB)", Color) = (1,1,1,1)
		_Color("Main Color", Color) = (1,1,1,0)
		_MainTex("Base (RGB)", 2D) = "white" {}
	}

	SubShader
	{
		Tags {"Queue" = "Transparent" "RenderType" = "Transparent" }
		Cull off
		ZWrite Off
		Blend SrcAlpha OneMinusSrcAlpha

		Pass 
		{
		
			Material
			{
			Diffuse[_Color]
			Ambient[_Color]
			}

			Lighting On

			SetTexture[_MainTex] 
			{
			constantColor[_IlluminCol]
			combine constant lerp(texture) previous
			}
		
			SetTexture[_MainTex] 
			{
			combine previous * texture
			}
		}

	}
}

