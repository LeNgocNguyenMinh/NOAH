using UnityEngine;
public class PlayerStatus : MonoBehaviour
{
    public static PlayerStatus Instance;
    [Header("---------Player basic information---------")] 
    public int playerLevel = 1;
    public int availablePoint;// Contain the points when level up (Only %2 = 0 level)
    public string playerName;
    public int playerAge = 22;
    public float playerCurrentDamage = 5;
    public int playerBullet = 3; //Number of Bullet
    public int playerCoin = 0;
    public float maxExp = 200;
    public float currentExp;
    public float maxHealth = 100;
    public float currentHealth = 100;
    [Header("---------Player Weapon---------")]
    public Item currentWeapon;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public PlayerSaveData GetPlayerInfo()
    {
        return new PlayerSaveData
        {
            playerLevelData = playerLevel,
            availablePointData = availablePoint,
            playerCurrentDamageData = playerCurrentDamage,
            playerBulletData = playerBullet,
            playerCoinData = playerCoin,
            maxExpData = maxExp,
            currentExpData = currentExp,
            maxHealthData = maxHealth,
            currentHealthData = currentHealth,
            currentWeaponID = PlayerWeaponParent.Instance.GetCurrentWeapon().itemID
        };
    }

    public void SetPlayerInfo(PlayerSaveData playerSaveData)
    {
        playerLevel = playerSaveData.playerLevelData;
        availablePoint = playerSaveData.availablePointData;
        playerCurrentDamage = playerSaveData.playerCurrentDamageData;
        playerBullet = playerSaveData.playerBulletData;
        playerCoin = playerSaveData.playerCoinData;
        maxExp = playerSaveData.maxExpData;
        currentExp = playerSaveData.currentExpData;
        maxHealth = playerSaveData.maxHealthData;
        currentHealth = playerSaveData.currentHealthData;
        currentWeapon = ItemDictionary.Instance.GetItemInfo(playerSaveData.currentWeaponID);
        PlayerEXPControl.Instance.SetCurrentExpStatus();
        PlayerHealthControl.Instance.SetCurrentHealthStatus();
        PlayerLoadout.Instance.EquipWeapon(currentWeapon);
    }
    public void SetLevel(int playerLevel)
    {
        this.playerLevel = playerLevel;
    }
    public void SetMaxEXP(float maxExp)
    {
        this.maxExp = maxExp;
    }
    public void SetCurrentEXP(float currentExp)
    {
        this.currentExp = currentExp;
    }
    public void SetName(string playerName)
    {
        this.playerName = playerName;
    }
    public void SetAge(int playerAge)
    {
        this.playerAge = playerAge;
    }
    //relate to player status UI
    public void SetDamageAmount(float newDamagePoint)
    {
        this.playerCurrentDamage += newDamagePoint;
    }
    public void SetBullet()
    {
        this.playerBullet ++;
    }
    public void SetMaxHealth(float newHealthpoint)
    {
        this.maxHealth += newHealthpoint;
    }
    public void SetAvailablePoint(int newPoint)
    {
        this.availablePoint += newPoint;
    }
    //////////
    public void SetCoin(int newCoin)
    {
        this.playerCoin = newCoin;
    }
    public void AddCoin(int newCoin)
    {
        this.playerCoin += newCoin;
    }
    public void SetCurrentHealth(float currentHealth)
    {
        this.currentHealth = currentHealth;
    }
    public void SetCurrentWeapon(Item newWeapon)
    {
        this.currentWeapon = newWeapon;
    }
}
