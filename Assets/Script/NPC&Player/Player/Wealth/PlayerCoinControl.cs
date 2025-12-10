using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCoinControl : MonoBehaviour
{   
    public static PlayerCoinControl Instance;
    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }
    public void AddCoin(int newCoin)
    {
        PlayerStatus.Instance.AddCoin(newCoin);
    }
}
