using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinTextManager : MonoBehaviour
{
    public Inventory PInventory;
    public TextMeshProUGUI CoinDisplay;
    public void UpdateCoinCOunt()
    {
        CoinDisplay.text = "" + PInventory.Coins;
    }
}
