// Shader created with Shader Forge v1.25 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.25;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:3,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:2,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:False,qofs:0,qpre:2,rntp:3,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False;n:type:ShaderForge.SFN_Final,id:4013,x:33969,y:32477,varname:node_4013,prsc:2|diff-7351-OUT,spec-6235-OUT,gloss-8347-OUT,normal-4233-RGB,emission-1382-OUT,clip-8078-OUT,voffset-3394-OUT;n:type:ShaderForge.SFN_Tex2d,id:6987,x:31549,y:33095,ptovrint:False,ptlb:Noise_for_dissolve,ptin:_Noise_for_dissolve,varname:node_6987,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:36dd0b22da8874ed38075789055ca664,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Slider,id:6963,x:30736,y:32769,ptovrint:False,ptlb:Dissolve amount,ptin:_Dissolveamount,varname:node_6963,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:1;n:type:ShaderForge.SFN_Add,id:3854,x:31810,y:32982,varname:node_3854,prsc:2|A-6875-OUT,B-6987-R;n:type:ShaderForge.SFN_RemapRange,id:6875,x:31568,y:32738,varname:node_6875,prsc:2,frmn:0,frmx:1,tomn:-1,tomx:0.7|IN-2714-OUT;n:type:ShaderForge.SFN_OneMinus,id:2714,x:31353,y:32738,varname:node_2714,prsc:2|IN-442-OUT;n:type:ShaderForge.SFN_RemapRange,id:2216,x:32013,y:32809,varname:node_2216,prsc:2,frmn:0,frmx:1,tomn:-4,tomx:4|IN-3854-OUT;n:type:ShaderForge.SFN_Append,id:1584,x:32729,y:32569,varname:node_1584,prsc:2|A-5181-OUT,B-7229-OUT;n:type:ShaderForge.SFN_Tex2dAsset,id:6582,x:32633,y:32759,ptovrint:False,ptlb:Ramp,ptin:_Ramp,varname:node_6582,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:0e675a08c985440e5869b3e14278db8d,ntxv:0,isnm:False;n:type:ShaderForge.SFN_OneMinus,id:8128,x:32231,y:32809,varname:node_8128,prsc:2|IN-2216-OUT;n:type:ShaderForge.SFN_Tex2d,id:3058,x:33393,y:31956,ptovrint:False,ptlb:Texture_main,ptin:_Texture_main,varname:node_3058,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:ee07ea8cb1a00c844b4048d37dcfe712,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:4233,x:33498,y:32334,ptovrint:False,ptlb:Normal map,ptin:_Normalmap,varname:node_4233,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:276a54cad51984ff89da8445c52523c7,ntxv:3,isnm:True;n:type:ShaderForge.SFN_Tex2d,id:3638,x:32966,y:32544,varname:node_3638,prsc:2,tex:0e675a08c985440e5869b3e14278db8d,ntxv:0,isnm:False|UVIN-1584-OUT,TEX-6582-TEX;n:type:ShaderForge.SFN_Clamp01,id:5181,x:32288,y:32582,varname:node_5181,prsc:2|IN-8128-OUT;n:type:ShaderForge.SFN_TexCoord,id:9665,x:31099,y:33296,varname:node_9665,prsc:2,uv:0;n:type:ShaderForge.SFN_ComponentMask,id:4194,x:31487,y:33323,varname:node_4194,prsc:2,cc1:0,cc2:-1,cc3:-1,cc4:-1|IN-9665-U;n:type:ShaderForge.SFN_Multiply,id:421,x:32260,y:33390,varname:node_421,prsc:2|A-3371-OUT,B-3684-OUT,C-5911-OUT,D-1746-OUT;n:type:ShaderForge.SFN_Sin,id:7605,x:32423,y:33390,varname:node_7605,prsc:2|IN-421-OUT;n:type:ShaderForge.SFN_Time,id:5117,x:31467,y:33621,varname:node_5117,prsc:2;n:type:ShaderForge.SFN_Add,id:3684,x:31932,y:33446,varname:node_3684,prsc:2|A-1988-OUT,B-5117-T,C-2342-OUT,D-5117-T;n:type:ShaderForge.SFN_ConstantClamp,id:5835,x:32607,y:33319,varname:node_5835,prsc:2,min:-1,max:1|IN-7605-OUT;n:type:ShaderForge.SFN_Tau,id:1746,x:32092,y:33483,varname:node_1746,prsc:2;n:type:ShaderForge.SFN_Multiply,id:4824,x:33138,y:33377,varname:node_4824,prsc:2|A-3974-OUT,B-9616-OUT;n:type:ShaderForge.SFN_Slider,id:114,x:31887,y:33230,ptovrint:False,ptlb:Frequency,ptin:_Frequency,varname:node_114,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.2,max:1;n:type:ShaderForge.SFN_RemapRange,id:3371,x:32260,y:33179,varname:node_3371,prsc:2,frmn:0,frmx:1,tomn:0,tomx:2|IN-114-OUT;n:type:ShaderForge.SFN_Slider,id:1588,x:32350,y:33744,ptovrint:False,ptlb:Amplitude,ptin:_Amplitude,varname:node_1588,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:2,max:3;n:type:ShaderForge.SFN_RemapRange,id:9616,x:32752,y:33696,varname:node_9616,prsc:2,frmn:0,frmx:1,tomn:0,tomx:8|IN-1588-OUT;n:type:ShaderForge.SFN_Slider,id:6235,x:33748,y:32018,ptovrint:False,ptlb:Metallic,ptin:_Metallic,varname:node_6235,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:1;n:type:ShaderForge.SFN_Slider,id:8347,x:33748,y:31917,ptovrint:False,ptlb:Gloss,ptin:_Gloss,varname:node_8347,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:1;n:type:ShaderForge.SFN_Color,id:484,x:33365,y:32149,ptovrint:False,ptlb:Main Color,ptin:_MainColor,varname:node_484,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:1,c3:1,c4:1;n:type:ShaderForge.SFN_Slider,id:5911,x:31802,y:33611,ptovrint:False,ptlb:Folds amount,ptin:_Foldsamount,varname:node_5911,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:2,max:3;n:type:ShaderForge.SFN_OneMinus,id:1988,x:31690,y:33357,varname:node_1988,prsc:2|IN-4194-OUT;n:type:ShaderForge.SFN_Add,id:6442,x:33688,y:32156,varname:node_6442,prsc:2|A-3058-RGB,B-484-RGB;n:type:ShaderForge.SFN_Multiply,id:3974,x:32942,y:33203,varname:node_3974,prsc:2|A-5835-OUT,B-6309-OUT;n:type:ShaderForge.SFN_Vector1,id:6309,x:32755,y:33333,varname:node_6309,prsc:2,v1:0.5;n:type:ShaderForge.SFN_ComponentMask,id:2342,x:31452,y:33479,varname:node_2342,prsc:2,cc1:0,cc2:-1,cc3:-1,cc4:-1|IN-9665-V;n:type:ShaderForge.SFN_VertexColor,id:6995,x:33166,y:33518,varname:node_6995,prsc:2;n:type:ShaderForge.SFN_Multiply,id:3394,x:33488,y:33338,varname:node_3394,prsc:2|A-4824-OUT,B-6995-R;n:type:ShaderForge.SFN_Tex2d,id:7647,x:31372,y:32091,ptovrint:False,ptlb:Mask_for_controls,ptin:_Mask_for_controls,varname:_Noise_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:aad29a4cc7dc94eb8ad469382532fa80,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Add,id:6563,x:31730,y:32237,varname:node_6563,prsc:2|A-9000-OUT,B-7647-R;n:type:ShaderForge.SFN_RemapRange,id:5295,x:32066,y:32332,varname:node_5295,prsc:2,frmn:0,frmx:1,tomn:-4,tomx:2|IN-6369-OUT;n:type:ShaderForge.SFN_Tex2dAsset,id:557,x:32538,y:31925,ptovrint:False,ptlb:Ramp 2,ptin:_Ramp2,varname:_Ramp_copy,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:0e675a08c985440e5869b3e14278db8d,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Append,id:4336,x:32538,y:32108,varname:node_4336,prsc:2|A-9939-OUT,B-9278-OUT;n:type:ShaderForge.SFN_Clamp01,id:9939,x:32348,y:32108,varname:node_9939,prsc:2|IN-4239-OUT;n:type:ShaderForge.SFN_OneMinus,id:4239,x:32172,y:32130,varname:node_4239,prsc:2|IN-5295-OUT;n:type:ShaderForge.SFN_OneMinus,id:9968,x:31388,y:32426,varname:node_9968,prsc:2|IN-9943-OUT;n:type:ShaderForge.SFN_Slider,id:3041,x:30842,y:32583,ptovrint:False,ptlb:Sail Controls,ptin:_SailControls,varname:_Dissolveamount_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:1;n:type:ShaderForge.SFN_RemapRange,id:9000,x:31576,y:32366,varname:node_9000,prsc:2,frmn:0,frmx:1,tomn:-1,tomx:1|IN-9968-OUT;n:type:ShaderForge.SFN_Vector1,id:9278,x:32330,y:32320,varname:node_9278,prsc:2,v1:4;n:type:ShaderForge.SFN_Vector1,id:7229,x:32447,y:32633,varname:node_7229,prsc:2,v1:4;n:type:ShaderForge.SFN_Tex2d,id:45,x:32904,y:32125,varname:node_45,prsc:2,tex:0e675a08c985440e5869b3e14278db8d,ntxv:0,isnm:False|UVIN-4336-OUT,TEX-557-TEX;n:type:ShaderForge.SFN_Multiply,id:1382,x:33386,y:32525,varname:node_1382,prsc:2|A-45-RGB,B-3638-RGB;n:type:ShaderForge.SFN_Blend,id:8078,x:33517,y:32778,varname:node_8078,prsc:2,blmd:12,clmp:True|SRC-3121-OUT,DST-9391-OUT;n:type:ShaderForge.SFN_Multiply,id:1900,x:32926,y:32908,varname:node_1900,prsc:2|A-3854-OUT,B-6963-OUT,C-9982-OUT;n:type:ShaderForge.SFN_OneMinus,id:9391,x:33317,y:32826,varname:node_9391,prsc:2|IN-1900-OUT;n:type:ShaderForge.SFN_Floor,id:3121,x:32228,y:32369,varname:node_3121,prsc:2|IN-5295-OUT;n:type:ShaderForge.SFN_Floor,id:6369,x:31916,y:32289,varname:node_6369,prsc:2|IN-6563-OUT;n:type:ShaderForge.SFN_RemapRange,id:9943,x:31223,y:32496,varname:node_9943,prsc:2,frmn:0,frmx:1,tomn:0.02,tomx:0.51|IN-3041-OUT;n:type:ShaderForge.SFN_Vector1,id:9982,x:32717,y:32996,varname:node_9982,prsc:2,v1:3;n:type:ShaderForge.SFN_RemapRange,id:442,x:31098,y:32770,varname:node_442,prsc:2,frmn:0,frmx:1,tomn:0.62,tomx:1|IN-6963-OUT;n:type:ShaderForge.SFN_Blend,id:7351,x:33926,y:32216,varname:node_7351,prsc:2,blmd:10,clmp:True|SRC-6442-OUT,DST-4131-RGB;n:type:ShaderForge.SFN_Color,id:4131,x:33746,y:32288,ptovrint:False,ptlb:Overlaycolor,ptin:_Overlaycolor,varname:node_4131,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.25,c2:0.25,c3:0.25,c4:1;proporder:3058-484-4233-6235-8347-3041-6963-6987-7647-6582-557-114-5911-1588-4131;pass:END;sub:END;*/

Shader "Shader Forge/SailDamage" {
    Properties {
        _Texture_main ("Texture_main", 2D) = "white" {}
        _MainColor ("Main Color", Color) = (1,1,1,1)
        _Normalmap ("Normal map", 2D) = "bump" {}
        _Metallic ("Metallic", Range(0, 1)) = 0
        _Gloss ("Gloss", Range(0, 1)) = 0
        _SailControls ("Sail Controls", Range(0, 1)) = 0
        _Dissolveamount ("Dissolve amount", Range(0, 1)) = 0
        _Noise_for_dissolve ("Noise_for_dissolve", 2D) = "white" {}
        _Mask_for_controls ("Mask_for_controls", 2D) = "white" {}
        _Ramp ("Ramp", 2D) = "white" {}
        _Ramp2 ("Ramp 2", 2D) = "white" {}
        _Frequency ("Frequency", Range(0, 1)) = 0.2
        _Foldsamount ("Folds amount", Range(0, 3)) = 2
        _Amplitude ("Amplitude", Range(0, 3)) = 2
        _Overlaycolor ("Overlaycolor", Color) = (0.25,0.25,0.25,1)
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
            Cull Off
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma multi_compile_fog
            #pragma exclude_renderers metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform float4 _TimeEditor;
            uniform sampler2D _Noise_for_dissolve; uniform float4 _Noise_for_dissolve_ST;
            uniform float _Dissolveamount;
            uniform sampler2D _Ramp; uniform float4 _Ramp_ST;
            uniform sampler2D _Texture_main; uniform float4 _Texture_main_ST;
            uniform sampler2D _Normalmap; uniform float4 _Normalmap_ST;
            uniform float _Frequency;
            uniform float _Amplitude;
            uniform float _Metallic;
            uniform float _Gloss;
            uniform float4 _MainColor;
            uniform float _Foldsamount;
            uniform sampler2D _Mask_for_controls; uniform float4 _Mask_for_controls_ST;
            uniform sampler2D _Ramp2; uniform float4 _Ramp2_ST;
            uniform float _SailControls;
            uniform float4 _Overlaycolor;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 texcoord0 : TEXCOORD0;
                float4 vertexColor : COLOR;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                float3 tangentDir : TEXCOORD3;
                float3 bitangentDir : TEXCOORD4;
                float4 vertexColor : COLOR;
                LIGHTING_COORDS(5,6)
                UNITY_FOG_COORDS(7)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.vertexColor = v.vertexColor;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.tangentDir = normalize( mul( _Object2World, float4( v.tangent.xyz, 0.0 ) ).xyz );
                o.bitangentDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
                float4 node_5117 = _Time + _TimeEditor;
                float node_3394 = (((clamp(sin(((_Frequency*2.0+0.0)*((1.0 - o.uv0.r.r)+node_5117.g+o.uv0.g.r+node_5117.g)*_Foldsamount*6.28318530718)),-1,1)*0.5)*(_Amplitude*8.0+0.0))*o.vertexColor.r);
                v.vertex.xyz += float3(node_3394,node_3394,node_3394);
                o.posWorld = mul(_Object2World, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
                i.normalDir = normalize(i.normalDir);
                i.normalDir *= faceSign;
                float3x3 tangentTransform = float3x3( i.tangentDir, i.bitangentDir, i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 _Normalmap_var = UnpackNormal(tex2D(_Normalmap,TRANSFORM_TEX(i.uv0, _Normalmap)));
                float3 normalLocal = _Normalmap_var.rgb;
                float3 normalDirection = normalize(mul( normalLocal, tangentTransform )); // Perturbed normals
                float3 viewReflectDirection = reflect( -viewDirection, normalDirection );
                float4 _Mask_for_controls_var = tex2D(_Mask_for_controls,TRANSFORM_TEX(i.uv0, _Mask_for_controls));
                float node_5295 = (floor((((1.0 - (_SailControls*0.49+0.02))*2.0+-1.0)+_Mask_for_controls_var.r))*6.0+-4.0);
                float4 _Noise_for_dissolve_var = tex2D(_Noise_for_dissolve,TRANSFORM_TEX(i.uv0, _Noise_for_dissolve));
                float node_3854 = (((1.0 - (_Dissolveamount*0.38+0.62))*1.7+-1.0)+_Noise_for_dissolve_var.r);
                clip(saturate((floor(node_5295) > 0.5 ?  (1.0-(1.0-2.0*(floor(node_5295)-0.5))*(1.0-(1.0 - (node_3854*_Dissolveamount*3.0)))) : (2.0*floor(node_5295)*(1.0 - (node_3854*_Dissolveamount*3.0)))) ) - 0.5);
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
                float3 lightColor = _LightColor0.rgb;
                float3 halfDirection = normalize(viewDirection+lightDirection);
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
                float Pi = 3.141592654;
                float InvPi = 0.31830988618;
///////// Gloss:
                float gloss = _Gloss;
                float specPow = exp2( gloss * 10.0+1.0);
/////// GI Data:
                UnityLight light;
                #ifdef LIGHTMAP_OFF
                    light.color = lightColor;
                    light.dir = lightDirection;
                    light.ndotl = LambertTerm (normalDirection, light.dir);
                #else
                    light.color = half3(0.f, 0.f, 0.f);
                    light.ndotl = 0.0f;
                    light.dir = half3(0.f, 0.f, 0.f);
                #endif
                UnityGIInput d;
                d.light = light;
                d.worldPos = i.posWorld.xyz;
                d.worldViewDir = viewDirection;
                d.atten = attenuation;
                Unity_GlossyEnvironmentData ugls_en_data;
                ugls_en_data.roughness = 1.0 - gloss;
                ugls_en_data.reflUVW = viewReflectDirection;
                UnityGI gi = UnityGlobalIllumination(d, 1, normalDirection, ugls_en_data );
                lightDirection = gi.light.dir;
                lightColor = gi.light.color;
////// Specular:
                float NdotL = max(0, dot( normalDirection, lightDirection ));
                float LdotH = max(0.0,dot(lightDirection, halfDirection));
                float4 _Texture_main_var = tex2D(_Texture_main,TRANSFORM_TEX(i.uv0, _Texture_main));
                float3 diffuseColor = saturate(( _Overlaycolor.rgb > 0.5 ? (1.0-(1.0-2.0*(_Overlaycolor.rgb-0.5))*(1.0-(_Texture_main_var.rgb+_MainColor.rgb))) : (2.0*_Overlaycolor.rgb*(_Texture_main_var.rgb+_MainColor.rgb)) )); // Need this for specular when using metallic
                float specularMonochrome;
                float3 specularColor;
                diffuseColor = DiffuseAndSpecularFromMetallic( diffuseColor, _Metallic, specularColor, specularMonochrome );
                specularMonochrome = 1-specularMonochrome;
                float NdotV = max(0.0,dot( normalDirection, viewDirection ));
                float NdotH = max(0.0,dot( normalDirection, halfDirection ));
                float VdotH = max(0.0,dot( viewDirection, halfDirection ));
                float visTerm = SmithBeckmannVisibilityTerm( NdotL, NdotV, 1.0-gloss );
                float normTerm = max(0.0, NDFBlinnPhongNormalizedTerm(NdotH, RoughnessToSpecPower(1.0-gloss)));
                float specularPBL = max(0, (NdotL*visTerm*normTerm) * unity_LightGammaCorrectionConsts_PIDiv4 );
                float3 directSpecular = (floor(attenuation) * _LightColor0.xyz) * pow(max(0,dot(halfDirection,normalDirection)),specPow)*specularPBL*lightColor*FresnelTerm(specularColor, LdotH);
                float3 specular = directSpecular;
/////// Diffuse:
                NdotL = max(0.0,dot( normalDirection, lightDirection ));
                half fd90 = 0.5 + 2 * LdotH * LdotH * (1-gloss);
                float3 directDiffuse = ((1 +(fd90 - 1)*pow((1.00001-NdotL), 5)) * (1 + (fd90 - 1)*pow((1.00001-NdotV), 5)) * NdotL) * attenColor;
                float3 indirectDiffuse = float3(0,0,0);
                indirectDiffuse += UNITY_LIGHTMODEL_AMBIENT.rgb; // Ambient Light
                float3 diffuse = (directDiffuse + indirectDiffuse) * diffuseColor;
////// Emissive:
                float2 node_4336 = float2(saturate((1.0 - node_5295)),4.0);
                float4 node_45 = tex2D(_Ramp2,node_4336);
                float2 node_1584 = float2(saturate((1.0 - (node_3854*8.0+-4.0))),4.0);
                float4 node_3638 = tex2D(_Ramp,node_1584);
                float3 emissive = (node_45.rgb*node_3638.rgb);
/// Final Color:
                float3 finalColor = diffuse + specular + emissive;
                fixed4 finalRGBA = fixed4(finalColor,1);
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
            Cull Off
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDADD
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #pragma multi_compile_fwdadd_fullshadows
            #pragma multi_compile_fog
            #pragma exclude_renderers metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform float4 _TimeEditor;
            uniform sampler2D _Noise_for_dissolve; uniform float4 _Noise_for_dissolve_ST;
            uniform float _Dissolveamount;
            uniform sampler2D _Ramp; uniform float4 _Ramp_ST;
            uniform sampler2D _Texture_main; uniform float4 _Texture_main_ST;
            uniform sampler2D _Normalmap; uniform float4 _Normalmap_ST;
            uniform float _Frequency;
            uniform float _Amplitude;
            uniform float _Metallic;
            uniform float _Gloss;
            uniform float4 _MainColor;
            uniform float _Foldsamount;
            uniform sampler2D _Mask_for_controls; uniform float4 _Mask_for_controls_ST;
            uniform sampler2D _Ramp2; uniform float4 _Ramp2_ST;
            uniform float _SailControls;
            uniform float4 _Overlaycolor;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 texcoord0 : TEXCOORD0;
                float4 vertexColor : COLOR;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                float3 tangentDir : TEXCOORD3;
                float3 bitangentDir : TEXCOORD4;
                float4 vertexColor : COLOR;
                LIGHTING_COORDS(5,6)
                UNITY_FOG_COORDS(7)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.vertexColor = v.vertexColor;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.tangentDir = normalize( mul( _Object2World, float4( v.tangent.xyz, 0.0 ) ).xyz );
                o.bitangentDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
                float4 node_5117 = _Time + _TimeEditor;
                float node_3394 = (((clamp(sin(((_Frequency*2.0+0.0)*((1.0 - o.uv0.r.r)+node_5117.g+o.uv0.g.r+node_5117.g)*_Foldsamount*6.28318530718)),-1,1)*0.5)*(_Amplitude*8.0+0.0))*o.vertexColor.r);
                v.vertex.xyz += float3(node_3394,node_3394,node_3394);
                o.posWorld = mul(_Object2World, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
                i.normalDir = normalize(i.normalDir);
                i.normalDir *= faceSign;
                float3x3 tangentTransform = float3x3( i.tangentDir, i.bitangentDir, i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 _Normalmap_var = UnpackNormal(tex2D(_Normalmap,TRANSFORM_TEX(i.uv0, _Normalmap)));
                float3 normalLocal = _Normalmap_var.rgb;
                float3 normalDirection = normalize(mul( normalLocal, tangentTransform )); // Perturbed normals
                float4 _Mask_for_controls_var = tex2D(_Mask_for_controls,TRANSFORM_TEX(i.uv0, _Mask_for_controls));
                float node_5295 = (floor((((1.0 - (_SailControls*0.49+0.02))*2.0+-1.0)+_Mask_for_controls_var.r))*6.0+-4.0);
                float4 _Noise_for_dissolve_var = tex2D(_Noise_for_dissolve,TRANSFORM_TEX(i.uv0, _Noise_for_dissolve));
                float node_3854 = (((1.0 - (_Dissolveamount*0.38+0.62))*1.7+-1.0)+_Noise_for_dissolve_var.r);
                clip(saturate((floor(node_5295) > 0.5 ?  (1.0-(1.0-2.0*(floor(node_5295)-0.5))*(1.0-(1.0 - (node_3854*_Dissolveamount*3.0)))) : (2.0*floor(node_5295)*(1.0 - (node_3854*_Dissolveamount*3.0)))) ) - 0.5);
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
                float3 lightColor = _LightColor0.rgb;
                float3 halfDirection = normalize(viewDirection+lightDirection);
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
                float Pi = 3.141592654;
                float InvPi = 0.31830988618;
///////// Gloss:
                float gloss = _Gloss;
                float specPow = exp2( gloss * 10.0+1.0);
////// Specular:
                float NdotL = max(0, dot( normalDirection, lightDirection ));
                float LdotH = max(0.0,dot(lightDirection, halfDirection));
                float4 _Texture_main_var = tex2D(_Texture_main,TRANSFORM_TEX(i.uv0, _Texture_main));
                float3 diffuseColor = saturate(( _Overlaycolor.rgb > 0.5 ? (1.0-(1.0-2.0*(_Overlaycolor.rgb-0.5))*(1.0-(_Texture_main_var.rgb+_MainColor.rgb))) : (2.0*_Overlaycolor.rgb*(_Texture_main_var.rgb+_MainColor.rgb)) )); // Need this for specular when using metallic
                float specularMonochrome;
                float3 specularColor;
                diffuseColor = DiffuseAndSpecularFromMetallic( diffuseColor, _Metallic, specularColor, specularMonochrome );
                specularMonochrome = 1-specularMonochrome;
                float NdotV = max(0.0,dot( normalDirection, viewDirection ));
                float NdotH = max(0.0,dot( normalDirection, halfDirection ));
                float VdotH = max(0.0,dot( viewDirection, halfDirection ));
                float visTerm = SmithBeckmannVisibilityTerm( NdotL, NdotV, 1.0-gloss );
                float normTerm = max(0.0, NDFBlinnPhongNormalizedTerm(NdotH, RoughnessToSpecPower(1.0-gloss)));
                float specularPBL = max(0, (NdotL*visTerm*normTerm) * unity_LightGammaCorrectionConsts_PIDiv4 );
                float3 directSpecular = attenColor * pow(max(0,dot(halfDirection,normalDirection)),specPow)*specularPBL*lightColor*FresnelTerm(specularColor, LdotH);
                float3 specular = directSpecular;
/////// Diffuse:
                NdotL = max(0.0,dot( normalDirection, lightDirection ));
                half fd90 = 0.5 + 2 * LdotH * LdotH * (1-gloss);
                float3 directDiffuse = ((1 +(fd90 - 1)*pow((1.00001-NdotL), 5)) * (1 + (fd90 - 1)*pow((1.00001-NdotV), 5)) * NdotL) * attenColor;
                float3 diffuse = directDiffuse * diffuseColor;
/// Final Color:
                float3 finalColor = diffuse + specular;
                fixed4 finalRGBA = fixed4(finalColor * 1,0);
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
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma multi_compile_fog
            #pragma exclude_renderers metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform float4 _TimeEditor;
            uniform sampler2D _Noise_for_dissolve; uniform float4 _Noise_for_dissolve_ST;
            uniform float _Dissolveamount;
            uniform float _Frequency;
            uniform float _Amplitude;
            uniform float _Foldsamount;
            uniform sampler2D _Mask_for_controls; uniform float4 _Mask_for_controls_ST;
            uniform float _SailControls;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
                float4 vertexColor : COLOR;
            };
            struct VertexOutput {
                V2F_SHADOW_CASTER;
                float2 uv0 : TEXCOORD1;
                float4 vertexColor : COLOR;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.vertexColor = v.vertexColor;
                float4 node_5117 = _Time + _TimeEditor;
                float node_3394 = (((clamp(sin(((_Frequency*2.0+0.0)*((1.0 - o.uv0.r.r)+node_5117.g+o.uv0.g.r+node_5117.g)*_Foldsamount*6.28318530718)),-1,1)*0.5)*(_Amplitude*8.0+0.0))*o.vertexColor.r);
                v.vertex.xyz += float3(node_3394,node_3394,node_3394);
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex );
                TRANSFER_SHADOW_CASTER(o)
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
                float4 _Mask_for_controls_var = tex2D(_Mask_for_controls,TRANSFORM_TEX(i.uv0, _Mask_for_controls));
                float node_5295 = (floor((((1.0 - (_SailControls*0.49+0.02))*2.0+-1.0)+_Mask_for_controls_var.r))*6.0+-4.0);
                float4 _Noise_for_dissolve_var = tex2D(_Noise_for_dissolve,TRANSFORM_TEX(i.uv0, _Noise_for_dissolve));
                float node_3854 = (((1.0 - (_Dissolveamount*0.38+0.62))*1.7+-1.0)+_Noise_for_dissolve_var.r);
                clip(saturate((floor(node_5295) > 0.5 ?  (1.0-(1.0-2.0*(floor(node_5295)-0.5))*(1.0-(1.0 - (node_3854*_Dissolveamount*3.0)))) : (2.0*floor(node_5295)*(1.0 - (node_3854*_Dissolveamount*3.0)))) ) - 0.5);
                SHADOW_CASTER_FRAGMENT(i)
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
