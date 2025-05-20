using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData 
{
    public string saveScene;
    public Vector3 playerPosition;
    public string mapBoundary;
    public List<InventorySaveData> inventorySaveData;
    public List<InventorySaveData> hotBarSaveData;
    public List<ShopSaveData> shopSaveData; 
    public List<ItemInGroundSaveData> itemInGroundSaveData;
    public TimeSaveData timeSaveData;
    public PlayerSaveData playerSaveData;
    public MissionSaveData missionSaveData;
    public List<WeaponData> weaponListData = new List<WeaponData>();
}
