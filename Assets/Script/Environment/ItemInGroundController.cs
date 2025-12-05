using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInGroundController : MonoBehaviour
{
    public static ItemInGroundController Instance;
    [SerializeField]private List<CollectableItems> itemsInGround = new List<CollectableItems>();
    public List<ItemInGroundSaveData> listItems = new List<ItemInGroundSaveData>();
    public List<GameObject> itemInGroudPrefab;
    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }
    public void SetItemIsCollect(string itemID, Vector3 pos)
    {
        for(int i = 0; i < listItems.Count; i++)
        {
            if(listItems[i].itemID == itemID && listItems[i].itemPos == pos)
            {
                listItems[i].isCollect = true;
                break;
            }
        }
    }
    public void AddNewItemInGround(string itemID, Vector3 pos)
    {
        listItems.Add(new ItemInGroundSaveData{itemID = itemID, itemPos = pos, isCollect = false});
    }
    public List<ItemInGroundSaveData> GetListItemInGround()
    {
        List<ItemInGroundSaveData> itemGroundData = new List<ItemInGroundSaveData>();
        for(int i = 0; i < listItems.Count; i++)
        {
            itemGroundData.Add(new ItemInGroundSaveData{itemID = listItems[i].itemID, itemPos = listItems[i].itemPos, isCollect = listItems[i].isCollect});
        }
        return itemGroundData;
    }
    public List<ItemInGroundSaveData> GetGroundItems()
    {
        List<ItemInGroundSaveData> itemGroundData = new List<ItemInGroundSaveData>();
        for(int i = 0; i < itemsInGround.Count; i++)
        {
            itemGroundData.Add(new ItemInGroundSaveData{itemID = itemsInGround[i].GetItemID(), itemPos = itemsInGround[i].GetItemPos()});
        }
        return itemGroundData;
    }
    public void SetItemInGround(List<ItemInGroundSaveData> itemGroundData)
    {
        listItems = itemGroundData;
        for(int i = 0; i < itemInGroudPrefab.Count; i++)
        {
            Destroy(itemInGroudPrefab[i]);
        }
        for(int i = 0; i < itemGroundData.Count; i++)
        {
            if(!itemGroundData[i].isCollect)
            {
                itemInGroudPrefab.Add(Instantiate(ItemDictionary.Instance.GetItemInfo(itemGroundData[i].itemID).itemPrefab, itemGroundData[i].itemPos, Quaternion.identity));
            }
        }
    }
}
[System.Serializable]
public class ItemInGroundSaveData
{
    public string itemID;
    public Vector3 itemPos;
    public bool isCollect = false;
}