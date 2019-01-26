Shader "Custom/FlooringVertical" {
	Properties {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Bump ("Normal map", 2D) = "bump" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
        _Scale ("Scale", Range(0.001, 10.0)) = 0.2
    }
    SubShader {
        Tags { "RenderType"="Opaque" }
        LOD 200
        //Cull off

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard
        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0
        sampler2D _MainTex;
        sampler2D _Bump;
        float _Scale;
        
        struct Input {
            float2 uv_MainTex;
            float2 uv_Bump;
            float3 worldNormal;
            float3 worldPos;
            INTERNAL_DATA
        };

        half _Glossiness;
        half _Metallic;
        fixed4 _Color;

       void surf (Input IN, inout SurfaceOutputStandard o) {
            // Albedo comes from a texture tinted by color
            if(abs(IN.worldNormal.y) > 0.5)
            {
                o.Albedo = tex2D(_MainTex, IN.worldPos.xz * _Scale) * _Color;
            }
            else if(abs(IN.worldNormal.x) > 0.5)
            {
                o.Albedo = tex2D(_MainTex, IN.worldPos.zy * _Scale) * _Color;
            }
            else
            {
                o.Albedo = tex2D(_MainTex, IN.worldPos.xy * _Scale) * _Color;
            }
            
            
            //o.Albedo = c.rgb;
            // Metallic and smoothness come from slider variables
            o.Emission = o.Albedo;
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            //o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Legacy Shaders/VertexLit"
}
