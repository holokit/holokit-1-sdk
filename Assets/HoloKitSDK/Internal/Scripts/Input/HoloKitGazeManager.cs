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

        private Collider currentTarget;
        private Vector3 initialScale;

        void Start()
        {
            initialScale = GazeCursor.localScale;
        }

        void LateUpdate()
        {
            Transform eyeCenter = HoloKitCameraRigController.Instance.CurrentEyeCenter;

            Ray ray = new Ray(
                eyeCenter.position, 
                eyeCenter.forward
            );

            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo, RaycastDistance, RaycastMask)) 
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
        }

        void OnDestroy()
        {
            instance = null;
        }
    }
}