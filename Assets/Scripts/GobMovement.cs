using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GobMovement : MonoBehaviour
{
    Rigidbody2D rb;
    const float MOVE_SPEED = 2.5f;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
       
    }

    void Update()
    {
        rb.velocity = new Vector2(-1 * MOVE_SPEED, rb.velocity.y);
        if (transform.position.y < -7.5f)
        {
            Destroy(gameObject);
        }
    }
}
