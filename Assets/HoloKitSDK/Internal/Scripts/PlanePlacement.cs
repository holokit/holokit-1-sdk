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
        public enum PlacePointer : int
        {
            CenterScreen = 0,
            Touch = 1
        }

        public UnityEvent onPlace = new UnityEvent();
        public UnityEvent onHide = new UnityEvent();

        public PlacePointer placePointer;
        public LayerMask placeMask;
        public bool isPlaceOnKey = true;
        public HoloKitKeyCode placeKey;
        public bool isPlaceOnTouch = true;
        public bool isRotateToCamera = false;

        public GameObject holder;
        
        private void Update()
        {
            if ((isPlaceOnTouch && Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began && !UITool.IsOverUI(Input.touches[0].position)) ||
                (isPlaceOnKey && HoloKitInputManager.Instance.GetKeyDown(placeKey)))
            {
                PlaceObject();
            }
        }

        private void PlaceObject()
        {
            Vector2 screenPoint = Vector2.zero;
            Camera camera = HoloKitCameraRigController.Instance.CenterCamera;

            switch (placePointer)
            {
                case PlacePointer.CenterScreen:
                    screenPoint = new Vector2(Screen.width * 0.5f, Screen.height * 0.5f);
                    break;
                case PlacePointer.Touch:
                    screenPoint = Input.mousePosition;
                    break;
            }

            Ray ray = camera.ScreenPointToRay(screenPoint);
            RaycastHit rayHit;
            if (Physics.Raycast(ray, out rayHit, float.MaxValue, placeMask))
            {
                holder.SetActive(!holder.activeSelf);
                
                if (holder.activeSelf)
                {
                    holder.transform.position = rayHit.point;
                    if (isRotateToCamera)
                    {
                        Quaternion lookRotation = camera.transform.rotation;
                        holder.transform.rotation = Quaternion.Euler(new Vector3(0f, lookRotation.eulerAngles.y + 180f, 0f));
                    }

                    if (onPlace != null)
                        onPlace.Invoke();
                }
                else
                {
                    if (onHide != null)
                        onHide.Invoke();
                }
            }
        }
    }
}