using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMovementScript : MonoBehaviour
{
    [SerializeField] PlayerState playerState;

    Rigidbody2D rb;
    Animator animator;
    float xDirection = 0.0f;
    float oldXDirection = 0.0f;
    float speed = 0.0f;
    const float MOVE_SPEED = 5.0f;
    const float JUMP_FORCE = 12f;
    const float MAX_GROUNDED_ANGLE = 30f;

    bool isOnIceTimer = false;
    float iceTimer = 0.0f;

    bool jump = false;
    bool isGrounded = false;
    private List<Collider2D> currentCollisions = new List<Collider2D>();

    private enum AnimationStateEnum
    {
        Idle = 0,
        Running = 1,
        Jumping = 2
    }

    void Start()
    {
        playerState.isOnIce = false;

        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        animator.SetInteger("playerState", (int)AnimationStateEnum.Running);
    }


    void Update()
    {
        if (isOnIceTimer)
        {
            iceTimer += Time.deltaTime;
            if (iceTimer > 0.1f)
            {
                isOnIceTimer = false;
                playerState.isOnIce = false;
                iceTimer = 0.0f;
            }
        }


        xDirection = Input.GetAxisRaw("Horizontal");

        if (xDirection != 0)
        {
            transform.localScale = new Vector3(xDirection, 1, 1);
            oldXDirection = xDirection;
        }
        if (playerState.isOnIce)
        {
            icePhysics();   
        }
        else
        {
            speed = 0f;
            rb.velocity = new Vector2(xDirection * MOVE_SPEED, rb.velocity.y);
        }
        

        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
        }
        if (Input.GetButtonUp("Jump"))
        {
            jump = false;
        }

        if (isGrounded && jump)
        {
            rb.velocity = new Vector2(rb.velocity.x, JUMP_FORCE);
        }
        SetAnimationState();
    }

    private void icePhysics()
    {
        // Apply input as a force instead of setting velocity directly.
        if (xDirection != 0)
        {
            speed = oldXDirection * MOVE_SPEED * .5f;

            if (xDirection < 1)
            {
                if (rb.velocity.x > MOVE_SPEED * oldXDirection)
                {
                    rb.AddForce(new Vector2(oldXDirection * MOVE_SPEED * 0.3f, 0));
                }

            }
            else
            {
                if (rb.velocity.x < MOVE_SPEED * oldXDirection)
                {
                    rb.AddForce(new Vector2(oldXDirection * MOVE_SPEED * 0.3f, 0));
                }
            }
        }
        else
        {

            if (oldXDirection > 0)
            {
                if (rb.velocity.x > 0)
                {
                    rb.AddForce(new Vector2(-oldXDirection * MOVE_SPEED * 0.05f, 0));
                }
                else
                {
                    rb.velocity = new Vector2(0.0f, rb.velocity.y);
                }
            }
            else if (oldXDirection < 0)
            { 
                if (rb.velocity.x < 0)
                {
                    rb.AddForce(new Vector2(-oldXDirection * MOVE_SPEED * 0.05f, 0));
                }
                else
                {
                    rb.velocity = new Vector2(0.0f, rb.velocity.y);
                }
            }
        }
    }

    //ANIMATION STATE
    void SetAnimationState()
    {
        AnimationStateEnum playerAnimationState;
        
        if(isGrounded)
        {
            if (xDirection == 0.0f)
            {
                playerAnimationState = AnimationStateEnum.Idle;
            }
            else
            {
                playerAnimationState = AnimationStateEnum.Running;
            }
        }
        else
        {
            playerAnimationState = AnimationStateEnum.Jumping;
        }
        animator.SetInteger("playerState", (int)playerAnimationState);

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Ground") || collision.gameObject.tag.Equals("Ice"))
        {


            // add the collider to the set of current collisions
            if (!currentCollisions.Contains(collision.collider))
            {
                currentCollisions.Add(collision.collider);
            }

                // Check if they can jump.

                // default value for if no contacts fulfilled the condition.
                isGrounded = false;


            Vector2 up = Vector2.up; // or you could use -Physics2D.gravity

            // They can jump if any contact has a normal pointing up (within a maximum angle).
            foreach (ContactPoint2D contact in collision.contacts)
            {

                // if within maximum angle
                if (Vector2.Angle(contact.normal, up) < MAX_GROUNDED_ANGLE)
                {
                    if (collision.gameObject.tag.Equals("Ice"))
                    {
                        playerState.isOnIce = true;
                    }
                    isGrounded = true;
                    break; // exit for loop
                }
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Ground") || collision.gameObject.tag.Equals("Ice"))
        {


            // add the collider to the set of current collisions
            if (!currentCollisions.Contains(collision.collider))
            {
                currentCollisions.Add(collision.collider);
            }



            // Check if they can jump.

            // default value for if no contacts fulfilled the condition.
            isGrounded = false;

            Vector2 up = Vector2.up; // or you could use -Physics2D.gravity

            // They can jump if any contact has a normal pointing up (within a maximum angle).
            foreach (ContactPoint2D contact in collision.contacts)
            {

                // if within maximum angle
                if (Vector2.Angle(contact.normal, up) < MAX_GROUNDED_ANGLE)
                {
                    if (collision.gameObject.tag.Equals("Ice"))
                    {
                        playerState.isOnIce = true;
                    }
                    isGrounded = true;
                    break; // exit for loop
                }
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Ground") || collision.gameObject.tag.Equals("Ice"))
        {
            if (collision.gameObject.tag.Equals("Ice"))
            {
                isOnIceTimer = true;
            }
            isGrounded = false;
        }
    }
}
