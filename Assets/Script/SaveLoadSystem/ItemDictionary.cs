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
        bossDictionary = new Dictionary<string, BossStatus>();
        foreach(BossStatus bossStatus in boss)
        {
            bossDictionary[bossStatus.bossID] = bossStatus;
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
    public BossStatus GetBossInfo(string bossID)
    {
        if (bossDictionary.TryGetValue(bossID, out BossStatus bossStatus))
        {
            return bossStatus;
        }
        Debug.LogWarning($"Không tìm thấy Boss ID {bossID} trong dictionary");
        return null; // Trả về null nếu không tìm thấy
    }
}
