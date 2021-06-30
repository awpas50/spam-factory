using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;
    public float walkSpeed;
    public float sprintSpeed;
    private float currentSpeed;
    //private float maxSpeed;
    Vector2 movement;
    
    public Animator animator;

    public float movementXRecord;
    public float movementYRecord;
    public float horizontal_locked;
    public float vertical_locked;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentSpeed = walkSpeed;
    }

    private void Update()
    {
        DetectDirection();
        // Animation
        DetectAiming();
        Walk();

    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * currentSpeed * Time.fixedDeltaTime);
    }
    void DetectAiming()
    {
        // 0 = not aiming, 1 = aiming
        if(Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetFloat("AimState", 1);

        }
        if(Input.GetKeyUp(KeyCode.Space))
        {
            animator.SetFloat("AimState", 0);
        }
        
    }
    void DetectDirection()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        if (movement.magnitude > 1)
        {
            movement = movement.normalized;
        }
        if(animator.GetFloat("AimState") == 0)
        {
            movementXRecord = animator.GetFloat("Horizontal");
            movementYRecord = animator.GetFloat("Vertical");
            horizontal_locked = 0f;
            vertical_locked = 0f;
        }
        
    }
    void Walk()
    {
        int seed = Random.Range(0, 2);
        if (movement != Vector2.zero)
        {
            animator.SetFloat("Horizontal", movement.x);
            animator.SetFloat("Vertical", movement.y);
            AudioManager.instance.PlayContinuously(SoundList.PlayerMove, 0.25f);
        }
        // Lock animation
        if(animator.GetFloat("AimState") == 1)
        {
            horizontal_locked = movementXRecord;
            vertical_locked = movementYRecord;
            animator.SetFloat("Horizontal", horizontal_locked);
            animator.SetFloat("Vertical", vertical_locked);
        }
        animator.SetFloat("Speed", movement.sqrMagnitude);
    }
}
