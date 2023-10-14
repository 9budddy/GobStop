using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UIElements;

public class MovingTraps1 : MonoBehaviour
{

    [SerializeField] private GameObject[] points;
    [SerializeField] private int targetPoint;
    [SerializeField] private float speed = 0f;

    private Rigidbody2D rb;
    private int i = 0;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        i = targetPoint;
    }

    void Update()
    {
        if (Vector2.Distance(transform.position, points[i].transform.position) < 0.2f)
        {
            i++;
        }
        if (i == points.Length)
        {
            i = 0;
        }
        transform.position = Vector2.MoveTowards(transform.position, points[i].transform.position, speed * Time.deltaTime);
                

    }

    private void OnDrawGizmos()
    {
        for(int i = 0; i < points.Length; i++)
        {
            Gizmos.DrawWireSphere(points[i].transform.position, 0.5f);
            if (i != points.Length - 1)
            {
                Gizmos.DrawLine(points[i].transform.position, points[i + 1].transform.position);
            }
            else
            {
                Gizmos.DrawLine(points[i].transform.position, points[0].transform.position);
            }
        }
    }
}
