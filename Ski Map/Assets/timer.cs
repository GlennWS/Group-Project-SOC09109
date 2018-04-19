using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//Script for controlling Time UI element for Photo Real Ski
//Callum Galbraith
//06.04.18
public class timer : MonoBehaviour {
    Text text;
    GameObject Restart;
    float time;
    public float speed = 1;
	// Use this for initialization
	void Start () {
        text = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        time += Time.deltaTime * speed;
        text.text = (time % 60).ToString();
	}
    public void finish()
    {
        this.enabled = false;
    }
    public void Crash()
    {
        this.enabled = false;
        
    }
}
