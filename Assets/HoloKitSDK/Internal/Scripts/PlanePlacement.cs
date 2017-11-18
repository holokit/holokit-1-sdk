using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.iOS;

namespace HoloKit
{
    public class PlanePlacement : MonoBehaviour
    {
        public HoloKitKeyCode PlacementKey;
        public bool PlacementOnTouch;

        bool HitTestWithResultType(ARPoint point, ARHitTestResultType resultTypes)
        {
            List<ARHitTestResult> hitResults = UnityARSessionNativeInterface.GetARSessionNativeInterface().HitTest(point, resultTypes);
            if (hitResults.Count > 0)
            {
                foreach (var hitResult in hitResults)
                {
                    Debug.Log("Got hit!");
                    transform.position = UnityARMatrixOps.GetPosition(hitResult.worldTransform);
                    transform.rotation = UnityARMatrixOps.GetRotation(hitResult.worldTransform);
                    Debug.Log(string.Format("x:{0:0.######} y:{1:0.######} z:{2:0.######}", transform.position.x, transform.position.y, transform.position.z));
                    return true;
                }
            }
            return false;
        }

        // Update is called once per frame
        void Update()
        {
            Vector3? hitTestPosition = null;
            Vector2 touchPosition;

            // TODO: Consider HoloKitOffset when it's not zero
            if (HoloKitInputManager.Instance.GetKeyDown(PlacementKey))
            {
                hitTestPosition = new Vector2(0.5f, 0.5f);
            }
            else if (PlacementOnTouch && HoloKitInputManager.Instance.GetTouchBegan(out touchPosition))
            {
                var screenPosition = Camera.main.ScreenToViewportPoint(new Vector3(touchPosition.x, touchPosition.y, 0));

                // Make a safe area
                if (screenPosition.x > 0.2f &&
                    screenPosition.x < 0.8f &&
                    screenPosition.y > 0.2f &&
                    screenPosition.y < 0.8f)
                {
                    hitTestPosition = new Vector2(screenPosition.x, screenPosition.y);
                }

            }

            if (hitTestPosition.HasValue)
            {
                Ray ray = Camera.main.ViewportPointToRay(hitTestPosition.Value);

                int layerMask = 1 << LayerMask.NameToLayer("ARGameObject"); // Planes are in layer ARGameObject

                RaycastHit rayHit;
                if (Physics.Raycast(ray, out rayHit, float.MaxValue, layerMask))
                    transform.position = rayHit.point;
            }
        }
    }
}