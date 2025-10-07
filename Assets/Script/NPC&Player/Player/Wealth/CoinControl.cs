using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinControl : MonoBehaviour
{   
    public static CoinControl Instance;
    [SerializeField]private PlayerStatus playerStatus;
    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }
    public void AddCoin(int newCoin)
    {
        playerStatus.AddCoin(newCoin);
    }
}
