Shader "MGShaders/PolyWater"
{
	Properties
	{
		_WaveBottom("Wave Bottom", Color) = (0.01624243,0.2488464,0.6886792,0)
		[HDR]_WaveTop("Wave Top", Color) = (0,0.8585172,1,0)
		_Opacity("Opacity", Float) = 1
		_BaseNormalTexture("Base Normal Texture", 2D) = "bump" {}
		_NormalTextureTiling("Normal Texture Tiling", Float) = 1
		_WavesTopTexture("Waves Top Texture", 2D) = "white" {}
		_WaveTopTiling("Wave Top Tiling", Float) = 1
		_WaveTopSize("Wave Top Size", Float) = 1
		_WaveTiling("Wave Tiling", Float) = 1
		_WaveStretching("Wave Stretching", Vector) = (0.1,0.1,0,0)
		_WaveSpeed("Wave Speed", Vector) = (0.5,0,0,0)
		_WavesHeight("Waves Height", Float) = 1
		_PlaneRotationAngle("Plane Rotation Angle", Range( 0 , 89)) = 0
		_Tesselation("Tesselation", Float) = 1
		_FoamSize("Foam Size", Float) = 0.5
		_FoamIntencity("Foam Intencity", Float) = 0.5
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Transparent"  "Queue" = "Transparent+0" "IgnoreProjector" = "True" "IsEmissive" = "true"  }
		Cull Back
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#include "UnityCG.cginc"
		#include "Tessellation.cginc"
		#pragma target 4.6
		#pragma surface surf Standard alpha:fade keepalpha noshadow vertex:vertexDataFunc tessellate:tessFunction 
		struct Input
		{
			float3 worldPos;
			float4 screenPos;
		};

		uniform half _PlaneRotationAngle;
		uniform half2 _WaveStretching;
		uniform half _WaveTiling;
		uniform half2 _WaveSpeed;
		uniform half _WavesHeight;
		uniform sampler2D _BaseNormalTexture;
		uniform half _NormalTextureTiling;
		uniform half4 _WaveBottom;
		uniform sampler2D _WavesTopTexture;
		uniform half _WaveTopTiling;
		uniform half4 _WaveTop;
		uniform half _WaveTopSize;
		UNITY_DECLARE_DEPTH_TEXTURE( _CameraDepthTexture );
		uniform float4 _CameraDepthTexture_TexelSize;
		uniform half _FoamSize;
		uniform half _FoamIntencity;
		uniform half _Opacity;
		uniform half _Tesselation;


		float3 RotateAroundAxis( float3 center, float3 original, float3 u, float angle )
		{
			original -= center;
			float C = cos( angle );
			float S = sin( angle );
			float t = 1 - C;
			float m00 = t * u.x * u.x + C;
			float m01 = t * u.x * u.y - S * u.z;
			float m02 = t * u.x * u.z + S * u.y;
			float m10 = t * u.x * u.y + S * u.z;
			float m11 = t * u.y * u.y + C;
			float m12 = t * u.y * u.z - S * u.x;
			float m20 = t * u.x * u.z - S * u.y;
			float m21 = t * u.y * u.z + S * u.x;
			float m22 = t * u.z * u.z + C;
			float3x3 finalMatrix = float3x3( m00, m01, m02, m10, m11, m12, m20, m21, m22 );
			return mul( finalMatrix, original ) + center;
		}


		float3 mod2D289( float3 x ) { return x - floor( x * ( 1.0 / 289.0 ) ) * 289.0; }

		float2 mod2D289( float2 x ) { return x - floor( x * ( 1.0 / 289.0 ) ) * 289.0; }

		float3 permute( float3 x ) { return mod2D289( ( ( x * 34.0 ) + 1.0 ) * x ); }

		float snoise( float2 v )
		{
			const float4 C = float4( 0.211324865405187, 0.366025403784439, -0.577350269189626, 0.024390243902439 );
			float2 i = floor( v + dot( v, C.yy ) );
			float2 x0 = v - i + dot( i, C.xx );
			float2 i1;
			i1 = ( x0.x > x0.y ) ? float2( 1.0, 0.0 ) : float2( 0.0, 1.0 );
			float4 x12 = x0.xyxy + C.xxzz;
			x12.xy -= i1;
			i = mod2D289( i );
			float3 p = permute( permute( i.y + float3( 0.0, i1.y, 1.0 ) ) + i.x + float3( 0.0, i1.x, 1.0 ) );
			float3 m = max( 0.5 - float3( dot( x0, x0 ), dot( x12.xy, x12.xy ), dot( x12.zw, x12.zw ) ), 0.0 );
			m = m * m;
			m = m * m;
			float3 x = 2.0 * frac( p * C.www ) - 1.0;
			float3 h = abs( x ) - 0.5;
			float3 ox = floor( x + 0.5 );
			float3 a0 = x - ox;
			m *= 1.79284291400159 - 0.85373472095314 * ( a0 * a0 + h * h );
			float3 g;
			g.x = a0.x * x0.x + h.x * x0.y;
			g.yz = a0.yz * x12.xz + h.yz * x12.yw;
			return 130.0 * dot( m, g );
		}


		float4 tessFunction( appdata_full v0, appdata_full v1, appdata_full v2 )
		{
			half4 temp_cast_0 = (_Tesselation).xxxx;
			return temp_cast_0;
		}

		void vertexDataFunc( inout appdata_full v )
		{
			half3 ase_worldPos = mul( unity_ObjectToWorld, v.vertex );
			float3 rotatedValue152 = RotateAroundAxis( float3( 0,0,0 ), ase_worldPos, normalize( float3( 0,1,0 ) ), (0.0 + (_PlaneRotationAngle - 0.0) * (1.55334 - 0.0) / (89.0 - 0.0)) );
			float3 break156 = rotatedValue152;
			float2 appendResult49 = (half2(break156.x , break156.z));
			float2 WorldWaveUV54 = ( ( appendResult49 * _WaveStretching ) * _WaveTiling );
			float2 panner57 = ( 1.0 * _Time.y * float2( 0,0 ) + WorldWaveUV54);
			float simplePerlin2D58 = snoise( panner57*0.5 );
			simplePerlin2D58 = simplePerlin2D58*0.5 + 0.5;
			float2 panner15 = ( 1.0 * _Time.y * _WaveSpeed + WorldWaveUV54);
			float simplePerlin2D2 = snoise( panner15*0.37 );
			simplePerlin2D2 = simplePerlin2D2*0.5 + 0.5;
			float clampResult61 = clamp( ( simplePerlin2D58 + simplePerlin2D2 ) , 0.0 , 1.0 );
			float WavePattern35 = ( pow( clampResult61 , 1.0 ) * 1.0 );
			float temp_output_13_0 = ( ( 1.0 - WavePattern35 ) / _WavesHeight );
			float3 appendResult11 = (half3(0.0 , temp_output_13_0 , 0.0));
			float3 WaveOffset38 = appendResult11;
			v.vertex.xyz += WaveOffset38;
		}

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 WaveSpeed109 = _WaveSpeed;
			half3 ase_worldPos = i.worldPos;
			float3 rotatedValue152 = RotateAroundAxis( float3( 0,0,0 ), ase_worldPos, normalize( float3( 0,1,0 ) ), (0.0 + (_PlaneRotationAngle - 0.0) * (1.55334 - 0.0) / (89.0 - 0.0)) );
			float3 break156 = rotatedValue152;
			float2 appendResult49 = (half2(break156.x , break156.z));
			float2 RotatedWorldPositionUV164 = appendResult49;
			float2 panner141 = ( 1.0 * _Time.y * ( WaveSpeed109 * float2( 0.025,0.025 ) ) + (RotatedWorldPositionUV164*_NormalTextureTiling + 0.0));
			float3 BaseNormal107 = UnpackNormal( tex2D( _BaseNormalTexture, panner141 ) );
			o.Normal = BaseNormal107;
			float2 panner110 = ( 1.0 * _Time.y * ( WaveSpeed109 * float2( -0.15,-0.15 ) ) + (RotatedWorldPositionUV164*_WaveTopTiling + 0.0));
			float2 WorldWaveUV54 = ( ( appendResult49 * _WaveStretching ) * _WaveTiling );
			float2 panner57 = ( 1.0 * _Time.y * float2( 0,0 ) + WorldWaveUV54);
			float simplePerlin2D58 = snoise( panner57*0.5 );
			simplePerlin2D58 = simplePerlin2D58*0.5 + 0.5;
			float2 panner15 = ( 1.0 * _Time.y * _WaveSpeed + WorldWaveUV54);
			float simplePerlin2D2 = snoise( panner15*0.37 );
			simplePerlin2D2 = simplePerlin2D2*0.5 + 0.5;
			float clampResult61 = clamp( ( simplePerlin2D58 + simplePerlin2D2 ) , 0.0 , 1.0 );
			float WavePattern35 = ( pow( clampResult61 , 1.0 ) * 1.0 );
			float temp_output_13_0 = ( ( 1.0 - WavePattern35 ) / _WavesHeight );
			float WaveOffsetForAlbedo63 = ( temp_output_13_0 * _WaveTopSize );
			float clampResult31 = clamp( WaveOffsetForAlbedo63 , 0.0 , 1.0 );
			float4 lerpResult18 = lerp( _WaveBottom , ( ( tex2D( _WavesTopTexture, panner110 ) * ( 1.0 - WavePattern35 ) ) * _WaveTop ) , clampResult31);
			float3 normalizeResult25 = normalize( cross( ddy( ase_worldPos ) , ddx( ase_worldPos ) ) );
			float dotResult27 = dot( normalizeResult25 , _WorldSpaceLightPos0.xyz );
			float LowPolySurface42 = dotResult27;
			float4 Albedo46 = ( lerpResult18 * LowPolySurface42 );
			float4 temp_output_47_0 = Albedo46;
			o.Albedo = temp_output_47_0.rgb;
			float4 ase_screenPos = float4( i.screenPos.xyz , i.screenPos.w + 0.00000000001 );
			float4 ase_screenPosNorm = ase_screenPos / ase_screenPos.w;
			ase_screenPosNorm.z = ( UNITY_NEAR_CLIP_VALUE >= 0 ) ? ase_screenPosNorm.z : ase_screenPosNorm.z * 0.5 + 0.5;
			float screenDepth113 = LinearEyeDepth(SAMPLE_DEPTH_TEXTURE( _CameraDepthTexture, ase_screenPosNorm.xy ));
			float distanceDepth113 = abs( ( screenDepth113 - LinearEyeDepth( ase_screenPosNorm.z ) ) / ( _FoamSize ) );
			float clampResult118 = clamp( ( ( 1.0 - distanceDepth113 ) * _FoamIntencity ) , 0.0 , 1.0 );
			float Foam123 = clampResult118;
			half3 temp_cast_1 = (Foam123).xxx;
			o.Emission = temp_cast_1;
			o.Alpha = _Opacity;
		}

		ENDCG
	}
	CustomEditor "ASEMaterialInspector"
}