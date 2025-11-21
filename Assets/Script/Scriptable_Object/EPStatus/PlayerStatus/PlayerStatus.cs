using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "NewStatus", menuName = "Status")]
public class PlayerStatus : ScriptableObject
{
    [Header("---------Player basic information---------")] 
    public int playerLevel = 1;
    public int availablePoint;// Contain the points when level up (Only %2 = 0 level)
    public string playerName;
    public int playerAge = 22;
    public float playerCurrentDamage = 5;
    public float playerWeaponDamage = 3;
    public int playerBullet = 3; //Number of Bullet
    public int playerCoin = 0;
    public Sprite playerSprite;
    public float maxExp = 200;
    public float currentExp;
    public float maxHealth = 100;
    public float currentHealth = 100;
    [Header("---------Player Weapon---------")]
    public Item defaultWeapon;
    public Item currentWeapon;
    [Header("---------Player Cloth---------")]
    public Item defaultHat;
    public Item defaultCoat;
    public Item currentHat;
    public Item currentCoat;
    private PlayerCurrentClothChange playerCurrentClothChange;
    
    public PlayerSaveData GetPlayerInfo()
    {
        return new PlayerSaveData
        {
            playerLevelData = playerLevel,
            availablePointData = availablePoint,
            playerCurrentDamageData = playerCurrentDamage,
            playerWeaponDamageData = playerWeaponDamage,
            playerBulletData = playerBullet,
            playerCoinData = playerCoin,
            maxExpData = maxExp,
            currentExpData = currentExp,
            maxHealthData = maxHealth,
            currentHealthData = currentHealth,
            currentWeaponID = currentWeapon != null ? currentWeapon.itemID : "None",
            currentHatID = currentHat != null ? currentHat.itemID : "None",
            currentCoatID = currentCoat != null ? currentCoat.itemID : "None"
        };
    }
    public void SetPlayerNewInfo()
    {
        playerLevel = 1;
        availablePoint = 3;
        playerCurrentDamage = 3;
        playerWeaponDamage = 3;
        playerBullet = 3;
        playerCoin = 0;
        maxExp = 40;
        currentExp = 0;
        maxHealth = 120;
        currentHealth = 120;
        currentWeapon = defaultWeapon;
        currentHat = null;
        currentCoat = null;
    }
    public void SetPlayerInfo(PlayerSaveData playerSaveData)
    {
        SetPlayerNewInfo();
        playerLevel = playerSaveData.playerLevelData;
        availablePoint = playerSaveData.availablePointData;
        playerCurrentDamage = playerSaveData.playerCurrentDamageData;
        playerWeaponDamage = playerSaveData.playerWeaponDamageData;
        playerBullet = playerSaveData.playerBulletData;
        playerCoin = playerSaveData.playerCoinData;
        maxExp = playerSaveData.maxExpData;
        currentExp = playerSaveData.currentExpData;
        maxHealth = playerSaveData.maxHealthData;
        currentHealth = playerSaveData.currentHealthData;
        currentWeapon = playerSaveData.currentWeaponID != "None" ? ItemDictionary.Instance.GetItemInfo(playerSaveData.currentWeaponID) : null;
        /* if(playerSaveData.currentHatID != "None")
        {
            currentHat = itemDictionary.GetItemInfo(playerSaveData.currentHatID);
            playerCurrentClothChange.ChangeCloth(currentHat);
        }
        if(playerSaveData.currentCoatID != "None")
        {
            currentCoat = itemDictionary.GetItemInfo(playerSaveData.currentCoatID);
            playerCurrentClothChange.ChangeCloth(currentCoat);
        } */
    }
    public void SetLevel(int playerLevel)
    {
        this.playerLevel = playerLevel;
    }
    public void SetName(string playerName)
    {
        this.playerName = playerName;
    }
    public void SetAge(int playerAge)
    {
        this.playerAge = playerAge;
    }
    public void SetDamageAmount(float newDamagePoint)
    {
        this.playerCurrentDamage += newDamagePoint;
    }
    public void SetCoin(int newCoin)
    {
        this.playerCoin = newCoin;
    }
    public void AddCoin(int newCoin)
    {
        this.playerCoin += newCoin;
    }
    public void SetBullet()
    {
        this.playerBullet ++;
    }
    public void SetAvailablePoint(int newPoint)
    {
        this.availablePoint += newPoint;
    }
    public void SetSprite(Sprite playerSprite)
    {
        this.playerSprite = playerSprite;
    }
    public void SetMaxEXP(float maxExp)
    {
        this.maxExp = maxExp;
    }
    public void SetCurrentEXP(float currentExp)
    {
        this.currentExp = currentExp;
    }
    public void SetMaxHealth(float newHealthpoint)
    {
        this.maxHealth += newHealthpoint;
    }
    public void SetCurrentHealth(float currentHealth)
    {
        this.currentHealth = currentHealth;
    }
    public void SetCurrentWeapon(Item newWeapon)
    {
        this.currentWeapon = newWeapon;
        this.playerWeaponDamage = this.currentWeapon.weaponDamage;
    }

    public void RespawnPlayerAfterDead()
    {
        this.currentHealth = this.maxHealth;
        this.currentExp = 0;//this and below code consider as death's punishment
        this.playerCoin /=2;
    }
    /////----------------------------------------------------------------
    public void SetCurrentHat(Item newHat)
    {
        this.currentHat = newHat;
    }
    public void SetCurrentCoat(Item newCoat)
    {
        this.currentCoat = newCoat;
    }
}
