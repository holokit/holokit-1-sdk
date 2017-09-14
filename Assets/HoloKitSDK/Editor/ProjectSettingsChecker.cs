using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace HoloKit.Editor
{
    [InitializeOnLoad]
    public class ProjectSettingsChecker
    {

        static ProjectSettingsChecker()
        {
            EditorApplication.update += EditorUpdate;
        }

        private static void EditorUpdate()
        {
            checkPlatform();
            checkCameraDescription();
            checkTargetSDKVersion();
            EditorApplication.update -= EditorUpdate;
        }

        private static void checkPlatform() {
            if (EditorUserBuildSettings.activeBuildTarget != BuildTarget.Android) {
                 if (EditorUtility.DisplayDialog(
                     "Switch Platform", 
                     "Switch to Android now? ", 
                     "Switch to Android",
                     "Cancel")) 
                {
                    EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTargetGroup.Android, BuildTarget.Android);
                }
            }
        }

        private static void checkCameraDescription()
        {
            if (PlayerSettings.iOS.cameraUsageDescription == null ||
                PlayerSettings.iOS.cameraUsageDescription.Length == 0) 
                {
                    PlayerSettings.iOS.cameraUsageDescription = "HoloKit needs to use your camera for inside-out tracking. ";
                    Debug.Log("HoloKit: Player Settings updated. Camera Usage Description set to default HoloKit message");
                }
        }

        private static void checkTargetSDKVersion()
        {
            int version = 0;
            if (PlayerSettings.iOS.targetOSVersionString != null)
            {
                string[] versions = PlayerSettings.iOS.targetOSVersionString.Split('.');
                if (versions.Length > 0) {
                    int.TryParse(versions[0], out version);
                }
            }

            if (version < 11) {
                PlayerSettings.iOS.targetOSVersionString = "11.0";
                Debug.Log("HoloKit: Player Settings updated. Target minimum iOS version set to 11.0");
            }
        }
    }
}