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

			struct appdata
			{
				float4 vertex : POSITION;
                float3 normal : NORMAL;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				fixed4 diff : COLOR0; //Diffuse lightning color
				UNITY_FOG_COORDS(1)
				float4 vertex : SV_POSITION;
                float3 worldNormal : TEXCOORD1;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);//Transforma do espaço de modelo pro espaço de clip
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);//a posição da textura
                o.worldNormal = UnityObjectToWorldNormal(v.normal);//A normal do vertice no espaço global
				UNITY_TRANSFER_FOG(o,o.vertex);
				//calculo do brilho
				half nl = max(0, dot(o.worldNormal, _WorldSpaceLightPos0.xyz));
				o.diff = nl * _LightColor0;
				return o;
			}

            //Na situação atual ainda não tem nada.
            //TODO: Fazer a escolha da cor de acordo com a inclinação[Pedreira;Grama]
            //TODO: Iluminação (fazendo)
            //TODO: Sombras
            //TODO: Escolha da cor de acordo com a altitude[Submarino;Praia;Emerso]

			//De acordo com o angulo entre a normal do fragmento e a normal vertical [0,1,0] eu determinarei
            //se é pra ser pedreira ou não.
            //De acordo com a altidude do fragmento, eu determinarei se é acima ou abaixo do mar e o tom de
            //verde.
			fixed4 frag (v2f i) : SV_Target
			{
				///2) Shader pra iluminação
				fixed4 color = tex2D(_MainTex, i.uv);
				color *= i.diff;
				return color;
				///1) Shader que usa a normal pra fazer a cor.
				/*fixed4 c = 0;
				c.rgb = i.worldNormal * 0.5 + 0.5;
				return c;*/

				///Shader original
                /*float3 verticalNormal = float3(0,1,0);
                float inclination = dot(verticalNormal, i.worldNormal);
				// sample the texture
				fixed4 col = fixed4(0, inclination, 0,1);//tex2D(_MainTex, i.uv);
				// apply fog
				UNITY_APPLY_FOG(i.fogCoord, col);
				return col;*/
			}
			ENDCG
		}
	}
}
