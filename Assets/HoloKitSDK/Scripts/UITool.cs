using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace HoloKit
{
    public class HoloKitUITool
    {
        //UI is defalt #5
        public static LayerMask uiLayer = 1 << 5;
        private static PointerEventData _pointerEventData = null;
        private static List<RaycastResult> _raycastResults = new List<RaycastResult>();
        /// <summary>
        /// Are screen position is on the UI
        /// </summary>
        /// <param name="screenPosition">coordinates of screen position</param>
        /// <returns></returns>
        public static bool IsOverUI(Vector2 screenPosition)
        {
            bool result = false;

            EventSystem currentEventSystem = EventSystem.current;
            if (currentEventSystem != null)
            {
                if (_pointerEventData == null)
                {
                    _pointerEventData = new PointerEventData(currentEventSystem);
                }
                else
                {
                    _pointerEventData.Reset();
                }

                _pointerEventData.position = screenPosition;
                _raycastResults.Clear();
                currentEventSystem.RaycastAll(_pointerEventData, _raycastResults);
                for (int i = 0; i < _raycastResults.Count; i++)
                {
                    if (((1 << _raycastResults[i].gameObject.layer) & uiLayer) != 0)
                    {
                        result = true;
                        break;
                    }
                }
            }

            return result;
        }
    }
}