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
    public void AddNewItemInGround(string itemID, Vector3 pos, int itemQuantity)
    {
        listItems.Add(new ItemInGroundSaveData{itemID = itemID, itemPos = pos, itemQuantity = itemQuantity, isCollect = false});
    }
    public List<ItemInGroundSaveData> GetListItemInGround()
    {
        List<ItemInGroundSaveData> itemGroundData = new List<ItemInGroundSaveData>();
        for(int i = 0; i < listItems.Count; i++)
        {
            itemGroundData.Add(new ItemInGroundSaveData{itemID = listItems[i].itemID, itemPos = listItems[i].itemPos, itemQuantity = listItems[i].itemQuantity, isCollect = listItems[i].isCollect});
        }
        return itemGroundData;
    }
    public List<ItemInGroundSaveData> GetGroundItems()
    {
        List<ItemInGroundSaveData> itemGroundData = new List<ItemInGroundSaveData>();
        for(int i = 0; i < itemsInGround.Count; i++)
        {
            itemGroundData.Add(new ItemInGroundSaveData{itemID = itemsInGround[i].GetItemID(), itemPos = itemsInGround[i].GetItemPos(), itemQuantity = itemsInGround[i].GetItemQuantity()});
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
        itemInGroudPrefab.Clear();
        for(int i = 0; i < itemGroundData.Count; i++)
        {
            if(!itemGroundData[i].isCollect)
            {
                GameObject tmpItem = Instantiate(ItemDictionary.Instance.GetItemInfo(itemGroundData[i].itemID).itemPrefab, itemGroundData[i].itemPos, Quaternion.identity);
                tmpItem.GetComponentInChildren<CollectableItems>().SetItemQuantity(itemGroundData[i].itemQuantity);
                itemInGroudPrefab.Add(tmpItem);
            }
        }
    }
}
[System.Serializable]
public class ItemInGroundSaveData
{
    public string itemID;
    public Vector3 itemPos;
    public int itemQuantity;
    public bool isCollect = false;
}