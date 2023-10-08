using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{
    Rigidbody2D rb;
    Animator animator;
    float xDirection = 0.0f;
    const float MOVE_SPEED = 5.0f;
    const float JUMP_FORCE = 12f;
    const float MAX_GROUNDED_ANGLE = 30f;

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
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        animator.SetInteger("playerState", (int)AnimationStateEnum.Running);
    }


    void Update()
    {
        xDirection = Input.GetAxisRaw("Horizontal");
        if (xDirection != 0)
        {
            transform.localScale = new Vector3(xDirection, 1, 1);
        }

        rb.velocity = new Vector2(xDirection * MOVE_SPEED, rb.velocity.y);

        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            rb.velocity = new Vector2(rb.velocity.x, JUMP_FORCE);
        }
        SetAnimationState();
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
        if (collision.gameObject.tag.Equals("Ground"))
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
                    isGrounded = true;
                    break; // exit for loop
                }
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Ground"))
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
                    isGrounded = true;
                    break; // exit for loop
                }
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Ground"))
        {
            isGrounded = false;
        }
    }
}
