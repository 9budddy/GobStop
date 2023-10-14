using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProfessorScript : MonoBehaviour
{
    [SerializeField] private PlayerState playerState;


    void Update()
    {
        Vector3 position = Vector3.zero;
        if (playerState.professorPosition == 1)
        {
            position = new Vector3(9.5f, 12.5f);
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else if (playerState.professorPosition == 2)
        {
            position = new Vector3(43.5f, 12.5f);
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else if (playerState.professorPosition == 3)
        {
            position = new Vector3(63.5f, 5.5f);
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        else if (playerState.professorPosition == 4)
        {
            position = new Vector3(103.5f, 13.5f);
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        else if (playerState.professorPosition == 5)
        {
            position = new Vector3(162.5f, 30.5f);
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else if (playerState.professorPosition == 6)
        {
            position = new Vector3(195.5f, 15.5f);
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else if (playerState.professorPosition == 7)
        {
            position = new Vector3(211.5f, 14.5f);
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        else if (playerState.professorPosition == 8)
        {
            position = new Vector3(274.5f, 2.5f);
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else if (playerState.professorPosition == 9)
        {
            position = new Vector3(309.5f, 13.5f);
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else if (playerState.professorPosition == 10)
        {
            //position = new Vector3(103.5f, 13.5f);
            //transform.localScale = new Vector3(1f, 1f, 1f);
        }
        else
        {
            position = new Vector3(-10f, -10f);
        }

        if (transform.position != position)
        {
            transform.position = position;
        }
    }
}
