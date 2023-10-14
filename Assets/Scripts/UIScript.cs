using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

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
        else if (playerState.gobPosition == 3)
        {
            return "Red Velvet";
        }
        else if (playerState.gobPosition == 4)
        {
            return "StrawBerry";
        }
        else if (playerState.gobPosition == 5)
        {
            return "Orange";
        }
        else if (playerState.gobPosition == 6)
        {
            return "Pumpkin";
        }
        else if (playerState.gobPosition == 7)
        {
            return "Carrot";
        }
        else if (playerState.gobPosition == 8)
        {
            return "Banana";
        }
        else if (playerState.gobPosition == 9)
        {
            return "Chocolate";
        }
        else if (playerState.gobPosition == 10)
        {
            return "Carolina Reaper";
        }
        else
        {
            return "Neopolitan";
        }
    }
}
