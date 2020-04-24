using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Ref: https://www.youtube.com/watch?v=mbzXIOKZurA
    public float moveSpeed = 5f;
    public Transform movePoint; // set up next position
    public Transform movePoint2;

    public LayerMask wallLayerMask;
    public LayerMask iceLayerMask;
    public LayerMask boxLayerMask;

    void Start()
    {
        movePoint.parent = null;
        movePoint2.parent = movePoint;
        movePoint2.position = new Vector3(0, 0, 0);
    }
    
    void Update()
    {
        Debug.Log(Input.GetAxisRaw("Horizontal") + " " + Input.GetAxisRaw("Vertical"));
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);
        
        // pressing all the way to the left (-1) /right (1)
        if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1f)
        {
            // if the player reached the position
            if (Vector3.Distance(transform.position, movePoint.position) <= 0.1f)
            {
                if (!Physics2D.OverlapCircle(movePoint.transform.position + new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f), 0.01f, wallLayerMask))
                {
                    MoveHorizontal();
                }
            }
        }
        // pressing all the way to up (1) /down (-1)
        else if (Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1f)
        {
            if(Vector3.Distance(transform.position, movePoint.position) <= 0.1f)
            {
                if (!Physics2D.OverlapCircle(movePoint.transform.position + new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f), 0.1f, wallLayerMask))
                {
                    MoveVertical();
                }
            }
        }
    }

    void MoveHorizontal()
    {
        //move along x-axis.
        movePoint.position += new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f);
        if (Input.GetAxisRaw("Horizontal") == 1)
        {
            movePoint2.localPosition = new Vector3(1f, 0, 0);
        }
        else if (Input.GetAxisRaw("Horizontal") == -1)
        {
            movePoint2.localPosition = new Vector3(-1f, 0, 0);
        }
    }

    void MoveVertical()
    {
        //move along y-axis.
        movePoint.position += new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f);
        if (Input.GetAxisRaw("Vertical") == 1)
        {
            movePoint2.localPosition = new Vector3(0, 1f, 0);
        }
        else if (Input.GetAxisRaw("Vertical") == -1)
        {
            movePoint2.localPosition = new Vector3(0, -1f, 0);
        }
    }
}
