using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HoloKit
{
    public class HoloKitInputManager : Singleton<HoloKitInputManager>
    {
        private List<char> chars = new List<char>(8);
        private List<KeyCode> keyCodes = new List<KeyCode>(8);

        /// <summary>
        /// Check if the given keyCode is pressed in this frame. Must be called in Update() functions.
        /// </summary>
        public bool GetKeyDown(HoloKitKeyCode keyCode, out HoloKitInputType inputType)
        {
            inputType = HoloKitInputType.Unknown;

            updateChars(keyCode);
            for (int i = 0; i < chars.Count; i++)
            {
                if (RemoteKeyboardReceiver.Instance.GetKeyDown(chars[i]))
                {
                    inputType = HoloKitInputType.RemoteKeyboard;
                    return true;
                }
                else if (RemoteControllerReceiver.Instance.GetKeyDown(chars[i]))
                {
                    inputType = HoloKitInputType.RemotePhone;
                    return true;
                }
            }

            updateUnityKeyCodes(keyCode);
            for (int i = 0; i < keyCodes.Count; i++) {
                if (Input.GetKeyDown(keyCodes[i])) {
                    inputType = HoloKitInputType.UnityInput;
                    return true;
                }
            }

            return false;
        }

        public bool GetKeyDown(HoloKitKeyCode keyCode) {
            HoloKitInputType inputType;
            return GetKeyDown(keyCode, out inputType);
        }

        public bool GetTouchBegan(out Vector2 position)
        {
            position = Vector2.zero;
            if (Input.touchCount == 0)
            {
                return false;
            }

            Touch t = Input.GetTouch(0);
            if (t.phase != TouchPhase.Began) {
                return false;
            }

            position = t.position;
            return true;
        }

        void Start()
        {

        }

        void Update()
        {

        }

        private void updateChars(HoloKitKeyCode keyCode)
        {
            chars.Clear();
            if (keyCode == HoloKitKeyCode.None) {
                return;
            }

            if ((int)keyCode <= (int)(HoloKitKeyCode.Num9))
            {
                chars.Add((char)((int)keyCode - (int)(HoloKitKeyCode.Num9) + '0'));
            }
            else
            {
                chars.Add((char)((int)keyCode - (int)(HoloKitKeyCode.A) + 'A'));
                chars.Add((char)((int)keyCode - (int)(HoloKitKeyCode.A) + 'a'));
            }
        }

        private void updateUnityKeyCodes(HoloKitKeyCode keyCode) 
        {
            keyCodes.Clear();
            if (keyCode == HoloKitKeyCode.None) {
                return;
            }

            if ((int)keyCode <= (int)(HoloKitKeyCode.Num9)) 
            {
                int num = (int)keyCode - (int)(HoloKitKeyCode.Num9);
                keyCodes.Add((KeyCode)((int)KeyCode.Alpha0 + num));
                keyCodes.Add((KeyCode)((int)KeyCode.Keypad0 + num));
            }
            else
            {
                keyCodes.Add((KeyCode)(int)(keyCode) - (int)(HoloKitKeyCode.A) + (int)KeyCode.A);
            }
        }
    }

    public enum HoloKitKeyCode
    {
        None = 0,
        Num0, Num1, Num2, Num3, Num4, Num5, Num6, Num7, Num8, Num9,
        A, B, C, D, E, F, G, H, I, J, K, L, M, N, O, P, Q, R, S, T, U, V, W, X, Y, Z,

        UtopiaA = F,     // Fire
        UtopiaB = R,     // Placement
        UtopiaC = T,     // See Through Mode
        UtopiaD = N,     // Back
        UtopiaFire1 = V, // Fire
        UtopiaFire2 = G, // Fire
        UtopiaForward = A,
        UtopiaBackward = D,
        UtopiaLeft = X,
        UtopiaRight = W,
        UtopiaForwardUp = Q,
        UtopiaBackwardUp = C,
        UtopiaLeftUp = Z,
        UtopiaRightUp = E,
    }

    public enum HoloKitInputType
    {
        Unknown = 0,
        UnityInput,
        RemoteKeyboard,
        RemotePhone,
        BluetoothGameController
    }
}
