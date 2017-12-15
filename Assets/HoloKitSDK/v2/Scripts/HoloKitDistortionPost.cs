using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HoloKit
{
    public enum HoloKitEyeSide : int
    {
        Right = 0,
        Left = 1
    }
    public class HoloKitDistortionPost : MonoBehaviour
    {
        private Mesh distortionMesh;
        private Material distortionMaterial;
        //[HideInInspector]
        public Vector3 addPosition;
        private HoloKitEyeSide eyeSide;
        private RenderTexture renderTexture;
        //[HideInInspector]
        public Camera mCamera;

        public void Initialize(Mesh distortionMesh, RenderTexture renderTexture, HoloKitEyeSide eyeSide)
        {
            this.distortionMesh = distortionMesh;
            this.renderTexture = renderTexture;
            this.eyeSide = eyeSide;
            mCamera = gameObject.AddComponent<Camera>();
            mCamera.clearFlags = CameraClearFlags.SolidColor;
            mCamera.backgroundColor = Color.black;// (eyeSide == HoloKitEyeSide.Right) ? Color.red : Color.blue;
            mCamera.orthographic = true;
            mCamera.orthographicSize = 0.5f;
            mCamera.cullingMask = 0;
            mCamera.useOcclusionCulling = false;
            mCamera.depth = 100;
            mCamera.nearClipPlane = 0.1f;
            mCamera.farClipPlane = 1f;
            distortionMaterial = new Material(Shader.Find("HoloKit/UnlitTexture"));
            distortionMaterial.mainTexture = renderTexture;
            switch (eyeSide)
            {
                case HoloKitEyeSide.Left:
                    mCamera.rect = new Rect(0f, 0f, 0.5f, 1f);
                    addPosition = new Vector3(0.45f, 0f, 0.2f);
                    break;
                case HoloKitEyeSide.Right:
                    mCamera.rect = new Rect(0.5f, 0f, 0.5f, 1f);
                    addPosition = new Vector3(-0.45f, 0f, 0.2f);
                    break;
            }
        }

        private void OnRenderObject()
        {
            if (Camera.current == mCamera && distortionMesh != null && distortionMaterial != null)
            {
                distortionMaterial.SetPass(0);
                Graphics.DrawMeshNow(distortionMesh, transform.position + addPosition + new Vector3((eyeSide == HoloKitEyeSide.Right) ? -mCamera.orthographicSize + 0.06f  : mCamera.orthographicSize - 0.06f, 0f, 0f), transform.rotation);
            }
        }

    }
}