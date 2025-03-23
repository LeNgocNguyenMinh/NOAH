using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Cinemachine;

public class SaveController : MonoBehaviour
{
    public static SaveController Instance;
    private string saveLocation = Path.Combine("D:/NOAHGame/NOAH/Assets/Script/SaveLoadSystem/", "saveData.json");
    private UIInventoryPage uiInventoryPage;
    private HotBarManager hotBarManager;
    private ShopController shopController;
    private TimeManager timeManager;
    [SerializeField]private PlayerStatus playerStatus;
    void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        LoadSave();
    }
    public void SaveGame()
    {
        uiInventoryPage = FindObjectOfType<UIInventoryPage>()?.GetComponent<UIInventoryPage>();
        hotBarManager = FindObjectOfType<HotBarManager>()?.GetComponent<HotBarManager>();
        timeManager = FindObjectOfType<TimeManager>()?.GetComponent<TimeManager>();
        shopController = FindObjectOfType<ShopController>()?.GetComponent<ShopController>();
        SaveData saveData = new SaveData
        {
            playerPosition = FindObjectOfType<PlayerControl>().transform.position,
            mapBoundary = FindObjectOfType<CinemachineConfiner>().m_BoundingShape2D.gameObject.name,
            inventorySaveData = uiInventoryPage?.GetInventoryItems(),
            hotBarSaveData = hotBarManager?.GetHotBarItems(),
            shopSaveData = shopController?.GetListItemInShop(),
            timeSaveData = timeManager?.GetTime(),
            playerSaveData = playerStatus?.GetPlayerInfo()
        };
        File.WriteAllText(saveLocation, JsonUtility.ToJson(saveData));
        PopUp.Instance.ShowNotification("Save success.");
    }
    public void LoadSave()
    {
        if(File.Exists(saveLocation))
        {
            SaveData saveData = JsonUtility.FromJson<SaveData>(File.ReadAllText(saveLocation));
            FindObjectOfType<PlayerControl>().transform.position = saveData.playerPosition;
            FindObjectOfType<CinemachineConfiner>().m_BoundingShape2D = GameObject.Find(saveData.mapBoundary).GetComponent<PolygonCollider2D>();
            uiInventoryPage = FindObjectOfType<UIInventoryPage>()?.GetComponent<UIInventoryPage>();
            timeManager = FindObjectOfType<TimeManager>()?.GetComponent<TimeManager>();
            hotBarManager = FindObjectOfType<HotBarManager>()?.GetComponent<HotBarManager>();
            shopController = FindObjectOfType<ShopController>()?.GetComponent<ShopController>();
            if(saveData.inventorySaveData!=null)
            {
                uiInventoryPage.SetInventoryItems(saveData.inventorySaveData);
            }
            if(saveData.hotBarSaveData!=null)
            {
                hotBarManager.SetHotBarItems(saveData.hotBarSaveData);
            }
            if(saveData.shopSaveData!=null)
            {
                shopController.SetListItemInShop(saveData.shopSaveData);
            }
            if(saveData.timeSaveData!=null)
            {
                timeManager.SetTime(saveData.timeSaveData);
            }
            if(saveData.playerSaveData!=null)
            {
                playerStatus.SetPlayerInfo(saveData.playerSaveData);
            }
        }
        else{
            SaveGame();
        }
    }
}
