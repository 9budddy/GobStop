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
            yield return new WaitForSeconds(7f);
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
        else
        {
            return new Vector2(8.5f, 12.5f);
        }
    }
}
