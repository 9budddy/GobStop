using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObjectController : MonoBehaviour
{

    [SerializeField] private GameObject pointBeg;
    [SerializeField] private GameObject pointEnd;
    [SerializeField] private float speed;
    [SerializeField] private bool vertical;
    private Rigidbody2D rb;
    private Transform currentPoint;
    

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentPoint = pointEnd.transform;
    }


    void Update()
    {
        Vector2 point = currentPoint.position - transform.position;
        if (vertical)
        {
            if (currentPoint == pointEnd.transform)
            {
                rb.velocity = new Vector2(0f, speed);
            }
            else
            {
                rb.velocity = new Vector2(0f, -speed);
            }
        }
        else
        {
            if (currentPoint == pointEnd.transform)
            {
                rb.velocity = new Vector2(speed, 0f);
            }
            else
            {
                rb.velocity = new Vector2(-speed, 0f);
            }
        }

        if (Vector2.Distance(transform.position, currentPoint.position) < 0.1f && currentPoint == pointEnd.transform)
        {
            currentPoint = pointBeg.transform;
        }
        if (Vector2.Distance(transform.position, currentPoint.position) < 0.2f && currentPoint == pointBeg.transform)
        {
            currentPoint = pointEnd.transform;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(pointBeg.transform.position, 0.5f);
        Gizmos.DrawWireSphere(pointEnd.transform.position, 0.5f);
        Gizmos.DrawLine(pointBeg.transform.position, pointEnd.transform.position);
    }
}
