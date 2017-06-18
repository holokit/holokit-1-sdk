using System;
using System.Collections;
using UnityEngine;
using UnityEngine.XR.iOS;

namespace HoloKit {
	public class PhoneSpaceController : PhoneSpaceControllerBase {

		public bool CameraSeeThrough;
		
		[Range(0, 0.5f)]
		public float fovCenterOffset;

		public Transform CameraRig;
		public Transform HoloKitOffset;
		public Camera LeftCamera;
		public Camera RightCamera;

		private BarrelDistortion leftBarrel;
		private BarrelDistortion rightBarrel;
		private Camera centerCamera;

		private Camera[] cameras;

		private int centerCullingMask;

		public override float FOV {
			get
			{
				return LeftCamera.fieldOfView;
			}
			set
			{
				LeftCamera.fieldOfView = value;
				RightCamera.fieldOfView = value;
				// centerCamera.fieldOfView = value;
			}
		}

		public override float BarrelRadius {
			get {
				return leftBarrel.FovRadians;
			}
			set {
				leftBarrel.FovRadians = value;
				rightBarrel.FovRadians = value;
			}
		}

		public override float PupilDistance {
			get {
				return RightCamera.transform.localPosition.x - LeftCamera.transform.localPosition.x;
			}
			set {
				float halfDist = value / 2;
				LeftCamera.transform.localPosition = new Vector3(-halfDist, 0, 0);
				RightCamera.transform.localPosition = new Vector3(halfDist, 0, 0);
			}
		}

		public override Vector3 CameraOffset {
			get {
				return HoloKitOffset.localPosition;
			}

			set {
				HoloKitOffset.localPosition = value;
			}
		}

		public override float FOVCenterOffset {
			get {
				return fovCenterOffset;
			}

			set {
				fovCenterOffset = value;
			}
		}

		// Use this for initialization
		void Start () {
			leftBarrel = LeftCamera.GetComponent<BarrelDistortion>();	
			rightBarrel = RightCamera.GetComponent<BarrelDistortion>();
			centerCamera = CameraRig.GetComponent<Camera>();
			centerCullingMask = centerCamera.cullingMask;
			cameras = GetComponentsInChildren<Camera>();

			StartCoroutine(fullscreenClear());
		}

		void OnGUI() {
			if (GUI.Button(new Rect(0, 0, 50, 50), "C")) {
				CameraSeeThrough = !CameraSeeThrough;
			}
		}

		IEnumerator fullscreenClear() {
			int cullingMask = centerCamera.cullingMask;
			bool wasEnabled = centerCamera.enabled;

			centerCamera.cullingMask = 0;
			centerCamera.enabled = true;
			yield return null;
			yield return null;

			centerCamera.enabled = wasEnabled;
			centerCamera.cullingMask = cullingMask;
		}
		
		// Update is called once per frame
		void Update () {
			LeftCamera.gameObject.SetActive(!CameraSeeThrough);
			RightCamera.gameObject.SetActive(!CameraSeeThrough);

			centerCamera.GetComponent<UnityARVideo>().enabled = CameraSeeThrough; 
			centerCamera.cullingMask = CameraSeeThrough ? centerCullingMask : 0;

			if (!CameraSeeThrough) {
				leftBarrel.Offset = fovCenterOffset;
				rightBarrel.Offset = -fovCenterOffset;
			}

			if (RemoteKeyboardReceiver.Instance.GetKeyDown('c')) {
				CameraSeeThrough = !CameraSeeThrough;
			}
		}

		public void SetCameraOffset(string msg)
		{
			string[] comp = msg.Split(',');

			if (comp.Length != 3) {
				Debug.LogWarning ("Failed to parse camera offset message: " + msg);
			}

			try {
				float[] coord = new float[comp.Length];
				for (int i = 0; i < comp.Length; i++) {
					coord[i] = float.Parse(comp[i]);				
				}	

				transform.GetChild(0).localPosition = new Vector3(coord[0], coord[1], coord[2]);

				Debug.Log(string.Format("VINS Unity: Camera offset changed. ({0}, {1}, {2})", 
					coord[0], coord[1], coord[2]
				));
			} catch (Exception e) {
				Debug.LogWarning ("Failed to parse camera offset message: " + msg);
				throw e;
			}
		}

		public void SetCameraFOV(string msg) {

			float fov = float.Parse(msg);

			for (int i = 0; i < cameras.Length; i++) {
				cameras[i].fieldOfView = fov;
			}

			Debug.Log ("VINS Unity: Set camera fov to " + fov);
		}

		public void SetCameraSeeThrough(string msg) {
			GetComponent<PhoneSpaceController>().CameraSeeThrough = (msg == "true" ? true : false);
		}

	}
}