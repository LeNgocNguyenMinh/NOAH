using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ShopController : MonoBehaviour
{   
    public static bool shopPanelIsOpen = false;
    [SerializeField]private List<ShopItem> listOfItem = new List<ShopItem>();
    [SerializeField]private List<Item> itemForShop = new List<Item>();
    [SerializeField]private TMP_Text playerCoin;
    [SerializeField]private PlayerStatus playerStatus;

    private void Start()
    {
        AddItemToShop();
    }
    public void UpdateWhenOpen()
    {
        playerCoin.text = playerStatus.playerCoin.ToString();
    }
    public void CoinTextUpdateAfterBuy(int newValue)
    {
        playerStatus.AddCoin(-newValue);
        playerCoin.text = playerStatus.playerCoin.ToString();
    }
    private void AddItemToShop()
    {
        for(int i = 0; i < listOfItem.Count; i++)
        {
            listOfItem[i].SetItem(itemForShop[i]);
            listOfItem[i].SetNumberOfItem(5);
        }
    }
}
