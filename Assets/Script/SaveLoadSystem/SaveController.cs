using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Cinemachine;
using UnityEngine.SceneManagement;

public class SaveController : MonoBehaviour
{
    public static SaveController Instance;
    private string saveLocation = Path.Combine("D:/NOAHGame/NOAH/Assets/Script/SaveLoadSystem/", "saveData.json");
    private string newGameSaveLocation = Path.Combine("D:/NOAHGame/NOAH/Assets/Script/SaveLoadSystem/", "newGameData.json");
    private UIInventoryPage uiInventoryPage;
    private HotBarManager hotBarManager;
    private ShopController shopController;
    private TimeManager timeManager;
    private ItemInGroundController itemInGroundController;
    [SerializeField]private PlayerStatus playerStatus;
    private SaveData existingData;
    void Awake()
    {
        Instance = this;
    }
    
    public void SaveGame()
    {
        existingData = File.Exists(saveLocation) ? 
            JsonUtility.FromJson<SaveData>(File.ReadAllText(saveLocation)) : 
            new SaveData();
        uiInventoryPage = FindObjectOfType<UIInventoryPage>()?.GetComponent<UIInventoryPage>();
        hotBarManager = FindObjectOfType<HotBarManager>()?.GetComponent<HotBarManager>();
        timeManager = FindObjectOfType<TimeManager>()?.GetComponent<TimeManager>();
        itemInGroundController = FindObjectOfType<ItemInGroundController>()?.GetComponent<ItemInGroundController>();
        SaveData saveData = new SaveData
        {
            saveScene = SceneManager.GetActiveScene().name,
            playerPosition = FindObjectOfType<PlayerControl>().transform.position,
            inventorySaveData = uiInventoryPage?.GetInventoryItems(),
            mapBoundary = existingData.mapBoundary,
            hotBarSaveData = hotBarManager?.GetHotBarItems(),
            shopSaveData = existingData.shopSaveData,
            itemInGroundSaveData = itemInGroundController?.GetListItemInGround(),
            timeSaveData = timeManager?.GetTime(),
            playerSaveData = playerStatus?.GetPlayerInfo()
        };
        Debug.Log(saveData.saveScene);
        if(FindObjectOfType<CinemachineConfiner>()!=null)
        {
            Debug.Log("mapBound: Tìm thấy");
            if(FindObjectOfType<CinemachineConfiner>()?.m_BoundingShape2D.gameObject.name!=null)
            {
                saveData.mapBoundary = FindObjectOfType<CinemachineConfiner>().m_BoundingShape2D.gameObject.name;
                Debug.Log("mapBound: " + saveData.mapBoundary);
            }
        }
        if(FindObjectOfType<ShopController>()!=null)
        {
            shopController = FindObjectOfType<ShopController>().GetComponent<ShopController>();
            saveData.shopSaveData = shopController.GetListItemInShop();
        }
        File.WriteAllText(saveLocation, JsonUtility.ToJson(saveData));
        PopUp.Instance.ShowNotification("Save success.");
    }
    public void SaveGameByBed()
    {
        existingData = File.Exists(saveLocation) ? 
            JsonUtility.FromJson<SaveData>(File.ReadAllText(saveLocation)) : 
            new SaveData();
        uiInventoryPage = FindObjectOfType<UIInventoryPage>()?.GetComponent<UIInventoryPage>();
        hotBarManager = FindObjectOfType<HotBarManager>()?.GetComponent<HotBarManager>();
        timeManager = FindObjectOfType<TimeManager>()?.GetComponent<TimeManager>();
        SaveData saveData = new SaveData
        {
            saveScene = SceneManager.GetActiveScene().name,
            playerPosition = FindObjectOfType<PlayerControl>().transform.position,
            inventorySaveData = uiInventoryPage?.GetInventoryItems(),
            mapBoundary = existingData.mapBoundary,
            hotBarSaveData = hotBarManager?.GetHotBarItems(),
            shopSaveData = existingData.shopSaveData,
            itemInGroundSaveData = existingData.itemInGroundSaveData,
            timeSaveData = timeManager?.GetTimeSkip(),
            playerSaveData = playerStatus?.GetPlayerInfo()
        };
        Debug.Log(saveData.saveScene);
        if(FindObjectOfType<CinemachineConfiner>()!=null)
        {
            Debug.Log("mapBound: Tìm thấy");
            if(FindObjectOfType<CinemachineConfiner>()?.m_BoundingShape2D.gameObject.name!=null)
            {
                saveData.mapBoundary = FindObjectOfType<CinemachineConfiner>().m_BoundingShape2D.gameObject.name;
                Debug.Log("mapBound: " + saveData.mapBoundary);
            }
        }
        if(FindObjectOfType<ShopController>()!=null)
        {
            shopController = FindObjectOfType<ShopController>().GetComponent<ShopController>();
            saveData.shopSaveData = shopController.GetListItemInShop();
        }
        File.WriteAllText(saveLocation, JsonUtility.ToJson(saveData));
        PopUp.Instance.ShowNotification("Save success.");
    }
    public void LoadSave(Vector3 playerPos = new Vector3(), List<InventorySaveData> inventoryItemsTMP = null, List<InventorySaveData> hotBarItemsTMP = null, List<ItemInGroundSaveData> listItemsTMP = null, TimeSaveData timeDataTMP = null, PlayerSaveData playerDataTMP = null)
    {
        if(File.Exists(saveLocation))
        {
            SaveData saveData = JsonUtility.FromJson<SaveData>(File.ReadAllText(saveLocation));
            if(playerPos != Vector3.zero)
            {
                FindObjectOfType<PlayerControl>().transform.position = playerPos;
            }
            else{
                FindObjectOfType<PlayerControl>().transform.position = saveData.playerPosition;
            }
            if(FindObjectOfType<CinemachineConfiner>()!=null && saveData.mapBoundary != null)
            {
                FindObjectOfType<CinemachineConfiner>().m_BoundingShape2D = GameObject.Find(saveData.mapBoundary).GetComponent<PolygonCollider2D>();
            }
            uiInventoryPage = FindObjectOfType<UIInventoryPage>()?.GetComponent<UIInventoryPage>();
            timeManager = FindObjectOfType<TimeManager>()?.GetComponent<TimeManager>();
            hotBarManager = FindObjectOfType<HotBarManager>()?.GetComponent<HotBarManager>();
            itemInGroundController = FindObjectOfType<ItemInGroundController>()?.GetComponent<ItemInGroundController>();
            shopController = FindObjectOfType<ShopController>()?.GetComponent<ShopController>();

            if(inventoryItemsTMP != null && uiInventoryPage != null)
            {
                uiInventoryPage.SetInventoryItems(inventoryItemsTMP);
            }
            else if(saveData.inventorySaveData!=null && uiInventoryPage != null)
            {
                uiInventoryPage.SetInventoryItems(saveData.inventorySaveData);
            }

            if(hotBarItemsTMP != null && hotBarManager != null)
            {
                hotBarManager.SetHotBarItems(hotBarItemsTMP);
            }
            else if(saveData.hotBarSaveData != null && hotBarManager != null)
            {
                hotBarManager.SetHotBarItems(saveData.hotBarSaveData);
            }

            if(saveData.shopSaveData != null && shopController != null)
            {
                shopController = FindObjectOfType<ShopController>().GetComponent<ShopController>();
                shopController.SetListItemInShop(saveData.shopSaveData);
            }

            if(timeDataTMP != null && timeManager != null)
            {
                timeManager.SetTime(timeDataTMP);
            }
            else if(saveData.timeSaveData != null && timeManager != null)
            {
                timeManager.SetTime(saveData.timeSaveData);
            }

            if(listItemsTMP != null && itemInGroundController != null)
            {
                itemInGroundController.SetItemInGround(listItemsTMP);
            }
            else if(saveData.itemInGroundSaveData != null && itemInGroundController != null)
            {
                itemInGroundController.SetItemInGround(saveData.itemInGroundSaveData);
            }

            if(playerDataTMP != null)
            {
                playerStatus.SetPlayerInfo(playerDataTMP);
            }
            else if(saveData.playerSaveData!=null)
            {
                playerStatus.SetPlayerInfo(saveData.playerSaveData);
            }
        }
    }

    public void LoadNewGame()
    {
        if(File.Exists(newGameSaveLocation))
        {
            SaveData saveData = JsonUtility.FromJson<SaveData>(File.ReadAllText(newGameSaveLocation));
            FindObjectOfType<PlayerControl>().transform.position = saveData.playerPosition;
            FindObjectOfType<CinemachineConfiner>().m_BoundingShape2D = GameObject.Find(saveData.mapBoundary).GetComponent<PolygonCollider2D>();
            uiInventoryPage = FindObjectOfType<UIInventoryPage>()?.GetComponent<UIInventoryPage>();
            timeManager = FindObjectOfType<TimeManager>()?.GetComponent<TimeManager>();
            hotBarManager = FindObjectOfType<HotBarManager>()?.GetComponent<HotBarManager>();
            itemInGroundController = FindObjectOfType<ItemInGroundController>()?.GetComponent<ItemInGroundController>();
            if(saveData.inventorySaveData!=null)
            {
                uiInventoryPage.SetInventoryItems(saveData.inventorySaveData);
            }
            if(saveData.hotBarSaveData!=null)
            {
                hotBarManager.SetHotBarItems(saveData.hotBarSaveData);
            }
            if(FindObjectOfType<ShopController>()!=null)
            {
                if(saveData.shopSaveData!=null)
                {
                    shopController = FindObjectOfType<ShopController>().GetComponent<ShopController>();
                    shopController.SetListItemInShop(saveData.shopSaveData);
                }
            }
            if(saveData.timeSaveData!=null)
            {
                timeManager.SetTime(saveData.timeSaveData);
            }
            if(saveData.playerSaveData!=null)
            {
                playerStatus.SetPlayerInfo(saveData.playerSaveData);
            }
            if(saveData.itemInGroundSaveData!=null)
            {
                itemInGroundController.SetItemInGround(saveData.itemInGroundSaveData);
            }
        }
    }
}
