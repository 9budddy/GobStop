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

    private Vector2 coin1 = new Vector2(-1.5f, 2.5f);
    private Vector2 coin2 = new Vector2(50.5f, 0.5f);
    private Vector2 coin3 = new Vector2(29.5f, 5.5f);
    void Start()
    {
        spawnCoins();
    }

    private void spawnCoins()
    {
        Instantiate(CoinPrefab, coin1, Quaternion.identity);
        Instantiate(CoinPrefab, coin2, Quaternion.identity);
        Instantiate(CoinPrefab, coin3, Quaternion.identity);
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
