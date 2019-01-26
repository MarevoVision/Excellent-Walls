Shader "Custom/Wall_Visualization"
{
    Properties {
    _Color ("Main Color", Color) = (1,1,1,1)
    _SpecColor ("Specular Color", Color) = (0.5,0.5,0.5,1)
    _ReflectColor ("Reflection Color", Color) = (1,1,1,0.5)
    _Shininess ("Shininess", Range (0.01, 1)) = 0.078125
    _MainTex ("Base (RGB) Gloss (A)", 2D) = "white" {}
    _BumpMap ("Mormals (RGB)", 2D) = "white" {}
    _NormalK ("Normal deep", Range (0.01, 1)) = 1.0
    _Scale ("Texture Scale", Range (0.01, 10)) = 1.0
  }

  SubShader { 
    Tags { "RenderType"="Opaque" }
    LOD 200
    CGPROGRAM
    #pragma surface surf BlinnPhong// vertex:vert// CustomBlinnPhong// BlinnPhong
    #pragma target 3.0
    
    sampler2D _MainTex, _BumpMap;
    //samplerCUBE _Cube;
    fixed4 _Color;
    fixed4 _ReflectColor;
    half _Shininess;

    float _Scale;
    float _NormalK;

    struct Input {
      //float2 uv_MainTex;
      //float2 uv_BumpMap;
      //float3 worldRefl;

      float3 worldNormal;
      float3 worldPos;
      INTERNAL_DATA
    };

    void surf (Input IN, inout SurfaceOutput o) {
      float2 UV;
      fixed4 c;

      // Не работает:
      /*if(abs(IN.worldNormal2.x)>0.5)
        UV = IN.worldPos.yz; // side
      else 
      if(abs(IN.worldNormal2.z)>0.5)
        UV = IN.worldPos.xy; // front --<
      else 
        UV = IN.worldPos.xz; // top*/
      
      // Работает:
            half faceUp =  WorldNormalVector ( IN , float3( 0, 0, 1 ) ).y;
            if(abs(faceUp)>0.5){
              UV = IN.worldPos.xz*faceUp;
            }else{
               half faceFront =  WorldNormalVector ( IN , float3( 0, 0, 1 ) ).z;
              if(abs(faceFront)>0.5){
                UV = IN.worldPos.xy; // front --<
              }else
                UV = IN.worldPos.zy;
            }

      UV *= _Scale;// IN.uv_MainTex;//
      c = tex2D(_MainTex, UV); // use WALL texture

      //fixed4 tex = tex2D(_MainTex, IN.uv_MainTex);
      //fixed4 c = tex * _Color;
      //o.Albedo = c.rgb;

      o.Albedo = c.rgb * _Color;

      o.Gloss = c.a;//tex.a;
      o.Specular = _Shininess;

      o.Normal = UnpackNormal (tex2D (_BumpMap, UV))*_NormalK;

      //float3 worldRefl = WorldReflectionVector (IN, o.Normal);
      //fixed4 reflcol = texCUBE (_Cube, worldRefl);
      //reflcol *= tex.a;
      o.Emission = _ReflectColor.rgb;//reflcol.rgb * _ReflectColor.rgb;
      o.Alpha = 0;//reflcol.a * _ReflectColor.a;*/
    }
    ENDCG
  }
}
