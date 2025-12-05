using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening; 

public class UIInventoryPage : MonoBehaviour
{
    public static UIInventoryPage Instance;
    [SerializeField]private UIInventoryItem itemPrefab;
    [SerializeField]private RectTransform contentPanel;
    [SerializeField]private RectTransform descriptionPanel;
    private List<UIInventoryItem> listOfUIItems;
    private UIInventoryDescription uiInventoryDescription;
    [SerializeField]private Vector2 hiddenPosition;
    [SerializeField]private Vector2 visiblePosition;
    [SerializeField]private float moveDuration = 0.5f; // Thời gian di chuyển
    public bool descriptionPanelOpen = false;
    [SerializeField]private int inventorySize;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }
    private void Start()
    {
        if(listOfUIItems == null)
        {
            InitializeInventoryUI();
        }
    }
    public void InitializeInventoryUI()
    {
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
        NotifPopUp.Instance.ShowNotification("Inventory full");
        return false;
    }
    public void OpenDescriptionPanel()
    {
        descriptionPanel.DOAnchorPos(visiblePosition, moveDuration).SetEase(Ease.OutQuad).SetUpdate(true).OnComplete(() =>
        {
            descriptionPanelOpen = true;
        });
    }
    public void CloseDescriptionPanel()
    {
        UIInventoryDescription.Instance.ItemHideInformation();
        descriptionPanel.DOAnchorPos(hiddenPosition, moveDuration).SetEase(Ease.OutQuad).SetUpdate(true).OnComplete(() =>
        {
            descriptionPanelOpen = false;
        });
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
                    MissionManager.Instance.UpdateCollectMission(item.itemID, amountOfItem);
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
                    MissionManager.Instance.UpdateCollectMission(item.itemID, amountOfItem);
                    break;
                }
            }
            return true;
        }
    }
    public void ClearInventory()
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
        if(item.itemID.Contains("Cloth")|| item.itemID.Contains("WP") || item.itemID.Contains("Stuff"))
        {
            NotifPopUp.Instance.ShowNotification("Add " + item.itemName + ".");
        }
        else{
            NotifPopUp.Instance.ShowNotification("Add " + itemQuantity + " " + item.itemName + ".");
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
            listOfUIItems[i].SetChoicePanel(false);
        }
    }
    //Some event when open inventory
    public void InventoryUpdateOpen()
    {
        OnlyClickOneSlot();
        OnlySellectOneSlot();
        uiInventoryDescription = GetComponent<UIInventoryDescription>();
        uiInventoryDescription.ItemHideInformation();
    }
    public void InventoryUpdateClose()
    {
        CloseDescriptionPanel();
    }
    public List<InventorySaveData> GetInventoryItems()
    {
        List<InventorySaveData> invData = new List<InventorySaveData>();
        for(int i = 0; i < inventorySize; i++)
        {
            if(!listOfUIItems[i].isEmpty)
            {
                invData.Add(new InventorySaveData {itemID = listOfUIItems[i].GetItemID(), itemQuantity = listOfUIItems[i].GetItemQuantity(), slotIndex = i});
            }
        }
        return invData;
    }
    public void SetInventoryItems(List<InventorySaveData> invData)
    {
        InitializeInventoryUI();
        for(int i = 0; i < invData.Count; i++)
        {
            if(invData[i].itemID != null)
            {
                listOfUIItems[invData[i].slotIndex].AddItem(ItemDictionary.Instance.GetItemInfo(invData[i].itemID), invData[i].itemQuantity);
            }
        }
    }
}
[System.Serializable]
public class InventorySaveData 
{
    public string itemID;
    public int itemQuantity;
    public int slotIndex;
}
