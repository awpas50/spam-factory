using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    public int value; // value of the box
    public int boxTagNumber; // 1: addition 2: subtraction 3: multiplication 4: division
    public Transform player;
    public float magnitude = 8f;
    private Vector3 force;
    private bool iceDetected = false;
    private bool positionSet = false;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Ice")
        {
            if (positionSet == false) // the box will silde on the ice
            {
                
                // determine which direction to slide
                force = transform.position - player.transform.position;
                force.Normalize();
            }
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Ice")
        {
            // make sure the force direction won't change when start sliding.
            // (OnTriggerStay fuction performed before OnTriggerExit)
            //positionSet = true;
            iceDetected = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Ice")
        {
            iceDetected = false;
            //  the direction of force can be adjusted after leaving the ice.
            positionSet = false; 
        }
    }

    private void Update()
    {
        if (rb.velocity.x >= Mathf.Abs(1f))
        {
            force.y = 0;
        }
        else if (rb.velocity.y >= Mathf.Abs(1f))
        {
            force.x = 0;
        }
        // slide on ice when iceDetected = true. Stop sliding when iceDetected = false.
        if (iceDetected)
        {
            Debug.Log(rb.velocity);
            // sliding speed
            rb.velocity = force * magnitude;

        }
        if (!iceDetected)
        {
            // stop movement
            rb.velocity = force * 0; 
        }
    }
}
