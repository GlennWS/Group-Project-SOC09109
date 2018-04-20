using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    public float speed;             //Floating point variable to store the player's movement speed.
    public float jump;
    public float maxSpeed = 100f;
    private Rigidbody2D rb2d;       //Store a reference to the Rigidbody2D component required to use 2D Physics.
    bool flag = false;
    private Vector2 touchOrigin = -Vector2.one; //Used to store location of screen touch origin for mobile controls
    // Use this for initialization
    void Start()
    {
        //Get and store a reference to the Rigidbody2D component so that we can access it.
        rb2d = GetComponent<Rigidbody2D>();
    }

    //FixedUpdate is called at a fixed interval and is independent of frame rate. Put physics code here.
    void Update()
    {
        if (rb2d.velocity.magnitude > maxSpeed)
        {
            rb2d.velocity = Vector3.ClampMagnitude(rb2d.velocity, maxSpeed);
        }
        //Call the AddForce function of our Rigidbody2D rb2d supplying movement multiplied by speed to move our player.
        int horizontal = 0;     //Used to store the horizontal move direction.          
                                //Get input from the input manager, round it to an integer and store in horizontal to set x axis move direction
                                //float MoveHorizontal = Input.GetAxisRaw("Horizontal");
        Vector2 movement = new Vector2(horizontal, 0);
        //Check if Input has registered more than zero touches
        if (Input.touchCount > 0)
        {
            //Store the first touch detected.
            Touch myTouch = Input.touches[0];
            //Check if the phase of that touch equals Began
            if (myTouch.phase == TouchPhase.Began)
            {
                //If so, set touchOrigin to the position of that touch
                touchOrigin = myTouch.position;
            }
            //If the touch phase is not Began, and instead is equal to Ended and the x of touchOrigin is greater or equal to zero:
            else if (myTouch.phase == TouchPhase.Ended && touchOrigin.x >= 0)
            {
                //Set touchEnd to equal the position of this touch
                Vector2 touchEnd = myTouch.position;
                //Calculate the difference between the beginning and end of the touch on the x axis.
                float x = touchEnd.x - touchOrigin.x;
                //Calculate the difference between the beginning and end of the touch on the y axis.
                float y = touchEnd.y - touchOrigin.y;
                //Set touchOrigin.x to -1 so that our else if statement will evaluate false and not repeat immediately.
                touchOrigin.x = -1;
                //Check if the difference along the x axis is greater than along the y axis.
                /* if (Mathf.Abs(x) > Mathf.Abs(y)) {
                     //If x is greater than zero, set horizontal to 1, otherwise set it to -1
                     horizontal = x > 0 ? 1 : -1;
                 }           */
                rb2d.drag = 0.5f;
                // rb2d.mass = 2.0f;
                rb2d.AddForce(new Vector2(x * speed * Time.deltaTime, 0));
            }
        }
        rb2d.drag = 0;
    }
}
