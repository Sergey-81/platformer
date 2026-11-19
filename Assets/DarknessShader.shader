Shader "Custom/DarknessShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _PlayerPos ("Player Position", Vector) = (0,0,0,0)
        _Radius ("Radius", Float) = 5.0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
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
                float4 screenPos : TEXCOORD1;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _PlayerPos;
            float _Radius;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.screenPos = ComputeScreenPos(o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // Получаем координаты на экране
                float2 screenUV = i.screenPos.xy / i.screenPos.w;
                
                // Переводим позицию игрока в координаты экрана
                float4 playerScreenPos = ComputeScreenPos(UnityWorldToClipPos(_PlayerPos));
                float2 playerUV = playerScreenPos.xy / playerScreenPos.w;

                // Считаем расстояние от игрока до пикселя
                float dist = distance(screenUV, playerUV);
                
                // Если пиксель дальше радиуса — он черный
                float darkness = smoothstep(_Radius, _Radius - 0.1, dist);
                
                // Рисуем тьму (черный цвет с прозрачностью)
                return fixed4(0, 0, 0, 1.0 - darkness);
            }
            ENDCG
        }
    }
}