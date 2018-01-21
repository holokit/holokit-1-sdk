using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace HoloKit
{
    public class HoloKitUI : MonoBehaviour
    {
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

        private void OnEnable()
        {
            bSeeMode.onClick.AddListener(WhenButtonSeeMode);
        }

        private void OnDisable()
        {
            bSeeMode.onClick.RemoveListener(WhenButtonSeeMode);
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
                        break;
                    case CameraType.MR:
                        tSeeMode.text = textMR;
                        splitter.gameObject.SetActive(true);
                        break;
                }
                oldSeeMode = HoloKitCamera.Instance.cameraType;
            }
        }

        private void Update()
        {
            if (HoloKitCamera.Instance && HoloKitCamera.Instance.cameraType != oldSeeMode)
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
    }
}
