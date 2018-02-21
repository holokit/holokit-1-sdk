using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityARInterface;

namespace HoloKit
{
    public class HoloKitPlacement : MonoBehaviour
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
        public bool isPlaceOnTouch = true;
        public bool isRotateToCamera = false;

        public GameObject holder;

        private void Update()
        {
            if ((isPlaceOnTouch && Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began && !HoloKitUITool.IsOverUI(Input.touches[0].position)))
            {
                PlaceObject();
            }
        }

        private void PlaceObject()
        {
            Vector2 screenPoint = Vector2.zero;
            Camera camera = HoloKitCamera.Instance.cameraCenter;

            switch (placePointer)
            {
                case PlacePointer.CenterScreen:
                    screenPoint = new Vector2(Screen.width * 0.5f, Screen.height * 0.5f);
                    break;
                case PlacePointer.Touch:
                    screenPoint = Input.touches[0].position;
                    break;
            }

            if (HoloKitUITool.IsOverUI(Input.touches[0].position))
                return;

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