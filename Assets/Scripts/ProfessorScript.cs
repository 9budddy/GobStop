using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProfessorScript : MonoBehaviour
{
    [SerializeField] private PlayerState playerState;


    void Update()
    {
        Vector3 position;
        if (playerState.professorPosition == 1)
        {
            position = new Vector3(9.5f, 12.5f);
        }
        else if (playerState.professorPosition == 2)
        {
            position = new Vector3(43.5f, 12.5f);
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
