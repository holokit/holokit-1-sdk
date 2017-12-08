using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace HoloKit
{
    public class HoloKitUISeeMode : MonoBehaviour
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

        private SeeThroughMode oldSeeMode = SeeThroughMode.AR;

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
            if (HoloKitCameraRigController.Instance)
            {
                switch (HoloKitCameraRigController.Instance.SeeThroughMode)
                {
                    case SeeThroughMode.AR:
                        tSeeMode.text = textAR;
                        splitter.gameObject.SetActive(false);
                        break;
                    case SeeThroughMode.MR:
                        tSeeMode.text = textMR;
                        splitter.gameObject.SetActive(true);
                        break;
                }
                oldSeeMode = HoloKitCameraRigController.Instance.SeeThroughMode;
            }
        }

        private void Update()
        {
            if (HoloKitCameraRigController.Instance && HoloKitCameraRigController.Instance.SeeThroughMode != oldSeeMode)
            {
                Localize();
            }
        }

        private void WhenButtonSeeMode()
        {
            if (HoloKitCameraRigController.Instance)
            {
                switch (HoloKitCameraRigController.Instance.SeeThroughMode)
                {
                    case SeeThroughMode.AR:
                        HoloKitCameraRigController.Instance.SeeThroughMode = SeeThroughMode.MR;
                        break;
                    case SeeThroughMode.MR:
                        HoloKitCameraRigController.Instance.SeeThroughMode = SeeThroughMode.AR;
                        break;
                }
            }
            Localize();
        }
    }
}
