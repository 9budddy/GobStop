using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GobSpawnerScript : MonoBehaviour
{
    [SerializeField] private GameObject GobPrefab;
    [SerializeField] private PlayerState playerState;

    void Start()
    {
        StartCoroutine(GobSpawner());
    }

    public IEnumerator GobSpawner()
    {
        while(true)
        {
            GameObject gob = Instantiate(GobPrefab, getPos(), Quaternion.identity);
            playerState.gobs.Add(gob);
            yield return new WaitForSeconds(getWaitSeconds());
        }
    }
    private float getWaitSeconds()
    {
        if (playerState.gobPosition == 1 ||
            playerState.gobPosition == 2)
        {
            return 7f;
        }
        else if (playerState.gobPosition == 3 ||
                playerState.gobPosition == 5)
        {
            return 5f;
        }
        else if (playerState.gobPosition == 9)
        {
            return 4f;
        }
        else if (playerState.gobPosition == 4 ||
                playerState.gobPosition == 6 ||
                playerState.gobPosition == 8) 
        {
            return 3f;
        }
        else if (playerState.gobPosition == 7)
        {
            return 2f;
        }
        else
        {
            return 7f;
        }
    }

    private Vector2 getPos()
    {
        if (playerState.gobPosition == 1)
        {
            return new Vector2(8.5f, 12.5f);
        } 
        else if (playerState.gobPosition == 2)
        {
            return new Vector2(42.5f, 12.5f);
        }
        else if (playerState.gobPosition == 3)
        {
            return new Vector2(63.5f, 6.5f);
        }
        else if (playerState.gobPosition == 4)
        {
            return new Vector2(103.5f, 13.5f);
        }
        else if (playerState.gobPosition == 5)
        {
            return new Vector2(161.5f, 30.5f);
        }
        else if (playerState.gobPosition == 6)
        {
            return new Vector2(194.5f, 15.5f);
        }
        else if (playerState.gobPosition == 7)
        {
            return new Vector2(212.5f, 14.5f);
        }
        else if (playerState.gobPosition == 8)
        {
            return new Vector2(274.5f, 3.5f);
        }
        else if (playerState.gobPosition == 9)
        {
            return new Vector2(309.5f, 14.5f);
        }
        else if (playerState.gobPosition == 10)
        {
            return new Vector2(0, 0);
        }
        else
        {
            return new Vector2(8.5f, 12.5f);
        }
    }
}
