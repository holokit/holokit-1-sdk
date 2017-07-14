using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HoloKit
{
    public class HoloKitGazeManager : MonoBehaviour
    {

        private static HoloKitGazeManager instance;

        public static HoloKitGazeManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<HoloKitGazeManager>();
                }

                return instance;
            }
        }

        public Transform GazeCursor;
        public float RaycastDistance = 10;

        public LayerMask RaycastMask;

        private HoloKitGazeTarget currentTarget;
        public HoloKitGazeTarget CurrentTarget {
            get {return currentTarget;}
        }

        private Vector3 initialScale;

        void Start()
        {
            initialScale = GazeCursor.localScale;
        }

        void Update()
        {
            if (currentTarget != null 
                && currentTarget.KeysToListenOnGaze != null
                && currentTarget.KeyDownOnGaze != null) 
            {
                for (int i = 0; i < currentTarget.KeysToListenOnGaze.Length; i++)
                {
                    HoloKitKeyCode keyCode = currentTarget.KeysToListenOnGaze[i];
                    if (HoloKitInputManager.Instance.GetKeyDown(keyCode))
                    {
                        currentTarget.KeyDownOnGaze.Invoke(keyCode);
                    }
                }
            }
        }

        void LateUpdate()
        {
            Transform eyeCenter = HoloKitCameraRigController.Instance.CurrentEyeCenter;

            Ray ray = new Ray(
                eyeCenter.position, 
                eyeCenter.forward
            );

            RaycastHit hitInfo;
            bool hit = Physics.Raycast(ray, out hitInfo, RaycastDistance, RaycastMask);
            if (hit) 
            {
                GazeCursor.position = hitInfo.point + hitInfo.normal * hitInfo.distance * 0.01f;
                GazeCursor.localScale = initialScale / RaycastDistance * hitInfo.distance;
                GazeCursor.forward = - hitInfo.normal;
            }
            else
            {
                GazeCursor.position = eyeCenter.position + eyeCenter.forward * RaycastDistance;
                GazeCursor.forward = eyeCenter.forward;
                GazeCursor.localScale = initialScale;
            }

            // Invoke gaze events on gaze targets
            HoloKitGazeTarget newTarget = hit ? hitInfo.collider.GetComponent<HoloKitGazeTarget>() : null;
            if (newTarget != currentTarget)  {
                if (currentTarget != null && currentTarget.GazeExit != null)
                {
                    currentTarget.GazeExit.Invoke();
                }

                if (newTarget != null && newTarget.GazeEnter != null)
                {
                    newTarget.GazeEnter.Invoke();
                }

                currentTarget = newTarget;
            }
        }

        void OnDestroy()
        {
            instance = null;
        }
    }
}