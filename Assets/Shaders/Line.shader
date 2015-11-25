// Shader created with Shader Forge v1.25 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.25;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:False,qofs:0,qpre:2,rntp:3,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False;n:type:ShaderForge.SFN_Final,id:3138,x:34021,y:32648,varname:node_3138,prsc:2|emission-8597-OUT,clip-1603-OUT;n:type:ShaderForge.SFN_Color,id:7241,x:32489,y:32461,ptovrint:False,ptlb:Color,ptin:_Color,varname:node_7241,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.03676468,c2:0.03676468,c3:0.03676468,c4:1;n:type:ShaderForge.SFN_Add,id:5669,x:32568,y:32952,varname:node_5669,prsc:2|A-5005-OUT,B-3776-OUT,C-9296-OUT;n:type:ShaderForge.SFN_Time,id:2262,x:32160,y:33299,varname:node_2262,prsc:2;n:type:ShaderForge.SFN_Multiply,id:3608,x:32953,y:32969,varname:node_3608,prsc:2|A-5669-OUT,B-1967-OUT,C-5395-OUT;n:type:ShaderForge.SFN_Sin,id:7268,x:33173,y:32994,varname:node_7268,prsc:2|IN-3608-OUT;n:type:ShaderForge.SFN_RemapRange,id:2639,x:33382,y:32976,varname:node_2639,prsc:2,frmn:0.5,frmx:1,tomn:-1,tomx:1|IN-7268-OUT;n:type:ShaderForge.SFN_TexCoord,id:3654,x:31451,y:32775,varname:node_3654,prsc:2,uv:0;n:type:ShaderForge.SFN_RemapRange,id:150,x:31845,y:32625,varname:node_150,prsc:2,frmn:0,frmx:1,tomn:0,tomx:1|IN-3654-U;n:type:ShaderForge.SFN_OneMinus,id:4035,x:32028,y:32625,varname:node_4035,prsc:2|IN-150-OUT;n:type:ShaderForge.SFN_Slider,id:1967,x:32599,y:33275,ptovrint:False,ptlb:Aplitude,ptin:_Aplitude,varname:node_1967,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:2.358904,max:50;n:type:ShaderForge.SFN_ConstantClamp,id:430,x:33600,y:32976,varname:node_430,prsc:2,min:0,max:1|IN-2639-OUT;n:type:ShaderForge.SFN_Lerp,id:6945,x:33484,y:32603,varname:node_6945,prsc:2|A-7241-RGB,B-8767-RGB,T-430-OUT;n:type:ShaderForge.SFN_Color,id:8767,x:32489,y:32661,ptovrint:False,ptlb:Color2,ptin:_Color2,varname:node_8767,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:1,c3:1,c4:1;n:type:ShaderForge.SFN_Tau,id:5395,x:32914,y:33196,varname:node_5395,prsc:2;n:type:ShaderForge.SFN_Add,id:4258,x:32380,y:33134,varname:node_4258,prsc:2|A-2262-T,B-903-OUT;n:type:ShaderForge.SFN_ValueProperty,id:903,x:32203,y:33465,ptovrint:False,ptlb:Speed MUL,ptin:_SpeedMUL,varname:node_903,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_Multiply,id:3776,x:32412,y:33272,varname:node_3776,prsc:2|A-4258-OUT,B-903-OUT;n:type:ShaderForge.SFN_RemapRange,id:8597,x:33689,y:32603,varname:node_8597,prsc:2,frmn:0,frmx:1,tomn:-1,tomx:1|IN-6945-OUT;n:type:ShaderForge.SFN_Add,id:1603,x:33827,y:32976,varname:node_1603,prsc:2|A-430-OUT,B-6875-OUT;n:type:ShaderForge.SFN_Slider,id:1724,x:33192,y:33203,ptovrint:False,ptlb:Width,ptin:_Width,varname:node_1724,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1,max:1;n:type:ShaderForge.SFN_RemapRange,id:6875,x:33600,y:33172,varname:node_6875,prsc:2,frmn:0,frmx:1,tomn:-0.45,tomx:0.45|IN-1724-OUT;n:type:ShaderForge.SFN_RemapRange,id:7695,x:31843,y:33004,varname:node_7695,prsc:2,frmn:0,frmx:1,tomn:0,tomx:1|IN-3654-V;n:type:ShaderForge.SFN_OneMinus,id:9656,x:32022,y:33004,varname:node_9656,prsc:2|IN-7695-OUT;n:type:ShaderForge.SFN_Multiply,id:9296,x:32198,y:33004,varname:node_9296,prsc:2|A-9656-OUT,B-5831-OUT;n:type:ShaderForge.SFN_ValueProperty,id:5831,x:31986,y:33274,ptovrint:False,ptlb:Horizontal MUL ,ptin:_HorizontalMUL,varname:node_5831,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0;n:type:ShaderForge.SFN_Multiply,id:5005,x:32239,y:32625,varname:node_5005,prsc:2|A-4035-OUT,B-3363-OUT;n:type:ShaderForge.SFN_ValueProperty,id:3363,x:31994,y:32862,ptovrint:False,ptlb:Vertical MUL,ptin:_VerticalMUL,varname:_HorizontalMUL_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;proporder:7241-1967-8767-903-1724-5831-3363;pass:END;sub:END;*/

Shader "Custom/Line" {
    Properties {
        _Color ("Color", Color) = (0.03676468,0.03676468,0.03676468,1)
        _Aplitude ("Aplitude", Range(0, 50)) = 2.358904
        _Color2 ("Color2", Color) = (1,1,1,1)
        _SpeedMUL ("Speed MUL", Float ) = 1
        _Width ("Width", Range(0, 1)) = 1
        _HorizontalMUL ("Horizontal MUL ", Float ) = 0
        _VerticalMUL ("Vertical MUL", Float ) = 1
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "Queue"="AlphaTest"
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
            uniform float4 _TimeEditor;
            uniform float4 _Color;
            uniform float _Aplitude;
            uniform float4 _Color2;
            uniform float _SpeedMUL;
            uniform float _Width;
            uniform float _HorizontalMUL;
            uniform float _VerticalMUL;
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
                float4 node_2262 = _Time + _TimeEditor;
                float node_430 = clamp((sin(((((1.0 - (i.uv0.r*1.0+0.0))*_VerticalMUL)+((node_2262.g+_SpeedMUL)*_SpeedMUL)+((1.0 - (i.uv0.g*1.0+0.0))*_HorizontalMUL))*_Aplitude*6.28318530718))*4.0+-3.0),0,1);
                clip((node_430+(_Width*0.9+-0.45)) - 0.5);
////// Lighting:
////// Emissive:
                float3 emissive = (lerp(_Color.rgb,_Color2.rgb,node_430)*2.0+-1.0);
                float3 finalColor = emissive;
                return fixed4(finalColor,1);
            }
            ENDCG
        }
        Pass {
            Name "ShadowCaster"
            Tags {
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
            uniform float4 _TimeEditor;
            uniform float _Aplitude;
            uniform float _SpeedMUL;
            uniform float _Width;
            uniform float _HorizontalMUL;
            uniform float _VerticalMUL;
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
                float4 node_2262 = _Time + _TimeEditor;
                float node_430 = clamp((sin(((((1.0 - (i.uv0.r*1.0+0.0))*_VerticalMUL)+((node_2262.g+_SpeedMUL)*_SpeedMUL)+((1.0 - (i.uv0.g*1.0+0.0))*_HorizontalMUL))*_Aplitude*6.28318530718))*4.0+-3.0),0,1);
                clip((node_430+(_Width*0.9+-0.45)) - 0.5);
                SHADOW_CASTER_FRAGMENT(i)
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
