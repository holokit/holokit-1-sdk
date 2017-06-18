using UnityEngine; 

namespace HoloKit {
	public abstract class PhoneSpaceControllerBase : MonoBehaviour {
		public abstract float FOV {get; set;}
		public abstract float BarrelRadius {get; set;}
		public abstract float PupilDistance {get; set;}
		public abstract Vector3 CameraOffset {get; set;}
		public abstract float FOVCenterOffset {get; set;}
	}
}