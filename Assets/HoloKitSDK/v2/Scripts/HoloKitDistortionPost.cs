using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HoloKit
{
    [ExecuteInEditMode]
    [RequireComponent(typeof(Camera))]
    public class HoloKitDistortionPost : PostEffectsBase
    {
        [Range(-5f, 5f)]
        public float BarrelDistortionFactor = 0f;
        public Shader BarrelDistortionShader = null;
        public Material BarrelDistortionMaterial = null;

        void OnRenderImage(RenderTexture src, RenderTexture dest)
        {
            if (!CheckResources())
            {
                Graphics.Blit(src, dest);
            }

            BarrelDistortionMaterial.SetFloat("_BarrelDistortionFactor", BarrelDistortionFactor);

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