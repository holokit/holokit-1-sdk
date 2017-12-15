using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HoloKit
{
    [System.Serializable]
    public class ProfileHoloKit
    {
        //All models of the supported phones
        public enum PhoneType : int
        {
            Default = 0,
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
        //All HoloKit models
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
            public float screenWidth; // The long screen edge of the phone.
            public float screenHeight; // The short screen edge of the phone.
            public float screenBottom; // The distance from the bottom of display area to the bottom of the holokit phone holder. (in meters)
            public Vector3 cameraOffset; //The 3D offset vector from center of the camera to the center of the display area. (in meters)
            public float separation; // Separation beetween eyes
        }
        [System.Serializable]
        public struct Model
        {
            public Vector3 mrOffset; // 3D offset vector from the bottom of the holokit phone holder to the center of two eyes. 
            public float fieldOfView; // Per eye camera FOV
            public float distortion; // Distortion intensity
            public float viewWidth; // Per eye view area width
            public float viewHeight; // Per eye view area height
        }

        public Phone phone;
        public Model model;

        #region HoloKit Model Parameters
        public static readonly Model ModelDefault = new Model
        {
            mrOffset = new Vector3(0f, 0.04f, 0f),
            fieldOfView = 60f,
            distortion = 1f,
            viewWidth = 0.06f,
            viewHeight = 0.06f
        };
        public static readonly Model ModelHoloKitv1 = new Model
        {
            mrOffset = new Vector3(0f, 0.04f, 0f),
            fieldOfView = 60f,
            distortion = 1f,
            viewWidth = 0.06f,
            viewHeight = 0.06f
        };
        public static readonly Model ModelHoloKitNetEase = new Model
        {
            mrOffset = new Vector3(0f, 0.04f, 0f),
            fieldOfView = 60f,
            distortion = 1f,
            viewWidth = 0.06f,
            viewHeight = 0.06f
        };
        public static readonly Model ModelHoloKitApple = new Model
        {
            mrOffset = new Vector3(0f, 0.04f, 0f),
            fieldOfView = 60f,
            distortion = 1f,
            viewWidth = 0.06f,
            viewHeight = 0.06f
        };
        #endregion

        #region Phone Parameters
        public static readonly Phone PhoneDefault = new Phone
        {
            screenWidth = 0.12f,
            screenHeight = 0.060f,
            screenBottom = 0f,
            cameraOffset = new Vector3(-0.05f, 0f, 0f),
            separation = 0f
        };
        public static readonly Phone PhoneAppleiPhone6S = new Phone
        {
            screenWidth = 0.104f,
            screenHeight = 0.058f,
            screenBottom = 0f,
            cameraOffset = new Vector3(-0.06f, 0.025f, 0f),
            separation = 0f
        };
        public static readonly Phone PhoneAppleiPhone7 = new Phone
        {
            screenWidth = 0.104f,
            screenHeight = 0.058f,
            screenBottom = 0f,
            cameraOffset = new Vector3(-0.06f, 0.025f, 0f),
            separation = 0f
        };
        public static readonly Phone PhoneAppleiPhone7Plus = new Phone
        {
            screenWidth = 0.122f,
            screenHeight = 0.068f,
            screenBottom = 0.005f,
            cameraOffset = new Vector3(-0.07f, 0.035f, 0f),
            separation = 0f
        };
        public static readonly Phone PhoneAppleiPhone8 = new Phone
        {
            screenWidth = 0.104f,
            screenHeight = 0.058f,
            screenBottom = 0f,
            cameraOffset = new Vector3(-0.06f, 0.025f, 0f),
            separation = 0f
        };
        public static readonly Phone PhoneAppleiPhone8Plus = new Phone
        {
            screenWidth = 0.122f,
            screenHeight = 0.068f,
            screenBottom = 0.005f,
            cameraOffset = new Vector3(-0.07f, 0.035f, 0f),
            separation = 0f
        };
        public static readonly Phone PhoneAppleiPhoneX = new Phone
        {
            screenWidth = 0.12f,
            screenHeight = 0.060f,
            screenBottom = 0f,
            cameraOffset = new Vector3(-0.05f, 0f, 0f),
            separation = 0f
        };
        public static readonly Phone PhoneGooglePixel = new Phone
        {
            screenWidth = 0.12f,
            screenHeight = 0.060f,
            screenBottom = 0f,
            cameraOffset = new Vector3(-0.05f, 0f, 0f),
            separation = 0f
        };
        public static readonly Phone PhoneGooglePixelXL = new Phone
        {
            screenWidth = 0.12f,
            screenHeight = 0.060f,
            screenBottom = 0f,
            cameraOffset = new Vector3(-0.05f, 0f, 0f),
            separation = 0f
        };
        public static readonly Phone PhoneGooglePixel2 = new Phone
        {
            screenWidth = 0.12f,
            screenHeight = 0.060f,
            screenBottom = 0f,
            cameraOffset = new Vector3(-0.05f, 0f, 0f),
            separation = 0f
        };
        public static readonly Phone PhoneGooglePixel2XL = new Phone
        {
            screenWidth = 0.12f,
            screenHeight = 0.060f,
            screenBottom = 0f,
            cameraOffset = new Vector3(-0.05f, 0f, 0f),
            separation = 0f
        };
        public static readonly Phone PhoneSamsungS8 = new Phone
        {
            screenWidth = 0.132f,
            screenHeight = 0.067f,
            screenBottom = 0.005f,
            cameraOffset = new Vector3(-0.05f, 0f, 0f),
            separation = 0f
        };
        #endregion

        public static ProfileHoloKit GetProfile(ModelType modelType, PhoneType phoneType)
        {
            ProfileHoloKit result = new ProfileHoloKit();
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
                    result.model = ModelDefault;
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
                    result.phone = PhoneDefault;
                    break;
            }

            return result;
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
            y = (ah < 1f) ? (phone.screenBottom / phone.screenWidth): 0f;
            result = new Rect(x, y, w, h);

            return result;
        }
    }

}
