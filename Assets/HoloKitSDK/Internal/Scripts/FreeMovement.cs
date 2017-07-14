using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HoloKit {
    public class FreeMovement : MonoBehaviour {

        public enum MoveDirection
        {
            ForwardBackwardLeftRight = 0,
            UpDownLeftRight,
        }

        public float MoveStep = 0.1f;
        public MoveDirection Direction = MoveDirection.ForwardBackwardLeftRight;

        private Vector3 movingDir = Vector3.zero;

        void Start () {
            
        }
        
        void Update () {
            if (HoloKitInputManager.Instance.GetKeyDown(HoloKitKeyCode.UtopiaForward)) {
                switch (Direction) {
                    case MoveDirection.ForwardBackwardLeftRight:
                    movingDir = HoloKitCameraRigController.Instance.CurrentEyeCenter.forward;
                    movingDir.y = 0;
                    movingDir.Normalize();
                    break;

                    case MoveDirection.UpDownLeftRight:
                    movingDir = new Vector3(0, 1, 0);
                    break;
                }
            } else if (HoloKitInputManager.Instance.GetKeyDown(HoloKitKeyCode.UtopiaBackward)) {
                switch (Direction) {
                    case MoveDirection.ForwardBackwardLeftRight:
                    movingDir = -HoloKitCameraRigController.Instance.CurrentEyeCenter.forward;
                    movingDir.y = 0;
                    movingDir.Normalize();
                    break;

                    case MoveDirection.UpDownLeftRight:
                    movingDir = new Vector3(0, -1, 0);
                    break;
                }
            } else if (HoloKitInputManager.Instance.GetKeyDown(HoloKitKeyCode.UtopiaLeft)) {
                movingDir = - HoloKitCameraRigController.Instance.CurrentEyeCenter.right;
            } else if (HoloKitInputManager.Instance.GetKeyDown(HoloKitKeyCode.UtopiaRight)) {
                movingDir = HoloKitCameraRigController.Instance.CurrentEyeCenter.right;
            } else if (
                HoloKitInputManager.Instance.GetKeyDown(HoloKitKeyCode.UtopiaForwardUp) || 
                HoloKitInputManager.Instance.GetKeyDown(HoloKitKeyCode.UtopiaBackwardUp) || 
                HoloKitInputManager.Instance.GetKeyDown(HoloKitKeyCode.UtopiaLeftUp) || 
                HoloKitInputManager.Instance.GetKeyDown(HoloKitKeyCode.UtopiaRightUp)
            ) {
                movingDir = Vector3.zero;
            }

            transform.position += MoveStep * movingDir;
        }
    }
}