// Shader created with Shader Forge v1.25 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.25;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:False,qofs:0,qpre:2,rntp:3,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False;n:type:ShaderForge.SFN_Final,id:3138,x:33837,y:32608,varname:node_3138,prsc:2|emission-1727-OUT,clip-4634-OUT;n:type:ShaderForge.SFN_Color,id:7241,x:32445,y:32572,ptovrint:False,ptlb:Color1,ptin:_Color1,varname:_Color,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:0,c3:0,c4:1;n:type:ShaderForge.SFN_TexCoord,id:9663,x:31913,y:33101,varname:node_9663,prsc:2,uv:0;n:type:ShaderForge.SFN_Color,id:8306,x:32445,y:32737,ptovrint:False,ptlb:Color2,ptin:_Color2,varname:_node_8306,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0,c2:1,c3:0.2551723,c4:1;n:type:ShaderForge.SFN_Lerp,id:1727,x:32743,y:32663,varname:node_1727,prsc:2|A-7241-RGB,B-8306-RGB,T-1838-OUT;n:type:ShaderForge.SFN_Slider,id:1838,x:31896,y:32901,ptovrint:False,ptlb:Health,ptin:_Health,varname:_Health,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1,max:1;n:type:ShaderForge.SFN_RemapRange,id:3565,x:32105,y:33101,varname:node_3565,prsc:2,frmn:0,frmx:1,tomn:-1,tomx:1|IN-9663-UVOUT;n:type:ShaderForge.SFN_Length,id:930,x:32292,y:33101,varname:node_930,prsc:2|IN-3565-OUT;n:type:ShaderForge.SFN_Floor,id:1157,x:32452,y:33126,varname:node_1157,prsc:2|IN-930-OUT;n:type:ShaderForge.SFN_OneMinus,id:8550,x:32624,y:33126,varname:node_8550,prsc:2|IN-1157-OUT;n:type:ShaderForge.SFN_Add,id:439,x:32452,y:32983,varname:node_439,prsc:2|A-14-OUT,B-930-OUT;n:type:ShaderForge.SFN_Floor,id:55,x:32624,y:32983,varname:node_55,prsc:2|IN-439-OUT;n:type:ShaderForge.SFN_Multiply,id:4634,x:33241,y:32993,varname:node_4634,prsc:2|A-55-OUT,B-8550-OUT,C-8854-OUT;n:type:ShaderForge.SFN_ArcTan2,id:3330,x:32486,y:33306,varname:node_3330,prsc:2,attp:2|A-5338-G,B-5338-R;n:type:ShaderForge.SFN_ComponentMask,id:5338,x:32255,y:33284,varname:node_5338,prsc:2,cc1:0,cc2:1,cc3:-1,cc4:-1|IN-3565-OUT;n:type:ShaderForge.SFN_Ceil,id:8854,x:33012,y:33302,varname:node_8854,prsc:2|IN-1012-OUT;n:type:ShaderForge.SFN_Subtract,id:1012,x:32825,y:33302,varname:node_1012,prsc:2|A-1838-OUT,B-7343-OUT;n:type:ShaderForge.SFN_OneMinus,id:7343,x:32655,y:33338,varname:node_7343,prsc:2|IN-3330-OUT;n:type:ShaderForge.SFN_Slider,id:14,x:32026,y:33006,ptovrint:False,ptlb:Width,ptin:_Width,varname:node_14,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.02129606,max:1;proporder:7241-8306-1838-14;pass:END;sub:END;*/

Shader "Shader Forge/Health" {
    Properties {
        _Color1 ("Color1", Color) = (1,0,0,1)
        _Color2 ("Color2", Color) = (0,1,0.2551723,1)
        _Health ("Health", Range(0, 1)) = 1
        _Width ("Width", Range(0, 1)) = 0.02129606
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "Queue"="Transparent+100"
            "RenderType"="TransparentCutout"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma exclude_renderers gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform float4 _Color1;
            uniform float4 _Color2;
            uniform float _Health;
            uniform float _Width;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex );
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                float2 node_3565 = (i.uv0*2.0+-1.0);
                float node_930 = length(node_3565);
                float2 node_5338 = node_3565.rg;
                float node_8854 = ceil((_Health-(1.0 - ((atan2(node_5338.g,node_5338.r)/6.28318530718)+0.5))));
                clip((floor((_Width+node_930))*(1.0 - floor(node_930))*node_8854) - 0.5);
////// Lighting:
////// Emissive:
                float3 emissive = lerp(_Color1.rgb,_Color2.rgb,_Health);
                float3 finalColor = emissive;
                return fixed4(finalColor,1);
            }
            ENDCG
        }
        Pass {
            Name "ShadowCaster"
            Tags {
            	"Queue"="Transparent+100"
                "LightMode"="ShadowCaster"
            }
            Offset 1, 1
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_SHADOWCASTER
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma exclude_renderers gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform float _Health;
            uniform float _Width;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                V2F_SHADOW_CASTER;
                float2 uv0 : TEXCOORD1;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex );
                TRANSFER_SHADOW_CASTER(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                float2 node_3565 = (i.uv0*2.0+-1.0);
                float node_930 = length(node_3565);
                float2 node_5338 = node_3565.rg;
                float node_8854 = ceil((_Health-(1.0 - ((atan2(node_5338.g,node_5338.r)/6.28318530718)+0.5))));
                clip((floor((_Width+node_930))*(1.0 - floor(node_930))*node_8854) - 0.5);
                SHADOW_CASTER_FRAGMENT(i)
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
