using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpData : MonoBehaviour {

    public int weight;
    public float durabilty; 

	// Use this for initialization
	void Start () {
        this.transform.localScale = this.transform.localScale *(weight*0.05f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
