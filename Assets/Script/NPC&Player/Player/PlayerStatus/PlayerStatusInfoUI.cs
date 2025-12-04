using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayerStatusInfoUI : MonoBehaviour
{
    public static PlayerStatusInfoUI Instance;
    [SerializeField]private TMP_Text playerAvailablePoint;
    [SerializeField]private TMP_Text playerLevel;
    [SerializeField]private TMP_Text playerHealth;
    [SerializeField]private TMP_Text playerBulletNumber;
    [SerializeField]private TMP_Text playerDamage;
    [SerializeField]private TMP_Text playerCoinNumber;
    [Header("Button")]
    [SerializeField]private Button bulletBtn;
    [SerializeField]private Button healthBtn;
    [SerializeField]private Button damageBtn;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }
    public void UpdateInfo()
    {
        CheckAvailablePoint();
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
    private void OnEnable()
    {
        healthBtn.onClick.AddListener(AddPointToHealth);
        damageBtn.onClick.AddListener(AddPointToDamage);
        bulletBtn.onClick.AddListener(AddPointToBullet);
    }
    public void CheckAvailablePoint()//Check only Number of available >= 2 then show all Button, else only show button which need 1 point
    {
        if(PlayerStatus.Instance.availablePoint <= 0)
        {
            healthBtn.gameObject.SetActive(false);
            damageBtn.gameObject.SetActive(false);
            bulletBtn.gameObject.SetActive(false);
        }
        else if(PlayerStatus.Instance.availablePoint <= 1)
        {
            healthBtn.gameObject.SetActive(true);
            damageBtn.gameObject.SetActive(true);
            bulletBtn.gameObject.SetActive(false);
        }
        else
        {
            healthBtn.gameObject.SetActive(true);
            damageBtn.gameObject.SetActive(true);
            bulletBtn.gameObject.SetActive(true);
        }
    }
    public void AddPointToDamage()
    {
        PlayerStatus.Instance.SetDamageAmount(2);//Add 5 damage to player Damage
        UpdateDamage();// Update UI
        AddPoint(1);//Check button
    }
    public void AddPointToHealth()
    {
        PlayerStatus.Instance.SetMaxHealth(10);
        UpdateMaxHealth();
        PlayerHealthControl.Instance.SetCurrentHealthStatus();//Update (Curren/MaxHealth) in UI Health Bar
        AddPoint(1);
    }
    public void AddPointToBullet()
    {
        PlayerStatus.Instance.SetBullet();
        UpdateMaxBullet();
        PlayerWeaponParent.Instance.UpdateMagazine();
        AddPoint(2);
    }
    private void AddPoint(int value)
    {
        PlayerStatus.Instance.SetAvailablePoint(-value);
        UpdateAvailablePoint();
        CheckAvailablePoint();
    }
}
