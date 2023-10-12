using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Playables;

public class PlayerCollision : MonoBehaviour
{
    [SerializeField] private PlayerState playerState;
    [SerializeField] private RespawnPlayer respawnPlayer;
    [SerializeField] private GameObject GobSpawner;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Coin"))
        {
            playerState.coins += 1;
            AudioManager.instance.Play("collectcoin");
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.tag.Equals("Wings"))
        {
            playerState.wings = true;
            playerState.spawnWings = true;
            playerState.WingsPosition = collision.gameObject.transform.position;
            AudioManager.instance.Play("wings");
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.tag.Equals("Bouncy"))
        {
            playerState.bounce = true;
            playerState.spawnBounce = true;
            playerState.BouncePosition = collision.gameObject.transform.position;
            AudioManager.instance.Play("redcowdrink");
            Destroy(collision.gameObject);
            
        }

        if (collision.gameObject.tag.Equals("Glue"))
        {
            playerState.glue = true;
            playerState.spawnGlue = true;
            playerState.GluePosition = collision.gameObject.transform.position;
            AudioManager.instance.Play("glue");
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.tag.Equals("Gob"))
        {
            respawnPlayer.playerRespawn();
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.tag.Equals("Saw"))
        {
            respawnPlayer.playerRespawn();
        }

        if (collision.gameObject.tag.Equals("Fire"))
        {
            respawnPlayer.playerRespawn();
        }

        if (collision.gameObject.tag.Equals("Trap"))
        {
            if (playerState.level == 1 && playerState.coins == 1)
            {
                playerState.level = 2;
                playerState.coinsRequired = 2;
                playerState.gobPosition = 2;
                playerState.professorPosition = 2;

                setExtras();
                
                AudioManager.instance.Play("purchase");
                Destroy(collision.gameObject);
                
                
            }
            else if (playerState.level == 2 && playerState.coins == 2)
            {
                playerState.level = 3;
                playerState.coinsRequired = 3;
                playerState.gobPosition = 3;
                playerState.professorPosition = 3;

                setExtras();
               
                AudioManager.instance.Play("purchase");
                Destroy(collision.gameObject);
            }
        }
    }

    private void setExtras()
    {
        playerState.coins = 0;
        Destroy(playerState.spawnerObject);
        foreach (GameObject gob in playerState.gobs)
        {
            Destroy(gob);
        }
        playerState.gobs = new List<GameObject>();
        playerState.spawnerObject = Instantiate(GobSpawner, new Vector3(6f, 4f), Quaternion.identity);
    }
}
