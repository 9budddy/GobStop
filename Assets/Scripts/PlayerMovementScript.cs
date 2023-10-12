using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMovementScript : MonoBehaviour
{
    [SerializeField] PlayerState playerState;
    [SerializeField] int level;
    [SerializeField] int attempt;
    [SerializeField] int coins;
    [SerializeField] PhysicsMaterial2D slip;
    [SerializeField] PhysicsMaterial2D bounce;
    [SerializeField] ItemSpawner itemSpawner;


    Rigidbody2D rb;
    Animator animator;
    float xDirection = 0.0f;
    float oldXDirection = 0.0f;
    float speed = 0.0f;
    float MOVE_SPEED = 5.0f;
    float JUMP_FORCE = 12f;
    const float MAX_GROUNDED_ANGLE = 30f;

    bool isOnIceTimer = false;
    float iceTimer = 0.0f;
    float wingsTimer = 0.0f;
    float bounceTimer = 0.0f;
    float slowTimer = 0.0f;

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
        playerState.coins = coins;
        playerState.coinsRequired = level;
        playerState.level = level;
        playerState.gobPosition = level;
        playerState.professorPosition = level;
        playerState.wings = false;
        playerState.glue = false;
        playerState.bounce = false;
        playerState.spawnBounce = false;
        playerState.spawnGlue = false;
        playerState.spawnWings = false;

        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        animator.SetInteger("playerState", (int)AnimationStateEnum.Running);
    }


    void Update()
    {
        playerState.position = transform.position;
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

        if (playerState.wings)
        {
            rb.velocity = new Vector2(rb.velocity.x, 5f);
            wingsTimer += Time.deltaTime;
            if (wingsTimer >= 5.0f)
            {
                wingsTimer = 0.0f;
                playerState.wings = false;
            }
        }

        if (playerState.bounce)
        {
            rb.sharedMaterial = bounce;
            bounceTimer += Time.deltaTime;
            if (bounceTimer >= 7.0f)
            {
                bounceTimer = 0.0f;
                playerState.bounce = false;
                rb.sharedMaterial = slip;
            }
        } 

        if (playerState.glue)
        {
            if (slowTimer == 0.0f)
            {
                MOVE_SPEED = MOVE_SPEED * .5f;
                JUMP_FORCE = JUMP_FORCE * .8f;
            }
            slowTimer += Time.deltaTime;
            if (slowTimer >= 7.0f)
            {
                slowTimer = 0.0f;
                playerState.glue = false;
                MOVE_SPEED = MOVE_SPEED / .5f;
                JUMP_FORCE = JUMP_FORCE / .8f;
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

    public void setTimers()
    {
        if (playerState.wings)
        {
            playerState.wings = false;
            wingsTimer = 0.0f;
        }
        if (playerState.bounce)
        {
            playerState.bounce = false;
            rb.sharedMaterial = slip;
            bounceTimer = 0.0f;
        }
        if (playerState.glue)
        {
            playerState.glue = false;
            MOVE_SPEED = MOVE_SPEED / .5f;
            JUMP_FORCE = JUMP_FORCE / .8f;
            slowTimer = 0.0f;
        }

        itemSpawner.spawnImmediate();
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

        if (collision.gameObject.tag.Equals("Ground") ||
            collision.gameObject.tag.Equals("Ice") ||
            collision.gameObject.tag.Equals("Trap"))
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
        if (collision.gameObject.tag.Equals("Ground") || 
            collision.gameObject.tag.Equals("Ice") ||
            collision.gameObject.tag.Equals("Trap"))
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
        if (collision.gameObject.tag.Equals("Ground") || 
            collision.gameObject.tag.Equals("Ice") ||
            collision.gameObject.tag.Equals("Trap"))
        {
            if (collision.gameObject.tag.Equals("Ice"))
            {
                isOnIceTimer = true;
            }
            isGrounded = false;
        }
    }
}
