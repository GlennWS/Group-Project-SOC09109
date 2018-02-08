using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour {

    GameObject playerObj;
    Vector3 cammeraOffSet;
	// Use this for initialization
	void Start () {
        playerObj = GameObject.Find("Ragdoll/Group/Main/DeformationSystem/Root_M/Spine1_M");
        cammeraOffSet = new Vector3(-1, 5, -5);
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = playerObj.transform.position + cammeraOffSet;
	}
}
