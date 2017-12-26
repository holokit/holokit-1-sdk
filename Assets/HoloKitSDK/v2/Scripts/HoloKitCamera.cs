using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HoloKit
{
    public enum CameraType : int
    {
        AR = 0,
        MR = 1
    }

    public enum EyeSide : int
    {
        Right = 0,
        Left = 1
    }

    public class HoloKitCamera : MonoBehaviour
    {
        private static HoloKitCamera instance;
        public static HoloKitCamera Instance
        {
            get {
                if (instance == null)
                {
                    instance = instance = FindObjectOfType<HoloKitCamera>();
                }
                return instance;
            }
        }
        public HoloKit.CameraType cameraType = CameraType.AR;
        public Camera cameraCenter;
        public Camera cameraRight;
        public Camera cameraLeft;
        public HoloKitDistortionPost postefRight;
        public HoloKitDistortionPost postefLeft;
        public Transform holoKitOffset;
        public Profile.ModelType profileModel;
        public Profile.PhoneType profilePhone;

        private int camCullingMask;
        private CameraClearFlags camClearFlags;
        private Color camColor;
        private HoloKit.CameraType oldCameraType;

        public Profile profile;
        private Profile.ModelType oldProfileModel;
        private Profile.PhoneType oldProfilePhone;

        private void Awake()
        {
            CreateAll();
            switch (cameraType)
            {
                case CameraType.AR:
                    SwitchToModeAR();
                    break;
                case CameraType.MR:
                    SwitchToModeMR();
                    break;
            }

            ChangeProfile();
            UpdateProfile();
        }

        void OnDestroy()
        {
            if (instance == this)
            {
                instance = null;
            }
        }

        private void Update()
        {
            if (oldCameraType != cameraType)
            {
                switch (cameraType)
                {
                    case CameraType.AR:
                        SwitchToModeAR();
                        break;
                    case CameraType.MR:
                        SwitchToModeMR();
                        break;
                }
                oldCameraType = cameraType;
            }

            if (oldProfileModel != profileModel || oldProfilePhone != profilePhone)
            {
                ChangeProfile();
                UpdateProfile();
            }
        }

        private void CreateAll()
        {
            camCullingMask = cameraCenter.cullingMask;
        }

        private void SwitchToModeAR()
        {
            holoKitOffset.gameObject.SetActive(false);
            cameraCenter.cullingMask = camCullingMask;
            cameraCenter.clearFlags = camClearFlags;
            cameraCenter.backgroundColor = camColor;
        }

        private void SwitchToModeMR()
        {
            holoKitOffset.gameObject.SetActive(true);
            cameraCenter.cullingMask = 0;
            cameraRight.cullingMask = camCullingMask;
            cameraLeft.cullingMask = camCullingMask;
            cameraCenter.clearFlags = CameraClearFlags.Color;
            cameraCenter.backgroundColor = Color.black;
        }

        private void ChangeProfile()
        {
            profile = Profile.GetProfile(profileModel);
            oldProfileModel = profileModel;
            oldProfilePhone = profilePhone;
        }

        private void UpdateProfile()
        {
            holoKitOffset.localPosition = profile.model.mrOffset + profile.phone.cameraOffset;
            cameraLeft.rect = profile.GetViewportRect(EyeSide.Left);
            cameraRight.rect = profile.GetViewportRect(EyeSide.Right);
            cameraLeft.fieldOfView = profile.model.fieldOfView;
            cameraRight.fieldOfView = profile.model.fieldOfView;
            cameraLeft.transform.localPosition = new Vector3(-profile.model.eyeDistance / 2f, 0f, 0f);
            cameraRight.transform.localPosition = new Vector3(profile.model.eyeDistance / 2f, 0f, 0f);
            postefRight.BarrelDistortionFactor = profile.model.distortion;
            postefLeft.BarrelDistortionFactor = profile.model.distortion;

            //Calc and update projection matrix
            float magnificationFactor = profile.model.lensLength / (profile.model.lensLength - profile.model.toScreenDist);
            float renderScale = 1f;
            float renderWidth = renderScale * profile.phone.screenWidth / 2f;
            float screenHeightRatio = 1f - (profile.phone.screenBottom / profile.phone.screenHeight);
            float renderHeight = renderScale * profile.phone.screenHeight * screenHeightRatio;
            float renderInnerEyeWidth = renderScale * profile.model.eyeDistance / 2f;
            float near = renderScale * profile.model.toEyeDist;
            float far = 1000f;
            //Debug.Log(string.Format("new {0} {1} {2} {3} {4}", magnificationFactor, renderScale, renderWidth, renderHeight, renderInnerEyeWidth, near, far));
            Matrix4x4 leftEyeProjectionMatrix = Matrix4x4.zero;
            leftEyeProjectionMatrix[0, 0] = 2.0f * near / renderWidth;
            leftEyeProjectionMatrix[1, 1] = 2.0f * near / renderHeight;
            leftEyeProjectionMatrix[0, 2] = (2f * renderInnerEyeWidth - renderWidth) / renderWidth;
            leftEyeProjectionMatrix[2, 2] = -(far + near) / (far - near);
            leftEyeProjectionMatrix[2, 3] = -(2.0f * far * near) / (far - near);
            leftEyeProjectionMatrix[3, 2] = -1.0f;
            Matrix4x4 rightEyeProjectionMatrix = leftEyeProjectionMatrix;
            rightEyeProjectionMatrix[0, 2] = (renderWidth - 2f * renderInnerEyeWidth) / renderWidth;
            cameraLeft.projectionMatrix = leftEyeProjectionMatrix;
            cameraRight.projectionMatrix = rightEyeProjectionMatrix;

            /*float[] rect = new float[4];
            rect[0] = -0.6153846f;
            rect[1] = 0.9652906f;
            rect[2] = 0.8205128f;
            rect[3] = -0.7692308f;
            Matrix4x4 leftEyeUndistortedProjection;
            Matrix4x4 rightEyeUndistortedProjection;
            leftEyeUndistortedProjection = MakeProjection(rect[0], rect[1], rect[2], rect[3], 1, 1000);
            rightEyeUndistortedProjection = leftEyeUndistortedProjection;
            rightEyeUndistortedProjection[0, 2] *= -1;*/

        }

        private static Matrix4x4 MakeProjection(float l, float t, float r, float b, float n, float f)
        {
            Matrix4x4 m = Matrix4x4.zero;
            m[0, 0] = 2 * n / (r - l);
            m[1, 1] = 2 * n / (t - b);
            m[0, 2] = (r + l) / (r - l);
            m[1, 2] = (t + b) / (t - b);
            m[2, 2] = (n + f) / (n - f);
            m[2, 3] = 2 * n * f / (n - f);
            m[3, 2] = -1;
            return m;
        }
    }
}
