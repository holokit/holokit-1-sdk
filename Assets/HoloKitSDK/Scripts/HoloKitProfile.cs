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
            AppleiPhone6S = 60,
            AppleiPhone6SPlus = 61,
            AppleiPhone7 = 70,
            AppleiPhone7Plus = 71,
            AppleiPhone8 = 80,
            AppleiPhone8Plus = 81,
            AppleiPhoneX = 10,
            GooglePixel = 200,
            GooglePixelXL = 201,
            GooglePixel2 = 210,
            GooglePixel2XL = 211,
            SamsungS8 = 220
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
            /// The distance from the bottom of display area to the touching surface of the holokit phone holder. (in meters)
            /// </summary>
            public float screenBottom;
            /// <summary>
            /// The 3D offset vector from center of the camera to the center of the display area's bottomline. (in meters)
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
            /// 3D offset from the center of bottomline of the holokit phone display to the center of two eyes.
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
            /// <summary>
            /// Bottom of the holder to bottom of the view
            /// </summary>
            public float viewBottom;
        }

        #region HoloKit Model Parameters
        private static readonly Model ModelHoloKitv1 = new Model
        {
            eyeDistance = 0.064f,
            mrOffset = new Vector3(0f, -0.041f, -0.08f - 0.012f),
            fieldOfView = 0f,
            distortion = 0f,
            viewWidth = 0.058f,
            viewHeight = 0.054f,
            lensLength = 0.090f,
            toScreenDist = 0.0762f,
            toEyeDist = 0.085f + 0.012f,
            viewBottom = 0.01f
        };
        private static readonly Model ModelHoloKitNetEase = new Model
        {
            eyeDistance = 0.064f,
            mrOffset = new Vector3(0f, -0.041f, -0.08f - 0.012f),
            fieldOfView = 0f,
            distortion = -0.17f,
            viewWidth = 0.058f,
            viewHeight = 0.054f,
            lensLength = 0.090f,
            toScreenDist = 0.0762f,
            toEyeDist = 0.085f + 0.012f,
            viewBottom = 0.01f
        };
        private static readonly Model ModelHoloKitApple = new Model
        {
            eyeDistance = 0.064f,
            mrOffset = new Vector3(0f, -0.030f, -0.07046f - 0.008f),
            fieldOfView = 0f,
            distortion = 0f,
            viewWidth = 0.059f,
            viewHeight = 0.051f,
            lensLength = 0.069f,
            toScreenDist = 0.06149f,
            toEyeDist = 0.06544f + 0.008f,
            viewBottom = 0.007f
        };
        #endregion

        #region Phone Parameters
        private static readonly Phone PhoneAppleiPhone6S = new Phone
        {
            screenWidth = 0.104f,
            screenHeight = 0.058f,
            screenBottom = 0.0045f,
            cameraOffset = new Vector3(0.058f, -0.055f, -0.0080f)
        };
        private static readonly Phone PhoneAppleiPhone6SPlus = new Phone
        {
            screenWidth = 0.122f,
            screenHeight = 0.068f,
            screenBottom = 0.0045f,
            cameraOffset = new Vector3(0.069f, -0.063f, -0.0086f)
        };
        private static readonly Phone PhoneAppleiPhone7 = new Phone
        {
            screenWidth = 0.104f,
            screenHeight = 0.058f,
            screenBottom = 0.0045f,
            cameraOffset = new Vector3(0.058f, -0.055f, -0.0080f)
        };
        private static readonly Phone PhoneAppleiPhone7Plus = new Phone
        {
            screenWidth = 0.122f,
            screenHeight = 0.068f,
            screenBottom = 0.0045f,
            cameraOffset = new Vector3(0.069f, -0.063f, -0.0086f)
        };
        private static readonly Phone PhoneAppleiPhone8 = new Phone
        {
            screenWidth = 0.104f,
            screenHeight = 0.058f,
            screenBottom = 0.0045f,
            cameraOffset = new Vector3(0.058f, -0.055f, -0.0080f)
        };
        private static readonly Phone PhoneAppleiPhone8Plus = new Phone
        {
            screenWidth = 0.122f,
            screenHeight = 0.068f,
            screenBottom = 0.0045f,
            cameraOffset = new Vector3(0.069f, -0.063f, -0.0086f)
        };
        private static readonly Phone PhoneAppleiPhoneX = new Phone
        {
            screenWidth = 0.135f,
            screenHeight = 0.062f,
            screenBottom = 0.0045f,
            cameraOffset = new Vector3(0.060f, -0.055f, -0.009f)
        };
        private static readonly Phone PhoneGooglePixel = new Phone
        {
            screenWidth = 0.122f,
            screenHeight = 0.068f,
            screenBottom = 0.0045f,
            cameraOffset = new Vector3(0.071f, -0.056f, -0.008f)
        };
        private static readonly Phone PhoneGooglePixelXL = new Phone
        {
            screenWidth = 0.12f,
            screenHeight = 0.060f,
            screenBottom = 0.0045f,
            cameraOffset = new Vector3(0.071f, -0.056f, -0.008f)
        };
        private static readonly Phone PhoneGooglePixel2 = new Phone
        {
            screenWidth = 0.12f,
            screenHeight = 0.060f,
            screenBottom = 0f,
            cameraOffset = new Vector3(0.071f, -0.056f, -0.008f)
        };
        private static readonly Phone PhoneGooglePixel2XL = new Phone
        {
            screenWidth = 0.12f,
            screenHeight = 0.060f,
            screenBottom = 0.0045f,
            cameraOffset = new Vector3(0.071f, -0.056f, -0.008f)
        };
        private static readonly Phone PhoneSamsungS8 = new Phone
        {
            screenWidth = 0.132f,
            screenHeight = 0.067f,
            screenBottom = 0.0015f,
            cameraOffset = new Vector3(0.071f, -0.056f, -0.008f)
        };
        private static readonly Phone PhoneUnknown = new Phone
        {
#if UNITY_EDITOR
            //
            screenWidth = 0.13f,
            screenHeight = 0.07f,
#else
            //Get screen size in inches and convert it to meters
            screenWidth = Mathf.Max((Screen.width / Screen.dpi) * 0.0254f, (Screen.height / Screen.dpi) * 0.0254f),
            screenHeight = Mathf.Min((Screen.width / Screen.dpi) * 0.0254f, (Screen.height / Screen.dpi) * 0.0254f),
#endif
            screenBottom = 0.0045f,
            cameraOffset = new Vector3(0.058f, -0.055f, -0.0080f)
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
                case PhoneType.AppleiPhone6SPlus:
                    result.phone = PhoneAppleiPhone6SPlus;
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
                case UnityEngine.iOS.DeviceGeneration.iPhone6SPlus:
                    phoneType = PhoneType.AppleiPhone6SPlus;
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

            float b = (model.viewBottom - phone.screenBottom) / phone.screenHeight;
            float x = 0f;
            float y = 0f;
            float w = 1f;
            float h = 1f;

            float aw = (model.viewWidth * 2f) / phone.screenWidth;
            float ah = model.viewHeight / phone.screenHeight;

            w = Mathf.Clamp01(aw);
            h = Mathf.Clamp01(ah + b) - b;
            x = 0.5f - (w / 2f);
            y = b;
            result = new Rect(x, y, w, h);

            return result;
        }


        public Rect GetViewportRectInMeter(EyeSide eyeSide)
        {
            Rect rect = GetViewportRect(eyeSide);
            return Rect.MinMaxRect(rect.xMin * phone.screenWidth, rect.yMin * phone.screenHeight, 
                                   rect.xMax * phone.screenWidth, rect.yMax * phone.screenHeight);
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
                case EyeSide.Left:
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
