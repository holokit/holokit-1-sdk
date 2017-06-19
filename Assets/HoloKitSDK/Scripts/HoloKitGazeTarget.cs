using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace HoloKit
{
    [RequireComponent(typeof(Collider))]
    public partial class HoloKitGazeTarget : MonoBehaviour
    {

        [Header("Gaze Callbacks")]
        public UnityEvent GazeEnter;
        public UnityEvent GazeExit;

        [Header("Input Handling on Gaze")]
        public HoloKitKeyCode[] KeysToListenOnGaze;
        public GazeKeyEvent KeyDownOnGaze;

    }

    [System.Serializable]
    public class GazeKeyEvent : UnityEvent<HoloKitKeyCode> {

    }
}
