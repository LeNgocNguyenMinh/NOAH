using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayerStatusInfo : MonoBehaviour
{
    public static PlayerStatusInfo Instance;
    [SerializeField]private PlayerStatus playerStatus;
    [SerializeField]private TMP_Text playerAvailablePoint;
    [SerializeField]private TMP_Text playerLevel;
    [SerializeField]private TMP_Text playerHealth;
    [SerializeField]private TMP_Text playerBulletNumber;
    [SerializeField]private TMP_Text playerDamage;
    [SerializeField]private TMP_Text playerCoinNumber;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }
    public void UpdateInfo()
    {
        this.playerAvailablePoint.text = playerStatus.availablePoint + "";
        this.playerLevel.text = "Level " + playerStatus.playerLevel + "";
        this.playerHealth.text = playerStatus.maxHealth + "";
        this.playerBulletNumber.text = playerStatus.playerBullet + "";
        this.playerDamage.text = playerStatus.playerCurrentDamage + "";
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
