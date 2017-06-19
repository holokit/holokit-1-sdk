using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloKit;

public class GazeTargetExample : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void GazeEnter() {
        Debug.Log("Gaze entered on " + gameObject.name);
        GetComponent<Renderer>().material.color = Color.red;
    }

    public void GazeExit() {
        Debug.Log("Gaze exit from " + gameObject.name);
        GetComponent<Renderer>().material.color = Color.white;
    }

    public void KeyDownOnGaze(HoloKitKeyCode KeyCode) {
        Debug.Log(KeyCode.ToString() +  " KeyDown while gazing on " + gameObject.name);
        StartCoroutine(keyDownCoroutine(KeyCode));
    }

    private IEnumerator keyDownCoroutine(HoloKitKeyCode KeyCode) {
        Color color;

        // Note that only keys that registered in "Keys to listen on gaze" can trigger KeyDownOnGaze event
        switch (KeyCode) {
            case HoloKitKeyCode.Z:
                color = Color.blue;
            break;

            case HoloKitKeyCode.X:
                color = Color.yellow;
                break;

            case HoloKitKeyCode.U:
                color = Color.cyan;
                break;

            default:
                color = Color.black;
                break;
        }

        GetComponent<Renderer>().material.color = color;
        yield return new WaitForSeconds(0.2f);

        GetComponent<Renderer>().material.color = Color.red;
    }
}
