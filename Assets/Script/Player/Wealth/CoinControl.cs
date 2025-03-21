using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinControl : MonoBehaviour
{   
    [SerializeField]private PlayerStatus playerStatus;
    private PlayerStatusUI playerStatusUI;

    public void AddCoin(int newCoin)
    {
        playerStatusUI = FindObjectOfType<PlayerStatusUI>().GetComponent<PlayerStatusUI>();
        playerStatus.AddCoin(newCoin);
        playerStatusUI.UpdateCoin();
    }
}
