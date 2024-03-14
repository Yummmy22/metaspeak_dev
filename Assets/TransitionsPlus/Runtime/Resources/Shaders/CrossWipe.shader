Shader "TransitionsPlus/CrossWipe"
{
    Properties
    {
        [HideInInspector] _T("Progress", Range(0, 1)) = 0
        [HideInInspector] _MainTex ("Texture", 2D) = "white" {}
        [HideInInspector] _NoiseTex ("Noise", 2D) = "white" {}
        [HideInInspector] _GradientTex ("Gradient Tex", 2D) = "white" {}
        [HideInInspector] _Color("Color", Color) = (0,0,0)
        [HideInInspector] _VignetteIntensity("Vignette Intensity", Range(0,1)) = 0.5
        [HideInInspector] _NoiseIntensity("Noise Intensity", Range(0,1)) = 0.5
        [HideInInspector] _ToonIntensity("Toon Intensity", Float) = 1
        [HideInInspector] _ToonDotIntensity("Toon Dot Intensity", Float) = 1
        [HideInInspector] _AspectRatio("Aspect Ratio", Float) = 1
        [HideInInspector] _Distortion("Distortion", Float) = 1
        [HideInInspector] _ToonDotRadius("Toon Dot Radius", Float) = 0
        [HideInInspector] _ToonDotCount("Toon Dot Count", Float) = 0
        [HideInInspector] _Contrast("Constrast", Float) = 1
        [HideInInspector] _CellDivisions("Cell Divisions", Float) = 32
        [HideInInspector] _Spread("Spread", Float) = 64
        [HideInInspector] _RotationMultiplier("Rotation", Float) = 0
        [HideInInspector] _Rotation("Rotation", Float) = 0
        [HideInInspector] _Splits("Splits", Float) = 2
        [HideInInspector] _Seed("Seed", Float) = 0
        [HideInInspector] _CentersCount("Seed", Int) = 1
        [HideInInspector] _TimeMultiplier("Time Multiplier", Float) = 1
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" }
        Cull Off
        Blend SrcAlpha OneMinusSrcAlpha
        ZWrite Off
        ZTest Always

        Pass
        {
            Name "Cross Wipe"
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_local _ TEXTURE GRADIENT_OPACITY GRADIENT_TIME GRADIENT_SPATIAL_RADIAL GRADIENT_SPATIAL_HORIZONTAL GRADIENT_SPATIAL_VERTICAL
            #pragma multi_compile_local _ TOON

            #include "TransitionsCommon.cginc"
         
            fixed4 frag (v2f i) : SV_Target
            {
                i.uv -= _Center;

                float aspect = _AspectRatio ? GetAspectRatio() : 1.0;
                i.uv.x = 0.5 + (i.uv.x - 0.5) * aspect;
                i.uv.y = 0.5 + (i.uv.y - 0.5) / aspect;

                RotateUV(i.uv);

                float split = floor(i.uv.y * _Splits) % 2;

                i.uv.x = split ? (1.0 - i.uv.x) : i.uv.x;

                float t = _T * 1.7;
                float a = t - 1.0;
                float b = t;
              
                fixed fade = 1.0 - smoothstep(a, b, i.uv.x);
                fixed noise = ComputeNoise(i.noiseUV);

                fixed ft = 2.0 * (0.5 - abs(fade - 0.5));
                fade = fade + (noise - 0.5) * ft;
                
                fixed4 color = ComputeOutputColor(i.uv, i.noiseUV, fade);
                return color;
            }
            ENDCG
        }
    }
}
