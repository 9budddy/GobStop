using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] private PlayerState playerState;
    [SerializeField] private GameObject CoinPrefab;
    [SerializeField] private GameObject Wings;
    [SerializeField] private GameObject Bouncy;
    [SerializeField] private GameObject Glue;

    private float glueTimer = 0.0f;
    private float bounceTimer = 0.0f;
    private float wingsTimer = 0.0f;

    [SerializeField] List<Vector2> coins = new List<Vector2>();


    void Start()
    {
        spawnCoins();
    }

    public void spawnCoins()
    {
        foreach (Vector2 coin in coins)
        {
            Instantiate(CoinPrefab, coin, Quaternion.identity);
        }
        
    }

    public void spawnImmediate()
    {
        if (playerState.spawnBounce)
        {
            playerState.spawnBounce = false;
            bounceTimer = 0.0f;
            Instantiate(Bouncy, playerState.BouncePosition, Quaternion.identity);
        }

        if (playerState.spawnGlue)
        {
            playerState.spawnGlue = false;
            glueTimer = 0.0f;
            Instantiate(Glue, playerState.GluePosition, Quaternion.identity);
        }

        if (playerState.spawnWings)
        {
            playerState.spawnWings = false;
            wingsTimer = 0.0f;
            Instantiate(Wings, playerState.WingsPosition, Quaternion.identity);
        }
    }

    private void spawnBounce()
    {
        if (playerState.spawnBounce)
        {
            bounceTimer += Time.deltaTime;
            if (bounceTimer >= 7.1f)
            {
                playerState.spawnBounce = false;
                bounceTimer = 0.0f;
                Instantiate(Bouncy, playerState.BouncePosition, Quaternion.identity);
            }
        }
    }

    private void spawnGlue()
    {
        if (playerState.spawnGlue)
        {
            glueTimer += Time.deltaTime;
            if (glueTimer >= 7.1f)
            {
                playerState.spawnGlue = false;
                glueTimer = 0.0f;
                Instantiate(Glue, playerState.GluePosition, Quaternion.identity);
            }
        }
    }

    private void spawnWings()
    {
        if (playerState.spawnWings)
        {
            wingsTimer += Time.deltaTime;
            if (wingsTimer >= 5.1f)
            {
                playerState.spawnWings = false;
                wingsTimer = 0.0f;
                Instantiate(Wings, playerState.WingsPosition, Quaternion.identity);
            }
        }
    }

    void Update()
    {
        spawnBounce();
        spawnGlue();
        spawnWings();
    }
}
