using System;
using UnityEngine;

namespace HoloKit {
	[ExecuteInEditMode]
    [RequireComponent(typeof(Camera))]
    public class BarrelDistortion : PostEffectsBase
    {
        [Range(-0.01f, 0.01f)]
        public float RedDistortionFactor = 0f;
        [Range(-0.01f, 0.01f)]
        public float GreenDistortionFactor = 0f;
        [Range(-0.01f, 0.01f)]
        public float BlueDistortionFactor = 0f;
        [Range(-5f, 5f)]
        public float BarrelDistortionFactor = 0f;
        [Range(-0.5f, 0.5f)]
        public float HorizontalOffsetFactor = 0f;
        [Range(-0.5f, 0.5f)]
        public float VerticalOffsetFactor = 0f;

        public Shader BarrelDistortionShader = null;
        public Material BarrelDistortionMaterial = null;

        void OnRenderImage(RenderTexture src, RenderTexture dest)
        {
            if (!CheckResources())
            {
                Graphics.Blit(src, dest);
            }

            BarrelDistortionMaterial.SetFloat("_RedDistortionFactor", RedDistortionFactor);
            BarrelDistortionMaterial.SetFloat("_GreenDistortionFactor", GreenDistortionFactor);
            BarrelDistortionMaterial.SetFloat("_BlueDistortionFactor", BlueDistortionFactor);
            BarrelDistortionMaterial.SetFloat("_BarrelDistortionFactor", BarrelDistortionFactor);
            BarrelDistortionMaterial.SetFloat("_HorizontalOffsetFactor", HorizontalOffsetFactor);
            BarrelDistortionMaterial.SetFloat("_VerticalOffsetFactor", VerticalOffsetFactor);

            Graphics.Blit(src, dest, BarrelDistortionMaterial);
        }

        public override bool CheckResources()
        {
            CheckSupport(false);
            BarrelDistortionMaterial = CheckShaderAndCreateMaterial(BarrelDistortionShader, BarrelDistortionMaterial);

            if (!isSupported)
            {
                ReportAutoDisable();
            }

            return isSupported;
        }

    }
}