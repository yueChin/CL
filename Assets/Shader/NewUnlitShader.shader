Shader "Unlit/Tutorial_Shader"{
    Properties{
        _Color("Color",Color)=(1,1,1,1)
        _MainTexture("Main Texture",2D)="white"{}
        _DissolveTexture("Dissolve Texture",2D)="white"{}
        _DissolveCutoff("Dissolve Cutoff",Range(0,1))=1
        _ExtrudeAmount("Extrue Amount",float)=0
    }
    Subshader{
        Pass{
            CGPROGRAM
                #pragma vertex vertexFunction
                #pragma fragment fragmentFunction
                #include "UnityCG.cginc"

                struct a2v{
                    float4 vertex:POSITION;
                    float2 uv:TEXCOORD0;
                    float3 normal:NORMAL;
                };
                struct v2f{
                    float4 position:SV_POSITION;
                    float2 uv:TEXCOORD0;
                };
                float4 _Color;
                sampler2D _MainTexture;
                sampler2D _DissolveTexture;
                float _DissolveCutoff;
                float _ExtrudeAmount;

                v2f vertexFunction(a2v v){
                    v2f o;
                    v.vertex.xyz+=v.normal.xyz*_ExtrudeAmount*sin(_Time.y);
                    o.position=UnityObjectToClipPos(v.vertex);
                    o.uv=v.uv;
                    return o;
                }

                fixed4 fragmentFunction(v2f i):SV_TARGET{
                    float4 textureColor=tex2D(_MainTexture,i.uv);
                    float4 dissolveColor=tex2D(_DissolveTexture,i.uv);
                    clip(dissolveColor.rgb-_DissolveCutoff);
                    return textureColor;
                }
            ENDCG
        }
    }
}