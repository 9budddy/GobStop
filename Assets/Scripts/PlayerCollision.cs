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

                setExtras(2);
                
                AudioManager.instance.Play("purchase");
                Destroy(collision.gameObject);
                
                
            }
            else if (playerState.level == 2 && playerState.coins == 2)
            {

                setExtras(3);
               
                AudioManager.instance.Play("purchase");
                Destroy(collision.gameObject);
            }
            else if (playerState.level == 3 && playerState.coins == 3)
            {

                setExtras(4);

                AudioManager.instance.Play("purchase");
                Destroy(collision.gameObject);
            }
            else if (playerState.level == 4 && playerState.coins == 4)
            {

                setExtras(5);

                AudioManager.instance.Play("purchase");
                Destroy(collision.gameObject);
            }
            else if (playerState.level == 5 && playerState.coins == 5)
            {

                setExtras(6);

                AudioManager.instance.Play("purchase");
                Destroy(collision.gameObject);
            }
            else if (playerState.level == 6 && playerState.coins == 6)
            {

                setExtras(7);

                AudioManager.instance.Play("purchase");
                Destroy(collision.gameObject);
            }
            else if (playerState.level == 7 && playerState.coins == 7)
            {

                setExtras(8);

                AudioManager.instance.Play("purchase");
                Destroy(collision.gameObject);
            }
            else if (playerState.level == 8 && playerState.coins == 8)
            {
                setExtras(9);

                AudioManager.instance.Play("purchase");
                Destroy(collision.gameObject);
            }
            else if (playerState.level == 9 && playerState.coins == 9)
            {
                setExtras(10);

                AudioManager.instance.Play("purchase");
                Destroy(collision.gameObject);
            }
        }
    }

    private void setExtras(int num)
    {
        playerState.level = num;
        playerState.coinsRequired = num;
        playerState.gobPosition = num;
        playerState.professorPosition = num;

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
