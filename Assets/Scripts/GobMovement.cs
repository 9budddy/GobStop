using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GobMovement : MonoBehaviour
{
    Rigidbody2D rb;
    const float MOVE_SPEED = 2.5f;
    [SerializeField] PlayerState playerState;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
       
    }

    void Update()
    {
        if (playerState.level == 3 ||
            playerState.level == 4)
        {
            rb.velocity = new Vector2(1.5f * MOVE_SPEED, rb.velocity.y);
        }
        else if (playerState.level == 7)
        {
            rb.velocity = new Vector2(2f * MOVE_SPEED, rb.velocity.y);
        }
        else if (playerState.level == 8 ||
            playerState.level == 9)
        {
            rb.velocity = new Vector2(-2f * MOVE_SPEED, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(-1 * MOVE_SPEED, rb.velocity.y);
        }
        if (transform.position.y < -7.5f)
        {
            Destroy(gameObject);
        }
    }
}
