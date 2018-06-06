// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "MyTerrain/MyTerrainShader"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_SeaLevel("Sea Level", float) = 0.0
		_BeachWidth("Beach Width", float)=0.0
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
			uniform float _SeaLevel;
			uniform float _BeachWidth;
			struct appdata
			{
				float4 vertex : POSITION;
                float3 normal : NORMAL;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float4 wp : TEXCOORD2;
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
				o.wp = v.vertex;
				o.pos = UnityObjectToClipPos(v.vertex);//Transforma do espaço de modelo pro espaço de clip
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);//a posição da textura
                o.worldNormal = UnityObjectToWorldNormal(v.normal);//A normal do vertice no espaço global
				UNITY_TRANSFER_FOG(o,o.pos);
				//calculo do brilho
				half nl = max(0, dot(o.worldNormal, _WorldSpaceLightPos0.xyz));
				o.diff = nl * _LightColor0.rgb;
				o.ambient = ShadeSH9(half4(o.worldNormal, 1));
				// compute shadows data
				TRANSFER_SHADOW(o);
				
				return o;
			}

			////Avalia se é pra usar a cor de pedreira ou se é pra usar a cor de grama
			fixed4 evaluateColorByInclination(float3 fragNormal) {
				float3 upVector = float3(0, 1, 0);
				half angle = dot(upVector, fragNormal);
				fixed4 basePedreiraColor = fixed4(0.65, 0.63, 0.63, 1.0);
				fixed4 baseGramaColor = fixed4(0.2, 0.35, 0.05, 1.0);

				fixed4 result = angle * baseGramaColor + (1 - angle) * basePedreiraColor;
				return result;
			}
			////Avalia se é para mudar a cor para submarino, praia, ou manter igual
			fixed4 evaluateColorByHeight(fixed4 color, float3 vertex) {
				if (vertex.y < _SeaLevel - _BeachWidth/2) {//Submerso
					color.b = 1.0;
					color.rgb = normalize(color.rgb);
				}
				else if (vertex.y < _SeaLevel + _BeachWidth/2)//areia
				{
					color.rgb = float3(0.95, 1.0, 0.6);
				}
				else {//Mantém como está
					  //...
				}
				return color;
			}

			//De acordo com o angulo entre a normal do fragmento e a normal vertical [0,1,0] eu determinarei
            //se é pra ser pedreira ou não.
            //De acordo com a altidude do fragmento, eu determinarei se é acima ou abaixo do mar e o tom de
            //verde.
			fixed4 frag (v2f i) : SV_Target
			{

				///2) Shader pra iluminação
				fixed4 color = evaluateColorByHeight(evaluateColorByInclination(i.worldNormal), i.wp);
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
