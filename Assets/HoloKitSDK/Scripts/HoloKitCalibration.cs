using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HoloKit {
    public static class HoloKitCalibration {
        private static void loadiPhone55InchCalibration(HoloKitCameraRigController cameraRig) 
        {
            cameraRig.CameraOffset = new Vector3(0.069f, -0.082f, -0.082f);
            cameraRig.FOV = 49.7f;
            cameraRig.BarrelRadius = 1.69f;
            cameraRig.PupilDistance = 0.064f;
            cameraRig.FOVCenterOffset = 0.03f;
            Debug.Log("Calibration data loaded for 5.5 inch device. ");
        }

        private static void loadiPhone45InchCalibration(HoloKitCameraRigController cameraRig) 
        {
            cameraRig.CameraOffset = new Vector3(0.069f, -0.082f, -0.082f);
            cameraRig.FOV = 49.7f;
            cameraRig.BarrelRadius = 1.69f;
            cameraRig.PupilDistance = 0.064f; // TODO: Find a better value
            cameraRig.FOVCenterOffset = 0.03f;
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

                case UnityEngine.iOS.DeviceGeneration.iPhone6Plus:
                    loadiPhone55InchCalibration(cameraRig);
                    Debug.LogWarning("Your iOS device is not officially supported by HoloKitSDK.");
                    break;

                default:
                    loadiPhone45InchCalibration(cameraRig);
                    Debug.LogWarning("Your iOS device is not officially supported by HoloKitSDK.");
                    break;
            }
        }
    }
}
