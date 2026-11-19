Shader "Custom/SoftGlow"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Color ("Color", Color) = (1,1,1,1)
        _Radius ("Radius", Range(0,1)) = 0.5
    }
    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }
        LOD 100

        Blend SrcAlpha OneMinusSrcAlpha
        ZWrite Off
        Cull Off

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
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _Color;
            float _Radius;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // Получаем цвет из текстуры
                fixed4 col = tex2D(_MainTex, i.uv);
                
                // Вычисляем расстояние от центра (для круга)
                float2 center = float2(0.5, 0.5);
                float dist = distance(i.uv, center);
                
                // Делаем края прозрачными в зависимости от радиуса
                float alpha = 1.0 - smoothstep(_Radius - 0.1, _Radius, dist);
                
                // Умножаем на цвет и прозрачность
                col *= _Color;
                col.a *= alpha;
                
                return col;
            }
            ENDCG
        }
    }
}