using System;
using System.Collections;
using UnityEngine;
using UnityEngine.XR.iOS;
using UnityEngine.XR;


namespace HoloKit
{
	#if UNITY_ANDROID
	using UnityTango = GoogleAR.UnityNative;
	#endif

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
        public float viewportHeightRatio;
        public float redDistortionFactor;
        public float greenDistortionFactor;        
        public float blueDistortionFactor;
        public float barrelDistortionFactor;

        public Camera CenterCamera;
        public Transform HoloKitOffset;
        public Camera LeftCamera;
        public Camera RightCamera;
        
#endregion

#region Private Properties
        private BarrelDistortion leftBarrel;
        private BarrelDistortion rightBarrel;

        private int centerCullingMask;


		private Texture2D rectTexture;
		private GUIStyle rectStyle;

#endregion

#region Getter/Setters for parameter tuning / loading. 
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

        public override float ViewportHeightRatio
        {
            get
            {
                return viewportHeightRatio;
            }

            set
            {
                viewportHeightRatio = value;
            }
        }

        public override float RedDistortionFactor
        {
            get
            {
                return redDistortionFactor;
            }

            set
            {
                redDistortionFactor = value;
            }
        }

        public override float GreenDistortionFactor
        {
            get
            {
                return greenDistortionFactor;
            }

            set
            {
                greenDistortionFactor = value;
            }
        }

        public override float BlueDistortionFactor
        {
            get
            {
                return blueDistortionFactor;
            }

            set
            {
                blueDistortionFactor = value;
            }
        }

        public override float BarrelDistortionFactor
        {
            get
            {
                return barrelDistortionFactor;
            }

            set
            {
                barrelDistortionFactor = value;
            }
        }
        
#endregion

        public Transform CurrentEyeCenter {
            get {
                return SeeThroughMode == SeeThroughMode.HoloKit ? HoloKitOffset : CenterCamera.transform;
            }
        }
        
        void UpdateProjectMatrix() {
            float magnificationFactor = fresnelLensFocalLength / (fresnelLensFocalLength - screenToFresnelDistance);
            float renderScale = 1;
            float renderWidth = renderScale * PhoneScreenHeight / 2;
            float renderHeight = renderScale * PhoneScreenWidth * ViewportHeightRatio;
            float renderInnerEyeWidth = renderScale * PupilDistance / 2;
            float near = renderScale * FresnelToEyeDistance;
            float far = 1000;
            
            Matrix4x4 leftEyeProjectionMatrix = Matrix4x4.zero;
            leftEyeProjectionMatrix[0, 0] = 2.0F * near / renderWidth;
            leftEyeProjectionMatrix[1, 1] = 2.0F * near / renderHeight;
            leftEyeProjectionMatrix[0, 2] = (2 * renderInnerEyeWidth - renderWidth) / renderWidth;
            leftEyeProjectionMatrix[2, 2] =  -(far + near) / (far - near);
            leftEyeProjectionMatrix[2, 3] = -(2.0F * far * near) / (far - near);
            leftEyeProjectionMatrix[3, 2] = -1.0F;

            Matrix4x4 rightEyeProjectionMatrix = leftEyeProjectionMatrix;
            rightEyeProjectionMatrix[0, 2] = (renderWidth - 2 * renderInnerEyeWidth) / renderWidth;

            LeftCamera.projectionMatrix = leftEyeProjectionMatrix;
            LeftCamera.rect = new Rect(0.0f, 0.0f, 0.5f, ViewportHeightRatio);
            RightCamera.projectionMatrix = rightEyeProjectionMatrix;
            RightCamera.rect = new Rect(0.5f, 0.0f, 0.5f, ViewportHeightRatio);

            leftBarrel.RedDistortionFactor = RedDistortionFactor;
            leftBarrel.GreenDistortionFactor = GreenDistortionFactor;
            leftBarrel.BlueDistortionFactor = BlueDistortionFactor;
            leftBarrel.BarrelDistortionFactor = BarrelDistortionFactor;
            leftBarrel.VerticalOffsetFactor = 0f;
            leftBarrel.HorizontalOffsetFactor = 0.5f - renderInnerEyeWidth / renderWidth;

            rightBarrel.RedDistortionFactor = RedDistortionFactor;
            rightBarrel.GreenDistortionFactor = GreenDistortionFactor;
            rightBarrel.BlueDistortionFactor = BlueDistortionFactor;
            rightBarrel.BarrelDistortionFactor = BarrelDistortionFactor;
            rightBarrel.VerticalOffsetFactor = 0f;
            rightBarrel.HorizontalOffsetFactor = renderInnerEyeWidth / renderWidth - 0.5f;
        }

        void Start()
        {
            leftBarrel = LeftCamera.GetComponent<BarrelDistortion>();
            rightBarrel = RightCamera.GetComponent<BarrelDistortion>();
            centerCullingMask = CenterCamera.cullingMask;


            HoloKitCalibration.LoadDefaultCalibration(this);
            UpdateProjectMatrix();

			rectTexture = new Texture2D(1, 1);
			rectTexture.SetPixel(0, 0, Color.black);
			rectTexture.Apply();
			rectStyle = new GUIStyle();
			rectStyle.normal.background = rectTexture;
        }

        void OnGUI()
        {
			if (SeeThroughMode == SeeThroughMode.HoloKit) {
				GUI.Box(new Rect(0, 0, Screen.width, Screen.height * (1 - 16f / 2f / 9f)), GUIContent.none, rectStyle);
			}

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

			bool arVideoEnabled = (SeeThroughMode == SeeThroughMode.Video);
			CenterCamera.cullingMask = arVideoEnabled ? centerCullingMask : 0;

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
