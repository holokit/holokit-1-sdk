using System;
using System.Collections;
using UnityEngine;
using UnityEngine.XR.iOS;

namespace HoloKit
{

    public enum SeeThroughMode
    {
        Video = 0,
        HoloKit,
    }

    public class HoloKitCameraRigController : PhoneSpaceControllerBase
    {
        private static HoloKitCameraRigController instance;
        public static HoloKitCameraRigController Instance {
            get {
                if (instance == null)
                {
                    instance = FindObjectOfType<HoloKitCameraRigController>();
                }

                return instance;
            }
        }

#region Inspector Properties
        public SeeThroughMode SeeThroughMode;

        public bool DisplayCameraModeSwitchButton = true;

        public HoloKitKeyCode SeeThroughModeToggleKey = HoloKitKeyCode.None;

        [Header("Prefab variables. Do not change.")]
        [Range(0, 0.5f)]
        public float fovCenterOffset;

        public float phoneScreenHeight;
        public float phoneScreenWidth;
        public float fresnelLensFocalLength;
        public float screenToFresnelDistance;
        public float fresnelToEyeDistance;
        public float pupilDistance;

        public Camera CenterCamera;
        public Transform HoloKitOffset;
        public Camera LeftCamera;
        public Camera RightCamera;
        
#endregion

#region Private Properties
        private BarrelDistortion leftBarrel;
        private BarrelDistortion rightBarrel;

        private int centerCullingMask;

        private UnityARVideo arKitVideo;
#endregion

#region Getter/Setters for parameter tuning / loading. 
        public override float FOV
        {
            get
            {
                return LeftCamera.fieldOfView;
            }
            set
            {
                LeftCamera.fieldOfView = value;
                RightCamera.fieldOfView = value;
            }
        }

        public float MagnificationFactor
        {
            get
            {
                return fresnelLensFocalLength / (fresnelLensFocalLength - screenToFresnelDistance);
            }
        }


        public override float BarrelRadius
        {
            get
            {
                return leftBarrel.FovRadians;
            }
            set
            {
                leftBarrel.FovRadians = value;
                rightBarrel.FovRadians = value;
            }
        }

      
        public override float PupilDistance
        {
            get
            {
                return RightCamera.transform.localPosition.x - LeftCamera.transform.localPosition.x;
            }
            set
            {
                pupilDistance = value;
                float halfDist = value / 2;
                LeftCamera.transform.localPosition = new Vector3(-halfDist, 0, 0);
                RightCamera.transform.localPosition = new Vector3(halfDist, 0, 0);
            }
        }

        public override Vector3 CameraOffset
        {
            get
            {
                return HoloKitOffset.localPosition;
            }

            set
            {
                HoloKitOffset.localPosition = value;
            }
        }


        public override float PhoneScreenHeight
        {
            get
            {
                return phoneScreenHeight;
            }

            set
            {
                phoneScreenHeight = value;
            }
        }

        public override float PhoneScreenWidth
        {
            get
            {
                return phoneScreenWidth;
            }

            set
            {
                phoneScreenWidth = value;
            }
        }

        public override float FresnelLensFocalLength
        {
            get
            {
                return fresnelLensFocalLength;
            }

            set
            {
                fresnelLensFocalLength = value;
            }
        }

        public override float ScreenToFresnelDistance
        {
            get
            {
                return screenToFresnelDistance;
            }

            set
            {
                screenToFresnelDistance = value;
            }
        }

        public override float FresnelToEyeDistance
        {
            get
            {
                return fresnelToEyeDistance;
            }

            set
            {
                fresnelToEyeDistance = value;
            }
        }


        public override float FOVCenterOffset
        {
            get
            {
                return fovCenterOffset;
            }

            set
            {
                fovCenterOffset = value;
            }
        }

#endregion

        public Transform CurrentEyeCenter {
            get {
                return SeeThroughMode == SeeThroughMode.HoloKit ? HoloKitOffset : CenterCamera.transform;
            }
        }
        
        void UpdateProjectMatrix() {
            float renderWidth = MagnificationFactor * PhoneScreenHeight / 2;
            float renderHeight = MagnificationFactor * PhoneScreenWidth;
            float renderInnerEyeWidth = MagnificationFactor * PupilDistance / 2;
            float near = MagnificationFactor * FresnelToEyeDistance;
            float far = 1000;
            
            Matrix4x4 leftEyeProjectionMatrix = Matrix4x4.zero;
            leftEyeProjectionMatrix[0, 0] = 2.0F * near / renderWidth;
            leftEyeProjectionMatrix[1, 1] = 2.0F * near / renderHeight;
            leftEyeProjectionMatrix[0, 2] = (2 * renderInnerEyeWidth - renderWidth) / renderWidth;
            leftEyeProjectionMatrix[2, 2] =  -(far + near) / (far - near);
            leftEyeProjectionMatrix[2, 3] = -(2.0F * far * near) / (far - near);
            leftEyeProjectionMatrix[3, 2] = -1.0F;

            LeftCamera.projectionMatrix = leftEyeProjectionMatrix;

            Matrix4x4 rightEyeProjectionMatrix = leftEyeProjectionMatrix;
            rightEyeProjectionMatrix[0, 2] = (renderWidth - 2 * renderInnerEyeWidth) / renderWidth;
            RightCamera.projectionMatrix = rightEyeProjectionMatrix;

            Debug.Log("renderWidth:" + renderWidth);
            Debug.Log("renderHeight:" + renderWidth);
            Debug.Log("renderInnerEyeWidth:" + renderWidth);
            Debug.Log("near:" + near);
            Debug.Log("far:" + far);
        }

        void Start()
        {
            leftBarrel = LeftCamera.GetComponent<BarrelDistortion>();
            rightBarrel = RightCamera.GetComponent<BarrelDistortion>();
            centerCullingMask = CenterCamera.cullingMask;
            arKitVideo = CenterCamera.GetComponent<UnityARVideo>();

            HoloKitCalibration.LoadDefaultCalibration(this);
            UpdateProjectMatrix();
        }

        void OnGUI()
        {
            if (DisplayCameraModeSwitchButton) {
                if (GUI.Button(new Rect(Screen.width - 75, 0, 75, 75), "C"))
                {
                    SeeThroughMode = (SeeThroughMode == SeeThroughMode.Video) 
                        ? SeeThroughMode.HoloKit 
                        : SeeThroughMode.Video;
                }
            }
        }

        void Update()
        {
            LeftCamera.gameObject.SetActive(SeeThroughMode == SeeThroughMode.HoloKit);
            RightCamera.gameObject.SetActive(SeeThroughMode == SeeThroughMode.HoloKit);

            arKitVideo.enabled = (SeeThroughMode == SeeThroughMode.Video);
            CenterCamera.cullingMask = (SeeThroughMode == SeeThroughMode.Video) ? centerCullingMask : 0;

            if (SeeThroughMode == SeeThroughMode.HoloKit)
            {
                leftBarrel.Offset = fovCenterOffset;
                rightBarrel.Offset = -fovCenterOffset;
            }

            if (HoloKitInputManager.Instance.GetKeyDown(SeeThroughModeToggleKey)) 
            {
                SeeThroughMode = (SeeThroughMode == SeeThroughMode.Video)
                    ? SeeThroughMode.HoloKit
                    : SeeThroughMode.Video;
            }
        }

        void OnDestroy()
        {
            instance = null;
        }
    }
}
