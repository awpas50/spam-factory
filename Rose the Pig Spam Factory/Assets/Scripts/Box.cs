using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    [Header("Barrier interaction")]
    public int value; // value of the box
    public int boxTagNumber; // 1: addition 2: subtraction 3: multiplication 4: division
    private Rigidbody2D rb;

    public bool overOtherBoxes = false;

    //DEBUG: avoid box stacking
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.GetComponent<Box>())
        {
            overOtherBoxes = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Box>())
        {
            overOtherBoxes = false;
        }
    }

    private void Update()
    {
        if(overOtherBoxes)
        {
            rb.isKinematic = true;
        }
        else
        {
            rb.isKinematic = false;
        }
    }
}
