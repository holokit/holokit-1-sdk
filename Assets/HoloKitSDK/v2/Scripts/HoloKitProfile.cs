using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HoloKit
{
    public class Profile
    {
        /// <summary>
        /// Models of the supported phones
        /// </summary>
        public enum PhoneType : int
        {
            Default = 0,
            Unknown = 1,
            AppleiPhone6S = 100,
            AppleiPhone7 = 101,
            AppleiPhone7Plus = 102,
            AppleiPhone8 = 103,
            AppleiPhone8Plus = 104,
            AppleiPhoneX = 105,
            GooglePixel = 200,
            GooglePixelXL = 201,
            GooglePixel2 = 202,
            GooglePixel2XL = 203,
            SamsungS8 = 204
        }

        /// <summary>
        /// HoloKit HMD models
        /// </summary>
        public enum ModelType : int
        {
            Default = 0,
            HoloKitv1 = 10,
            HoloKitNetEase = 11,
            HoloKitApple = 12
        }

        [System.Serializable]
        public struct Phone
        {
            /// <summary>
            /// The long screen edge of the phone
            /// </summary>
            public float screenWidth;
            /// <summary>
            /// The short screen edge of the phone
            /// </summary>
            public float screenHeight;
            /// <summary>
            /// The distance from the bottom of display area to the bottom of the holokit phone holder. (in meters)
            /// </summary>
            public float screenBottom;
            /// <summary>
            /// The 3D offset vector from center of the camera to the center of the display area. (in meters)
            /// </summary>
            public Vector3 cameraOffset;
        }

        [System.Serializable]
        public struct Model
        {
            /// <summary>
            /// Distance beetween eyes
            /// </summary>
            public float eyeDistance;
            /// <summary>
            /// 3D offset from the bottom of the holokit phone holder to the center of two eyes.
            /// </summary>
            public Vector3 mrOffset;
            /// <summary>
            /// Eye camera Field of view
            /// </summary>
            public float fieldOfView;
            /// <summary>
            /// Distortion intensity
            /// </summary>
            public float distortion;
            /// <summary>
            /// Eye view area width
            /// </summary>
            public float viewWidth;
            /// <summary>
            /// Eye view area height
            /// </summary>
            public float viewHeight;
            /// <summary>
            /// Fresnel Lens Focal Length
            /// </summary>
            public float lensLength;
            /// <summary>
            /// Screen To Fresnel Distance
            /// </summary>
            public float toScreenDist;
            /// <summary>
            /// Fresnel To Eye Distance
            /// </summary>
            public float toEyeDist;
        }

        #region HoloKit Model Parameters
        private static readonly Model ModelHoloKitv1 = new Model
        {
            eyeDistance = 0.064f,
            mrOffset = new Vector3(0f, 0.04f, 0f),
            fieldOfView = 49.7f,
            distortion = 1.7f,
            viewWidth = 0.06f,
            viewHeight = 0.06f,
            lensLength = 0.090f,
            toScreenDist = 0.0762f,
            toEyeDist = 0.085f + 0.012f
        };
        private static readonly Model ModelHoloKitNetEase = new Model
        {
            eyeDistance = 0.064f,
            mrOffset = new Vector3(0f, 0.04f, 0f),
            fieldOfView = 49.7f,
            distortion = 1.7f,
            viewWidth = 0.06f,
            viewHeight = 0.06f,
            lensLength = 0.090f,
            toScreenDist = 0.0762f,
            toEyeDist = 0.085f + 0.012f
        };
        private static readonly Model ModelHoloKitApple = new Model
        {
            eyeDistance = 0.064f,
            mrOffset = new Vector3(0f, 0.04f, 0f),
            fieldOfView = 49.7f,
            distortion = 1.7f,
            viewWidth = 0.06f,
            viewHeight = 0.06f,
            lensLength = 0.090f,
            toScreenDist = 0.0762f,
            toEyeDist = 0.085f + 0.012f
        };
        #endregion

        #region Phone Parameters
        private static readonly Phone PhoneAppleiPhone6S = new Phone
        {
            screenWidth = 0.104f,
            screenHeight = 0.058f,
            screenBottom = 0f,
            cameraOffset = new Vector3(-0.06f, 0.025f, 0f)
        };
        private static readonly Phone PhoneAppleiPhone7 = new Phone
        {
            screenWidth = 0.104f,
            screenHeight = 0.058f,
            screenBottom = 0f,
            cameraOffset = new Vector3(-0.06f, 0.025f, 0f)
        };
        private static readonly Phone PhoneAppleiPhone7Plus = new Phone
        {
            screenWidth = 0.122f,
            screenHeight = 0.068f,
            screenBottom = 0.005f,
            cameraOffset = new Vector3(-0.07f, 0.035f, 0f)
        };
        private static readonly Phone PhoneAppleiPhone8 = new Phone
        {
            screenWidth = 0.104f,
            screenHeight = 0.058f,
            screenBottom = 0f,
            cameraOffset = new Vector3(-0.06f, 0.025f, 0f)
        };
        private static readonly Phone PhoneAppleiPhone8Plus = new Phone
        {
            screenWidth = 0.122f,
            screenHeight = 0.068f,
            screenBottom = 0.005f,
            cameraOffset = new Vector3(-0.07f, 0.035f, 0f)
        };
        private static readonly Phone PhoneAppleiPhoneX = new Phone
        {
            screenWidth = 0.12f,
            screenHeight = 0.060f,
            screenBottom = 0f,
            cameraOffset = new Vector3(-0.05f, 0f, 0f)
        };
        private static readonly Phone PhoneGooglePixel = new Phone
        {
            screenWidth = 0.12f,
            screenHeight = 0.060f,
            screenBottom = 0f,
            cameraOffset = new Vector3(-0.05f, 0f, 0f)
        };
        private static readonly Phone PhoneGooglePixelXL = new Phone
        {
            screenWidth = 0.12f,
            screenHeight = 0.060f,
            screenBottom = 0f,
            cameraOffset = new Vector3(-0.05f, 0f, 0f)
        };
        private static readonly Phone PhoneGooglePixel2 = new Phone
        {
            screenWidth = 0.12f,
            screenHeight = 0.060f,
            screenBottom = 0f,
            cameraOffset = new Vector3(-0.05f, 0f, 0f)
        };
        private static readonly Phone PhoneGooglePixel2XL = new Phone
        {
            screenWidth = 0.12f,
            screenHeight = 0.060f,
            screenBottom = 0f,
            cameraOffset = new Vector3(-0.05f, 0f, 0f)
        };
        private static readonly Phone PhoneSamsungS8 = new Phone
        {
            screenWidth = 0.132f,
            screenHeight = 0.067f,
            screenBottom = 0.005f,
            cameraOffset = new Vector3(-0.05f, 0f, 0f)
        };
        private static readonly Phone PhoneUnknown = new Phone
        {
#if UNITY_EDITOR
            //
            screenWidth = 0.120f,
            screenHeight = 0.06f,
#else
            //Get screen size in inches and convert it to meters
            screenWidth = Mathf.Max((Screen.width / Screen.dpi) * 0.0254f, (Screen.height / Screen.dpi) * 0.0254f),
            screenHeight = Mathf.Min((Screen.width / Screen.dpi) * 0.0254f, (Screen.height / Screen.dpi) * 0.0254f),
#endif
            screenBottom = 0.000f,
            cameraOffset = new Vector3(0f, 0f, 0f)
        };
#endregion

        public Phone phone;
        public Model model;

        public static Profile GetProfile(ModelType modelType, PhoneType phoneType)
        {
            Profile result = new Profile();
            switch (modelType)
            {
                case ModelType.HoloKitv1:
                    result.model = ModelHoloKitv1;
                    break;
                case ModelType.HoloKitNetEase:
                    result.model = ModelHoloKitNetEase;
                    break;
                case ModelType.HoloKitApple:
                    result.model = ModelHoloKitApple;
                    break;
                default:
                    result.model = ModelHoloKitv1;
                    break;
            }

            switch (phoneType)
            {
                case PhoneType.AppleiPhone6S:
                    result.phone = PhoneAppleiPhone6S;
                    break;
                case PhoneType.AppleiPhone7:
                    result.phone = PhoneAppleiPhone7;
                    break;
                case PhoneType.AppleiPhone7Plus:
                    result.phone = PhoneAppleiPhone7Plus;
                    break;
                case PhoneType.AppleiPhone8:
                    result.phone = PhoneAppleiPhone8;
                    break;
                case PhoneType.AppleiPhone8Plus:
                    result.phone = PhoneAppleiPhone8Plus;
                    break;
                case PhoneType.AppleiPhoneX:
                    result.phone = PhoneAppleiPhoneX;
                    break;
                case PhoneType.GooglePixel:
                    result.phone = PhoneGooglePixel;
                    break;
                case PhoneType.GooglePixelXL:
                    result.phone = PhoneGooglePixelXL;
                    break;
                case PhoneType.GooglePixel2:
                    result.phone = PhoneGooglePixel2;
                    break;
                case PhoneType.GooglePixel2XL:
                    result.phone = PhoneGooglePixel2XL;
                    break;
                case PhoneType.SamsungS8:
                    result.phone = PhoneSamsungS8;
                    break;
                default:
                    result.phone = PhoneUnknown;
                    break;
            }

            return result;
        }

        public static Profile GetProfile(ModelType modelType)
        {
            PhoneType phoneType = PhoneType.Default;
            string sDeviceModel = SystemInfo.deviceModel;
#if UNITY_IOS
            var deviceGen = UnityEngine.iOS.Device.generation;

            switch (deviceGen)
            {
                case UnityEngine.iOS.DeviceGeneration.iPhone6S:
                    phoneType = PhoneType.AppleiPhone6S;
                    break;
                case UnityEngine.iOS.DeviceGeneration.iPhone7:
                    phoneType = PhoneType.AppleiPhone7;
                    break;
                case UnityEngine.iOS.DeviceGeneration.iPhone7Plus:
                    phoneType = PhoneType.AppleiPhone7Plus;
                    break;
                case UnityEngine.iOS.DeviceGeneration.iPhone8:
                    phoneType = PhoneType.AppleiPhone8;
                    break;
                case UnityEngine.iOS.DeviceGeneration.iPhone8Plus:
                    phoneType = PhoneType.AppleiPhone8Plus;
                    break;
                case UnityEngine.iOS.DeviceGeneration.iPhoneX:
                    phoneType = PhoneType.AppleiPhoneX;
                    break;
                default:
                    phoneType = PhoneType.Unknown;
                        Debug.LogWarning(
                        string.Format("HoloKit: Your device is not officially supported by HoloKitSDK. Screen w(m):{0} h(m):{1} w(p):{2} h(p):{3} dpi:{4}",
                        PhoneUnknown.screenWidth,
                        PhoneUnknown.screenHeight,
                        Screen.width,
                        Screen.height,
                        Screen.dpi));
                    break;
            }
#elif UNITY_ANDROID
            switch (sDeviceModel)
            {
                case "Google Pixel":
                    phoneType = PhoneType.GooglePixel;
                    Debug.Log("HoloKit: Loaded configuration for Google Pixel");
                    break;
                case "Google Pixel XL":
                    phoneType = PhoneType.GooglePixelXL;
                    Debug.Log("HoloKit: Loaded configuration for Google Pixel XL");
                    break;
                case "Google Pixel 2":
                    phoneType = PhoneType.GooglePixel;
                    Debug.Log("HoloKit: Loaded configuration for Google Pixel 2");
                    break;
                case "Google Pixel 2 XL":
                    phoneType = PhoneType.GooglePixelXL;
                    Debug.Log("HoloKit: Loaded configuration for Google Pixel 2 XL");
                    break;
                case "samsung SM-G950F":
                    phoneType = PhoneType.SamsungS8;
                    Debug.Log("HoloKit: Loaded configuration for Samsung S8");
                    break;
                default:
                    phoneType = PhoneType.Unknown;
                    Debug.LogWarning(
                        string.Format("HoloKit: Your device is not officially supported by HoloKitSDK. Screen w(m):{0} h(m):{1} w(p):{2} h(p):{3} dpi:{4}",
                        PhoneUnknown.screenWidth,
                        PhoneUnknown.screenHeight,
                        Screen.width,
                        Screen.height,
                        Screen.dpi));
                    break;
            }
#endif
            return GetProfile(modelType, phoneType);

        }

        public Rect GetViewportRect()
        {
            Rect result = new Rect(0f, 0f, 1f, 1f);

            float x = 0f;
            float y = 0f;
            float w = 1f;
            float h = 1f;

            float aw = (model.viewWidth * 2f) / phone.screenWidth;
            float ah = model.viewHeight / phone.screenHeight;

            w = Mathf.Clamp01(aw);
            h = Mathf.Clamp01(ah);
            x = 0.5f - (w / 2f);
            y = (ah < 1f) ? (phone.screenBottom / phone.screenWidth) : 0f;
            result = new Rect(x, y, w, h);

            return result;
        }

        public Rect GetViewportRect(EyeSide eyeSide)
        {
            Rect total = GetViewportRect();
            Rect result;
            switch (eyeSide)
            {
                case EyeSide.Right:
                    result = new Rect(
                        0.5f,
                        total.y,
                        total.width / 2f,
                        total.height);
                    break;
                default:
                    result = new Rect(
                         0.5f - (total.width / 2f),
                        total.y,
                        total.width / 2f,
                        total.height);
                    break;
            }
            return result;
        }
    }
}
