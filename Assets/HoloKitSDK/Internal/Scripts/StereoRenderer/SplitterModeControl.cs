using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloKit;

public class SplitterModeControl : MonoBehaviour {

    private GameObject panel;

	// Use this for initialization
	void Start () {
        	panel = transform.GetChild(0).gameObject;
	}
	
	// Update is called once per frame
	void Update () {
        	panel.SetActive(HoloKitCameraRigController.Instance.SeeThroughMode == SeeThroughMode.HoloKit);
	}
}
