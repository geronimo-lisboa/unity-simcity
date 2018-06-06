Shader "MyTerrain/MyTerrainShader"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" "LightMode" = "ForwardBase" }
		LOD 100

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			// make fog work
			#pragma multi_compile_fog
			
			#include "UnityCG.cginc"
			#include "UnityLightingCommon.cginc" 
			#include "Lighting.cginc"
			// compile shader into multiple variants, with and without shadows
			// (we don't care about any lightmaps yet, so skip these variants)
			#pragma multi_compile_fwdbase nolightmap nodirlightmap nodynlightmap novertexlight
			// shadow helper functions and macros
			#include "AutoLight.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
                float3 normal : NORMAL;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				fixed3 diff : COLOR0; //Diffuse lightning color
				fixed3 ambient : COLOR1;
				UNITY_FOG_COORDS(1)
				float4 pos : SV_POSITION;
                float3 worldNormal : NORMAL;
				SHADOW_COORDS(1) // put shadows data into TEXCOORD1
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.pos = UnityObjectToClipPos(v.vertex);//Transforma do espaço de modelo pro espaço de clip
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);//a posição da textura
                o.worldNormal = UnityObjectToWorldNormal(v.normal);//A normal do vertice no espaço global
				UNITY_TRANSFER_FOG(o,o.pos);
				//calculo do brilho
				half nl = max(0, dot(o.worldNormal, _WorldSpaceLightPos0.xyz));
				o.diff = nl * _LightColor0.rgb;
				o.ambient = ShadeSH9(half4(o.worldNormal, 1));
				// compute shadows data
				TRANSFER_SHADOW(o)
				return o;
			}

            //Na situação atual ainda não tem nada.
            //TODO: Fazer a escolha da cor de acordo com a inclinação[Pedreira;Grama]
            //TODO: Escolha da cor de acordo com a altitude[Submarino;Praia;Emerso]

			//De acordo com o angulo entre a normal do fragmento e a normal vertical [0,1,0] eu determinarei
            //se é pra ser pedreira ou não.
            //De acordo com a altidude do fragmento, eu determinarei se é acima ou abaixo do mar e o tom de
            //verde.
			fixed4 frag (v2f i) : SV_Target
			{
				///2) Shader pra iluminação
				fixed4 color = tex2D(_MainTex, i.uv);
				// compute shadow attenuation (1.0 = fully lit, 0.0 = fully shadowed)
				fixed shadow = SHADOW_ATTENUATION(i);
				// darken light's illumination with shadow, keep ambient intact
				fixed3 lighting = i.diff * shadow + i.ambient*0.5 ;

				color.rgb *= lighting;

				return color;
			}
			ENDCG
		}
		// pull in shadow caster from VertexLit built-in shader
		UsePass "Legacy Shaders/VertexLit/SHADOWCASTER"
	}
}
