using System;
using UnityEngine;

#if !UNITY_EDITOR && UNITY_IOS
using System.Runtime.InteropServices;
#endif

namespace HoloKit
{
    public class HoloKitScreenBrightnessManager : MonoBehaviour
    {
#if !UNITY_EDITOR && UNITY_ANDROID
        public bool CanWriteSettings { get; set; }

        class HolokitPermissionCallback : AndroidJavaProxy
        {
            private HoloKitScreenBrightnessManager brightnessManager;

            public HolokitPermissionCallback(HoloKitScreenBrightnessManager brightnessManager) : base("com.ambergarage.holokitsdk.holokitscreenbrightnessmanager.IHolokitPermissionCallback")
            {
                this.brightnessManager = brightnessManager;
            }

            public void granted()
            {
                brightnessManager.CanWriteSettings = true;
            }

            public void denied()
            {
                brightnessManager.CanWriteSettings = false;
            }
        }

        private AndroidJavaClass unityPlayerClass;
        private AndroidJavaObject unityPlayerActivity;
        private AndroidJavaClass systemClass;
        private AndroidJavaObject contentResolver;
        private HolokitPermissionCallback _holokitPermissionCallback;

        // Avoid calling java method each frame
        private float nextUpdateTime;
#endif

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
#if !UNITY_EDITOR && UNITY_ANDROID
            _holokitPermissionCallback = new HolokitPermissionCallback(this);

            unityPlayerClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            unityPlayerActivity = unityPlayerClass.GetStatic<AndroidJavaObject>("currentActivity");
            systemClass = new AndroidJavaClass("android.provider.Settings$System");
            contentResolver = unityPlayerActivity.Call<AndroidJavaObject>("getContentResolver");

            var HolokitScreenBrightnessManagerActivityClass = new AndroidJavaClass("com.ambergarage.holokitsdk.holokitscreenbrightnessmanager.HolokitScreenBrightnessManagerActivity");
            HolokitScreenBrightnessManagerActivityClass.CallStatic("setPermissionCallback", _holokitPermissionCallback);

            var intent = new AndroidJavaObject("android.content.Intent", unityPlayerActivity, HolokitScreenBrightnessManagerActivityClass);
            unityPlayerActivity.Call("startActivity", intent);
            nextUpdateTime = Time.time;
#else
            SetBrightness(brightness);
#endif
        }

        void Update()
        {
#if !UNITY_EDITOR && UNITY_ANDROID
            if (CanWriteSettings && Time.time > nextUpdateTime && Math.Abs(GetBrightness() - brightness) > 0.0001f)
#else
            if (Math.Abs(GetBrightness() - brightness) > 0.0001f)
#endif
            {
                SetBrightness(brightness);
#if !UNITY_EDITOR && UNITY_ANDROID
                nextUpdateTime = Time.time + 5.0f;
#endif
            }
        }

#if !UNITY_EDITOR && UNITY_IOS
        [DllImport("__Internal")]
        private static extern void nativeInterface_setBrightness(float brightness);
        [DllImport("__Internal")]
        private static extern float nativeInterface_getBrightness();
#endif
        public float GetBrightness()
        {
            float brightness = 1.0f;
#if UNITY_EDITOR
#elif UNITY_IOS
            brightness = nativeInterface_getBrightness();
#elif UNITY_ANDROID
            var b = systemClass.CallStatic<int>("getInt", contentResolver, systemClass.GetStatic<string>("SCREEN_BRIGHTNESS"));
            brightness = b / 255f;
#endif
            return brightness;
        }

        public void SetBrightness(float brightness)
        {
#if UNITY_EDITOR
#elif UNITY_IOS
            nativeInterface_setBrightness(brightness);
#elif UNITY_ANDROID
            unityPlayerActivity.Call("runOnUiThread", new AndroidJavaRunnable(() => {
                int b = 1 + (int)(Math.Min(1, Math.Max(0, brightness)) * 254f); // 1 <= b <= 255
                systemClass.CallStatic<bool>("putInt", contentResolver, systemClass.GetStatic<string>("SCREEN_BRIGHTNESS"), b);
            }));
#endif
        }
    }
}