Shader "DebugTurbulence"
{
    Properties
    {
    }

    SubShader
    {
        Tags { "RenderPipeline" = "UniversalPipeline" }

        Pass
        {
            HLSLPROGRAM
            #pragma vertex Vert
            #pragma fragment Frag
            #pragma target 3.5

            
            #pragma multi_compile _ OCEAN_THREE_CASCADES OCEAN_FOUR_CASCADES
            #pragma multi_compile _ OCEAN_UNDERWATER_ENABLED
            #pragma multi_compile _ OCEAN_TRANSPARENCY_ENABLED
            #pragma shader_feature_local WAVES_FOAM_ENABLED
            #pragma shader_feature_local CONTACT_FOAM_ENABLED

            // URP Keywords
            #pragma shader_feature_local _RECEIVE_SHADOWS_OFF
            #pragma multi_compile _ _MAIN_LIGHT_SHADOWS
            #pragma multi_compile _ _MAIN_LIGHT_SHADOWS_CASCADE
            
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            #include "../OceanSystem/Shaders/OceanSimulationSampling.hlsl"
            #include "../OceanSystem/Shaders/OceanFoam.hlsl"
            #include "../OceanSystem/Shaders/GeoClipMap.hlsl"

            struct Attributes
            {
                float4 positionOS   : POSITION;
                float2 uv           : TEXCOORD0;
            };


            struct Varyings
            {
                float4 positionHCS  : SV_POSITION;
                float3 positionWS   : TEXCOORD0;
                float viewDepth     : TEXCOORD1;
                float4 positionNDC  : TEXCOORD2;
                float2 worldUV      : TEXCOORD3;
                #ifdef REQUIRES_VERTEX_SHADOW_COORD_INTERPOLATOR
                float4 shadowCoord  : TEXCOORD4;
                #endif
            };

            Varyings Vert(Attributes input)
            {
                Varyings output;

                output.positionWS = ClipMapVertex(input.positionOS.xyz, input.uv);
                output.positionWS.y += 100;
                output.worldUV = output.positionWS.xz;

                float3 viewVector = output.positionWS - _WorldSpaceCameraPos;
                float viewDist = length(viewVector);
                float viewDistXzSquared = dot(viewVector.xz, viewVector.xz);

                float warpDistance = Ocean_LengthScales.x * 0.5;
                output.worldUV += sin(output.worldUV.yx / warpDistance)
                    * min(1, viewDistXzSquared / (warpDistance * warpDistance * 100))
                    * warpDistance * 0.4 * _UvWarpStrength;

                float4 weights = LodWeights(viewDist, _CascadesFadeDist);
                output.positionWS += SampleDisplacement(output.worldUV, weights, 1);

                float3 positionOS = TransformWorldToObject(output.positionWS);
                VertexPositionInputs positionInputs = GetVertexPositionInputs(positionOS);
                output.viewDepth = -positionInputs.positionVS.z;
                output.positionNDC = positionInputs.positionNDC;
                output.positionHCS = positionInputs.positionCS;
                #ifdef REQUIRES_VERTEX_SHADOW_COORD_INTERPOLATOR
                output.shadowCoord = GetShadowCoord(positionInputs);
                #endif
                return output;
            }

            sampler2D _MainTex;

            float4 Frag(Varyings input) : SV_Target
            {
                                float3 viewDir = _WorldSpaceCameraPos - input.positionWS;
                float viewDist = length(viewDir);
                viewDir = viewDir / viewDist;

                float4 lodWeights = LodWeights(viewDist, _CascadesFadeDist);

                float4 shoreWeights = 1;// ShoreModulation(i.shore.x);
                float4x4 derivatives = SampleDerivatives(input.worldUV, lodWeights * shoreWeights);
                float3 normal = NormalFromDerivatives(derivatives, 1);
          
                float4x4 fTurbulence = SampleTurbulence(input.worldUV, lodWeights * shoreWeights);

                float4 turbulence = MixTurbulence(fTurbulence, Ocean_FoamCascadesWeights, lodWeights * ACTIVE_CASCADES);
                float foamValueCurrent = lerp(turbulence.y, turbulence.x, Ocean_FoamSharpness);
                float foamValuePersistent = (turbulence.z + turbulence.w) * 0.5;
                foamValueCurrent = lerp(foamValueCurrent, foamValuePersistent, Ocean_FoamPersistence);
                foamValueCurrent -= 1;
                foamValuePersistent -= 1;
                float4 finalColor = float4(foamValueCurrent, 0, 0, 1);
                return finalColor;
            }
            ENDHLSL
        }
    }
}