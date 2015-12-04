// Shader created with Shader Forge v1.25 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.25;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:True,hqlp:True,rprd:True,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:3,bdst:7,dpts:2,wrdp:False,dith:0,rfrpo:False,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False;n:type:ShaderForge.SFN_Final,id:3138,x:34733,y:32874,varname:node_3138,prsc:2|emission-7093-OUT,alpha-2070-OUT,clip-8975-R;n:type:ShaderForge.SFN_Color,id:7241,x:33930,y:32243,ptovrint:False,ptlb:Color,ptin:_Color,varname:node_7241,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:1,c3:1,c4:1;n:type:ShaderForge.SFN_TexCoord,id:2017,x:32176,y:32942,varname:node_2017,prsc:2,uv:0;n:type:ShaderForge.SFN_Multiply,id:3932,x:32372,y:33007,varname:node_3932,prsc:2|A-2017-UVOUT,B-4012-OUT;n:type:ShaderForge.SFN_ValueProperty,id:4012,x:32176,y:33129,ptovrint:False,ptlb:Wave_amount,ptin:_Wave_amount,varname:node_4012,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:2;n:type:ShaderForge.SFN_Panner,id:6901,x:32573,y:33082,varname:node_6901,prsc:2,spu:0.03,spv:0.05|UVIN-3932-OUT,DIST-3837-OUT;n:type:ShaderForge.SFN_Tex2d,id:8975,x:32745,y:33261,ptovrint:False,ptlb:AlphaWave2,ptin:_AlphaWave2,varname:node_8975,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:36dd0b22da8874ed38075789055ca664,ntxv:0,isnm:False|UVIN-6901-UVOUT;n:type:ShaderForge.SFN_ValueProperty,id:4323,x:32159,y:33334,ptovrint:False,ptlb:Panner,ptin:_Panner,varname:node_4323,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:11;n:type:ShaderForge.SFN_Blend,id:4934,x:32962,y:33171,varname:node_4934,prsc:2,blmd:10,clmp:True|SRC-5285-RGB,DST-8975-RGB;n:type:ShaderForge.SFN_Tex2d,id:5285,x:32719,y:32916,ptovrint:False,ptlb:AlphaWave,ptin:_AlphaWave,varname:node_5285,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:36dd0b22da8874ed38075789055ca664,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Blend,id:6561,x:33180,y:33124,varname:node_6561,prsc:2,blmd:10,clmp:True|SRC-5285-RGB,DST-4934-OUT;n:type:ShaderForge.SFN_Lerp,id:777,x:33602,y:32841,varname:node_777,prsc:2|A-5285-RGB,B-6561-OUT,T-2352-OUT;n:type:ShaderForge.SFN_Multiply,id:7551,x:34130,y:32758,varname:node_7551,prsc:2|A-815-RGB,B-5580-OUT,C-777-OUT,D-6561-OUT;n:type:ShaderForge.SFN_TexCoord,id:3580,x:32682,y:32655,varname:node_3580,prsc:2,uv:0;n:type:ShaderForge.SFN_Multiply,id:4814,x:32918,y:32741,varname:node_4814,prsc:2|A-3580-UVOUT,B-4260-OUT;n:type:ShaderForge.SFN_Panner,id:7106,x:33087,y:32698,varname:node_7106,prsc:2,spu:-0.04,spv:-0.08|UVIN-4814-OUT;n:type:ShaderForge.SFN_Vector2,id:4260,x:32682,y:32799,varname:node_4260,prsc:2,v1:10,v2:10;n:type:ShaderForge.SFN_Tex2d,id:815,x:33314,y:32666,ptovrint:False,ptlb:WaveTexture,ptin:_WaveTexture,varname:node_815,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:36dd0b22da8874ed38075789055ca664,ntxv:0,isnm:False|UVIN-7106-UVOUT;n:type:ShaderForge.SFN_ValueProperty,id:2352,x:33248,y:32951,ptovrint:False,ptlb:Lerp,ptin:_Lerp,varname:node_2352,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0.5;n:type:ShaderForge.SFN_Color,id:8272,x:33711,y:33017,ptovrint:False,ptlb:Color_lerp,ptin:_Color_lerp,varname:node_8272,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:0.9558824,c3:0.9558824,c4:1;n:type:ShaderForge.SFN_Lerp,id:5580,x:33924,y:33173,varname:node_5580,prsc:2|A-8272-RGB,B-6561-OUT,T-5285-RGB;n:type:ShaderForge.SFN_Power,id:2265,x:34396,y:32608,varname:node_2265,prsc:2|VAL-7551-OUT,EXP-6910-OUT;n:type:ShaderForge.SFN_ValueProperty,id:6910,x:34180,y:32923,ptovrint:False,ptlb:Power,ptin:_Power,varname:node_6910,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0.15;n:type:ShaderForge.SFN_Add,id:7093,x:34697,y:32587,cmnt:Texture,varname:node_7093,prsc:2|A-2901-OUT,B-2265-OUT;n:type:ShaderForge.SFN_Multiply,id:2901,x:34457,y:32354,varname:node_2901,prsc:2|A-1712-RGB,B-5054-OUT;n:type:ShaderForge.SFN_Lerp,id:5054,x:34214,y:32289,varname:node_5054,prsc:2|A-3034-RGB,B-7241-RGB,T-8601-OUT;n:type:ShaderForge.SFN_ComponentMask,id:8601,x:34059,y:32462,varname:node_8601,prsc:2,cc1:0,cc2:-1,cc3:-1,cc4:-1|IN-4080-UVOUT;n:type:ShaderForge.SFN_TexCoord,id:426,x:33676,y:32414,varname:node_426,prsc:2,uv:0;n:type:ShaderForge.SFN_ValueProperty,id:4276,x:33676,y:32583,ptovrint:False,ptlb:Angle,ptin:_Angle,varname:node_4276,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:15;n:type:ShaderForge.SFN_Rotator,id:4080,x:33871,y:32462,varname:node_4080,prsc:2|UVIN-426-UVOUT,ANG-4276-OUT;n:type:ShaderForge.SFN_Color,id:1712,x:34214,y:32118,ptovrint:False,ptlb:Deep_Color,ptin:_Deep_Color,varname:node_1712,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0,c2:0,c3:0,c4:1;n:type:ShaderForge.SFN_Color,id:3034,x:33949,y:32055,ptovrint:False,ptlb:Color2,ptin:_Color2,varname:node_3034,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:1,c3:1,c4:1;n:type:ShaderForge.SFN_Time,id:1276,x:32075,y:33434,varname:node_1276,prsc:2;n:type:ShaderForge.SFN_Add,id:3837,x:32442,y:33301,varname:node_3837,prsc:2|A-4323-OUT,B-1276-T,C-1276-TTR,D-1276-TTR;n:type:ShaderForge.SFN_Slider,id:2070,x:34213,y:33019,ptovrint:False,ptlb:Transparency,ptin:_Transparency,varname:node_2070,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.5582745,max:1;proporder:7241-3034-4012-8975-4323-5285-815-2352-8272-1712-6910-4276-2070;pass:END;sub:END;*/

Shader "Custom/ShipTrail" {
    Properties {
        _Color ("Color", Color) = (1,1,1,1)
        _Color2 ("Color2", Color) = (1,1,1,1)
        _Wave_amount ("Wave_amount", Float ) = 2
        _AlphaWave2 ("AlphaWave2", 2D) = "white" {}
        _Panner ("Panner", Float ) = 11
        _AlphaWave ("AlphaWave", 2D) = "white" {}
        _WaveTexture ("WaveTexture", 2D) = "white" {}
        _Lerp ("Lerp", Float ) = 0.5
        _Color_lerp ("Color_lerp", Color) = (1,0.9558824,0.9558824,1)
        _Deep_Color ("Deep_Color", Color) = (0,0,0,1)
        _Power ("Power", Float ) = 0.15
        _Angle ("Angle", Float ) = 15
        _Transparency ("Transparency", Range(0, 1)) = 0.5582745
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #define _GLOSSYENV 1
            #include "UnityCG.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #pragma multi_compile_fwdbase
            #pragma multi_compile_fog
            #pragma exclude_renderers gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform float4 _TimeEditor;
            uniform float4 _Color;
            uniform float _Wave_amount;
            uniform sampler2D _AlphaWave2; uniform float4 _AlphaWave2_ST;
            uniform float _Panner;
            uniform sampler2D _AlphaWave; uniform float4 _AlphaWave_ST;
            uniform sampler2D _WaveTexture; uniform float4 _WaveTexture_ST;
            uniform float _Lerp;
            uniform float4 _Color_lerp;
            uniform float _Power;
            uniform float _Angle;
            uniform float4 _Deep_Color;
            uniform float4 _Color2;
            uniform float _Transparency;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                UNITY_FOG_COORDS(3)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(_Object2World, v.vertex);
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                float3 viewReflectDirection = reflect( -viewDirection, normalDirection );
                float4 node_1276 = _Time + _TimeEditor;
                float2 node_6901 = ((i.uv0*_Wave_amount)+(_Panner+node_1276.g+node_1276.a+node_1276.a)*float2(0.03,0.05));
                float4 _AlphaWave2_var = tex2D(_AlphaWave2,TRANSFORM_TEX(node_6901, _AlphaWave2));
                clip(_AlphaWave2_var.r - 0.5);
////// Lighting:
////// Emissive:
                float node_4080_ang = _Angle;
                float node_4080_spd = 1.0;
                float node_4080_cos = cos(node_4080_spd*node_4080_ang);
                float node_4080_sin = sin(node_4080_spd*node_4080_ang);
                float2 node_4080_piv = float2(0.5,0.5);
                float2 node_4080 = (mul(i.uv0-node_4080_piv,float2x2( node_4080_cos, -node_4080_sin, node_4080_sin, node_4080_cos))+node_4080_piv);
                float4 node_8279 = _Time + _TimeEditor;
                float2 node_7106 = ((i.uv0*float2(10,10))+node_8279.g*float2(-0.04,-0.08));
                float4 _WaveTexture_var = tex2D(_WaveTexture,TRANSFORM_TEX(node_7106, _WaveTexture));
                float4 _AlphaWave_var = tex2D(_AlphaWave,TRANSFORM_TEX(i.uv0, _AlphaWave));
                float3 node_6561 = saturate(( saturate(( _AlphaWave2_var.rgb > 0.5 ? (1.0-(1.0-2.0*(_AlphaWave2_var.rgb-0.5))*(1.0-_AlphaWave_var.rgb)) : (2.0*_AlphaWave2_var.rgb*_AlphaWave_var.rgb) )) > 0.5 ? (1.0-(1.0-2.0*(saturate(( _AlphaWave2_var.rgb > 0.5 ? (1.0-(1.0-2.0*(_AlphaWave2_var.rgb-0.5))*(1.0-_AlphaWave_var.rgb)) : (2.0*_AlphaWave2_var.rgb*_AlphaWave_var.rgb) ))-0.5))*(1.0-_AlphaWave_var.rgb)) : (2.0*saturate(( _AlphaWave2_var.rgb > 0.5 ? (1.0-(1.0-2.0*(_AlphaWave2_var.rgb-0.5))*(1.0-_AlphaWave_var.rgb)) : (2.0*_AlphaWave2_var.rgb*_AlphaWave_var.rgb) ))*_AlphaWave_var.rgb) ));
                float3 emissive = ((_Deep_Color.rgb*lerp(_Color2.rgb,_Color.rgb,node_4080.r))+pow((_WaveTexture_var.rgb*lerp(_Color_lerp.rgb,node_6561,_AlphaWave_var.rgb)*lerp(_AlphaWave_var.rgb,node_6561,_Lerp)*node_6561),_Power));
                float3 finalColor = emissive;
                fixed4 finalRGBA = fixed4(finalColor,_Transparency);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
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
            #define _GLOSSYENV 1
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma multi_compile_fog
            #pragma exclude_renderers gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform float4 _TimeEditor;
            uniform float _Wave_amount;
            uniform sampler2D _AlphaWave2; uniform float4 _AlphaWave2_ST;
            uniform float _Panner;
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
                float4 node_1276 = _Time + _TimeEditor;
                float2 node_6901 = ((i.uv0*_Wave_amount)+(_Panner+node_1276.g+node_1276.a+node_1276.a)*float2(0.03,0.05));
                float4 _AlphaWave2_var = tex2D(_AlphaWave2,TRANSFORM_TEX(node_6901, _AlphaWave2));
                clip(_AlphaWave2_var.r - 0.5);
                SHADOW_CASTER_FRAGMENT(i)
            }
            ENDCG
        }
        Pass {
            Name "Meta"
            Tags {
                "LightMode"="Meta"
            }
            Cull Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_META 1
            #define _GLOSSYENV 1
            #include "UnityCG.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #include "UnityMetaPass.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma multi_compile_fog
            #pragma exclude_renderers gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform float4 _TimeEditor;
            uniform float4 _Color;
            uniform float _Wave_amount;
            uniform sampler2D _AlphaWave2; uniform float4 _AlphaWave2_ST;
            uniform float _Panner;
            uniform sampler2D _AlphaWave; uniform float4 _AlphaWave_ST;
            uniform sampler2D _WaveTexture; uniform float4 _WaveTexture_ST;
            uniform float _Lerp;
            uniform float4 _Color_lerp;
            uniform float _Power;
            uniform float _Angle;
            uniform float4 _Deep_Color;
            uniform float4 _Color2;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
                float2 texcoord1 : TEXCOORD1;
                float2 texcoord2 : TEXCOORD2;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.pos = UnityMetaVertexPosition(v.vertex, v.texcoord1.xy, v.texcoord2.xy, unity_LightmapST, unity_DynamicLightmapST );
                return o;
            }
            float4 frag(VertexOutput i) : SV_Target {
                UnityMetaInput o;
                UNITY_INITIALIZE_OUTPUT( UnityMetaInput, o );
                
                float node_4080_ang = _Angle;
                float node_4080_spd = 1.0;
                float node_4080_cos = cos(node_4080_spd*node_4080_ang);
                float node_4080_sin = sin(node_4080_spd*node_4080_ang);
                float2 node_4080_piv = float2(0.5,0.5);
                float2 node_4080 = (mul(i.uv0-node_4080_piv,float2x2( node_4080_cos, -node_4080_sin, node_4080_sin, node_4080_cos))+node_4080_piv);
                float4 node_2837 = _Time + _TimeEditor;
                float2 node_7106 = ((i.uv0*float2(10,10))+node_2837.g*float2(-0.04,-0.08));
                float4 _WaveTexture_var = tex2D(_WaveTexture,TRANSFORM_TEX(node_7106, _WaveTexture));
                float4 _AlphaWave_var = tex2D(_AlphaWave,TRANSFORM_TEX(i.uv0, _AlphaWave));
                float4 node_1276 = _Time + _TimeEditor;
                float2 node_6901 = ((i.uv0*_Wave_amount)+(_Panner+node_1276.g+node_1276.a+node_1276.a)*float2(0.03,0.05));
                float4 _AlphaWave2_var = tex2D(_AlphaWave2,TRANSFORM_TEX(node_6901, _AlphaWave2));
                float3 node_6561 = saturate(( saturate(( _AlphaWave2_var.rgb > 0.5 ? (1.0-(1.0-2.0*(_AlphaWave2_var.rgb-0.5))*(1.0-_AlphaWave_var.rgb)) : (2.0*_AlphaWave2_var.rgb*_AlphaWave_var.rgb) )) > 0.5 ? (1.0-(1.0-2.0*(saturate(( _AlphaWave2_var.rgb > 0.5 ? (1.0-(1.0-2.0*(_AlphaWave2_var.rgb-0.5))*(1.0-_AlphaWave_var.rgb)) : (2.0*_AlphaWave2_var.rgb*_AlphaWave_var.rgb) ))-0.5))*(1.0-_AlphaWave_var.rgb)) : (2.0*saturate(( _AlphaWave2_var.rgb > 0.5 ? (1.0-(1.0-2.0*(_AlphaWave2_var.rgb-0.5))*(1.0-_AlphaWave_var.rgb)) : (2.0*_AlphaWave2_var.rgb*_AlphaWave_var.rgb) ))*_AlphaWave_var.rgb) ));
                o.Emission = ((_Deep_Color.rgb*lerp(_Color2.rgb,_Color.rgb,node_4080.r))+pow((_WaveTexture_var.rgb*lerp(_Color_lerp.rgb,node_6561,_AlphaWave_var.rgb)*lerp(_AlphaWave_var.rgb,node_6561,_Lerp)*node_6561),_Power));
                
                float3 diffColor = float3(0,0,0);
                o.Albedo = diffColor;
                
                return UnityMetaFragment( o );
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
