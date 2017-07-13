using UnityEngine; 

namespace HoloKit {
	public abstract class PhoneSpaceControllerBase : MonoBehaviour {
		public abstract float FOV {get; set;}
		public abstract float FOVCenterOffset {get; set;}

		public abstract float BarrelRadius {get; set;}
		public abstract float PupilDistance {get; set;}
		public abstract Vector3 CameraOffset {get; set;}
		public abstract float PhoneScreenHeight {get; set;}
		public abstract float PhoneScreenWidth {get; set;}
		public abstract float FresnelLensFocalLength {get; set;}
		public abstract float ScreenToFresnelDistance {get; set;}
		public abstract float FresnelToEyeDistance {get; set;}

	}
}