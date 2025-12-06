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
    [SerializeField]private List<Item> weaponList;
    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    public void SaveGame()
    {
        saveLocation = Path.Combine(Application.persistentDataPath, "saveData.json");
        existingData = File.Exists(saveLocation) ? 
            JsonUtility.FromJson<SaveData>(File.ReadAllText(saveLocation)) : 
            new SaveData();
        SaveData saveData = new SaveData
        {
            saveScene = SceneManager.GetActiveScene().name,
            playerPosition = Player.Instance.transform.position,
            inventorySaveData = UIInventoryPage.Instance.GetInventoryItems(),
            mapBoundary = existingData.mapBoundary,
            hotBarSaveData = HotBarManager.Instance.GetHotBarItems(),
            shopSaveData = ShopController.Instance.GetListItemInShop(),
            itemInGroundSaveData = ItemInGroundController.Instance.GetListItemInGround(),
            bossSaveData = BossSaveData.Instance.GetAllBossCurrentStatus(),
            timeSaveData = TimeManager.Instance.GetTime(),
            playerSaveData = PlayerStatus.Instance.GetPlayerInfo(),
            missionSaveData = MissionManager.Instance.GetMissionList(),
        };
        for(int i = 0; i < weaponList.Count; i++)
        {
            saveData.weaponListData.Add(weaponList[i].GetWeaponData());
        }
        File.WriteAllText(saveLocation, JsonUtility.ToJson(saveData, true));
        NotifPopUp.Instance.ShowNotification("Save success.");
    }
    public void SaveGameByBed()
    {
        saveLocation = Path.Combine(Application.persistentDataPath, "saveData.json");
        existingData = File.Exists(saveLocation) ? 
            JsonUtility.FromJson<SaveData>(File.ReadAllText(saveLocation)) : 
            new SaveData();
        SaveData saveData = new SaveData
        {
            saveScene = SceneManager.GetActiveScene().name,
            playerPosition = Player.Instance.transform.position,
            inventorySaveData = UIInventoryPage.Instance.GetInventoryItems(),
            mapBoundary = existingData.mapBoundary,
            hotBarSaveData = HotBarManager.Instance.GetHotBarItems(),
            shopSaveData = existingData.shopSaveData,
            itemInGroundSaveData = existingData.itemInGroundSaveData,
            bossSaveData = existingData.bossSaveData,
            timeSaveData = TimeManager.Instance.GetTimeSkip(),
            playerSaveData = PlayerStatus.Instance.GetPlayerInfo(),
            missionSaveData = MissionManager.Instance.GetMissionList()
        };
           for(int i = 0; i < weaponList.Count; i++)
        {
            saveData.weaponListData.Add(weaponList[i].GetWeaponData());
        }
        File.WriteAllText(saveLocation, JsonUtility.ToJson(saveData, true));
        NotifPopUp.Instance.ShowNotification("Save success.");
    }
    public void LoadSave()
    {
        saveLocation = Path.Combine(Application.persistentDataPath, "saveData.json");
        if(File.Exists(saveLocation))
        {
            SaveData saveData = JsonUtility.FromJson<SaveData>(File.ReadAllText(saveLocation));
            Player.Instance.transform.position = saveData.playerPosition;
            UIInventoryPage.Instance.SetInventoryItems(saveData.inventorySaveData);
            UIInventoryPage.Instance.SetInventoryItems(saveData.inventorySaveData);
            HotBarManager.Instance.SetHotBarItems(saveData.hotBarSaveData);
            ShopController.Instance.SetListItemInShop(saveData.shopSaveData);
            TimeManager.Instance.SetTime(saveData.timeSaveData);
            ItemInGroundController.Instance.SetItemInGround(saveData.itemInGroundSaveData);
            BossSaveData.Instance.SetBossCurrentStatus(saveData.bossSaveData);
            PlayerStatus.Instance.SetPlayerInfo(saveData.playerSaveData);
            MissionManager.Instance.SetMissionList(saveData.missionSaveData);          
        }
    }

    public void LoadNewGame()
    {
        newGameSaveLocation = Path.Combine(Application.persistentDataPath, "newGameData.json");
        
        string defaultNewGameJson = @"{
        ""saveScene"": ""Level1"",
        ""playerPosition"": { ""x"": -26.4, ""y"": 6.7, ""z"": 0.0 },
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
            { ""itemID"": ""HPFruit_01"", ""itemPos"": { ""x"": -15.3, ""y"": 3.2, ""z"": 0.0 }, ""itemQuantity"": 1, ""isCollect"": false },
            { ""itemID"": ""HPFruit_01"", ""itemPos"": { ""x"": -23.05, ""y"": -2.18, ""z"": 0.0 }, ""itemQuantity"": 1, ""isCollect"": false },
            { ""itemID"": ""HPFruit_01"", ""itemPos"": { ""x"": -22.03, ""y"": -2.16, ""z"": 0.0 }, ""itemQuantity"": 1, ""isCollect"": false },
            { ""itemID"": ""HPFruit_01"", ""itemPos"": { ""x"": -3.53, ""y"": -8.92, ""z"": 0.0 }, ""itemQuantity"": 1, ""isCollect"": false },
            { ""itemID"": ""HPPotion_03"", ""itemPos"": { ""x"": -2.29, ""y"": -8.72, ""z"": 0.0 }, ""itemQuantity"": 1, ""isCollect"": false },
            { ""itemID"": ""HPPotion_01"", ""itemPos"": { ""x"": -23.83, ""y"": -1.7, ""z"": 0.0 }, ""itemQuantity"": 1, ""isCollect"": false },
            { ""itemID"": ""WP_04"", ""itemPos"": { ""x"": -15.0, ""y"": 9.86, ""z"": 0.0 }, ""itemQuantity"": 1, ""isCollect"": false },
            { ""itemID"": ""Stuff_Note_01"", ""itemPos"": { ""x"": -1.31, ""y"": 1.46, ""z"": 0.0 }, ""itemQuantity"": 1, ""isCollect"": false }
        ],
        ""bossSaveData"": [
            { ""bossID"": ""B_01"", ""isDead"": ""false"", ""bossPos"": { ""x"": -16.73, ""y"": 75.6, ""z"": 0.0 }},
            { ""bossID"": ""B_03"", ""isDead"": ""false"", ""bossPos"": { ""x"": 118.42, ""y"": 6.6, ""z"": 0.0 }}
        ],
        ""timeSaveData"": { ""minData"": 0.0, ""hourData"": 0.0, ""dateData"": 0 },
        ""playerSaveData"": {
            ""playerLevelData"": 1,
            ""availablePointData"": 3,
            ""playerCurrentDamageData"": 5.0,
            ""playerBulletData"": 6,
            ""playerCoinData"": 50,
            ""maxExpData"": 40.0,
            ""currentExpData"": 0.0,
            ""maxHealthData"": 200.0,
            ""currentHealthData"": 200.0,
            ""currentWeaponID"": ""WP_03""
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
        SaveData saveData = JsonUtility.FromJson<SaveData>(File.ReadAllText(newGameSaveLocation));    
        Player.Instance.transform.position = saveData.playerPosition;
        UIInventoryPage.Instance.SetInventoryItems(saveData.inventorySaveData);
        UIInventoryPage.Instance.SetInventoryItems(saveData.inventorySaveData);
        HotBarManager.Instance.SetHotBarItems(saveData.hotBarSaveData);
        ShopController.Instance.SetListItemInShop(saveData.shopSaveData);
        TimeManager.Instance.SetTime(saveData.timeSaveData);
        ItemInGroundController.Instance.SetItemInGround(saveData.itemInGroundSaveData);
        BossSaveData.Instance.SetBossCurrentStatus(saveData.bossSaveData);
        PlayerStatus.Instance.SetPlayerInfo(saveData.playerSaveData);
        MissionManager.Instance.SetMissionList(saveData.missionSaveData);     
        SaveGame();
    }
}
