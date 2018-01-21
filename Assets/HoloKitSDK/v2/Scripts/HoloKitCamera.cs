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
                    instance = FindObjectOfType<HoloKitCamera>();
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

            ChangeStartProfile();
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

        private void ChangeStartProfile()
        {
            profile = Profile.GetProfile(profileModel);
            oldProfileModel = profileModel;
            oldProfilePhone = profilePhone;
        }

        private void ChangeProfile()
        {
            profile = Profile.GetProfile(profileModel, profilePhone);
            oldProfileModel = profileModel;
            oldProfilePhone = profilePhone;
        }

        private void UpdateProfile()
        {
            holoKitOffset.localPosition = profile.model.mrOffset + profile.phone.cameraOffset;
            Rect viewportLeft = profile.GetViewportRect(EyeSide.Left);
            Rect viewportRight = profile.GetViewportRect(EyeSide.Right);
            cameraLeft.rect = viewportLeft;
            cameraRight.rect = viewportRight;
            cameraLeft.fieldOfView = profile.model.fieldOfView;
            cameraRight.fieldOfView = profile.model.fieldOfView;
            cameraLeft.transform.localPosition = new Vector3(-profile.model.eyeDistance / 2f, 0f, 0f);
            cameraRight.transform.localPosition = new Vector3(profile.model.eyeDistance / 2f, 0f, 0f);

            //Calc and update projection matrix
            Matrix4x4 leftEyeProjectionMatrix = Matrix4x4.zero;
            Matrix4x4 rightEyeProjectionMatrix = Matrix4x4.zero;
            // Old matrix system
            /*float magnificationFactor = profile.model.lensLength / (profile.model.lensLength - profile.model.toScreenDist);
            float renderScale = 1f;
            float renderWidth = renderScale * profile.phone.screenWidth / 2f;
            float screenHeightRatio = 1f - (profile.phone.screenBottom / profile.phone.screenHeight);
            float renderHeight = renderScale * profile.phone.screenHeight * screenHeightRatio;
            float renderInnerEyeWidth = renderScale * profile.model.eyeDistance / 2f;
            float near = cameraCenter.nearClipPlane;
            float far = cameraCenter.farClipPlane;
            leftEyeProjectionMatrix[0, 0] = 2.0f * near / renderWidth;
            leftEyeProjectionMatrix[1, 1] = 2.0f * near / renderHeight;
            leftEyeProjectionMatrix[0, 2] = (2f * renderInnerEyeWidth - renderWidth) / renderWidth;
            leftEyeProjectionMatrix[2, 2] = -(far + near) / (far - near);
            leftEyeProjectionMatrix[2, 3] = -(2.0f * far * near) / (far - near);
            leftEyeProjectionMatrix[3, 2] = -1.0f;
            rightEyeProjectionMatrix = leftEyeProjectionMatrix;
            rightEyeProjectionMatrix[0, 2] = (renderWidth - 2f * renderInnerEyeWidth) / renderWidth;
            cameraLeft.projectionMatrix = leftEyeProjectionMatrix;
            cameraRight.projectionMatrix = rightEyeProjectionMatrix;*/
            
            /*
            leftEyeProjectionMatrix = MakeProjection(viewportLeft.xMin, viewportLeft.yMin, viewportLeft.xMax, viewportLeft.yMax, cameraCenter.nearClipPlane, cameraCenter.farClipPlane);
            rightEyeProjectionMatrix = MakeProjection(viewportRight.xMin, viewportRight.yMin, viewportRight.xMax, viewportRight.yMax, cameraCenter.nearClipPlane, cameraCenter.farClipPlane);
            rightEyeProjectionMatrix[0, 2] *= -1f;
            cameraLeft.projectionMatrix = leftEyeProjectionMatrix;
            cameraRight.projectionMatrix = rightEyeProjectionMatrix;
            */
            postefRight.BarrelDistortionFactor = profile.model.distortion;
            postefLeft.BarrelDistortionFactor = profile.model.distortion;
            //postefLeft.HorizontalOffsetFactor = 0.5f - renderInnerEyeWidth / renderWidth;
            postefLeft.HorizontalOffsetFactor = 0f;
            postefLeft.VerticalOffsetFactor = 0f;
            //postefRight.HorizontalOffsetFactor = renderInnerEyeWidth / renderWidth - 0.5f;
            postefRight.HorizontalOffsetFactor = 0f;
            postefRight.VerticalOffsetFactor = 0f;
        }

        private Matrix4x4 MakeProjection(float left, float top, float right, float bottom, float near, float far)
        {
            Matrix4x4 m = Matrix4x4.zero;
            m[0, 0] = 2f * near / (right - left);
            m[1, 1] = 2f * near / (top - bottom);
            m[0, 2] = (right + left) / (right - left);
            m[1, 2] = (top + bottom) / (top - bottom);
            m[2, 2] = (near + far) / (near - far);
            m[2, 3] = 2f * near * far / (near - far);
            m[3, 2] = -1f;
            return m;
        }
    }
}
