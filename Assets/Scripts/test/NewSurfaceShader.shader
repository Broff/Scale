// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

Shader "Custom/TriplanarBump" { 
 
Properties { 
 
_AColor ("Diffuse Color", Color) = (1.0, 1.0, 1.0, 1.0) 
 
_MainTex ("Base (RGB)", 2D) = "white" {} 
 
_TexScale ("Texture scale", float) = 1 
 
_BumpTex ("Bumpmap", 2D) = "white" {} 
 
_BumpValue ("Bump value", float) = 1 
 
} 
 
SubShader { 
 
Tags { "RenderType"="Opaque" } 
 
 
 
CGPROGRAM 
 
#pragma exclude_renderers flash 
 
#pragma target 2.0 
 
#pragma surface surf Lambert vertex:vert 
 
#include "UnityCG.cginc" 
 
 
 
float4 _AColor; 
 
sampler _MainTex;  
 
sampler _BumpTex; 
 
float _TexScale; 
 
float _BumpValue; 
 
 
 
struct Input 
 
{ 
 
float3 myVertex; 
 
float3 myNormal; 
 
}; 
 
 
 
void vert(inout appdata_full v, out Input o) 
 
{ 
 
o.myNormal = v.normal; 
 
o.myVertex = mul(unity_ObjectToWorld, v.vertex);  
 
} 
 
 
 
void surf (Input IN, inout SurfaceOutput o) 
 
{ 
 
float4 Dcolor = {0.2, 0.2, 0.2, 1.0}; 
 
 
 
float3 blend_weights = abs(IN.myNormal); 
 
blend_weights = blend_weights - 0.2679f; 
 
blend_weights = max(blend_weights, 0); 
 
//force sum to 1.0 
 
blend_weights /= (blend_weights.x + blend_weights.y + blend_weights.z).xxx; 
 
 
 
float4 blended_color; 
 
float3 blended_bump_vec; 
 
 
 
float3 coord1 = IN.myVertex.xyz * _TexScale; 
 
float3 coord2 = IN.myVertex.xyz * _TexScale; 
 
float3 coord3 = IN.myVertex.xyz * _TexScale; 
 
 
 
float2 bumpFetch1 = _BumpValue * tex2D(_BumpTex,coord1).wy * 2 - 1; 
 
float2 bumpFetch2 = _BumpValue * tex2D(_BumpTex,coord2).wy * 2 - 1; 
 
float2 bumpFetch3 = _BumpValue * tex2D(_BumpTex,coord3).wy * 2 - 1; 
 
 
 
float3 bump1 = float3(0, bumpFetch1.x, bumpFetch1.y);  
 
float3 bump2 = float3(bumpFetch2.y, 0, bumpFetch2.x);  
 
float3 bump3 = float3(bumpFetch3.x, bumpFetch3.y, 0); 
 
 
 
float4 col1 = tex2D(_MainTex, coord1); 
 
float4 col2 = tex2D(_MainTex, coord2); 
 
float4 col3 = tex2D(_MainTex, coord3); 
 
 
 
blended_color = col1.xyzw * blend_weights.xxxx +  
 
col2.xyzw * blend_weights.yyyy +  
 
col3.xyzw * blend_weights.zzzz; 
 
 
 
blended_bump_vec = bump1.xyz * blend_weights.xxx +  
 
bump2.xyz * blend_weights.yyy +  
 
bump3.xyz * blend_weights.zzz; 
 
 
 
o.Albedo = blended_color * _AColor; 
 
o.Normal = blended_bump_vec; 
 
} 
 
ENDCG 
 
} 
 
} 
