using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ShopItemSlot : MonoBehaviour
{
    private Item item;
    private int numberOfItem;
    [SerializeField]private TMP_Text itemName;
    [SerializeField]private TMP_Text itemLeftNumber;
    [SerializeField]private TMP_Text itemUses;
    [SerializeField]private TMP_Text itemPrice;
    [SerializeField]private Image itemImage;
    [SerializeField]private PlayerStatus playerStatus; 
    private ShopController shopController;
    private UIInventoryPage uiInventoryPage;
    public void SetItem(Item newValue)
    {
        item = newValue;
        itemName.text = newValue.itemName;
        itemUses.text = "Recover " + newValue.healthRecover + " HP";
        itemPrice.text = newValue.itemPrice.ToString();
        itemImage.sprite = newValue.itemSprite;
    }
    public void SetNumberOfItem(int newValue)
    {
        numberOfItem = newValue;
        itemLeftNumber.text = newValue.ToString(); 
    }
    public string GetItemID()
    {
        return item.itemID;
    }
    public int GetLeftNumber()
    {
        return numberOfItem;
    }
    public void BuyItem()
    {
        if(numberOfItem == 0)
        {
            NotifPopUp.Instance.ShowNotification("Nah, we run out of " + item.itemName);
            return;
        }
        if(playerStatus.playerCoin >= item.itemPrice)
        {
            uiInventoryPage = FindObjectOfType<UIInventoryPage>().GetComponent<UIInventoryPage>();
            if(!uiInventoryPage.AddItem(item, 1))
            {
                return;
            }
            NotifPopUp.Instance.ShowNotification("Add 1 " + item.itemName + " to inventory.");
            numberOfItem--;
            Debug.Log("Left: " + numberOfItem);
            SetNumberOfItem(numberOfItem);
            shopController = FindObjectOfType<ShopController>().GetComponent<ShopController>();
            shopController.CoinTextUpdateAfterBuy(item.itemPrice);
        }
        else
        {
            NotifPopUp.Instance.ShowNotification("Ha ha poor guy!!!");
        }
    }
}
