using UnityEngine; 

namespace HoloKit {
	public abstract class PhoneSpaceControllerBase : MonoBehaviour {
		public abstract float PupilDistance {get; set;}
		public abstract Vector3 CameraOffset {get; set;}
		public abstract float PhoneScreenHeight {get; set;}
		public abstract float PhoneScreenWidth {get; set;}
		public abstract float FresnelLensFocalLength {get; set;}
		public abstract float ScreenToFresnelDistance {get; set;}
		public abstract float FresnelToEyeDistance {get; set;}
		public abstract float ViewportHeightRatio {get; set;}

		public abstract float RedDistortionFactor {get; set;}
		public abstract float GreenDistortionFactor {get; set;}
		public abstract float BlueDistortionFactor {get; set;}
		public abstract float BarrelDistortionFactor {get; set;}
	}
}