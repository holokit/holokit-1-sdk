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

        public SeeThroughMode SeeThroughMode;

        [Range(0, 0.5f)]
        public float fovCenterOffset;

        public Camera CenterCamera;
        public Transform HoloKitOffset;
        public Camera LeftCamera;
        public Camera RightCamera;

        private BarrelDistortion leftBarrel;
        private BarrelDistortion rightBarrel;

        private int centerCullingMask;

        private UnityARVideo arKitVideo;

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

        public Transform CurrentEyeCenter {
            get {
                return SeeThroughMode == SeeThroughMode.HoloKit ? HoloKitOffset : CenterCamera.transform;
            }
        }

        void Start()
        {
            leftBarrel = LeftCamera.GetComponent<BarrelDistortion>();
            rightBarrel = RightCamera.GetComponent<BarrelDistortion>();
            centerCullingMask = CenterCamera.cullingMask;
            arKitVideo = CenterCamera.GetComponent<UnityARVideo>();
        }

        void OnGUI()
        {
            if (GUI.Button(new Rect(Screen.width - 75, 0, 75, 75), "C"))
            {
                SeeThroughMode = (SeeThroughMode == SeeThroughMode.Video) 
                    ? SeeThroughMode.HoloKit 
                    : SeeThroughMode.Video;
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
        }

        void OnDestroy()
        {
            instance = null;
        }
    }
}
