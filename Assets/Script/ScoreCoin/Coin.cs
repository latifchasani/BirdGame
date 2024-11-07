using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Coin : MonoBehaviour
{
    public Text scr;

    public void updateCoin(int coin)
    {
        scr.text = coin.ToString();

    }
}
