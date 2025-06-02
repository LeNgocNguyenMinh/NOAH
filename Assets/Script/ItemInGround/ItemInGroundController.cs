using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInGroundController : MonoBehaviour
{
    [SerializeField]private List<CollectableItems> itemsInGround = new List<CollectableItems>();
    public List<ItemInGroundSaveData> listItems = new List<ItemInGroundSaveData>();
    public List<GameObject> itemInGroudPrefab;
    private ItemDictionary itemDictionary;

    public void SetItemIsCollect(string itemID, Vector3 pos)
    {
        for(int i = 0; i < listItems.Count; i++)
        {
            if(listItems[i].itemID == itemID && listItems[i].itemPos == pos)
            {
                Debug.Log("CÓ");
                listItems[i].isCollect = true;
                break;
            }
        }
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
        itemDictionary = FindObjectOfType<ItemDictionary>().GetComponent<ItemDictionary>();
        listItems = itemGroundData;
        for(int i = 0; i < itemInGroudPrefab.Count; i++)
        {
            Destroy(itemInGroudPrefab[i]);
        }
        for(int i = 0; i < itemGroundData.Count; i++)
        {
            if(!itemGroundData[i].isCollect)
            {
                itemInGroudPrefab.Add(Instantiate(itemDictionary.GetItemInfo(itemGroundData[i].itemID).itemPrefab, itemGroundData[i].itemPos, Quaternion.identity));
            }
        }
    }
}
