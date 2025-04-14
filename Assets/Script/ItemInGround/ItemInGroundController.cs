using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemInGroundController : MonoBehaviour
{
    [SerializeField]private List<CollectableItems> listItemInGround = new List<CollectableItems>();
    public List<ItemInGroundSaveData> tmpList = new List<ItemInGroundSaveData>();
    public List<GameObject> itemInGroudPrefab;
    private ItemDictionary itemDictionary;
   /*  private void Start()
    {
        tmpList = GetGroundItems();
        Debug.Log(tmpList.Count);
    } */
    public void SetItemIsCollect(string itemID, Vector3 pos)
    {
        for(int i = 0; i < tmpList.Count; i++)
        {
            if(tmpList[i].itemID == itemID && tmpList[i].itemPos == pos)
            {
                Debug.Log("CÓ");
                tmpList[i].isCollect = true;
                break;
            }
        }
    }

    public List<ItemInGroundSaveData> GetListItemInGround()
    {
        List<ItemInGroundSaveData> itemGroundData = new List<ItemInGroundSaveData>();
        for(int i = 0; i < tmpList.Count; i++)
        {
            itemGroundData.Add(new ItemInGroundSaveData{itemID = tmpList[i].itemID, itemPos = tmpList[i].itemPos, isCollect = tmpList[i].isCollect});
        }
        return itemGroundData;
    }
    public List<ItemInGroundSaveData> GetGroundItems()
    {
        List<ItemInGroundSaveData> itemGroundData = new List<ItemInGroundSaveData>();
        for(int i = 0; i < listItemInGround.Count; i++)
        {
            itemGroundData.Add(new ItemInGroundSaveData{itemID = listItemInGround[i].GetItemID(), itemPos = listItemInGround[i].GetItemPos()});
        }
        return itemGroundData;
    }
    public void SetItemInGround(List<ItemInGroundSaveData> itemGroundData)
    {
        itemDictionary = FindObjectOfType<ItemDictionary>().GetComponent<ItemDictionary>();
        tmpList = itemGroundData;
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
