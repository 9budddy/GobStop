using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIScript : MonoBehaviour
{
    [SerializeField] private PlayerState playerState;
    [SerializeField] private TextMeshProUGUI CoinsForGobs;


    void Update()
    {
        CoinsForGobs.text = getGobName() + "\n" + playerState.coins.ToString() + "/" + playerState.coinsRequired.ToString() + " Coins";
    }

    private string getGobName()
    {
        if (playerState.gobPosition == 1)
        {
            return "Neopolitan";
        }
        else if (playerState.gobPosition == 2)
        {
            return "Blueberry";
        }
        else
        {
            return "Neopolitan";
        }
    }
}
