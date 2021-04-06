Shader "Unlit/Atmosphere"
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

            #include "UnityCG.cginc"
            #include "./Includes/Math.cginc"

            struct appdata {
                float4 vertex : POSITION;
                float4 uv : TEXCOORD0;
			};

			struct v2f {
                float4 pos : SV_POSITION;
                float2 uv : TEXCOORD0;
                float3 viewVector : TEXCOORD1;
			};

			sampler2D _BlueNoise;
			sampler2D _MainTex;
			sampler2D _BakedOpticalDepth;
			sampler2D _CameraDepthTexture;
			float4 params;

			float3 dirToSun;

			float3 planetCentre;
			float atmosphereRadius;
			float oceanRadius;
			float planetRadius;

			// Paramaters
			int numInScatteringPoints;
			int numOpticalDepthPoints;
			float intensity;
			float4 scatteringCoefficients;
			float ditherStrength;
			float ditherScale;
			float densityFalloff;
			
            v2f vert (appdata v) {
                v2f output;
                output.pos = UnityObjectToClipPos(v.vertex);
                output.uv = v.uv;
                // Camera space matches OpenGL convention where cam forward is -z. In unity forward is positive z.
                // (https://docs.unity3d.com/ScriptReference/Camera-cameraToWorldMatrix.html)
                float3 viewVector = mul(unity_CameraInvProjection, float4(v.uv.xy * 2 - 1, 0, -1));
                output.viewVector = mul(unity_CameraToWorld, float4(viewVector,0));
                return output;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float4 originalCol = tex2D(_MainTex, i.uv);

                float3 rayOrigin = _WorldSpaceCameraPos;
				float3 rayDir = normalize(i.viewVector);
				
				float2 hitInfo = raySphere(planetCentre, atmosphereRadius, rayOrigin, rayDir);
                float dstToAtmosphere = hitInfo.x;
                float dstThroughAtmosphere = hitInfo.y;

                return dstThroughAtmosphere / (atmosphereRadius * 2);

            }
            ENDCG
        }
    }
}
