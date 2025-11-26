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
    }
    public Item GetItemInfo(string itemID)
    {
        itemDictionary = new Dictionary<string, Item>();
        foreach(Item itm in itemList)
        {
            itemDictionary[itm.itemID] = itm;
        }
        if (itemDictionary.TryGetValue(itemID, out Item item))
        {
            return item;
        }
        Debug.LogWarning($"Không tìm thấy Item ID {itemID} trong dictionary");
        return null; // Trả về null nếu không tìm thấy
    }
    public BossStatus GetBossInfo(string bossID)
    {
        bossDictionary = new Dictionary<string, BossStatus>();
        foreach(BossStatus bossStat in boss)
        {
            bossDictionary[bossStat.bossID] = bossStat;
        }
        if (bossDictionary.TryGetValue(bossID, out BossStatus bossStatus))
        {
            return bossStatus;
        }
        Debug.LogWarning($"Không tìm thấy Boss ID {bossID} trong dictionary");
        return null; // Trả về null nếu không tìm thấy
    }
}
