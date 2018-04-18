using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour {

    public GameObject playerObj;
    Vector3 cammeraOffSet;
	// Use this for initialization
	void Start () {
        //playerObj = GameObject.Find("Stick Man Rag/Bone002");
        cammeraOffSet = new Vector3(-1,-6,-50);
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = playerObj.transform.position + cammeraOffSet;
	}
}
