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
        public Mesh distortedMeshR;
        public Mesh distortedMeshL;
        public Camera cameraCenter;
        public ProfileHoloKit.ModelType profileModel;
        public ProfileHoloKit.PhoneType profilePhone;

        private Camera cameraLeft;
        private Camera cameraRight;
        private Transform holoKitOffset;
        private RenderTexture renderTextureR;
        private RenderTexture renderTextureL;
        private GameObject distortedRoot;
        private HoloKitDistortionPost distortedPostR;
        private HoloKitDistortionPost distortedPostL;
        private int camCullingMask;
        private CameraClearFlags camClearFlags;
        private Color camColor;
        private HoloKit.CameraType oldCameraType;

        private ProfileHoloKit profile;
        private ProfileHoloKit.ModelType oldProfileModel;
        private ProfileHoloKit.PhoneType oldProfilePhone;

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
            GameObject cgo, goCamR, goCamL;
            camCullingMask = cameraCenter.cullingMask;
            camClearFlags = cameraCenter.clearFlags;
            camColor = cameraCenter.backgroundColor;
            //Creating offset
            cgo = new GameObject("HoloKitOffset");
            holoKitOffset = cgo.GetComponent<Transform>();

            
            //Creating right eye camera
            goCamR = Instantiate(cameraCenter.gameObject, holoKitOffset);
            goCamR.name = "cameraRight";
            goCamR.tag = "Untagged";
            goCamR.transform.localPosition = new Vector3(0.032f, 0f, 0f);
            cameraRight = goCamR.GetComponent<Camera>();
            renderTextureR = new RenderTexture(1024, 1024, 24, RenderTextureFormat.ARGB32, RenderTextureReadWrite.Default);
            cameraRight.targetTexture = renderTextureR;
            cameraRight.cullingMask = 0;
            
            //Creating left eye camera
            goCamL = Instantiate(cameraCenter.gameObject, holoKitOffset);
            goCamL.name = "cameraLeft";
            goCamL.tag = "Untagged";
            goCamL.transform.localPosition = new Vector3(-0.032f, 0f, 0f);
            cameraLeft = goCamL.GetComponent<Camera>();
            renderTextureL = new RenderTexture(1024, 1024, 24, RenderTextureFormat.ARGB32, RenderTextureReadWrite.Default);
            cameraLeft.targetTexture = renderTextureL;
            cameraLeft.cullingMask = 0;

            holoKitOffset.parent = cameraCenter.transform;

            //Creating Distorted objects holder
            distortedRoot = new GameObject("HoloKitDistorted");
            //Creating Distorted right camera
            cgo = new GameObject("HoloKitDistortedR");
            cgo.transform.parent = distortedRoot.transform;
            distortedPostR = cgo.AddComponent<HoloKitDistortionPost>();
            distortedPostR.Initialize(distortedMeshR, renderTextureR, HoloKitEyeSide.Right);
            //Creating Distorted left camera
            cgo = new GameObject("HoloKitDistortedL");
            cgo.transform.parent = distortedRoot.transform;
            distortedPostL = cgo.AddComponent<HoloKitDistortionPost>();
            distortedPostL.Initialize(distortedMeshL, renderTextureL, HoloKitEyeSide.Left);
        }

        private void SwitchToModeAR()
        {
            holoKitOffset.gameObject.SetActive(false);
            distortedRoot.SetActive(false);
            cameraCenter.cullingMask = camCullingMask;
            cameraCenter.clearFlags = camClearFlags;
            cameraCenter.backgroundColor = camColor;
        }

        private void SwitchToModeMR()
        {
            holoKitOffset.gameObject.SetActive(true);
            distortedRoot.SetActive(true);
            cameraCenter.cullingMask = 0;
            cameraRight.cullingMask = camCullingMask;
            cameraLeft.cullingMask = camCullingMask;
            cameraCenter.clearFlags = CameraClearFlags.Color;
            cameraCenter.backgroundColor = Color.black;
        }

        private void ChangeProfile()
        {
            profile = ProfileHoloKit.GetProfile(profileModel, profilePhone);
            oldProfileModel = profileModel;
            oldProfilePhone = profilePhone;
        }

        private void UpdateProfile()
        {
            holoKitOffset.localPosition = profile.model.mrOffset;
            distortedPostR.addPosition = new Vector3(profile.phone.separation, 0f, 0.2f);
            distortedPostL.addPosition = new Vector3(-profile.phone.separation, 0f, 0.2f);
            Rect profileRect = profile.GetViewportRect();

            distortedPostR.mCamera.rect = new Rect(
                0.5f,
                profileRect.y,
                profileRect.width / 2f,
                profileRect.height);
            distortedPostR.mCamera.orthographicSize = 0.5f; // Mathf.Min((profileRect.width / profileRect.height), (profileRect.height / profileRect.width));

            distortedPostL.mCamera.rect = new Rect(
                0.5f - (profileRect.width / 2f),
                profileRect.y,
                profileRect.width / 2f,
                profileRect.height);
            distortedPostL.mCamera.orthographicSize = 0.5f; // Mathf.Min((profileRect.width / profileRect.height), (profileRect.height / profileRect.width));

        }
    }
}
