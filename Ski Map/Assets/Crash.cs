using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Script for ragdolling the character for photoreal sking
//Should be attached to Bone015, Bone012, Bone004, Bone005, Bone018, Bone019 and Bone003
public class Crash : MonoBehaviour {
    //this can be attached to any part of the ragdoll
    // Use this for initialization
    Rigidbody Leg_rigidbody;
	void Start () {
        Leg_rigidbody = GetComponent<Rigidbody>();
	}
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("r"))
            Leg_rigidbody.constraints = RigidbodyConstraints.None; //the constraints will be set to none and should be changed to create a respawn "FreazeRotationX"
	}
}
//To Sober Callum
//From Drunk Callum xoxo