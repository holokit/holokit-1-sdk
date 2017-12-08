using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityARInterface;

namespace HoloKit
{
    public class PlanePlacement : MonoBehaviour
    {
        public UnityEvent onPlace = new UnityEvent();
        public UnityEvent onHide = new UnityEvent();

        public HoloKitKeyCode placeOnKey;
        public bool placeOnTouch;
        public bool rotateToCamera = false;
        public GameObject holder;
        public LayerMask placeLayerMask;

        private void Update()
        {
            if (Input.GetMouseButton(0))
            {
                Camera camera = HoloKitCameraRigController.Instance.CenterCamera;
                Ray ray = camera.ScreenPointToRay(Input.mousePosition);
                RaycastHit rayHit;
                if (Physics.Raycast(ray, out rayHit, float.MaxValue, placeLayerMask))
                {
                    holder.transform.position = rayHit.point;
                }
            }
        }

        private void PlaceObject()
        {

        }
    }
}