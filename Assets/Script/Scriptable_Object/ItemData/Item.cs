using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "Item")]
public class Item : ScriptableObject
{
    [Header("----General Item Info----")] 
    public string itemName;
    public string itemDescription;
    public string itemID;
    public int itemPrice;
    public Sprite itemSprite;
    public GameObject itemPrefab;
    //Those below attributes only use for the HP item.
    [Header("----HP Item Info----")] 
    public float healthRecover;
    //Those below attributes only use for the weapon item.
    [Header("----Weapon Item Info----")] 
    public GameObject weaponBulletType;
}
