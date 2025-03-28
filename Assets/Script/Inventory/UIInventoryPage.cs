using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIInventoryPage : MonoBehaviour
{
    [SerializeField]private UIInventoryItem itemPrefab;
    [SerializeField]private RectTransform contentPanel;
    private List<UIInventoryItem> listOfUIItems;
    private UIInventoryDescription uiInventoryDescription;
    public static bool inventoryOpen = false;
    [SerializeField]private int inventorySize = 10;
    ///
    private ItemDictionary itemDictionary;
    public void InitializeInventoryUI(int size)
    {
        inventorySize = size;
        if (listOfUIItems != null)
        {
            foreach (var item in listOfUIItems)
            {
                Destroy(item.gameObject);
            }
        }
        listOfUIItems = new List<UIInventoryItem>();
        for (int i = 0; i < inventorySize; i++)
        {
            UIInventoryItem uiItem = Instantiate(itemPrefab, Vector3.zero, Quaternion.identity);
            uiItem.transform.SetParent(contentPanel);
            listOfUIItems.Add(uiItem);
        }
    }
    //check available slot
    public bool InventoryHaveSpace()
    {
        for (int i = 0; i < inventorySize; i++)
        {
            if(listOfUIItems[i].isEmpty==true)
            {
                return true;
            }
        }
        PopUp.Instance.ShowNotification("Inventory full");
        return false;
    }
    //Find slot for new item
    public bool AddItem(Item item, int amountOfItem)
    {
        //merge
        for (int i = 0; i < inventorySize; i++)
        {
            if(listOfUIItems[i].isEmpty==false)
            {
                if(listOfUIItems[i].GetItem().itemID == item.itemID)
                {
                    listOfUIItems[i].AddQuantity(amountOfItem);
                    return true;
                }
            }
        }
        //add
        if(!InventoryHaveSpace())
        {
            return false;
        }
        else{
            for(int i = 0; i < inventorySize; i++)
            {
                if(listOfUIItems[i].isEmpty)
                {
                    listOfUIItems[i].AddItem(item, amountOfItem);
                    break;
                }
            }
            return true;
        }
    }
    private void ClearInventory()
    {
        for(int i = 0; i < inventorySize; i++)
        {
            if(!listOfUIItems[i].isEmpty)
            {
                listOfUIItems[i].DeleteItem();
            }
        }
    }
    public void AddItemPopUp(Item item, int itemQuantity)
    {
        if(item.itemID.Contains("Cloth")|| item.itemID.Contains("WP"))
        {
            PopUp.Instance.ShowNotification("Add " + item.itemName + ".");
        }
        else{
            PopUp.Instance.ShowNotification("Add " + itemQuantity + " " + item.itemName + ".");
        }
    }
    //event when left click
    public void OnlySellectOneSlot()
    {
        for (int i = 0; i < inventorySize; i++)
        {
            listOfUIItems[i].border.SetActive(false);
            listOfUIItems[i].SetIsSelect(false);
        }
    }
    //events when right click
    public void OnlyClickOneSlot()
    {
        if(listOfUIItems.Count < 0) return;
        for (int i = 0; i < inventorySize; i++)
        {
            listOfUIItems[i].SetChoicePanel();
        }
    }
    //Some event when open inventory
    public void InventoryOpen()
    {
        OnlyClickOneSlot();
        OnlySellectOneSlot();
        inventoryOpen = true;
        uiInventoryDescription = GetComponentInChildren<UIInventoryDescription>();
        uiInventoryDescription.ItemHideInformation();
    }
    public void InventoryClose()
    {
        inventoryOpen = false;
    }
    public List<InventorySaveData> GetInventoryItems()
    {
        List<InventorySaveData> invData = new List<InventorySaveData>();
        for(int i = 0; i < inventorySize; i++)
        {
            if(!listOfUIItems[i].isEmpty)
            {
                invData.Add(new InventorySaveData {itemID = listOfUIItems[i].itemID, itemQuantity = listOfUIItems[i].itemQuantity, slotIndex = i});
            }
        }
        return invData;
    }
    public void SetInventoryItems(List<InventorySaveData> invData)
    {
        itemDictionary = FindObjectOfType<ItemDictionary>().GetComponent<ItemDictionary>();
        InitializeInventoryUI(inventorySize);
        Debug.Log("length: " + invData.Count);
        for(int i = 0; i < invData.Count; i++)
        {
            if(invData[i].itemID != null)
            {
                Debug.Log(invData[i].slotIndex + ", " + invData[i].itemID + ", " + invData[i].itemQuantity);
                listOfUIItems[invData[i].slotIndex].AddItem(itemDictionary.GetItemInfo(invData[i].itemID), invData[i].itemQuantity);
            }
        }
    }
}
