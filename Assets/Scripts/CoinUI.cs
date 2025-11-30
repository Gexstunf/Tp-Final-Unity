using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinUI : MonoBehaviour
{
    public TextMeshProUGUI coinText;

    private void Start()
    {
        coinText.text = "0 / " +
                        GameManager.instance.totalCoins;

        UpdateCounter();
    }

    public void UpdateCounter()
    {
        coinText.text = GameManager.instance.collectedCoins + " / " +
                        GameManager.instance.totalCoins;
    }
}
