using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualCrosshair : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    void OnGUI()
    {
        GUI.Box(new Rect(Screen.width / 2, Screen.height / 2, 20, 20),"");
    }

}
