using System;
using UnityEngine;

#if !UNITY_EDITOR && UNITY_IOS
using System.Runtime.InteropServices;
#endif

namespace HoloKit
{
    public class HoloKitScreenBrightnessManager : MonoBehaviour
    {
        private static HoloKitScreenBrightnessManager instance;
        public static HoloKitScreenBrightnessManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<HoloKitScreenBrightnessManager>();
                }
                return instance;
            }
        }

        void OnDestroy()
        {
            if (instance == this)
            {
                instance = null;
            }
        }


        [Range(0f, 1f)]
        public float brightness = 1.0f;

        void Awake()
        {
            SetBrightness(brightness);
        }

        void Update()
        {
            if (Math.Abs(GetBrightness() - brightness) > 0.0001f) {
                SetBrightness(brightness);
            }
        }

#if !UNITY_EDITOR && UNITY_IOS
        [DllImport("__Internal")]
        private static extern void nativeInterface_setBrightness(float brightness);
        [DllImport("__Internal")]
        private static extern float nativeInterface_getBrightness();
#endif
        public static float GetBrightness() 
        {
            float brightness = 1.0f;
#if UNITY_EDITOR
#elif UNITY_IOS
            brightness = nativeInterface_getBrightness();
#elif UNITY_ANDROID
            var unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            var activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
            var system = new AndroidJavaClass("android.provider.Settings.System");
            var contentResolver = activity.Call<AndroidJavaObject>("getContentResolver");
            var b = system.CallStatic<int>("getInt", contentResolver, system.CallStatic<string>("SCREEN_BRIGHTNESS"));
            brightness = b / 255f;
#endif
            return brightness;
        }

        public static void SetBrightness(float brightness)
        {
#if UNITY_EDITOR
#elif UNITY_IOS
            nativeInterface_setBrightness(brightness);
#elif UNITY_ANDROID
            var unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            var activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
            activity.Call("runOnUiThread", new AndroidJavaRunnable(() => {
                var window = activity.Call<AndroidJavaObject>("getWindow");
                var lp = window.Call<AndroidJavaObject>("getAttributes");
                lp.Set("screenBrightness", brightness);
                window.Call("setAttributes", lp);

                int b = 1 + (int)(Math.Min(1, Math.Max(0, brightness)) * 254f); // 1 <= b <= 255
                var system = new AndroidJavaClass("android.provider.Settings.System");
                var contentResolver = activity.Call<AndroidJavaObject>("getContentResolver");
                system.CallStatic("putInt", contentResolver, system.CallStatic<string>("SCREEN_BRIGHTNESS_MODE"), 0);
                system.CallStatic("putInt", contentResolver, system.CallStatic<string>("SCREEN_BRIGHTNESS"), b);
            }));
#endif
        }
    }
}