using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HoloKit {
    public class FreeMovement : MonoBehaviour {

        public float MoveStep = 0.1f;

        private Vector3 movingDir = Vector3.zero;

        void Start () {
            
        }
        
        void Update () {
            Vector3? localDir = null;

            if (HoloKitInputManager.Instance.GetKeyDown(HoloKitKeyCode.UtopiaForward)) {
                localDir = new Vector3(0, 0, 1);
            } else if (HoloKitInputManager.Instance.GetKeyDown(HoloKitKeyCode.UtopiaBackward)) {
                localDir = new Vector3(0, 0, -1);
            } else if (HoloKitInputManager.Instance.GetKeyDown(HoloKitKeyCode.UtopiaLeft)) {
                localDir = new Vector3(-1, 0, 0);
            } else if (HoloKitInputManager.Instance.GetKeyDown(HoloKitKeyCode.UtopiaRight)) {
                localDir = new Vector3(1, 0, 0);
            } else if (
                HoloKitInputManager.Instance.GetKeyDown(HoloKitKeyCode.UtopiaForwardUp) || 
                HoloKitInputManager.Instance.GetKeyDown(HoloKitKeyCode.UtopiaBackwardUp) || 
                HoloKitInputManager.Instance.GetKeyDown(HoloKitKeyCode.UtopiaLeftUp) || 
                HoloKitInputManager.Instance.GetKeyDown(HoloKitKeyCode.UtopiaRightUp)
            ) {
                localDir = Vector3.zero;
            }

            if (localDir.HasValue) {
                movingDir = HoloKitCameraRigController.Instance.CurrentEyeCenter.TransformVector(localDir.Value);
            }

            transform.position += MoveStep * movingDir;
        }
    }
}