using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData 
{
    public Vector3 playerPosition;
    public List<InventorySaveData> inventorySaveData;
    public List<InventorySaveData> hotBarSaveData;
    public List<ShopSaveData> shopSaveData; 
    public List<ItemInGroundSaveData> itemInGroundSaveData;
    public List<BossCurrentStatus> bossSaveData;
    public TimeSaveData timeSaveData;
    public PlayerSaveData playerSaveData;
    public MissionSaveData missionSaveData;
    public List<PuzzleSaveData> puzzleSaveData;
    public List<WeaponData> weaponListData = new List<WeaponData>();
}
