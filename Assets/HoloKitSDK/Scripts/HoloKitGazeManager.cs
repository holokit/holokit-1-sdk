using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events; 

namespace HoloKit
{
    [RequireComponent(typeof(Collider))]
    public partial class HoloKitGazeTarget : MonoBehaviour
    {
        [Header("Gaze Callbacks")]
        public UnityEvent GazeEnter;
        public UnityEvent GazeExit;
    }

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

        public HoloKitGazeTarget CurrentTarget 
        {
            get { return currentTarget; }
        }

        private Vector3 initialScale;

        void Start()
        {
            initialScale = GazeCursor.localScale;
        }

        void Update()
        {
            Transform eyeCenter = HoloKitCamera.Instance.cameraType == CameraType.AR ?
                                  HoloKitCamera.Instance.cameraCenter.transform :
                                  HoloKitCamera.Instance.holoKitOffset.transform;
           
            Ray ray = new Ray(
                eyeCenter.position, 
                eyeCenter.forward
            );

            RaycastHit hitInfo;
            bool hit = Physics.Raycast(ray, out hitInfo, RaycastDistance, RaycastMask);
            if (hit) 
            {
                GazeCursor.position = hitInfo.point + hitInfo.normal.normalized * hitInfo.distance * 0.01f;
                GazeCursor.localScale = initialScale / RaycastDistance * hitInfo.distance;
                GazeCursor.forward = -hitInfo.normal;
            }
            else
            {
                GazeCursor.position = eyeCenter.position + eyeCenter.forward.normalized * RaycastDistance;
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