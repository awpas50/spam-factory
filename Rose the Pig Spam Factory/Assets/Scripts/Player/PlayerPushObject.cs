using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPushObject : MonoBehaviour
{
    public float grappingDistance = 1f;
    public LayerMask boxMask;

    GameObject box;

    private bool facingUp;
    private bool facingDown;
    private bool facingLeft;
    private bool facingRight;

    private bool fixedGrapper;

    // Update is called once per frame
    void Update()
    {
        // queriesStartInColliders: When performing a ray/line cast, the start point can begin inside a collider. When this occurs, 
        // this property controls whether these colliders are returned or not.
        // When set to true, such colliders are returned.
        Physics2D.queriesStartInColliders = false;

        if(!fixedGrapper)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                facingUp = true; facingDown = false; facingLeft = false; facingRight = false;
            }
            else if (Input.GetKeyDown(KeyCode.A))
            {
                facingUp = false; facingDown = false; facingLeft = true; facingRight = false;
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                facingUp = false; facingDown = true; facingLeft = false; facingRight = false;
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                facingUp = false; facingDown = false; facingLeft = false; facingRight = true;
            }
        }

        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right * transform.localScale.x, grappingDistance, boxMask);
        
        if (facingUp)
        {
            hit = Physics2D.Raycast(transform.position, Vector2.up * transform.localScale.x, grappingDistance, boxMask);
        }
        if (facingDown)
        {
            hit = Physics2D.Raycast(transform.position, Vector2.down * transform.localScale.x, grappingDistance, boxMask);
        }
        if (facingLeft)
        {
            hit = Physics2D.Raycast(transform.position, Vector2.left * transform.localScale.x, grappingDistance, boxMask);
        }
        if (facingRight)
        {
            hit = Physics2D.Raycast(transform.position, Vector2.right * transform.localScale.x, grappingDistance, boxMask);
        }

        if (Input.GetKey(KeyCode.Space))
        {
            fixedGrapper = true;
        }
        if(Input.GetKeyUp(KeyCode.Space))
        {
            fixedGrapper = false;
        }

        if (hit.collider != null && Input.GetKey(KeyCode.Space))
        {
            box = hit.collider.gameObject;
            box.GetComponent<Collider2D>().enabled = false;
            // set boxes to moveable weight
            box.GetComponent<Rigidbody2D>().mass = 1;
            box.GetComponent<FixedJoint2D>().enabled = true;
            box.GetComponent<FixedJoint2D>().connectedBody = GetComponent<Rigidbody2D>();
        }
        else if(Input.GetKeyUp(KeyCode.Space))
        {
            box.GetComponent<Collider2D>().enabled = true;
            box.GetComponent<FixedJoint2D>().enabled = false;
            box.GetComponent<FixedJoint2D>().connectedBody = null;
            // disable all velocity
            box.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            // reset boxes to unmoveable weight
            box.GetComponent<Rigidbody2D>().mass = 10000;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        if (facingUp)
        {
            Gizmos.DrawLine(transform.position, (Vector2)transform.position + Vector2.up * transform.localScale.x * grappingDistance);
        }
        else if (facingDown)
        {
            Gizmos.DrawLine(transform.position, (Vector2)transform.position + Vector2.down * transform.localScale.x * grappingDistance);
        }
        else if (facingLeft)
        {
            Gizmos.DrawLine(transform.position, (Vector2)transform.position + Vector2.left * transform.localScale.x * grappingDistance);
        }
        else if (facingRight)
        {
            Gizmos.DrawLine(transform.position, (Vector2)transform.position + Vector2.right * transform.localScale.x * grappingDistance);
        }
    }
}