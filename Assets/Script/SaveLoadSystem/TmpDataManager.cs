using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TmpDataManager : MonoBehaviour
{
    private ItemInGroundController itemInGroundController;
    private UIInventoryPage inventoryPage;
    private HotBarManager hotBarManager;
    [SerializeField]private PlayerStatus playerStatus;

    public static List<ItemInGroundSaveData> tmpListItemsInGround = new List<ItemInGroundSaveData>();
    public static TimeSaveData tmpTime = new TimeSaveData();
    public static List<InventorySaveData> tmpInventory = new List<InventorySaveData>();
    public static List<InventorySaveData> tmpHotBar = new List<InventorySaveData>();
    public static PlayerSaveData tmpPlayer = new PlayerSaveData();
    public static MissionSaveData tmpMission = new MissionSaveData(); 


    public void SetAllTMPData()
    {
        itemInGroundController = FindObjectOfType<ItemInGroundController>()?.GetComponent<ItemInGroundController>();
        if(itemInGroundController != null)
        {
            tmpListItemsInGround = itemInGroundController.GetListItemInGround();
        }  
        
        inventoryPage = FindObjectOfType<UIInventoryPage>()?.GetComponent<UIInventoryPage>();
        if(inventoryPage != null)
        {
            tmpInventory = inventoryPage.GetInventoryItems();
        }
        
        
        hotBarManager = FindObjectOfType<HotBarManager>()?.GetComponent<HotBarManager>();
        if(hotBarManager != null)
        {
            tmpHotBar = hotBarManager.GetHotBarItems();
        }

        tmpPlayer = playerStatus.GetPlayerInfo();

        TimeManager timeManager = FindObjectOfType<TimeManager>()?.GetComponent<TimeManager>();
        if(timeManager != null)
        {
            tmpTime = timeManager.GetTime();
        }

        /* MissionManager missionManager = FindObjectOfType<MissionManager>()?.GetComponent<MissionManager>();
        if(missionManager != null)
        {
            tmpMission = missionManager.GetCurrentMission();
        } */
    }
}
