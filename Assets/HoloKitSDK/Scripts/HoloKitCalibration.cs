using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HoloKit {
    public static class HoloKitCalibration {
		private static void loadiPhone55InchCalibration(HoloKitCameraRigController cameraRig) 
        {
            cameraRig.CameraOffset = new Vector3(0.06960f, -0.10945f, -0.09065f - 0.012f);
            cameraRig.PupilDistance = 0.064f;
            cameraRig.PhoneScreenHeight = 0.122f;
            cameraRig.PhoneScreenWidth = 0.068f;
            cameraRig.FresnelLensFocalLength = 0.090f;
            cameraRig.ScreenToFresnelDistance = 0.0762f;
            cameraRig.FresnelToEyeDistance = 0.085f + 0.012f;
            cameraRig.ViewportHeightRatio = 0.889f;

            cameraRig.RedDistortionFactor = 0f;
            cameraRig.GreenDistortionFactor = 0f;
            cameraRig.BlueDistortionFactor = 0f;
            cameraRig.BarrelDistortionFactor = 1.6f;

            Debug.Log("Calibration data loaded for 5.5 inch device. ");
        }

        private static void loadiPhone45InchCalibration(HoloKitCameraRigController cameraRig) 
        {
            cameraRig.CameraOffset = new Vector3(0.05915f, -0.09893f, -0.09025f - 0.012f);
            cameraRig.PupilDistance = 0.064f;
            cameraRig.PhoneScreenHeight = 0.104f;
            cameraRig.PhoneScreenWidth = 0.058f;
            cameraRig.FresnelLensFocalLength = 0.090f;
            cameraRig.ScreenToFresnelDistance = 0.0762f;
            cameraRig.FresnelToEyeDistance = 0.085f + 0.012f;
            cameraRig.ViewportHeightRatio = 1.0f;

            cameraRig.RedDistortionFactor = 0f;
            cameraRig.GreenDistortionFactor = 0f;
            cameraRig.BlueDistortionFactor = 0f;
            cameraRig.BarrelDistortionFactor = 1.6f;

            Debug.Log("Calibration data loaded for 4.5 inch device. ");
        }

        private static void loadZenPhoneARTangoCalibration(HoloKitCameraRigController cameraRig) 
        {
            cameraRig.CameraOffset = new Vector3(0.022f, -0.09313f, -0.08595f - 0.012f);
            cameraRig.PupilDistance = 0.064f;
            cameraRig.PhoneScreenHeight = 0.125f;
            cameraRig.PhoneScreenWidth = 0.070f;
            cameraRig.FresnelLensFocalLength = 0.090f;
            cameraRig.ScreenToFresnelDistance = 0.0762f;
            cameraRig.FresnelToEyeDistance = 0.085f + 0.012f;
            cameraRig.ViewportHeightRatio = 0.889f;

            cameraRig.RedDistortionFactor = 0f;
            cameraRig.GreenDistortionFactor = 0f;
            cameraRig.BlueDistortionFactor = 0f;
            cameraRig.BarrelDistortionFactor = 1.6f;

            Debug.Log("Calibration data loaded for ASUS ZenFone AR. ");
        }
        
        private static void loadLenovoPhab2ProTangoCalibration(HoloKitCameraRigController cameraRig) 
        {
            cameraRig.CameraOffset = new Vector3(0.04045f, -0.09348f, -0.08459f - 0.012f);
            cameraRig.PupilDistance = 0.064f;
            cameraRig.PhoneScreenHeight = 0.145f;
            cameraRig.PhoneScreenWidth = 0.08f;
            cameraRig.FresnelLensFocalLength = 0.090f;
            cameraRig.ScreenToFresnelDistance = 0.0762f;
            cameraRig.FresnelToEyeDistance = 0.085f + 0.012f;
            cameraRig.ViewportHeightRatio = 0.889f;

            cameraRig.RedDistortionFactor = 0f;
            cameraRig.GreenDistortionFactor = 0f;
            cameraRig.BlueDistortionFactor = 0f;
            cameraRig.BarrelDistortionFactor = 1.6f;

			Debug.Log("Calibration data loaded for Lenovo Phab2 Pro. ");
        }

		private static void loadGooglePixelCalibration(HoloKitCameraRigController cameraRig) 
		{
			cameraRig.CameraOffset = new Vector3(0.05915f, -0.09893f, -0.09025f - 0.012f);
            cameraRig.PupilDistance = 0.064f;
            cameraRig.PhoneScreenHeight = 0.104f;
            cameraRig.PhoneScreenWidth = 0.058f;
            cameraRig.FresnelLensFocalLength = 0.090f;
            cameraRig.ScreenToFresnelDistance = 0.0762f;
            cameraRig.FresnelToEyeDistance = 0.085f + 0.012f;
            cameraRig.ViewportHeightRatio = 1.0f;

            cameraRig.RedDistortionFactor = 0f;
            cameraRig.GreenDistortionFactor = 0f;
            cameraRig.BlueDistortionFactor = 0f;
            cameraRig.BarrelDistortionFactor = 1.6f;

			Debug.Log("Calibration data loaded for Google Pixel. ");
		}

		private static void loadGooglePixelXLCalibration(HoloKitCameraRigController cameraRig) 
		{
			cameraRig.CameraOffset = new Vector3(0.022f, -0.09313f, -0.08595f - 0.012f);
			cameraRig.PupilDistance = 0.064f;
			cameraRig.PhoneScreenHeight = 0.121f;
			cameraRig.PhoneScreenWidth = 0.065f;
			cameraRig.FresnelLensFocalLength = 0.090f;
			cameraRig.ScreenToFresnelDistance = 0.0762f;
			cameraRig.FresnelToEyeDistance = 0.085f + 0.012f;
			cameraRig.ViewportHeightRatio = 0.889f;

			cameraRig.RedDistortionFactor = 0f;
			cameraRig.GreenDistortionFactor = 0f;
			cameraRig.BlueDistortionFactor = 0f;
			cameraRig.BarrelDistortionFactor = 1.6f;

			Debug.Log("Calibration data loaded for Google Pixel XL. ");
		}

		private static void loadSamsungS8Calibration(HoloKitCameraRigController cameraRig) 
		{
			cameraRig.CameraOffset = new Vector3(0.01590f, -0.09188f, -0.07414f - 0.012f);
			cameraRig.PupilDistance = 0.064f;
			cameraRig.PhoneScreenHeight = 0.125f;
			cameraRig.PhoneScreenWidth = 0.070f;
			cameraRig.FresnelLensFocalLength = 0.090f;
			cameraRig.ScreenToFresnelDistance = 0.0762f;
			cameraRig.FresnelToEyeDistance = 0.085f + 0.012f;
			cameraRig.ViewportHeightRatio = 0.889f;

			cameraRig.RedDistortionFactor = 0f;
			cameraRig.GreenDistortionFactor = 0f;
			cameraRig.BlueDistortionFactor = 0f;
			cameraRig.BarrelDistortionFactor = 1.6f;

			Debug.Log("Calibration data loaded for Samsung S8. ");
		}


        public static void LoadDefaultCalibration(HoloKitCameraRigController cameraRig) {
            
			#if UNITY_IOS
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
			loadiPhone55InchCalibration(cameraRig);
			Debug.LogWarning("Your iOS device is not officially supported by HoloKitSDK.");
			break;
			}
			#elif UNITY_ANDROID

			Debug.Log("SystemInfo.deviceModel:" + SystemInfo.deviceModel);

            switch (SystemInfo.deviceModel) {
				case "asus ASUS_A002":
                    loadZenPhoneARTangoCalibration(cameraRig);
                    break;
				case "LENOVO Lenovo PB2-690Y":
                    loadLenovoPhab2ProTangoCalibration(cameraRig);
                    break;
				case "Google Pixel":
					loadGooglePixelCalibration(cameraRig);
					break;
				case "Google Pixel XL":
					loadGooglePixelXLCalibration(cameraRig);
					break;
				case "Samsung S8":
					loadSamsungS8Calibration(cameraRig);
					break;
                default:
                    loadZenPhoneARTangoCalibration(cameraRig);
                    Debug.LogWarning("Your Android device is not officially supported by HoloKitSDK.");
                    break;
            }
			#endif


        }
    }
}
