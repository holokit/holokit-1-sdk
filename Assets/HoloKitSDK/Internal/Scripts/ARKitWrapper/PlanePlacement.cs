using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.iOS;
using Tango;

namespace HoloKit
{
    public class PlanePlacement : MonoBehaviour
    {
        public HoloKitKeyCode PlacementKey;
        public bool PlacementOnTouch;

        private TangoApplication tangoApplication;
        private TangoPointCloud tangoPointCloud;
        private TangoPointCloudFloor tangoFloor;

        void Start()
        {
            tangoApplication = FindObjectOfType<TangoApplication>();
            tangoPointCloud = FindObjectOfType<TangoPointCloud>();
            tangoFloor = FindObjectOfType<TangoPointCloudFloor>();
        }

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
            ARPoint? hitTestPosition = null;
            Vector2 touchPosition;

            // TODO: Consider HoloKitOffset when it's not zero
            if (HoloKitInputManager.Instance.GetKeyDown(PlacementKey)) 
            {
                hitTestPosition = new ARPoint {
                    x = 0.5f,
                    y = 0.5f,
                };
            }
            else if (PlacementOnTouch && HoloKitInputManager.Instance.GetTouchBegan(out touchPosition))
            {
                Debug.Log("HoloKit: Touch: " + touchPosition);
                var screenPosition = Camera.main.ScreenToViewportPoint(new Vector3(touchPosition.x, touchPosition.y, 0));
                Debug.Log("HoloKit: Touch screen position: " + screenPosition);

                // Make a safe area
                if (screenPosition.x > 0.2f &&
                    screenPosition.x < 0.8f &&
                    screenPosition.y > 0.2f &&
                    screenPosition.y < 0.8f) 
                {
                    hitTestPosition = new ARPoint
                    {
                        x = screenPosition.x,
                        y = screenPosition.y,
                    };
                }

            }

            if (hitTestPosition.HasValue)
            {
#if UNITY_IOS && !UNITY_EDITOR
                // prioritize reults types
                ARHitTestResultType[] resultTypes = {
                    ARHitTestResultType.ARHitTestResultTypeExistingPlaneUsingExtent, 
                    // if you want to use infinite planes use this:
                    //ARHitTestResultType.ARHitTestResultTypeExistingPlane,
                    ARHitTestResultType.ARHitTestResultTypeHorizontalPlane,
                    ARHitTestResultType.ARHitTestResultTypeFeaturePoint
                };

                foreach (ARHitTestResultType resultType in resultTypes)
                {
                    if (HitTestWithResultType(hitTestPosition.Value, resultType))
                    {
                        return;
                    }
                }
#elif UNITY_ANDROID && !UNITY_EDITOR
            Debug.Log("HoloKit: Finding floor...");
            tangoApplication.SetDepthCameraRate(TangoEnums.TangoDepthCameraRate.MAXIMUM);
            tangoPointCloud.FindFloor();
            StartCoroutine(waitForTangoFloor(new Vector3(
                (float)hitTestPosition.Value.x, 
                (float)hitTestPosition.Value.y, 
                0
            )));
#endif
            }
        }

        private IEnumerator waitForTangoFloor(Vector3 hitTestPosition) {
            while (!tangoFloor.m_floorFound) {
                yield return null;
            }

            Debug.Log("HoloKit: Floor found! ");

            Ray ray = Camera.main.ViewportPointToRay(hitTestPosition);
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo)) {
                Debug.Log("HoloKit: Got hit!");
                transform.position = hitInfo.point;
                transform.up = hitInfo.normal;
                Debug.Log(string.Format("HoloKit: x:{0:0.######} y:{1:0.######} z:{2:0.######}", transform.position.x, transform.position.y, transform.position.z));
            }
        }
    }
}