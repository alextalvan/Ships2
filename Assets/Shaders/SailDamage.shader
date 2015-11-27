// Shader created with Shader Forge v1.25 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.25;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:3,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:2,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:False,qofs:0,qpre:2,rntp:3,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False;n:type:ShaderForge.SFN_Final,id:4013,x:33708,y:32601,varname:node_4013,prsc:2|diff-6442-OUT,spec-6235-OUT,gloss-8347-OUT,normal-4233-RGB,emission-3638-RGB,clip-3854-OUT,voffset-4824-OUT;n:type:ShaderForge.SFN_Tex2d,id:6987,x:31566,y:33148,ptovrint:False,ptlb:Noise,ptin:_Noise,varname:node_6987,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:36dd0b22da8874ed38075789055ca664,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Slider,id:6963,x:30853,y:32787,ptovrint:False,ptlb:Dissolve amount,ptin:_Dissolveamount,varname:node_6963,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:1;n:type:ShaderForge.SFN_Add,id:3854,x:31824,y:33025,varname:node_3854,prsc:2|A-6875-OUT,B-6987-R;n:type:ShaderForge.SFN_RemapRange,id:6875,x:31571,y:32738,varname:node_6875,prsc:2,frmn:0,frmx:1,tomn:-1,tomx:0.7|IN-2714-OUT;n:type:ShaderForge.SFN_OneMinus,id:2714,x:31353,y:32738,varname:node_2714,prsc:2|IN-6963-OUT;n:type:ShaderForge.SFN_RemapRange,id:2216,x:32064,y:32809,varname:node_2216,prsc:2,frmn:0,frmx:1,tomn:-4,tomx:4|IN-3854-OUT;n:type:ShaderForge.SFN_Append,id:1584,x:32732,y:32569,varname:node_1584,prsc:2|A-5181-OUT,B-4167-OUT;n:type:ShaderForge.SFN_ValueProperty,id:4167,x:32428,y:32738,ptovrint:False,ptlb:Value,ptin:_Value,varname:node_4167,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:4;n:type:ShaderForge.SFN_Tex2dAsset,id:6582,x:32638,y:32786,ptovrint:False,ptlb:Ramp,ptin:_Ramp,varname:node_6582,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:0e675a08c985440e5869b3e14278db8d,ntxv:0,isnm:False;n:type:ShaderForge.SFN_OneMinus,id:8128,x:32234,y:32809,varname:node_8128,prsc:2|IN-2216-OUT;n:type:ShaderForge.SFN_Tex2d,id:3058,x:32927,y:32042,ptovrint:False,ptlb:Texture,ptin:_Texture,varname:node_3058,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:4233,x:32744,y:32379,ptovrint:False,ptlb:0mm,ptin:_0mm,varname:node_4233,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:2,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:3638,x:32969,y:32544,varname:node_3638,prsc:2,tex:0e675a08c985440e5869b3e14278db8d,ntxv:0,isnm:False|UVIN-1584-OUT,TEX-6582-TEX;n:type:ShaderForge.SFN_Clamp01,id:5181,x:32291,y:32582,varname:node_5181,prsc:2|IN-8128-OUT;n:type:ShaderForge.SFN_TexCoord,id:9665,x:31099,y:33296,varname:node_9665,prsc:2,uv:0;n:type:ShaderForge.SFN_ComponentMask,id:4194,x:31487,y:33323,varname:node_4194,prsc:2,cc1:0,cc2:-1,cc3:-1,cc4:-1|IN-1263-Y;n:type:ShaderForge.SFN_Multiply,id:421,x:32260,y:33390,varname:node_421,prsc:2|A-3371-OUT,B-3684-OUT,C-5911-OUT,D-1746-OUT;n:type:ShaderForge.SFN_Sin,id:7605,x:32428,y:33390,varname:node_7605,prsc:2|IN-421-OUT;n:type:ShaderForge.SFN_Time,id:5117,x:31467,y:33621,varname:node_5117,prsc:2;n:type:ShaderForge.SFN_Add,id:3684,x:31932,y:33446,varname:node_3684,prsc:2|A-1988-OUT,B-5117-T,C-2342-OUT;n:type:ShaderForge.SFN_ConstantClamp,id:5835,x:32580,y:33289,varname:node_5835,prsc:2,min:0,max:1|IN-7605-OUT;n:type:ShaderForge.SFN_Tau,id:1746,x:32092,y:33483,varname:node_1746,prsc:2;n:type:ShaderForge.SFN_NormalVector,id:5914,x:32704,y:33501,prsc:2,pt:False;n:type:ShaderForge.SFN_Multiply,id:4824,x:33138,y:33377,varname:node_4824,prsc:2|A-3974-OUT,B-5914-OUT,C-9616-OUT;n:type:ShaderForge.SFN_Slider,id:114,x:31853,y:33228,ptovrint:False,ptlb:Frequency,ptin:_Frequency,varname:node_114,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.565028,max:1;n:type:ShaderForge.SFN_RemapRange,id:3371,x:32260,y:33179,varname:node_3371,prsc:2,frmn:0,frmx:1,tomn:0,tomx:2|IN-114-OUT;n:type:ShaderForge.SFN_Slider,id:1588,x:32199,y:33687,ptovrint:False,ptlb:Amplitude,ptin:_Amplitude,varname:node_1588,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.2126689,max:1;n:type:ShaderForge.SFN_RemapRange,id:9616,x:32752,y:33696,varname:node_9616,prsc:2,frmn:0,frmx:1,tomn:0,tomx:4|IN-1588-OUT;n:type:ShaderForge.SFN_Slider,id:6235,x:33342,y:32224,ptovrint:False,ptlb:Metallic,ptin:_Metallic,varname:node_6235,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:1;n:type:ShaderForge.SFN_Slider,id:8347,x:33342,y:32123,ptovrint:False,ptlb:Gloss,ptin:_Gloss,varname:node_8347,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:1;n:type:ShaderForge.SFN_Color,id:484,x:32927,y:32250,ptovrint:False,ptlb:Color,ptin:_Color,varname:node_484,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.8235294,c2:0.8235294,c3:0.8235294,c4:1;n:type:ShaderForge.SFN_FragmentPosition,id:1263,x:31163,y:33469,varname:node_1263,prsc:2;n:type:ShaderForge.SFN_Slider,id:5911,x:31932,y:33617,ptovrint:False,ptlb:Folds,ptin:_Folds,varname:node_5911,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.1310446,max:1;n:type:ShaderForge.SFN_OneMinus,id:1988,x:31690,y:33357,varname:node_1988,prsc:2|IN-4194-OUT;n:type:ShaderForge.SFN_Add,id:6442,x:33382,y:32324,varname:node_6442,prsc:2|A-3058-RGB,B-484-RGB;n:type:ShaderForge.SFN_Multiply,id:3974,x:32944,y:33206,varname:node_3974,prsc:2|A-5835-OUT,B-6309-OUT;n:type:ShaderForge.SFN_Vector1,id:6309,x:32755,y:33333,varname:node_6309,prsc:2,v1:0.5;n:type:ShaderForge.SFN_ComponentMask,id:2342,x:31452,y:33479,varname:node_2342,prsc:2,cc1:0,cc2:-1,cc3:-1,cc4:-1|IN-1263-X;proporder:6987-6963-4167-6582-4233-3058-114-1588-6235-8347-484-5911;pass:END;sub:END;*/

Shader "Shader Forge/SailDamage" {
    Properties {
        _Noise ("Noise", 2D) = "white" {}
        _Dissolveamount ("Dissolve amount", Range(0, 1)) = 0
        _Value ("Value", Float ) = 4
        _Ramp ("Ramp", 2D) = "white" {}
        _0mm ("0mm", 2D) = "black" {}
        _Texture ("Texture", 2D) = "white" {}
        _Frequency ("Frequency", Range(0, 1)) = 0.565028
        _Amplitude ("Amplitude", Range(0, 1)) = 0.2126689
        _Metallic ("Metallic", Range(0, 1)) = 0
        _Gloss ("Gloss", Range(0, 1)) = 0
        _Color ("Color", Color) = (0.8235294,0.8235294,0.8235294,1)
        _Folds ("Folds", Range(0, 1)) = 0.1310446
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
            uniform sampler2D _Noise; uniform float4 _Noise_ST;
            uniform float _Dissolveamount;
            uniform float _Value;
            uniform sampler2D _Ramp; uniform float4 _Ramp_ST;
            uniform sampler2D _Texture; uniform float4 _Texture_ST;
            uniform sampler2D _0mm; uniform float4 _0mm_ST;
            uniform float _Frequency;
            uniform float _Amplitude;
            uniform float _Metallic;
            uniform float _Gloss;
            uniform float4 _Color;
            uniform float _Folds;
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
                LIGHTING_COORDS(5,6)
                UNITY_FOG_COORDS(7)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.tangentDir = normalize( mul( _Object2World, float4( v.tangent.xyz, 0.0 ) ).xyz );
                o.bitangentDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
                float4 node_5117 = _Time + _TimeEditor;
                v.vertex.xyz += ((clamp(sin(((_Frequency*2.0+0.0)*((1.0 - mul(_Object2World, v.vertex).g.r)+node_5117.g+mul(_Object2World, v.vertex).r.r)*_Folds*6.28318530718)),0,1)*0.5)*v.normal*(_Amplitude*4.0+0.0));
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
                float4 _0mm_var = tex2D(_0mm,TRANSFORM_TEX(i.uv0, _0mm));
                float3 normalLocal = _0mm_var.rgb;
                float3 normalDirection = normalize(mul( normalLocal, tangentTransform )); // Perturbed normals
                float3 viewReflectDirection = reflect( -viewDirection, normalDirection );
                float4 _Noise_var = tex2D(_Noise,TRANSFORM_TEX(i.uv0, _Noise));
                float node_3854 = (((1.0 - _Dissolveamount)*1.7+-1.0)+_Noise_var.r);
                clip(node_3854 - 0.5);
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
                float4 _Texture_var = tex2D(_Texture,TRANSFORM_TEX(i.uv0, _Texture));
                float3 diffuseColor = (_Texture_var.rgb+_Color.rgb); // Need this for specular when using metallic
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
                float2 node_1584 = float2(saturate((1.0 - (node_3854*8.0+-4.0))),_Value);
                float4 node_3638 = tex2D(_Ramp,node_1584);
                float3 emissive = node_3638.rgb;
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
            uniform sampler2D _Noise; uniform float4 _Noise_ST;
            uniform float _Dissolveamount;
            uniform float _Value;
            uniform sampler2D _Ramp; uniform float4 _Ramp_ST;
            uniform sampler2D _Texture; uniform float4 _Texture_ST;
            uniform sampler2D _0mm; uniform float4 _0mm_ST;
            uniform float _Frequency;
            uniform float _Amplitude;
            uniform float _Metallic;
            uniform float _Gloss;
            uniform float4 _Color;
            uniform float _Folds;
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
                LIGHTING_COORDS(5,6)
                UNITY_FOG_COORDS(7)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.tangentDir = normalize( mul( _Object2World, float4( v.tangent.xyz, 0.0 ) ).xyz );
                o.bitangentDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
                float4 node_5117 = _Time + _TimeEditor;
                v.vertex.xyz += ((clamp(sin(((_Frequency*2.0+0.0)*((1.0 - mul(_Object2World, v.vertex).g.r)+node_5117.g+mul(_Object2World, v.vertex).r.r)*_Folds*6.28318530718)),0,1)*0.5)*v.normal*(_Amplitude*4.0+0.0));
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
                float4 _0mm_var = tex2D(_0mm,TRANSFORM_TEX(i.uv0, _0mm));
                float3 normalLocal = _0mm_var.rgb;
                float3 normalDirection = normalize(mul( normalLocal, tangentTransform )); // Perturbed normals
                float4 _Noise_var = tex2D(_Noise,TRANSFORM_TEX(i.uv0, _Noise));
                float node_3854 = (((1.0 - _Dissolveamount)*1.7+-1.0)+_Noise_var.r);
                clip(node_3854 - 0.5);
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
                float4 _Texture_var = tex2D(_Texture,TRANSFORM_TEX(i.uv0, _Texture));
                float3 diffuseColor = (_Texture_var.rgb+_Color.rgb); // Need this for specular when using metallic
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
            uniform sampler2D _Noise; uniform float4 _Noise_ST;
            uniform float _Dissolveamount;
            uniform float _Frequency;
            uniform float _Amplitude;
            uniform float _Folds;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                V2F_SHADOW_CASTER;
                float2 uv0 : TEXCOORD1;
                float4 posWorld : TEXCOORD2;
                float3 normalDir : TEXCOORD3;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                float4 node_5117 = _Time + _TimeEditor;
                v.vertex.xyz += ((clamp(sin(((_Frequency*2.0+0.0)*((1.0 - mul(_Object2World, v.vertex).g.r)+node_5117.g+mul(_Object2World, v.vertex).r.r)*_Folds*6.28318530718)),0,1)*0.5)*v.normal*(_Amplitude*4.0+0.0));
                o.posWorld = mul(_Object2World, v.vertex);
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex );
                TRANSFER_SHADOW_CASTER(o)
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
                i.normalDir = normalize(i.normalDir);
                i.normalDir *= faceSign;
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                float4 _Noise_var = tex2D(_Noise,TRANSFORM_TEX(i.uv0, _Noise));
                float node_3854 = (((1.0 - _Dissolveamount)*1.7+-1.0)+_Noise_var.r);
                clip(node_3854 - 0.5);
                SHADOW_CASTER_FRAGMENT(i)
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
