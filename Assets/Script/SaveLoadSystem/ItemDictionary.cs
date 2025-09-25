using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDictionary : MonoBehaviour
{
    public static ItemDictionary Instance;
    [SerializeField]private List<Item> itemList;
    [SerializeField]private List<BossStatus> boss;
    private Dictionary<string, Item> itemDictionary;
    private Dictionary<string, BossStatus> bossDictionary;
    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
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
}
