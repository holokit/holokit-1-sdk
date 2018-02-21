using UnityEngine;
using System.Collections;

namespace HoloKit
{
    public class HoloKitAirplay : MonoBehaviour
    {
        // Use this for initialization
        void Start()
        {

        }

        public void OnPreRender()
        {
            if (Display.displays.Length > 1)
            {
                Display secondDisplay = Display.displays[1];
                GetComponent<Camera>().SetTargetBuffers(secondDisplay.colorBuffer, secondDisplay.depthBuffer);
                Debug.LogFormat("Display {0}", Display.displays.Length);
            }
            else
            {
                GetComponent<Camera>().SetTargetBuffers(Display.main.colorBuffer, Display.main.depthBuffer);
            }
        }

        // Update is called once per frame
        void Update()
        {

        }
    }

}