using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;

public class AddAvailablePoint : MonoBehaviour
{
    private HealthControl playerHealthControl;//Add player Health Script
    private WeaponParent weaponParent;
    [SerializeField]private GameObject healthButton;//Add 'Add' button point in Health
    [SerializeField]private GameObject damageButton;//Add 'Add' button point in Damage
    [SerializeField]private GameObject bulletButton;//Add 'Add' button point in Bullet
    [SerializeField]private PlayerStatus playerStatus;
  
    public void CheckAvailablePoint()//Check only Number of available >= 2 then show all Button, else only show button which need 1 point
    {
        if(playerStatus.availablePoint <= 0)
        {
            healthButton.SetActive(false);
            damageButton.SetActive(false);
            bulletButton.SetActive(false);
        }
        else if(playerStatus.availablePoint <= 1)
        {
            healthButton.SetActive(true);
            damageButton.SetActive(true);
            bulletButton.SetActive(false);// Because bullet need 2 point to Upgrade
        }
        else
        {
            healthButton.SetActive(true);
            damageButton.SetActive(true);
            bulletButton.SetActive(true);
        }
    }
    public void AddPointToDamage()
    {
        playerStatus.SetDamageAmount(2);//Add 5 damage to player Damage
        PlayerStatusUI.Instance.UpdateDamage();// Update UI
        AddOnePoint();//Check button
    }
    public void AddPointToHealth()
    {
        playerStatus.SetMaxHealth(10);
        PlayerStatusUI.Instance.UpdateMaxHealth();
        HealthControl.Instance.UpdateMaxHealth();//Update (Curren/MaxHealth) in UI Health Bar
        AddOnePoint();
    }
    public void AddPointToBullet()
    {
        weaponParent = FindObjectOfType<PlayerControl>().GetComponentInChildren<WeaponParent>();
        playerStatus.SetBullet();
        PlayerStatusUI.Instance.UpdateMaxBullet();
        weaponParent.UpdateMagazine();
        AddTwoPoint();
    }
    private void AddOnePoint()
    {
        playerStatus.SetAvailablePoint(-1);
        PlayerStatusUI.Instance.UpdateAvailablePoint();
        CheckAvailablePoint();
    }
    private void AddTwoPoint()
    {
        playerStatus.SetAvailablePoint(-2);
        PlayerStatusUI.Instance.UpdateAvailablePoint();
        CheckAvailablePoint();
    }
}
