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


    public void SetAllTMPData()
    {
        itemInGroundController = FindObjectOfType<ItemInGroundController>()?.GetComponent<ItemInGroundController>();
        tmpListItemsInGround = itemInGroundController.GetListItemInGround();
        

        inventoryPage = FindObjectOfType<UIInventoryPage>()?.GetComponent<UIInventoryPage>();
        tmpInventory = inventoryPage.GetInventoryItems();
        
        hotBarManager = FindObjectOfType<HotBarManager>()?.GetComponent<HotBarManager>();
        tmpHotBar = hotBarManager.GetHotBarItems();

        tmpPlayer = playerStatus.GetPlayerInfo();

        TimeManager timeManager = FindObjectOfType<TimeManager>()?.GetComponent<TimeManager>();
        tmpTime = timeManager.GetTime();
    }
}
