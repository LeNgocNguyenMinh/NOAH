using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Cinemachine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class SaveController : MonoBehaviour
{
    public static SaveController Instance;
    private string saveLocation;
    private string newGameSaveLocation;
    private SaveData existingData;
    private UIInventoryPage uiInventoryPage;
    private HotBarManager hotBarManager;
    private ShopController shopController;
    private TimeManager timeManager;
    private ItemInGroundController itemInGroundController;
    private MissionManager missionManager;
    [SerializeField]private PlayerStatus playerStatus;
    [SerializeField]private List<Item> weaponList;
    [SerializeField]private MissionScriptable missionScriptable;
    void Awake()
    {
        saveLocation = Path.Combine(Application.persistentDataPath, "saveData.json");
        newGameSaveLocation = Path.Combine(Application.persistentDataPath, "newGameData.json");
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
        missionManager = FindObjectOfType<MissionManager>()?.GetComponent<MissionManager>();
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
            playerSaveData = playerStatus?.GetPlayerInfo(),
            missionSaveData = missionManager?.GetMissionList()
        };
        for(int i = 0; i < weaponList.Count; i++)
        {
            saveData.weaponListData.Add(weaponList[i].GetWeaponData());
        }
        if(FindObjectOfType<CinemachineConfiner>()!=null)
        {
            if(FindObjectOfType<CinemachineConfiner>()?.m_BoundingShape2D.gameObject.name!=null)
            {
                saveData.mapBoundary = FindObjectOfType<CinemachineConfiner>().m_BoundingShape2D.gameObject.name;
            }
        }
        if(FindObjectOfType<ShopController>()!=null)
        {
            shopController = FindObjectOfType<ShopController>().GetComponent<ShopController>();
            saveData.shopSaveData = shopController.GetListItemInShop();
        }
        File.WriteAllText(saveLocation, JsonUtility.ToJson(saveData, true));
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
        missionManager = FindObjectOfType<MissionManager>()?.GetComponent<MissionManager>();
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
            playerSaveData = playerStatus?.GetPlayerInfo(),
            missionSaveData = missionManager?.GetMissionList()
        };
        if(FindObjectOfType<CinemachineConfiner>()!=null)
        {
            if(FindObjectOfType<CinemachineConfiner>()?.m_BoundingShape2D.gameObject.name!=null)
            {
                saveData.mapBoundary = FindObjectOfType<CinemachineConfiner>().m_BoundingShape2D.gameObject.name;
            }
        }
        if(FindObjectOfType<ShopController>()!=null)
        {
            shopController = FindObjectOfType<ShopController>().GetComponent<ShopController>();
            saveData.shopSaveData = shopController.GetListItemInShop();
        }
        for(int i = 0; i < weaponList.Count; i++)
        {
            saveData.weaponListData.Add(weaponList[i].GetWeaponData());
        }
        File.WriteAllText(saveLocation, JsonUtility.ToJson(saveData, true));
        PopUp.Instance.ShowNotification("Save success.");
    }
    public void LoadSave(Vector3 playerPos = new Vector3(), List<InventorySaveData> inventoryItemsTMP = null, List<InventorySaveData> hotBarItemsTMP = null, List<ItemInGroundSaveData> listItemsTMP = null, TimeSaveData timeDataTMP = null, PlayerSaveData playerDataTMP = null, MissionSaveData missionSaveDataTMP = null, bool loadWeaponData = true)
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
            missionManager = FindObjectOfType<MissionManager>()?.GetComponent<MissionManager>();

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
            if(missionSaveDataTMP != null)
            {
                missionManager.SetMissionList(missionSaveDataTMP);
            }
            else if(saveData.missionSaveData != null && missionManager != null)
            {
                missionManager.SetMissionList(saveData.missionSaveData);
            }
            if(loadWeaponData == true)
            {
                for(int i = 0; i < weaponList.Count; i++)
                {
                    weaponList[i].SetWeaponData(saveData.weaponListData[i]);
                }
            }            
        }
    }

    public void LoadNewGame()
    {
        if(File.Exists(newGameSaveLocation))
        {
            Debug.Log("Co file new game");
            SaveData saveData = JsonUtility.FromJson<SaveData>(File.ReadAllText(newGameSaveLocation));
            FindObjectOfType<PlayerControl>().transform.position = saveData.playerPosition;
            FindObjectOfType<CinemachineConfiner>().m_BoundingShape2D = GameObject.Find(saveData.mapBoundary).GetComponent<PolygonCollider2D>();
            uiInventoryPage = FindObjectOfType<UIInventoryPage>()?.GetComponent<UIInventoryPage>();
            timeManager = FindObjectOfType<TimeManager>()?.GetComponent<TimeManager>();
            hotBarManager = FindObjectOfType<HotBarManager>()?.GetComponent<HotBarManager>();
            itemInGroundController = FindObjectOfType<ItemInGroundController>()?.GetComponent<ItemInGroundController>();
            missionManager = FindObjectOfType<MissionManager>()?.GetComponent<MissionManager>();
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
            for(int i = 0; i < weaponList.Count; i++)
            {
                weaponList[i].SetWeaponDefaultData();
            }
            if(missionManager != null)
            {
                missionManager.SetMissionList(saveData.missionSaveData);
            }
            SaveGame();
        }
        else{
            Debug.Log("khong co file new game");
                string defaultNewGameJson = @"{
                ""saveScene"": ""Level1"",
                ""playerPosition"": { ""x"": 1.7463607, ""y"": -4.6992025, ""z"": 0.0 },
                ""mapBoundary"": ""L1"",
                ""inventorySaveData"": [],
                ""hotBarSaveData"": [],
                ""shopSaveData"": [
                    { ""itemID"": ""HPFruit_05"", ""itemLeftNumber"": 5 },
                    { ""itemID"": ""HPFruit_06"", ""itemLeftNumber"": 5 },
                    { ""itemID"": ""HPFruit_07"", ""itemLeftNumber"": 5 },
                    { ""itemID"": ""HPPotion_03"", ""itemLeftNumber"": 5 },
                    { ""itemID"": ""HPPotion_02"", ""itemLeftNumber"": 5 }
                ],
                ""itemInGroundSaveData"": [
                    { ""itemID"": ""HPFruit_01"", ""itemPos"": { ""x"": -15.3, ""y"": 3.2, ""z"": 0.0 }, ""isCollect"": false },
                    { ""itemID"": ""HPFruit_01"", ""itemPos"": { ""x"": -23.05, ""y"": -2.18, ""z"": 0.0 }, ""isCollect"": false },
                    { ""itemID"": ""HPFruit_01"", ""itemPos"": { ""x"": -22.03, ""y"": -2.16, ""z"": 0.0 }, ""isCollect"": false },
                    { ""itemID"": ""HPFruit_01"", ""itemPos"": { ""x"": -3.53, ""y"": -8.92, ""z"": 0.0 }, ""isCollect"": false },
                    { ""itemID"": ""HPPotion_03"", ""itemPos"": { ""x"": -2.29, ""y"": -8.72, ""z"": 0.0 }, ""isCollect"": false },
                    { ""itemID"": ""HPPotion_01"", ""itemPos"": { ""x"": -23.83, ""y"": -1.7, ""z"": 0.0 }, ""isCollect"": false },
                    { ""itemID"": ""FireCloth_Hat_FireHat"", ""itemPos"": { ""x"": 7.02, ""y"": -7.23, ""z"": 0.0 }, ""isCollect"": false },
                    { ""itemID"": ""FireCloth_Coat_FireCoat"", ""itemPos"": { ""x"": 11.57, ""y"": -7.08, ""z"": 0.0 }, ""isCollect"": false },
                    { ""itemID"": ""IceCloth_Coat_IceCoat"", ""itemPos"": { ""x"": 10.64, ""y"": -10.47, ""z"": 0.0 }, ""isCollect"": false },
                    { ""itemID"": ""IceCloth_Hat_IceHat"", ""itemPos"": { ""x"": 5.42, ""y"": -10.25, ""z"": 0.0 }, ""isCollect"": false },
                    { ""itemID"": ""WP_04"", ""itemPos"": { ""x"": -15.0, ""y"": 9.86, ""z"": 0.0 }, ""isCollect"": false },
                    { ""itemID"": ""Stuff_Note_01"", ""itemPos"": { ""x"": -1.31, ""y"": 1.46, ""z"": 0.0 }, ""isCollect"": false }
                ],
                ""timeSaveData"": { ""minData"": 0.0, ""hourData"": 0.0, ""dateData"": 0 },
                ""playerSaveData"": {
                    ""playerLevelData"": 1,
                    ""availablePointData"": 3,
                    ""playerCurrentDamageData"": 20.0,
                    ""playerWeaponDamageData"": 3.0,
                    ""playerBulletData"": 6,
                    ""playerCoinData"": 250,
                    ""maxExpData"": 40.0,
                    ""currentExpData"": 0.0,
                    ""maxHealthData"": 200.0,
                    ""currentHealthData"": 200.0,
                    ""currentWeaponID"": ""WP_03"",
                    ""currentHatID"": ""None"",
                    ""currentCoatID"": ""None""
                },
                ""missionSaveData"": {
                    ""missionList"": [
                        {
                            ""currentAmount"": 0,
                            ""isFinish"": false,
                            ""missionID"": ""MS01""
                        },
                        {
                            ""currentAmount"": 0,
                            ""isFinish"": false,
                            ""missionID"": ""MS02""
                        },
                        {
                            ""currentAmount"": 0,
                            ""isFinish"": false,
                            ""missionID"": ""MS03""
                        },
                        {
                            ""currentAmount"": 0,
                            ""isFinish"": false,
                            ""missionID"": ""NPC_MS01""
                        }
                    ],
                    ""currentMissionID"": """"
                },
                ""weaponListData"": [
                    { ""weaponID"": ""WP_01"", ""weaponLevel"": 1, ""materialNeedToUpgrade"": 50, ""weaponDamage"": 10.0 },
                    { ""weaponID"": ""WP_05"", ""weaponLevel"": 1, ""materialNeedToUpgrade"": 45, ""weaponDamage"": 8.0 },
                    { ""weaponID"": ""WP_02"", ""weaponLevel"": 1, ""materialNeedToUpgrade"": 50, ""weaponDamage"": 8.0 },
                    { ""weaponID"": ""WP_04"", ""weaponLevel"": 1, ""materialNeedToUpgrade"": 50, ""weaponDamage"": 9.0 },
                    { ""weaponID"": ""WP_03"", ""weaponLevel"": 1, ""materialNeedToUpgrade"": 75, ""weaponDamage"": 3.0 }
                ]
            }";

            File.WriteAllText(newGameSaveLocation, defaultNewGameJson);
            LoadNewGame();
        }
    }
}
