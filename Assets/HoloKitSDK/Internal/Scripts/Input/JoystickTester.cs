using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace HoloKit 
{
    public class JoystickTester : MonoBehaviour {

        private bool connected;

        void Start () {
            StartCoroutine(CheckForControllers());            
        }
        
        void Update () {
            Array allKeyCodes = Enum.GetValues(typeof(KeyCode));
            foreach (var item in allKeyCodes) {
                KeyCode keyCode = (KeyCode)item;
                if (Input.GetKey(keyCode)) {
                    Debug.Log("JoystickTester: Key " + keyCode.ToString());
                }
            }
            
            string[] axisNames = new string[] {
                "Horizontal",
                "Vertical",
            };

            foreach (var name in axisNames) {
                float val = Input.GetAxis(name);
                if (!Mathf.Approximately(val, 0)) {
                    Debug.Log("JoystickTester: Axis " + name + " = " + val);
                }
            }
        }

        IEnumerator CheckForControllers() {
            while (true) {
                var controllers = Input.GetJoystickNames();
                if (!connected && controllers.Length > 0) {
                    connected = true;
                    Debug.Log("JoystickTester: Joystick Connected");
                    for (int i = 0; i < controllers.Length; i++) {
                        Debug.Log("JoystickTester: Joystick name " + controllers[i]);
                    }
                } else if (connected && controllers.Length == 0) {
                    connected = false;
                    Debug.Log("JoystickTester: Joystick Disconnected");
                }
                yield return new WaitForSeconds(1f);
            }
        }
    }
}