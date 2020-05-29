using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPushObject : MonoBehaviour
{
    public float grappingDistance = 1f;
    public LayerMask boxMask;

    public GameObject box;

    [Header("VFX")]
    public GameObject objectEffect;
    private LineRenderer lineRenderer;

    private bool facingUp;
    private bool facingDown;
    private bool facingLeft;
    private bool facingRight;
    private bool fixedGrapper;
    private bool isGrapped;

    private void Start()
    {
        lineRenderer = objectEffect.GetComponent<LineRenderer>();
        fixedGrapper = false;
        isGrapped = false;
    }

    void Update()
    {
        if(box != null)
        {
            lineRenderer.enabled = true;
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, box.transform.position);
        }
        else
        {
            lineRenderer.enabled = false;
        }
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

        // fixed grapper direction
        if (Input.GetMouseButton(0))
        {
            fixedGrapper = true;
        }
        if(Input.GetMouseButtonUp(0))
        {
            fixedGrapper = false;
        }
        // push the key
        if (hit.collider != null && !isGrapped && Input.GetMouseButton(0))
        {
            isGrapped = true;
            // 1.create a reference to the box
            box = hit.collider.gameObject;

            // 2. adjust grap condition
            box.layer = LayerMask.NameToLayer("Box_grapped");

            // 3.VFX
            objectEffect.GetComponent<SpriteRenderer>().enabled = true;
            objectEffect.transform.position = box.transform.position;

            // 4.adjust physics
            // set boxes to moveable weight
            box.GetComponent<Rigidbody2D>().mass = 1;
            box.GetComponent<FixedJoint2D>().enabled = true;
            box.GetComponent<FixedJoint2D>().connectedBody = GetComponent<Rigidbody2D>();

            // 5. adjust layer
            box.GetComponent<SpriteRenderer>().sortingOrder += 5;
        }
        //release the key
        else if(box != null && Input.GetMouseButtonUp(0))
        {
            isGrapped = false;
            // 1. adjust grap condition
            box.layer = LayerMask.NameToLayer("Box_ungrapped");

            // 2.VFX
            objectEffect.GetComponent<LineRenderer>().enabled = false;
            objectEffect.GetComponent<SpriteRenderer>().enabled = false;

            // 3.adjust physics
            box.GetComponent<FixedJoint2D>().enabled = false;
            box.GetComponent<FixedJoint2D>().connectedBody = null;
            // disable all velocity
            box.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            // reset boxes to unmoveable weight
            box.GetComponent<Rigidbody2D>().mass = 10000;

            // 4. adjust layer
            box.GetComponent<SpriteRenderer>().sortingOrder -= 5;

            // 5. remove reference of the box
            box = null;
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