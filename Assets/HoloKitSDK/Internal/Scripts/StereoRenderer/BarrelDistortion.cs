using System;
using UnityEngine;

namespace HoloKit {
	[ExecuteInEditMode]
    [RequireComponent(typeof(Camera))]
    public class BarrelDistortion : PostEffectsBase
    {
        public float FovRadians = 1.69f;

        [Range(-1, 1)]
        public float Offset = 0f;

        public Shader BarrelDistortionShader = null;
        public Material BarrelDistortionMaterial = null;

        void OnRenderImage(RenderTexture src, RenderTexture dest)
        {
            if (!CheckResources())
            {
                Graphics.Blit(src, dest);
            }

            BarrelDistortionMaterial.SetFloat("_FOV", FovRadians);
            BarrelDistortionMaterial.SetFloat("_Offset", Offset);

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