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
            CheckPlatform();
            CheckCameraDescription();
            CheckTargetSDKVersion();
            EditorApplication.update -= EditorUpdate;
        }

        private static void CheckPlatform()
        {
            switch (EditorUserBuildSettings.activeBuildTarget)
            {
                case BuildTarget.Android:
                    break;
                case BuildTarget.iOS:
                    break;
                default:

                    int answerId = EditorUtility.DisplayDialogComplex(
                        "Switch Platform",
                        "Incorrect Target Platform: Switch to Android or iOS",
                        "Switch to Android",
                        "Switch to iOS",
                        "Cancel"
                    );

                    switch (answerId)
                    {
                        case 0:
                            EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTargetGroup.Android, BuildTarget.Android);
                            break;
                        case 1:
                            EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTargetGroup.iOS, BuildTarget.iOS);
                            break;
                        case 2:
                            break;
                    }

                    break;
            }
        }

        private static void CheckCameraDescription()
        {
            if (PlayerSettings.iOS.cameraUsageDescription == null ||
                PlayerSettings.iOS.cameraUsageDescription.Length == 0) 
                {
                    PlayerSettings.iOS.cameraUsageDescription = "HoloKit needs to use your camera for inside-out tracking. ";
                    Debug.Log("HoloKit: Player Settings updated. Camera Usage Description set to default HoloKit message");
                }
        }

        private static void CheckTargetSDKVersion()
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