using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RespawnPlayer : MonoBehaviour
{
    [SerializeField] private GameObject Player;
    [SerializeField] private CinemachineVirtualCamera cinemachineVirtualCamera;

    private CinemachineFramingTransposer cinemachineFramingTransposer;

    private float respawnTimer = 0.0f;
    private bool isRespawn = false;

    private enum CheckPoints
    {
        ONE = 25
    }

    private void Awake()
    {
        cinemachineFramingTransposer = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
    }

    void Update()
    {
        if (isRespawn)
        {
            if (respawnTimer > 0.5f)
            {
                cinemachineFramingTransposer.m_DeadZoneHeight = 0.47f;
                isRespawn = false;
            }
            respawnTimer += Time.deltaTime;
        }
        
        

        if (Player.transform.position.y < -5f)
        {
            playerRespawn();
            isRespawn = true;
            respawnTimer = 0.0f;
        }
    }

    private void playerRespawn()
    {
        float x = Player.transform.position.x;

        if (x < (float)CheckPoints.ONE)
        {
            Vector2 respawnPlayerPos = new Vector2(-4.45f, -2.45f);
            Player.transform.localPosition = respawnPlayerPos;
            cinemachineFramingTransposer.m_DeadZoneHeight = 0f;
        }
    }
}
