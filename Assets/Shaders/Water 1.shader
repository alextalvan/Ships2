// Shader created with Shader Forge v1.25 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.25;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:True,hqlp:True,rprd:True,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:3,bdst:7,dpts:2,wrdp:True,dith:0,rfrpo:False,rfrpn:Refraction,coma:15,ufog:True,aust:False,igpj:True,qofs:0,qpre:1,rntp:0,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False;n:type:ShaderForge.SFN_Final,id:3138,x:37490,y:32418,varname:node_3138,prsc:2|emission-7093-OUT,custl-916-OUT,alpha-2190-OUT,refract-6367-OUT;n:type:ShaderForge.SFN_Color,id:7241,x:33930,y:32243,ptovrint:False,ptlb:Color,ptin:_Color,varname:node_7241,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0,c2:0,c3:0,c4:1;n:type:ShaderForge.SFN_TexCoord,id:2017,x:32176,y:32942,varname:node_2017,prsc:2,uv:0;n:type:ShaderForge.SFN_Multiply,id:3932,x:32372,y:33007,varname:node_3932,prsc:2|A-2017-UVOUT,B-4012-OUT;n:type:ShaderForge.SFN_ValueProperty,id:4012,x:32176,y:33129,ptovrint:False,ptlb:Wave_amount,ptin:_Wave_amount,varname:node_4012,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_Panner,id:6901,x:32573,y:33082,varname:node_6901,prsc:2,spu:0.03,spv:0.05|UVIN-3932-OUT,DIST-3837-OUT;n:type:ShaderForge.SFN_Tex2d,id:8975,x:32771,y:33256,ptovrint:False,ptlb:AlphaWave2,ptin:_AlphaWave2,varname:node_8975,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:28c7aad1372ff114b90d330f8a2dd938,ntxv:0,isnm:False|UVIN-6901-UVOUT;n:type:ShaderForge.SFN_ValueProperty,id:4323,x:32159,y:33334,ptovrint:False,ptlb:Panner,ptin:_Panner,varname:node_4323,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:11.42;n:type:ShaderForge.SFN_Blend,id:4934,x:32962,y:33171,varname:node_4934,prsc:2,blmd:13,clmp:True|SRC-5285-RGB,DST-8975-RGB;n:type:ShaderForge.SFN_Tex2d,id:5285,x:32719,y:32916,ptovrint:False,ptlb:AlphaWave,ptin:_AlphaWave,varname:node_5285,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:cee2491bf9f104825a5ca3ffd00453aa,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Blend,id:6561,x:33180,y:33124,varname:node_6561,prsc:2,blmd:19,clmp:True|SRC-5285-RGB,DST-4934-OUT;n:type:ShaderForge.SFN_Lerp,id:777,x:33602,y:32841,varname:node_777,prsc:2|A-5285-RGB,B-6561-OUT,T-2352-OUT;n:type:ShaderForge.SFN_Multiply,id:7551,x:34130,y:32758,varname:node_7551,prsc:2|A-815-RGB,B-5580-OUT,C-777-OUT,D-6561-OUT;n:type:ShaderForge.SFN_TexCoord,id:3580,x:32682,y:32655,varname:node_3580,prsc:2,uv:0;n:type:ShaderForge.SFN_Multiply,id:4814,x:32918,y:32741,varname:node_4814,prsc:2|A-3580-UVOUT,B-4260-OUT;n:type:ShaderForge.SFN_Panner,id:7106,x:33087,y:32698,varname:node_7106,prsc:2,spu:-0.04,spv:-0.08|UVIN-4814-OUT;n:type:ShaderForge.SFN_Vector2,id:4260,x:32682,y:32799,varname:node_4260,prsc:2,v1:10,v2:10;n:type:ShaderForge.SFN_Tex2d,id:815,x:33314,y:32666,ptovrint:False,ptlb:WaveTexture,ptin:_WaveTexture,varname:node_815,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:36dd0b22da8874ed38075789055ca664,ntxv:0,isnm:False|UVIN-7106-UVOUT;n:type:ShaderForge.SFN_ValueProperty,id:2352,x:33248,y:32951,ptovrint:False,ptlb:Lerp,ptin:_Lerp,varname:node_2352,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1.64;n:type:ShaderForge.SFN_Color,id:8272,x:33670,y:33091,ptovrint:False,ptlb:Color_lerp,ptin:_Color_lerp,varname:node_8272,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0,c2:1,c3:0.04705882,c4:1;n:type:ShaderForge.SFN_Lerp,id:5580,x:33878,y:33259,varname:node_5580,prsc:2|A-8272-RGB,B-6561-OUT,T-5285-RGB;n:type:ShaderForge.SFN_Power,id:2265,x:34417,y:32602,varname:node_2265,prsc:2|VAL-7551-OUT,EXP-6910-OUT;n:type:ShaderForge.SFN_ValueProperty,id:6910,x:34209,y:32907,ptovrint:False,ptlb:Power,ptin:_Power,varname:node_6910,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0.85;n:type:ShaderForge.SFN_Add,id:7093,x:34697,y:32587,cmnt:Texture,varname:node_7093,prsc:2|A-2901-OUT,B-2265-OUT;n:type:ShaderForge.SFN_Multiply,id:2901,x:34457,y:32354,varname:node_2901,prsc:2|A-1712-RGB,B-5054-OUT;n:type:ShaderForge.SFN_Lerp,id:5054,x:34214,y:32289,varname:node_5054,prsc:2|A-3034-RGB,B-7241-RGB,T-8601-OUT;n:type:ShaderForge.SFN_ComponentMask,id:8601,x:34059,y:32462,varname:node_8601,prsc:2,cc1:0,cc2:-1,cc3:-1,cc4:-1|IN-4080-UVOUT;n:type:ShaderForge.SFN_TexCoord,id:426,x:33676,y:32414,varname:node_426,prsc:2,uv:0;n:type:ShaderForge.SFN_ValueProperty,id:4276,x:33676,y:32583,ptovrint:False,ptlb:Angle,ptin:_Angle,varname:node_4276,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:15;n:type:ShaderForge.SFN_Rotator,id:4080,x:33871,y:32462,varname:node_4080,prsc:2|UVIN-426-UVOUT,ANG-4276-OUT;n:type:ShaderForge.SFN_Color,id:1712,x:34214,y:32118,ptovrint:False,ptlb:Deep_Color,ptin:_Deep_Color,varname:node_1712,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0,c2:0.75,c3:0.5952381,c4:1;n:type:ShaderForge.SFN_Slider,id:2190,x:36899,y:32842,ptovrint:False,ptlb:Transparancy,ptin:_Transparancy,varname:node_2190,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.2547994,max:1;n:type:ShaderForge.SFN_Dot,id:3082,x:34711,y:32067,varname:node_3082,prsc:2,dt:1|A-1305-OUT,B-5819-OUT;n:type:ShaderForge.SFN_LightVector,id:1305,x:34472,y:32011,varname:node_1305,prsc:2;n:type:ShaderForge.SFN_NormalVector,id:5819,x:34490,y:32173,prsc:2,pt:False;n:type:ShaderForge.SFN_Vector1,id:6054,x:35234,y:32017,varname:node_6054,prsc:2,v1:0.6;n:type:ShaderForge.SFN_Add,id:1241,x:35394,y:32093,varname:node_1241,prsc:2|A-6054-OUT,B-6709-OUT;n:type:ShaderForge.SFN_Multiply,id:1230,x:35595,y:32093,varname:node_1230,prsc:2|A-1241-OUT,B-6054-OUT;n:type:ShaderForge.SFN_Multiply,id:6393,x:35812,y:32162,varname:node_6393,prsc:2|A-1230-OUT,B-1230-OUT,C-7093-OUT;n:type:ShaderForge.SFN_Multiply,id:6709,x:35025,y:32042,varname:node_6709,prsc:2|A-3082-OUT,B-7824-OUT;n:type:ShaderForge.SFN_LightAttenuation,id:7824,x:34846,y:32173,varname:node_7824,prsc:2;n:type:ShaderForge.SFN_Multiply,id:9475,x:36053,y:32261,varname:node_9475,prsc:2|A-6393-OUT,B-2418-RGB;n:type:ShaderForge.SFN_LightColor,id:2418,x:35812,y:32331,varname:node_2418,prsc:2;n:type:ShaderForge.SFN_Dot,id:9280,x:35216,y:32316,varname:node_9280,prsc:2,dt:1|A-1305-OUT,B-6980-OUT;n:type:ShaderForge.SFN_Power,id:2730,x:35441,y:32393,varname:node_2730,prsc:2|VAL-9280-OUT,EXP-4025-OUT;n:type:ShaderForge.SFN_Exp,id:4025,x:35267,y:32553,varname:node_4025,prsc:2,et:1|IN-2541-OUT;n:type:ShaderForge.SFN_Slider,id:2541,x:34888,y:32741,ptovrint:False,ptlb:RefelectionBlur,ptin:_RefelectionBlur,varname:node_2541,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0.1,cur:2.37,max:8;n:type:ShaderForge.SFN_Slider,id:1718,x:35432,y:32713,ptovrint:False,ptlb:Refelcetion_Int,ptin:_Refelcetion_Int,varname:node_1718,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.3006639,max:1;n:type:ShaderForge.SFN_Multiply,id:5208,x:35666,y:32447,varname:node_5208,prsc:2|A-2730-OUT,B-1718-OUT;n:type:ShaderForge.SFN_Add,id:916,x:36321,y:32445,varname:node_916,prsc:2|A-5208-OUT,B-5786-OUT,C-9475-OUT;n:type:ShaderForge.SFN_Transform,id:2409,x:36019,y:33142,varname:node_2409,prsc:2,tffrom:0,tfto:2|IN-730-OUT;n:type:ShaderForge.SFN_ComponentMask,id:7676,x:36344,y:33193,varname:node_7676,prsc:2,cc1:0,cc2:-1,cc3:-1,cc4:-1|IN-2409-XYZ;n:type:ShaderForge.SFN_ViewReflectionVector,id:6980,x:34654,y:32354,varname:node_6980,prsc:2;n:type:ShaderForge.SFN_Multiply,id:6367,x:37193,y:33099,varname:node_6367,prsc:2|A-9964-OUT,B-8325-OUT,C-980-UVOUT,D-4014-OUT;n:type:ShaderForge.SFN_Slider,id:8325,x:36453,y:33368,ptovrint:False,ptlb:Reflection,ptin:_Reflection,varname:node_8325,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:-10,cur:0.25,max:10;n:type:ShaderForge.SFN_Add,id:9964,x:36897,y:33111,varname:node_9964,prsc:2|A-3444-OUT,B-7676-OUT;n:type:ShaderForge.SFN_Tex2d,id:9794,x:36416,y:32681,ptovrint:False,ptlb:Normal for refl,ptin:_Normalforrefl,varname:node_9794,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:276a54cad51984ff89da8445c52523c7,ntxv:3,isnm:True;n:type:ShaderForge.SFN_Fresnel,id:9444,x:35446,y:32871,varname:node_9444,prsc:2|NRM-5819-OUT,EXP-4867-OUT;n:type:ShaderForge.SFN_Slider,id:5233,x:34749,y:33067,ptovrint:False,ptlb:Fresnel,ptin:_Fresnel,varname:node_5233,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:-5,cur:1.56,max:5;n:type:ShaderForge.SFN_Multiply,id:5786,x:35979,y:32867,varname:node_5786,prsc:2|A-7093-OUT,B-6033-OUT;n:type:ShaderForge.SFN_Multiply,id:6033,x:35622,y:32996,varname:node_6033,prsc:2|A-9444-OUT,B-3701-OUT,C-9555-OUT;n:type:ShaderForge.SFN_Slider,id:3701,x:35135,y:33183,ptovrint:False,ptlb:F_Intensity,ptin:_F_Intensity,varname:node_3701,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1.758879,max:5;n:type:ShaderForge.SFN_Color,id:4569,x:35782,y:32713,ptovrint:False,ptlb:Fresnel Color,ptin:_FresnelColor,varname:node_4569,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0,c2:1,c3:1,c4:1;n:type:ShaderForge.SFN_OneMinus,id:4867,x:35104,y:32990,varname:node_4867,prsc:2|IN-5233-OUT;n:type:ShaderForge.SFN_Rotator,id:980,x:36958,y:33306,varname:node_980,prsc:2|UVIN-9964-OUT,ANG-3499-OUT,SPD-7616-OUT;n:type:ShaderForge.SFN_Slider,id:3499,x:36376,y:33517,ptovrint:False,ptlb:Edge,ptin:_Edge,varname:node_3499,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:-10,cur:0.13,max:10;n:type:ShaderForge.SFN_Negate,id:730,x:35792,y:33128,varname:node_730,prsc:2|IN-5819-OUT;n:type:ShaderForge.SFN_ComponentMask,id:3444,x:36631,y:32785,varname:node_3444,prsc:2,cc1:0,cc2:1,cc3:-1,cc4:-1|IN-9794-RGB;n:type:ShaderForge.SFN_Color,id:3034,x:33945,y:32049,ptovrint:False,ptlb:Color2,ptin:_Color2,varname:node_3034,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0,c2:0,c3:0,c4:1;n:type:ShaderForge.SFN_Time,id:1276,x:32159,y:33434,varname:node_1276,prsc:2;n:type:ShaderForge.SFN_Add,id:3837,x:32442,y:33301,varname:node_3837,prsc:2|A-4323-OUT,B-1276-TSL,C-1276-TSL,D-1276-TSL,E-1276-TSL;n:type:ShaderForge.SFN_Sin,id:7616,x:36469,y:33679,varname:node_7616,prsc:2|IN-5730-TTR;n:type:ShaderForge.SFN_Time,id:5730,x:36206,y:33693,varname:node_5730,prsc:2;n:type:ShaderForge.SFN_Abs,id:6827,x:37021,y:33516,varname:node_6827,prsc:2|IN-4967-OUT;n:type:ShaderForge.SFN_Add,id:4014,x:37211,y:33421,varname:node_4014,prsc:2|A-6827-OUT,B-8325-OUT;n:type:ShaderForge.SFN_Multiply,id:4967,x:36851,y:33633,varname:node_4967,prsc:2|A-7616-OUT,B-589-OUT;n:type:ShaderForge.SFN_Slider,id:589,x:36538,y:33849,ptovrint:False,ptlb:Reflection_Move_amount,ptin:_Reflection_Move_amount,varname:node_589,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.2,max:1;n:type:ShaderForge.SFN_Sin,id:9327,x:35952,y:33551,varname:node_9327,prsc:2|IN-5730-T;n:type:ShaderForge.SFN_Abs,id:1392,x:36105,y:33494,varname:node_1392,prsc:2|IN-9327-OUT;n:type:ShaderForge.SFN_RemapRange,id:9555,x:35515,y:33319,varname:node_9555,prsc:2,frmn:0,frmx:1,tomn:0.7,tomx:1|IN-1392-OUT;proporder:7241-3034-4012-8975-4323-5285-815-2352-8272-1712-6910-4276-2190-8325-9794-3499-2541-1718-5233-3701-4569-589;pass:END;sub:END;*/

Shader "Custom/Water" {
    Properties {
        _Color ("Color", Color) = (0,0,0,1)
        _Color2 ("Color2", Color) = (0,0,0,1)
        _Wave_amount ("Wave_amount", Float ) = 1
        _AlphaWave2 ("AlphaWave2", 2D) = "white" {}
        _Panner ("Panner", Float ) = 11.42
        _AlphaWave ("AlphaWave", 2D) = "white" {}
        _WaveTexture ("WaveTexture", 2D) = "white" {}
        _Lerp ("Lerp", Float ) = 1.64
        _Color_lerp ("Color_lerp", Color) = (0,1,0.04705882,1)
        _Deep_Color ("Deep_Color", Color) = (0,0.75,0.5952381,1)
        _Power ("Power", Float ) = 0.85
        _Angle ("Angle", Float ) = 15
        _Transparancy ("Transparancy", Range(0, 1)) = 0.2547994
        _Reflection ("Reflection", Range(-10, 10)) = 0.25
        _Normalforrefl ("Normal for refl", 2D) = "bump" {}
        _Edge ("Edge", Range(-10, 10)) = 0.13
        _RefelectionBlur ("RefelectionBlur", Range(0.1, 8)) = 2.37
        _Refelcetion_Int ("Refelcetion_Int", Range(0, 1)) = 0.3006639
        _Fresnel ("Fresnel", Range(-5, 5)) = 1.56
        _F_Intensity ("F_Intensity", Range(0, 5)) = 1.758879
        _FresnelColor ("Fresnel Color", Color) = (0,1,1,1)
        _Reflection_Move_amount ("Reflection_Move_amount", Range(0, 1)) = 0.2
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
        }
        GrabPass{ "Refraction" }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend SrcAlpha OneMinusSrcAlpha
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #define _GLOSSYENV 1
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #pragma multi_compile_fwdbase
            #pragma multi_compile_fog
            #pragma exclude_renderers gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform sampler2D Refraction;
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
            uniform float _Transparancy;
            uniform float _RefelectionBlur;
            uniform float _Refelcetion_Int;
            uniform float _Reflection;
            uniform sampler2D _Normalforrefl; uniform float4 _Normalforrefl_ST;
            uniform float _Fresnel;
            uniform float _F_Intensity;
            uniform float _Edge;
            uniform float4 _Color2;
            uniform float _Reflection_Move_amount;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                float3 tangentDir : TEXCOORD3;
                float3 bitangentDir : TEXCOORD4;
                float4 screenPos : TEXCOORD5;
                UNITY_FOG_COORDS(6)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.tangentDir = normalize( mul( _Object2World, float4( v.tangent.xyz, 0.0 ) ).xyz );
                o.bitangentDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
                o.posWorld = mul(_Object2World, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                o.screenPos = o.pos;
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                #if UNITY_UV_STARTS_AT_TOP
                    float grabSign = -_ProjectionParams.x;
                #else
                    float grabSign = _ProjectionParams.x;
                #endif
                i.normalDir = normalize(i.normalDir);
                i.screenPos = float4( i.screenPos.xy / i.screenPos.w, 0, 0 );
                i.screenPos.y *= _ProjectionParams.x;
                float3x3 tangentTransform = float3x3( i.tangentDir, i.bitangentDir, i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                float3 viewReflectDirection = reflect( -viewDirection, normalDirection );
                float3 _Normalforrefl_var = UnpackNormal(tex2D(_Normalforrefl,TRANSFORM_TEX(i.uv0, _Normalforrefl)));
                float2 node_9964 = (_Normalforrefl_var.rgb.rg+mul( tangentTransform, (-1*i.normalDir) ).xyz.rgb.r);
                float4 node_5730 = _Time + _TimeEditor;
                float node_7616 = sin(node_5730.a);
                float node_980_ang = _Edge;
                float node_980_spd = node_7616;
                float node_980_cos = cos(node_980_spd*node_980_ang);
                float node_980_sin = sin(node_980_spd*node_980_ang);
                float2 node_980_piv = float2(0.5,0.5);
                float2 node_980 = (mul(node_9964-node_980_piv,float2x2( node_980_cos, -node_980_sin, node_980_sin, node_980_cos))+node_980_piv);
                float2 sceneUVs = float2(1,grabSign)*i.screenPos.xy*0.5+0.5 + (node_9964*_Reflection*node_980*(abs((node_7616*_Reflection_Move_amount))+_Reflection));
                float4 sceneColor = tex2D(Refraction, sceneUVs);
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
                float3 lightColor = _LightColor0.rgb;
////// Lighting:
                float attenuation = 1;
////// Emissive:
                float node_4080_ang = _Angle;
                float node_4080_spd = 1.0;
                float node_4080_cos = cos(node_4080_spd*node_4080_ang);
                float node_4080_sin = sin(node_4080_spd*node_4080_ang);
                float2 node_4080_piv = float2(0.5,0.5);
                float2 node_4080 = (mul(i.uv0-node_4080_piv,float2x2( node_4080_cos, -node_4080_sin, node_4080_sin, node_4080_cos))+node_4080_piv);
                float4 node_9186 = _Time + _TimeEditor;
                float2 node_7106 = ((i.uv0*float2(10,10))+node_9186.g*float2(-0.04,-0.08));
                float4 _WaveTexture_var = tex2D(_WaveTexture,TRANSFORM_TEX(node_7106, _WaveTexture));
                float4 _AlphaWave_var = tex2D(_AlphaWave,TRANSFORM_TEX(i.uv0, _AlphaWave));
                float4 node_1276 = _Time + _TimeEditor;
                float2 node_6901 = ((i.uv0*_Wave_amount)+(_Panner+node_1276.r+node_1276.r+node_1276.r+node_1276.r)*float2(0.03,0.05));
                float4 _AlphaWave2_var = tex2D(_AlphaWave2,TRANSFORM_TEX(node_6901, _AlphaWave2));
                float3 node_6561 = saturate((saturate(( _AlphaWave_var.rgb > 0.5 ? (_AlphaWave2_var.rgb/((1.0-_AlphaWave_var.rgb)*2.0)) : (1.0-(((1.0-_AlphaWave2_var.rgb)*0.5)/_AlphaWave_var.rgb))))-_AlphaWave_var.rgb));
                float3 node_7093 = ((_Deep_Color.rgb*lerp(_Color2.rgb,_Color.rgb,node_4080.r))+pow((_WaveTexture_var.rgb*lerp(_Color_lerp.rgb,node_6561,_AlphaWave_var.rgb)*lerp(_AlphaWave_var.rgb,node_6561,_Lerp)*node_6561),_Power)); // Texture
                float3 emissive = node_7093;
                float node_1392 = abs(sin(node_5730.g));
                float node_6033 = (pow(1.0-max(0,dot(i.normalDir, viewDirection)),(1.0 - _Fresnel))*_F_Intensity*(node_1392*0.3+0.7));
                float node_6054 = 0.6;
                float node_1230 = ((node_6054+(max(0,dot(lightDirection,i.normalDir))*attenuation))*node_6054);
                float3 finalColor = emissive + ((pow(max(0,dot(lightDirection,viewReflectDirection)),exp2(_RefelectionBlur))*_Refelcetion_Int)+(node_7093*node_6033)+((node_1230*node_1230*node_7093)*_LightColor0.rgb));
                fixed4 finalRGBA = fixed4(lerp(sceneColor.rgb, finalColor,_Transparancy),1);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
        Pass {
            Name "FORWARD_DELTA"
            Tags {
                "LightMode"="ForwardAdd"
            }
            Blend One One
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDADD
            #define _GLOSSYENV 1
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #include "Lighting.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #pragma multi_compile_fwdadd
            #pragma multi_compile_fog
            #pragma exclude_renderers gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform sampler2D Refraction;
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
            uniform float _Transparancy;
            uniform float _RefelectionBlur;
            uniform float _Refelcetion_Int;
            uniform float _Reflection;
            uniform sampler2D _Normalforrefl; uniform float4 _Normalforrefl_ST;
            uniform float _Fresnel;
            uniform float _F_Intensity;
            uniform float _Edge;
            uniform float4 _Color2;
            uniform float _Reflection_Move_amount;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                float3 tangentDir : TEXCOORD3;
                float3 bitangentDir : TEXCOORD4;
                float4 screenPos : TEXCOORD5;
                LIGHTING_COORDS(6,7)
                UNITY_FOG_COORDS(8)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.tangentDir = normalize( mul( _Object2World, float4( v.tangent.xyz, 0.0 ) ).xyz );
                o.bitangentDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
                o.posWorld = mul(_Object2World, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                o.screenPos = o.pos;
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                #if UNITY_UV_STARTS_AT_TOP
                    float grabSign = -_ProjectionParams.x;
                #else
                    float grabSign = _ProjectionParams.x;
                #endif
                i.normalDir = normalize(i.normalDir);
                i.screenPos = float4( i.screenPos.xy / i.screenPos.w, 0, 0 );
                i.screenPos.y *= _ProjectionParams.x;
                float3x3 tangentTransform = float3x3( i.tangentDir, i.bitangentDir, i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                float3 viewReflectDirection = reflect( -viewDirection, normalDirection );
                float3 _Normalforrefl_var = UnpackNormal(tex2D(_Normalforrefl,TRANSFORM_TEX(i.uv0, _Normalforrefl)));
                float2 node_9964 = (_Normalforrefl_var.rgb.rg+mul( tangentTransform, (-1*i.normalDir) ).xyz.rgb.r);
                float4 node_5730 = _Time + _TimeEditor;
                float node_7616 = sin(node_5730.a);
                float node_980_ang = _Edge;
                float node_980_spd = node_7616;
                float node_980_cos = cos(node_980_spd*node_980_ang);
                float node_980_sin = sin(node_980_spd*node_980_ang);
                float2 node_980_piv = float2(0.5,0.5);
                float2 node_980 = (mul(node_9964-node_980_piv,float2x2( node_980_cos, -node_980_sin, node_980_sin, node_980_cos))+node_980_piv);
                float2 sceneUVs = float2(1,grabSign)*i.screenPos.xy*0.5+0.5 + (node_9964*_Reflection*node_980*(abs((node_7616*_Reflection_Move_amount))+_Reflection));
                float4 sceneColor = tex2D(Refraction, sceneUVs);
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
                float3 lightColor = _LightColor0.rgb;
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float node_4080_ang = _Angle;
                float node_4080_spd = 1.0;
                float node_4080_cos = cos(node_4080_spd*node_4080_ang);
                float node_4080_sin = sin(node_4080_spd*node_4080_ang);
                float2 node_4080_piv = float2(0.5,0.5);
                float2 node_4080 = (mul(i.uv0-node_4080_piv,float2x2( node_4080_cos, -node_4080_sin, node_4080_sin, node_4080_cos))+node_4080_piv);
                float4 node_2228 = _Time + _TimeEditor;
                float2 node_7106 = ((i.uv0*float2(10,10))+node_2228.g*float2(-0.04,-0.08));
                float4 _WaveTexture_var = tex2D(_WaveTexture,TRANSFORM_TEX(node_7106, _WaveTexture));
                float4 _AlphaWave_var = tex2D(_AlphaWave,TRANSFORM_TEX(i.uv0, _AlphaWave));
                float4 node_1276 = _Time + _TimeEditor;
                float2 node_6901 = ((i.uv0*_Wave_amount)+(_Panner+node_1276.r+node_1276.r+node_1276.r+node_1276.r)*float2(0.03,0.05));
                float4 _AlphaWave2_var = tex2D(_AlphaWave2,TRANSFORM_TEX(node_6901, _AlphaWave2));
                float3 node_6561 = saturate((saturate(( _AlphaWave_var.rgb > 0.5 ? (_AlphaWave2_var.rgb/((1.0-_AlphaWave_var.rgb)*2.0)) : (1.0-(((1.0-_AlphaWave2_var.rgb)*0.5)/_AlphaWave_var.rgb))))-_AlphaWave_var.rgb));
                float3 node_7093 = ((_Deep_Color.rgb*lerp(_Color2.rgb,_Color.rgb,node_4080.r))+pow((_WaveTexture_var.rgb*lerp(_Color_lerp.rgb,node_6561,_AlphaWave_var.rgb)*lerp(_AlphaWave_var.rgb,node_6561,_Lerp)*node_6561),_Power)); // Texture
                float node_1392 = abs(sin(node_5730.g));
                float node_6033 = (pow(1.0-max(0,dot(i.normalDir, viewDirection)),(1.0 - _Fresnel))*_F_Intensity*(node_1392*0.3+0.7));
                float node_6054 = 0.6;
                float node_1230 = ((node_6054+(max(0,dot(lightDirection,i.normalDir))*attenuation))*node_6054);
                float3 finalColor = ((pow(max(0,dot(lightDirection,viewReflectDirection)),exp2(_RefelectionBlur))*_Refelcetion_Int)+(node_7093*node_6033)+((node_1230*node_1230*node_7093)*_LightColor0.rgb));
                fixed4 finalRGBA = fixed4(finalColor * _Transparancy,0);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
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
                float4 node_9749 = _Time + _TimeEditor;
                float2 node_7106 = ((i.uv0*float2(10,10))+node_9749.g*float2(-0.04,-0.08));
                float4 _WaveTexture_var = tex2D(_WaveTexture,TRANSFORM_TEX(node_7106, _WaveTexture));
                float4 _AlphaWave_var = tex2D(_AlphaWave,TRANSFORM_TEX(i.uv0, _AlphaWave));
                float4 node_1276 = _Time + _TimeEditor;
                float2 node_6901 = ((i.uv0*_Wave_amount)+(_Panner+node_1276.r+node_1276.r+node_1276.r+node_1276.r)*float2(0.03,0.05));
                float4 _AlphaWave2_var = tex2D(_AlphaWave2,TRANSFORM_TEX(node_6901, _AlphaWave2));
                float3 node_6561 = saturate((saturate(( _AlphaWave_var.rgb > 0.5 ? (_AlphaWave2_var.rgb/((1.0-_AlphaWave_var.rgb)*2.0)) : (1.0-(((1.0-_AlphaWave2_var.rgb)*0.5)/_AlphaWave_var.rgb))))-_AlphaWave_var.rgb));
                float3 node_7093 = ((_Deep_Color.rgb*lerp(_Color2.rgb,_Color.rgb,node_4080.r))+pow((_WaveTexture_var.rgb*lerp(_Color_lerp.rgb,node_6561,_AlphaWave_var.rgb)*lerp(_AlphaWave_var.rgb,node_6561,_Lerp)*node_6561),_Power)); // Texture
                o.Emission = node_7093;
                
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
