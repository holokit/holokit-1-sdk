using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

namespace HoloKit {
	public class CalibrationTuner : MonoBehaviour {
		public CalibrationParam paramPrefab;

        public PhoneSpaceControllerBase phoneSpaceController;

        public Text deltaLabel;
        public Text fpsLabel;

        private List<CalibrationParam> calibrationParams = new List<CalibrationParam>();

        private List<int> deltaPows = new List<int>();

        private const int deltaBase = 10;

        private float fpsDeltaTime = 0.0f;

        void Start()
        {
            if (phoneSpaceController == null)
            {
                phoneSpaceController = FindObjectOfType<PhoneSpaceControllerBase>();
            }

            CreateParam("Offset X",
                () => phoneSpaceController.CameraOffset.x,
                (value) =>
                {
                    Vector3 localPos = phoneSpaceController.CameraOffset;
                    phoneSpaceController.CameraOffset = new Vector3(value, localPos.y, localPos.z);
                },
                deltaPow: -2

            );

            CreateParam("Offset Y",
                () => phoneSpaceController.CameraOffset.y,
                (value) =>
                {
                    Vector3 localPos = phoneSpaceController.CameraOffset;
                    phoneSpaceController.CameraOffset = new Vector3(localPos.x, value, localPos.z);
                },
                deltaPow: -2

            );

            CreateParam("Offset Z",
                () => phoneSpaceController.CameraOffset.z,
                (value) =>
                {
                    Vector3 localPos = phoneSpaceController.CameraOffset;
                    phoneSpaceController.CameraOffset = new Vector3(localPos.x, localPos.y, value);
                },
                deltaPow: -2
            );

            CreateParam("RedDistortionFactor",
                () => phoneSpaceController.RedDistortionFactor,
                (value) =>
                {
                    phoneSpaceController.RedDistortionFactor = value;
                },
                deltaPow: -3
            );

            CreateParam("BlueDistortionFactor",
                () => phoneSpaceController.BlueDistortionFactor,
                (value) =>
                {
                    phoneSpaceController.BlueDistortionFactor = value;
                },
                deltaPow: -3
            );

            CreateParam("GreenDistortionFactor",
                () => phoneSpaceController.GreenDistortionFactor,
                (value) =>
                {
                    phoneSpaceController.GreenDistortionFactor = value;
                },
                deltaPow: -3
            );

            CreateParam("BarrelDistortionFactor",
                () => phoneSpaceController.BarrelDistortionFactor,
                (value) =>
                {
                    phoneSpaceController.BarrelDistortionFactor = value;
                },
                deltaPow: -2
            );

            CreateParam("IPD",
                () => phoneSpaceController.PupilDistance,
                (value) =>
                {
                    phoneSpaceController.PupilDistance = value;
                },
                deltaPow: -3
            );

            SelectedIndex = 0;
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.DownArrow) ||
                HoloKitInputManager.Instance.GetKeyDown(HoloKitKeyCode.S))
            {
                SelectedIndex = (SelectedIndex + 1) % calibrationParams.Count;
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow) ||
              HoloKitInputManager.Instance.GetKeyDown(HoloKitKeyCode.W))
            {
                int selected = (SelectedIndex - 1) % calibrationParams.Count;
                if (selected < 0)
                {
                    selected += calibrationParams.Count;
                }
                SelectedIndex = selected;
            }
            else if (Input.GetKeyDown(KeyCode.Period) ||
              HoloKitInputManager.Instance.GetKeyDown(HoloKitKeyCode.Q))
            {
                deltaPows[SelectedIndex]++;
            }
            else if (Input.GetKeyDown(KeyCode.Comma) ||
              HoloKitInputManager.Instance.GetKeyDown(HoloKitKeyCode.E))
            {
                deltaPows[SelectedIndex]--;
            }
            else if (Input.GetKey(KeyCode.RightArrow) ||
              HoloKitInputManager.Instance.GetKeyDown(HoloKitKeyCode.D))
            {
                CalibrationParam param = calibrationParams[SelectedIndex];
                float value = param.ValueOnUpdate();
                value += Mathf.Pow(deltaBase, deltaPows[SelectedIndex]);
                param.SetValue(value);
            }
            else if (Input.GetKey(KeyCode.LeftArrow) ||
              HoloKitInputManager.Instance.GetKeyDown(HoloKitKeyCode.A))
            {
                CalibrationParam param = calibrationParams[SelectedIndex];
                float value = param.ValueOnUpdate();
                value -= Mathf.Pow(deltaBase, deltaPows[SelectedIndex]);
                param.SetValue(value);
            }

            deltaLabel.text = string.Format("Delta = {0}", Mathf.Pow(deltaBase, deltaPows[SelectedIndex]));

            updateFps();
        }

        private void CreateParam(string name, Func<float> valueOnUpdate, Action<float> setValue, int deltaPow = -1)
        {
            CalibrationParam param = Instantiate(paramPrefab);
            param.name = name;
            param.ValueOnUpdate = valueOnUpdate;
            param.SetValue = setValue;

            param.transform.SetParent(transform);

            calibrationParams.Add(param);

            deltaPows.Add(deltaPow);
        }

        private void updateFps()
        {
            fpsDeltaTime += (Time.deltaTime - fpsDeltaTime) * 0.1f;
            float msec = fpsDeltaTime * 1000.0f;
            float fps = 1.0f / fpsDeltaTime;

            if (fpsLabel != null)
            {
                fpsLabel.text = string.Format("{0:0.0} ms ({1:0.} fps)", msec, fps);
            }
        }

        private int selectedIndex;
        public int SelectedIndex
        {
            get
            {
                return selectedIndex;
            }

            set
            {
                calibrationParams[selectedIndex].IsHighlighted = false;
                selectedIndex = value;
                calibrationParams[selectedIndex].IsHighlighted = true;
            }
        }
    }
}