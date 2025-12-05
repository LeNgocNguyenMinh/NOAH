using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotBarManager : MonoBehaviour
{
    public static HotBarManager Instance;
    // Start is called before the first frame update
    [SerializeField]private UIInventoryItem slot1;
    [SerializeField]private UIInventoryItem slot2;
    [SerializeField]private UIInventoryItem slot3;
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            slot1.Equip();
        }
        if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            slot2.Equip();
        }
        if(Input.GetKeyDown(KeyCode.Alpha3))
        {
            slot3.Equip();
        }

    }
    public List<InventorySaveData> GetHotBarItems()
    {
        List<InventorySaveData> hotBarData = new List<InventorySaveData>();
        if(!slot1.isEmpty)
        {
            hotBarData.Add(new InventorySaveData{itemID = slot1.GetItemID(), itemQuantity = slot1.GetItemQuantity(), slotIndex = 0});
        }
        if(!slot2.isEmpty)
        {
            hotBarData.Add(new InventorySaveData{itemID = slot2.GetItemID(), itemQuantity = slot2.GetItemQuantity(), slotIndex = 1});
        }
        if(!slot3.isEmpty)
        {
            hotBarData.Add(new InventorySaveData{itemID = slot3.GetItemID(), itemQuantity = slot3.GetItemQuantity(), slotIndex = 2});
        }
        return hotBarData;
    }
    public void SetHotBarItems(List<InventorySaveData> hotBarData)
    {
        for(int i = 0; i < hotBarData.Count; i++)
        {
            if(hotBarData[i].slotIndex == 0)
            {
                slot1.AddItem(ItemDictionary.Instance.GetItemInfo(hotBarData[i].itemID), hotBarData[i].itemQuantity);
            }
            if(hotBarData[i].slotIndex == 1)
            {
                slot2.AddItem(ItemDictionary.Instance.GetItemInfo(hotBarData[i].itemID), hotBarData[i].itemQuantity);
            }
            if(hotBarData[i].slotIndex == 2)
            {
                slot3.AddItem(ItemDictionary.Instance.GetItemInfo(hotBarData[i].itemID), hotBarData[i].itemQuantity);
            }
        }
    }
}
