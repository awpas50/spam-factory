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
        Aim();
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * currentSpeed * Time.fixedDeltaTime);
    }
    void DetectAiming()
    {
        // 0 = not aiming, 1 = aiming
        if(Input.GetMouseButtonDown(0))
        {
            animator.SetFloat("AimState", 1);

        }
        if(Input.GetMouseButtonUp(0))
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
        animator.SetFloat("Speed", movement.sqrMagnitude);
    }

    void Aim()
    {

    }
}
