using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayerStatusUI : MonoBehaviour
{
    public static bool playerStatusUIOpen = false;
    [SerializeField]private PlayerStatus playerStatus;
    [SerializeField]private TMP_Text playerAvailablePoint;
    [SerializeField]private TMP_Text playerLevel;
    [SerializeField]private TMP_Text playerName;
    [SerializeField]private TMP_Text playerAge;
    [SerializeField]private TMP_Text playerHealth;
    [SerializeField]private TMP_Text playerBulletNumber;
    [SerializeField]private TMP_Text playerDamage;
    [SerializeField]private TMP_Text playerCoinNumber;
    [SerializeField]private Image playerCurrentWeaponImage;
    [SerializeField]private TMP_Text playerCurrentWeaponName;
    [SerializeField]private Image playerHat;
    [SerializeField]private Image playerCoat;
    private SpriteRenderer playerSprite;

    private void Start()
    {
        /* if (SaveLoadManager.Instance._gameData == null)
            playerStatus.CreateIntanceGame(); */
    }
    public void OpenPlayerStatus()
    {
        playerStatusUIOpen = true;
        UpdateWhenOpen();
    }
    public void ClosePlayerStatus()
    {
        playerStatusUIOpen = false;
    }
    public void UpdateWhenOpen()
    {
        this.playerAvailablePoint.text = playerStatus.availablePoint + "";
        this.playerLevel.text = playerStatus.playerLevel + "";
        this.playerName.text = playerStatus.playerName + "";
        this.playerAge.text = playerStatus.playerAge + "";
        this.playerHealth.text = playerStatus.maxHealth + "";
        this.playerBulletNumber.text = playerStatus.playerBullet + "";
        this.playerDamage.text = playerStatus.playerCurrentDamage + "";
        this.playerCoinNumber.text = playerStatus.playerCoin + "";
        this.playerCurrentWeaponImage.sprite = playerStatus.currentWeapon.itemSprite;
        this.playerCurrentWeaponName.text = playerStatus.currentWeapon.itemName;
        if(playerStatus.currentHat != null)
        {
            playerHat.sprite = playerStatus.currentHat.itemSprite;
        }
        else 
        {
            playerHat.sprite = playerStatus.defaultHat.itemSprite;
        }
        if(playerStatus.currentCoat != null)
        {
            playerCoat.sprite = playerStatus.currentCoat.itemSprite;
        }
        else 
        {
            playerCoat.sprite = playerStatus.defaultCoat.itemSprite;
        }
    }
    public void UpdateCoin()
    {
        this.playerCoinNumber.text = playerStatus.playerCoin + "";
    }

    public void UpdateDamage()
    {
        this.playerDamage.text = playerStatus.playerCurrentDamage + "";
    }
    public void UpdateMaxHealth()
    {
        this.playerHealth.text = playerStatus.maxHealth + "";
    }
    public void UpdateMaxBullet()
    {
        this.playerBulletNumber.text = playerStatus.playerBullet + "";
    }
    public void UpdateAvailablePoint()
    {
        this.playerAvailablePoint.text = playerStatus.availablePoint + "";
    }

    
}
