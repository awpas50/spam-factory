using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridBasedMovement : MonoBehaviour
{
    public GameObject player;

    // 'distanceToMove' will be set to 1
    public float distanceToMove; 
    public float moveSpeed;
    private float initialMoveSpeed;
    private float moveSpeed_Ice;

    // invisible ray to detect obstacles
    private Vector3 rayStart;
    private Vector3 rayEnd;

    // invisible ray to detect box
    private Vector3 rayStart2;
    private Vector3 rayEnd2; 

    private Vector3 locationToMove;
    // record the previous position
    private Vector3 previousPosition; 

    // Sprites
    public Sprite moveUp;
    public Sprite moveDown;
    public Sprite moveLeft;
    public Sprite moveRight;

    // variable will be assigned to true if ice is detected.
    private bool iceDetected = false; 

    // these 4 variables controls the behaviour of the player when standing on ice
    private bool isMovingUp = false; 
    private bool isMovingDown = false;
    private bool isMovingLeft = false;
    private bool isMovingRight = false;

    // location is confirmed, than trigger a signal to move toward a certain location
    private bool locationConfirmed = false;
    // prevernt dianogal movement
    private bool finishedMoving = true;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        locationToMove = transform.position;
        previousPosition = transform.position;

        initialMoveSpeed = moveSpeed;
        moveSpeed_Ice = moveSpeed * 1.2f;
    }

    void Update()
    {
        // adjust move speed.
        if (iceDetected)
        {
            moveSpeed = moveSpeed_Ice;
        }
        else
        {
            moveSpeed = initialMoveSpeed;
        }
        // to visialize the linecast for object detection
        Debug.DrawLine(rayStart, rayEnd, Color.red);
        Debug.DrawLine(rayStart2, rayEnd2, Color.green);
        // Decide which direction to move
        Move();
    }

    //these codes perform the moving action
    void FixedUpdate()
    {
        // when moving key (W,A,S,D) is pressed, it means the location is confirmed
        if (locationConfirmed) 
        {
            // move toward to a certain location
            transform.position = Vector3.MoveTowards(transform.position, locationToMove, moveSpeed * Time.deltaTime);
            // Detect obstacles
            DetectObstacles();
            // determine if finished moving
            if (transform.position == locationToMove) 
            {
                // record the previous grid position
                previousPosition = transform.position;
                // if ice is detected, than keep moving.
                SlideOnIce();
                // if no ice is detected, than reset all booleans to stop all actions.
                if(!iceDetected)
                {
                    // variable "finishedMoving" inform the player to perform next move
                    finishedMoving = true;
                    locationConfirmed = false;
                    // these 4 variables controls the behaviour of the player when standing on ice
                    isMovingUp = false;
                    isMovingDown = false;
                    isMovingLeft = false;
                    isMovingRight = false;
                }
            }
        }
    }

    // these codes determine which direction the player will go
    void Move()
    {
        if(finishedMoving)
        {
            if (Input.GetKey(KeyCode.W))
            {
                MoveUp();
            }
            else if (Input.GetKey(KeyCode.S))
            {
                MoveDown();
            }
            else if (Input.GetKey(KeyCode.A))
            {
                MoveLeft();
            }
            else if (Input.GetKey(KeyCode.D))
            {
                MoveRight();
            }
        }
        // reducing repetitive codes
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            // record the previous position
            previousPosition = transform.position;
            // prevent diagonal movement
            finishedMoving = false;
            // send a signal that indicate player to move
            locationConfirmed = true;
        }
    }

    void DetectObstacles()
    {
        //projecting a invisible ray to detect wall, ice or box
        RaycastHit2D hit = Physics2D.Linecast(rayStart, rayEnd);
        RaycastHit2D hit_2 = Physics2D.Linecast(rayStart2, rayEnd2);
        // if hit something
        if (hit.collider != null)
        {
            // debug, otherwise iceDetected will == false, and thus player cannot slide on ice
            if (hit.collider.gameObject.tag == "Player")
            {
                return;
            }
            // projecting a invisible ray to detect if there's a wall, if so, prohibit moving towards the wall 
            // by sending back the player to the previous position (code to perform this action: "previousPosition = transform.position" in FixedUpdate)
            if (hit.collider.gameObject.tag == "Wall")
            {
                Debug.Log("touched the wall");
                // force the player to stop
                locationToMove = previousPosition;
                transform.position = previousPosition;
                finishedMoving = true;
                //important
                iceDetected = false; 
            }
            // trigger function SildeOnIce()
            if (hit.collider.gameObject.tag == "Ice")
            {
                Debug.Log("touched the ice");
                
                iceDetected = true;             
            }
            // gain control to the player immediately after exiting from ice, if touched something else
            // * even on ice
            if (hit.collider.gameObject.tag != "Ice")
            {
                iceDetected = false; 
            }
            // if the player encounter a box, than these code will check if any object behind the box.
            if (hit.collider.gameObject.GetComponent<Box>())
            {
                if (hit_2.collider == null)
                {
                    Debug.Log("nothing behind the box");
                    return;
                }
                if (hit_2.collider.gameObject.GetComponent<Box>() || hit_2.collider.gameObject.tag == "Wall")
                {
                    Debug.Log("two objects");
                    // force the player to stop
                    locationToMove = previousPosition;
                    transform.position = previousPosition;
                    finishedMoving = true;
                }
            }
        }
        else
        // if the player doesn't hit anything gain control to the player immediately after exiting from ice
        {
            iceDetected = false; 
        }
    }

    // automatically determine the next position to move (player cannot be controlled at this moment)
    void SlideOnIce()
    {
        if (iceDetected)
        {
            if (isMovingUp)
            {
                MoveUp();
            }
            else if (isMovingDown)
            {
                MoveDown();
            }
            else if (isMovingLeft)
            {
                MoveLeft();
            }
            else if (isMovingRight)
            {
                MoveRight();
            }
        }
    }

    void MoveUp()
    {
        rayStart = new Vector2(locationToMove.x, locationToMove.y + distanceToMove - 0.3f); // indicate the position of the ray
        rayEnd = new Vector2(locationToMove.x, locationToMove.y + distanceToMove + 0.3f); // indicate the position of the ray
        rayStart2 = new Vector2(locationToMove.x, locationToMove.y + distanceToMove - 0.3f + 1f); // indicate the position of the ray2
        rayEnd2 = new Vector2(locationToMove.x, locationToMove.y + distanceToMove + 0.3f + 1f); // indicate the position of the ray2
        locationToMove = new Vector2(locationToMove.x, locationToMove.y + distanceToMove); // indicate next position

        GetComponent<SpriteRenderer>().sprite = moveUp;
        isMovingUp = true;
    }
    void MoveDown()
    {
        rayStart = new Vector2(locationToMove.x, locationToMove.y - distanceToMove + 0.3f);
        rayEnd = new Vector2(locationToMove.x, locationToMove.y - distanceToMove - 0.3f);
        rayStart2 = new Vector2(locationToMove.x, locationToMove.y - distanceToMove - 0.3f - 1f);
        rayEnd2 = new Vector2(locationToMove.x, locationToMove.y - distanceToMove + 0.3f - 1f);
        locationToMove = new Vector2(locationToMove.x, locationToMove.y - distanceToMove);

        GetComponent<SpriteRenderer>().sprite = moveDown;
        isMovingDown = true;
    }
    void MoveLeft()
    {
        rayStart = new Vector2(locationToMove.x - distanceToMove + 0.3f, locationToMove.y);
        rayEnd = new Vector2(locationToMove.x - distanceToMove - 0.3f, locationToMove.y);
        rayStart2 = new Vector2(locationToMove.x - distanceToMove + 0.3f - 1f, locationToMove.y);
        rayEnd2 = new Vector2(locationToMove.x - distanceToMove - 0.3f - 1f, locationToMove.y);
        locationToMove = new Vector2(locationToMove.x - distanceToMove, locationToMove.y);

        GetComponent<SpriteRenderer>().sprite = moveLeft;
        isMovingLeft = true;
    }
    void MoveRight()
    {
        rayStart = new Vector2(locationToMove.x + distanceToMove - 0.3f, locationToMove.y);
        rayEnd = new Vector2(locationToMove.x + distanceToMove + 0.3f, locationToMove.y);
        rayStart2 = new Vector2(locationToMove.x + distanceToMove - 0.3f + 1f, locationToMove.y);
        rayEnd2 = new Vector2(locationToMove.x + distanceToMove + 0.3f + 1f, locationToMove.y);
        locationToMove = new Vector2(locationToMove.x + distanceToMove, locationToMove.y);

        GetComponent<SpriteRenderer>().sprite = moveRight;
        isMovingRight = true;
    }
}
