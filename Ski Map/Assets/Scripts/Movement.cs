using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

    public float speed;             //Floating point variable to store the player's movement speed.
	public float jump;
    private Rigidbody rb2d;       //Store a reference to the Rigidbody2D component required to use 2D Physics.
	bool flag = false;

    // Use this for initialization
    void Start()
    {
        //Get and store a reference to the Rigidbody2D component so that we can access it.
        rb2d = GetComponent<Rigidbody>();
    }

    //FixedUpdate is called at a fixed interval and is independent of frame rate. Put physics code here.
    void FixedUpdate()
    {
		//rb2d.gravityScale = 1;
        //Store the current horizontal input in the float moveHorizontal.
        float moveHorizontal = Input.GetAxis("Horizontal");
        //Store the current vertical input in the float moveVertical.
        float moveVertical = Input.GetAxis("Vertical");
        //Use the two store floats to create a new Vector2 variable movement.
        Vector3 movement = new Vector3(moveHorizontal, -1f + moveVertical, (-0.75f + moveVertical) / 4f);
		//rigidbody.angularVelocity *= friction
		if (Input.GetAxis ("Horizontal") != 0f) {
			rb2d.drag = 1;
			rb2d.AddForce (movement * speed * 3);
		} else if (Input.GetAxis ("Vertical") != 0f) {
			rb2d.drag = 0;
			rb2d.AddForce (movement * (speed / 2f));
		} else if (Input.GetKeyDown (KeyCode.Space)) {
			flag = true;
			//rb2d.gravityScale = 0;
			rb2d.AddForce (Vector3.up * jump * 10);
		} else {
			rb2d.drag = 0;
			rb2d.AddForce(movement * speed);
		}
        //Call the AddForce function of our Rigidbody2D rb2d supplying movement multiplied by speed to move our player.
        //camera.transform.po
    }
}
