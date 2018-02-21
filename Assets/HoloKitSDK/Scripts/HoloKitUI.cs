using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace HoloKit
{
    public class HoloKitUI : MonoBehaviour
    {
        [SerializeField]
        private Button bHoloKitModel;
        [SerializeField]
        private Text tHoloKitModel;
        [SerializeField]
        private string textHoloKit1 = "v1";
        [SerializeField]
        private string textHoloKitApple = "vA";

        [SerializeField]
        private Button bSeeMode;
        [SerializeField]
        private Text tSeeMode;
        [SerializeField]
        private string textAR = "AR";
        [SerializeField]
        private string textMR = "MR";
        [SerializeField]
        private GameObject splitter;

        private CameraType oldSeeMode = CameraType.AR;
        private Profile.ModelType oldHoloKitModel = Profile.ModelType.HoloKitv1;

        private void OnEnable()
        {
            bSeeMode.onClick.AddListener(WhenButtonSeeMode);
            bHoloKitModel.onClick.AddListener(WhenButtonHoloKitModel);
        }

        private void OnDisable()
        {
            bSeeMode.onClick.RemoveListener(WhenButtonSeeMode);
            bHoloKitModel.onClick.RemoveListener(WhenButtonHoloKitModel);
        }

        private void Start()
        {
            Localize();
        }

        private void Localize()
        {
            if (HoloKitCamera.Instance)
            {
                switch (HoloKitCamera.Instance.cameraType)
                {
                    case CameraType.AR:
                        tSeeMode.text = textAR;
                        splitter.gameObject.SetActive(false);
                        bHoloKitModel.gameObject.SetActive(false);
                        break;
                    case CameraType.MR:
                        tSeeMode.text = textMR;
                        splitter.gameObject.SetActive(true);
                        bHoloKitModel.gameObject.SetActive(true);
                        break;
                }
                oldSeeMode = HoloKitCamera.Instance.cameraType;

                switch (HoloKitCamera.Instance.profileModel)
                {
                    case Profile.ModelType.HoloKitApple:
                        tHoloKitModel.text = textHoloKitApple;
                        break;
                    default:
                        tHoloKitModel.text = textHoloKit1;
                        break;
                }
                oldHoloKitModel = HoloKitCamera.Instance.profileModel;
            }
        }

        private void Update()
        {
            if (HoloKitCamera.Instance && (HoloKitCamera.Instance.cameraType != oldSeeMode 
                                           || HoloKitCamera.Instance.profileModel != oldHoloKitModel))
            {
                Localize();
            }
        }

        private void WhenButtonSeeMode()
        {
            if (HoloKitCamera.Instance)
            {
                switch (HoloKitCamera.Instance.cameraType)
                {
                    case CameraType.AR:
                        HoloKitCamera.Instance.cameraType = CameraType.MR;
                        break;
                    case CameraType.MR:
                        HoloKitCamera.Instance.cameraType = CameraType.AR;
                        break;
                }
            }
            Localize();
        }

        private void WhenButtonHoloKitModel()
        {
            if (HoloKitCamera.Instance)
            {
                switch (HoloKitCamera.Instance.profileModel)
                {
                    case Profile.ModelType.HoloKitApple:
                        HoloKitCamera.Instance.profileModel = Profile.ModelType.HoloKitv1;
                        break;
                    default:
                        HoloKitCamera.Instance.profileModel = Profile.ModelType.HoloKitApple;
                        break;
                }
            }
            Localize();
        }
    }
}
