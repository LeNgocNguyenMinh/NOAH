using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class ShopController : MonoBehaviour
{   
    public static bool shopPanelIsOpen = false;
    [SerializeField]private List<ShopItemSlot> listOfItemSlot = new List<ShopItemSlot>();
    [SerializeField]private List<Item> listItemForShop = new List<Item>();
    [SerializeField]private TMP_Text playerCoin;
    [SerializeField]private PlayerStatus playerStatus;
    private ItemDictionary itemDictionary;
   /*  private void Start()
    {
        AddItemToShop();
    } */
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
        int randomStartIndex = Random.Range(0, listItemForShop.Count - 4);
        for(int i = 0; i < listOfItemSlot.Count; i++)
        {
            listOfItemSlot[i].SetItem(listItemForShop[randomStartIndex]);
            listOfItemSlot[i].SetNumberOfItem(5);
            randomStartIndex ++;
        }
    }
    public List<ShopSaveData> GetListItemInShop()
    {
        List<ShopSaveData> shopData = new List<ShopSaveData>();
        for(int i = 0; i < listOfItemSlot.Count; i++)
        {
            shopData.Add(new ShopSaveData{itemID = listOfItemSlot[i].GetItemID(), itemLeftNumber = listOfItemSlot[i].GetLeftNumber()});
        }
        return shopData;
    }
    public void SetListItemInShop(List<ShopSaveData> shopData)
    {
        itemDictionary = FindObjectOfType<ItemDictionary>().GetComponent<ItemDictionary>();
        for(int i = 0; i < shopData.Count; i++)
        {
            listOfItemSlot[i].SetItem(itemDictionary.GetItemInfo(shopData[i].itemID));
            listOfItemSlot[i].SetNumberOfItem(shopData[i].itemLeftNumber);
        }
    }
}
