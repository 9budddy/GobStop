using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RespawnPlayer : MonoBehaviour
{
    [SerializeField] private PlayerState playerState;
    [SerializeField] private GameObject Player;
    [SerializeField] private CinemachineVirtualCamera cinemachineVirtualCamera;
    [SerializeField] private GameObject GobSpawner;
    [SerializeField] private PlayerMovementScript playerMovementScript;

    private CinemachineFramingTransposer cinemachineFramingTransposer;

    private float respawnTimer = 0.0f;
    private bool isRespawn = false;
    

    private void Start()
    {
        
        cinemachineFramingTransposer = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
        playerState.spawnerObject = Instantiate(GobSpawner, new Vector3(6f, 4f), Quaternion.identity);
        playerRespawn();
    }

    void Update()
    {
        if (isRespawn)
        {
            if (respawnTimer > .5f)
            {
                playerState.spawnerObject = Instantiate(GobSpawner, new Vector3(6f, 4f), Quaternion.identity);
                cinemachineFramingTransposer.m_DeadZoneHeight = 0.47f;
                isRespawn = false;
            }
            respawnTimer += Time.deltaTime;
        }
        
        

        if (Player.transform.position.y < -5f)
        {
            playerRespawn();
        }

        
    }

    public void playerRespawn()
    {
        isRespawn = true;
        respawnTimer = 0.0f;
        float x = Player.transform.position.x;
        Vector2 respawnPlayerPos;
        float endLevelX;

        if (playerState.level == 1)
        {
            endLevelX = 25f;
            respawnPlayerPos = new Vector2(-4.45f, -2.45f);

        } 
        else if (playerState.level == 2)
        {
            endLevelX = 58f;
            respawnPlayerPos = new Vector2(28.45f, -2.45f);
        }
        else
        {
            endLevelX = -20f;
            respawnPlayerPos = new Vector2(-4.45f, -2.45f);
        }

        if (x < endLevelX)
        {
            playerMovementScript.setTimers();
            Destroy(playerState.spawnerObject);
            foreach (GameObject gob in playerState.gobs)
            {
                Destroy(gob);
            }
            playerState.gobs = new List<GameObject>();
            Rigidbody2D rb = Player.GetComponent<Rigidbody2D>();
            rb.velocity = new Vector2(0f, 0f);
            Player.transform.localPosition = respawnPlayerPos;
            cinemachineFramingTransposer.m_DeadZoneHeight = 0f;
        }
    }
}
