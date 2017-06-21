using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HoloKit {
    public static class HoloKitCalibration {
        public static void LoadDefaultCalibration(HoloKitCameraRigController cameraRig) {
            cameraRig.CameraOffset = new Vector3(0.069f, -0.082f, -0.082f);
            cameraRig.FOV = 49.7f;
            cameraRig.BarrelRadius = 1.69f;
            cameraRig.PupilDistance = 0.064f;
            cameraRig.FOVCenterOffset = 0.03f;
        }
    }
}
