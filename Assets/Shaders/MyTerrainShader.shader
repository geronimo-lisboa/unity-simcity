Shader "MyTerrain/MyTerrainShader"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 100

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			// make fog work
			#pragma multi_compile_fog
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
                float3 normal : NORMAL;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				UNITY_FOG_COORDS(1)
				float4 vertex : SV_POSITION;
                float3 worldNormal : TEXCOORD1;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.worldNormal = UnityObjectToWorldNormal(v.normal);
				UNITY_TRANSFER_FOG(o,o.vertex);
				return o;
			}

            //Na situação atual ainda não tem nada.
            //TODO: Fazer a escolha da cor de acordo com a inclinação[Pedreira;Grama]
            //TODO: Iluminação
            //TODO: Sombras
            //TODO: Escolha da cor de acordo com a altitude[Submarino;Praia;Emerso]

			//De acordo com o angulo entre a normal do fragmento e a normal vertical [0,1,0] eu determinarei
            //se é pra ser pedreira ou não.
            //De acordo com a altidude do fragmento, eu determinarei se é acima ou abaixo do mar e o tom de
            //verde.
			fixed4 frag (v2f i) : SV_Target
			{
                float3 verticalNormal = float3(0,1,0);
                float inclination = verticalNormal, i.worldNormal;
				// sample the texture
				fixed4 col = fixed4(0, inclination, 0,1);//tex2D(_MainTex, i.uv);
				// apply fog
				UNITY_APPLY_FOG(i.fogCoord, col);
				return col;
			}
			ENDCG
		}
	}
}
