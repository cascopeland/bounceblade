Shader "Custom/Checkerboard"
{
    Properties
    {
        _MainColor ("Main Color", Color) = (1,1,1,1)
        _SecondaryColor ("Secondary Color", Color) = (0,0,0,1)
        _TileSize ("Tile Size", Range(1, 20)) = 2
    }

    SubShader
    {
        Tags { "RenderType"="Opaque" }

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // Include UnityCG for built-in shader variables
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            float4 _MainColor;
            float4 _SecondaryColor;
            float _TileSize;

            // Vertex shader
            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            // Fragment shader
            half4 frag (v2f i) : SV_Target
            {
                // Calculate checkerboard pattern
                float2 coords = floor(i.uv * _TileSize);
                bool checkerboard = fmod(coords.x + coords.y, 2) == 0;

                // Assign colors based on the checkerboard pattern
                half4 color = checkerboard ? _MainColor : _SecondaryColor;

                return color;
            }
            ENDCG
        }
    }
}