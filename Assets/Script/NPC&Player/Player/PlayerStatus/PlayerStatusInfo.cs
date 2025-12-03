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
        this.playerAvailablePoint.text = PlayerStatus.Instance.availablePoint + "";
        this.playerLevel.text = "Level " + PlayerStatus.Instance.playerLevel + "";
        this.playerHealth.text = PlayerStatus.Instance.maxHealth + "";
        this.playerBulletNumber.text = PlayerStatus.Instance.playerBullet + "";
        this.playerDamage.text = PlayerStatus.Instance.playerCurrentDamage + "";
        this.playerCoinNumber.text = PlayerStatus.Instance.playerCoin + "";
    }
    public void UpdateDamage()
    {
        this.playerDamage.text = PlayerStatus.Instance.playerCurrentDamage + "";
    }
    public void UpdateMaxHealth()
    {
        this.playerHealth.text = PlayerStatus.Instance.maxHealth + "";
    }
    public void UpdateMaxBullet()
    {
        this.playerBulletNumber.text = PlayerStatus.Instance.playerBullet + "";
    }
    public void UpdateAvailablePoint()
    {
        this.playerAvailablePoint.text = PlayerStatus.Instance.availablePoint + "";
    }  
}
