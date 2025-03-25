using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDictionary : MonoBehaviour
{
    [SerializeField]private List<Item> itemList;
    /* [SerializeField]private List<GameObject> itemObjectList; */
    private Dictionary<string, Item> itemDictionary;
    private void Awake()
    {
        itemDictionary = new Dictionary<string, Item>();
        foreach(Item item in itemList)
        {
            itemDictionary[item.itemID] = item;
        }
    }
    public Item GetItemInfo(string itemID)
    {
        if (itemDictionary.TryGetValue(itemID, out Item item))
        {
            return item;
        }
        Debug.LogWarning($"Không tìm thấy Item ID {itemID} trong dictionary");
        return null; // Trả về null nếu không tìm thấy
    }
    /* public GameObject GetItemObject(string itemID)
    {
        for(int i = 0; i < itemObjectList.Count; i++)
        {
            if(itemObjectList[i].GetComponent<CollectableItems>().GetItemID() == itemID)
            {
                return itemObjectList[i];
            }
        }
    } */
}
