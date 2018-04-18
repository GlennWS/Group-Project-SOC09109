using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//Script for ragdoll colisions used for Photo Real Ski
//Callum Galbraith
//06.04.18
public class Colision : MonoBehaviour {
        GameObject lefleg;
        GameObject RightLeg;
        GameObject lefski;
        GameObject Rightski;
        GameObject time;
        GameObject Restart;

    // Use this for initialization
    void Start () {
        lefleg = GameObject.Find("Stick Man Rag/Bone015");
        RightLeg = GameObject.Find("Stick Man Rag/Bone012");
        lefski = GameObject.Find("Stick Man Rag/Bone012/Bone013/Bone014/left skii");
        Rightski = GameObject.Find("Stick Man Rag/Bone015/Bone016/Bone017/right skii");
        time = GameObject.Find("Canvas/Time");
        Restart = GameObject.Find("Canvas/Button");
        Restart.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Tree")
       {
            Rigidbody ridleft = lefleg.GetComponent<Rigidbody>();
            Rigidbody ridRight = RightLeg.GetComponent<Rigidbody>();
            Rigidbody ridskilef = lefski.GetComponent<Rigidbody>();
            Rigidbody ridRightski = Rightski.GetComponent<Rigidbody>();
            ridleft.constraints = RigidbodyConstraints.None;
            ridRight.constraints = RigidbodyConstraints.None;
            ridskilef.constraints = RigidbodyConstraints.None;
            ridRightski.constraints = RigidbodyConstraints.None;
            time.GetComponent<timer>().Crash();
            Restart.SetActive(true);
        }
        if(collision.gameObject.tag == "Finish")
        {
            time.GetComponent<timer>().finish();

        }

    }
}
