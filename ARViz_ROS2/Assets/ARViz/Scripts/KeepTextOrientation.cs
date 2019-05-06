using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepTextOrientation : MonoBehaviour {

    Quaternion orientation;

	void Start () {
        orientation = this.transform.rotation;
    }
	
	void Update () {
        this.transform.rotation = orientation;
	}
}
