using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInGroundController : MonoBehaviour
{
    /* [SerializeField]private List<CollectableItems> listItemInGround = new List<CollectableItems>();
    private ItemDictionary itemDictionary;
    public List<ItemInGroundData> GetListItemInGround()
    {
        List<ItemInGroundData> itemGroundData = new List<ItemInGroundData>();
        for(int i = 0; i < listItemInGround.Count; i++)
        {
            itemGroundData.Add(new ItemInGroundData{itemIDData = listItemInGround[i].itemID, itemPos = listItemInGround});
        }
    }
    public void SetListItemInGround(List<ItemInGroundData> itemGroundData)
    {
        itemDictionary = FindObjectOfType<ItemDictionary>().GetComponent<ItemDictionary>();
        for(int i = 0; i < itemGroundData.Count; i++)
        {
            Instantiate(itemDictionary.GetItemObject(itemGroundData[i].itemIDData), temGroundData[i].itemPos, Quaternion.Identity);
        }
    } */
}
