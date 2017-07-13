using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HoloKit {
    public static class HoloKitCalibration {
        private static void loadiPhone55InchCalibration(HoloKitCameraRigController cameraRig) 
        {
            cameraRig.CameraOffset = new Vector3(0.06958f, -0.1037f, -0.090f);
            cameraRig.FOV = 49f;
            cameraRig.FOVCenterOffset = 0f;

            cameraRig.PupilDistance = 0.064f;
            cameraRig.PhoneScreenHeight = 0.122f;
            cameraRig.PhoneScreenWidth = 0.068f;
            cameraRig.FresnelLensFocalLength = 0.090f;
            cameraRig.ScreenToFresnelDistance = 0.0745f;
            cameraRig.FresnelToEyeDistance = 0.083f;

            Debug.Log("Calibration data loaded for 5.5 inch device. ");
        }

        private static void loadiPhone45InchCalibration(HoloKitCameraRigController cameraRig) 
        {
            cameraRig.CameraOffset = new Vector3(0.069f, -0.0937f, -0.090f);
            cameraRig.FOV = 49f;
            cameraRig.FOVCenterOffset = 0f;
            cameraRig.PupilDistance = 0.064f;
            cameraRig.PhoneScreenHeight = 0.104f;
            cameraRig.PhoneScreenWidth = 0.058f;
            cameraRig.FresnelLensFocalLength = 0.090f;
            cameraRig.ScreenToFresnelDistance = 0.0745f;
            cameraRig.FresnelToEyeDistance = 0.083f;

            Debug.Log("Calibration data loaded for 4.5 inch device. ");
        }

        public static void LoadDefaultCalibration(HoloKitCameraRigController cameraRig) {
            
            var deviceGen = UnityEngine.iOS.Device.generation;

            switch (deviceGen) {
                case UnityEngine.iOS.DeviceGeneration.iPhone6S:
                case UnityEngine.iOS.DeviceGeneration.iPhone7:
                    loadiPhone45InchCalibration(cameraRig);
                    break;

                case UnityEngine.iOS.DeviceGeneration.iPhone6SPlus:
                case UnityEngine.iOS.DeviceGeneration.iPhone7Plus:
                    loadiPhone55InchCalibration(cameraRig);
                    break;

                default:
                    loadiPhone45InchCalibration(cameraRig);
                    Debug.LogWarning("Your iOS device is not officially supported by HoloKitSDK.");
                    break;
            }
        }
    }
}
