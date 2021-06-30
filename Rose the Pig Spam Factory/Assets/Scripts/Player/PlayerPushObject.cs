using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPushObject : MonoBehaviour
{
    public float grappingDistance = 1f;
    public LayerMask boxMask;

    public GameObject box;
    Animator animator;

    [Header("VFX")]
    public GameObject objectEffect;
    private LineRenderer lineRenderer;
    private CameraShake cameraShake;

    private bool facingUp;
    private bool facingDown;
    private bool facingLeft;
    private bool facingRight;
    private bool fixedGrapper;
    private bool isGrapped;

    private void Start()
    {
        animator = GetComponent<Animator>();
        lineRenderer = objectEffect.GetComponent<LineRenderer>();
        cameraShake = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraShake>();
        fixedGrapper = false;
        isGrapped = false;
    }

    void Update()
    {
        // fixed grapper direction
        if (Input.GetKey(KeyCode.Space))
        {
            fixedGrapper = true;
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            fixedGrapper = false;
        }

        if (box != null)
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
            // upper right - facingUp
            if (animator.GetFloat("Horizontal") > 0 && animator.GetFloat("Vertical") > 0)
            {
                facingUp = true; facingDown = false; facingLeft = false; facingRight = false;
            }
            // lower right - facingDown
            if (animator.GetFloat("Horizontal") > 0 && animator.GetFloat("Vertical") < 0)
            {
                facingUp = false; facingDown = true; facingLeft = false; facingRight = false;
            }
            // upper left - facingUp
            if (animator.GetFloat("Horizontal") < 0 && animator.GetFloat("Vertical") > 0)
            {
                facingUp = true; facingDown = false; facingLeft = false; facingRight = false;
            }
            // lower left - facingDown
            if (animator.GetFloat("Horizontal") < 0 && animator.GetFloat("Vertical") < 0)
            {
                facingUp = false; facingDown = true; facingLeft = false; facingRight = false;
            }
            // up
            if (animator.GetFloat("Horizontal") == 0 && animator.GetFloat("Vertical") > 0)
            {
                facingUp = true; facingDown = false; facingLeft = false; facingRight = false;
            }
            // left
            if (animator.GetFloat("Horizontal") < 0 && animator.GetFloat("Vertical") ==  0)
            {
                facingUp = false; facingDown = false; facingLeft = true; facingRight = false;
            }
            // down
            if (animator.GetFloat("Horizontal") == 0 && animator.GetFloat("Vertical") < 0)
            {
                facingUp = false; facingDown = true; facingLeft = false; facingRight = false;
            }
            // right
            if (animator.GetFloat("Horizontal") > 0 && animator.GetFloat("Vertical") == 0)
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

        
        // push the key
        if (hit.collider != null && !isGrapped && Input.GetKey(KeyCode.Space))
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
            //box.GetComponent<Rigidbody2D>().mass = 1;
            box.GetComponent<FixedJoint2D>().enabled = true;
            box.GetComponent<FixedJoint2D>().connectedBody = GetComponent<Rigidbody2D>();

            // 5. adjust layer
            box.GetComponent<SpriteRenderer>().sortingOrder += 5;
        }
        //release key
        else if(box != null && Input.GetKeyUp(KeyCode.Space))
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
            //box.GetComponent<Rigidbody2D>().mass = 100000000;

            // 4. adjust layer
            box.GetComponent<SpriteRenderer>().sortingOrder -= 5;
            
            // 5. remove reference of the box
            box = null;

            // 6. Shake effect
            //StartCoroutine(cameraShake.Shake(0.05f, 0.1f));
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