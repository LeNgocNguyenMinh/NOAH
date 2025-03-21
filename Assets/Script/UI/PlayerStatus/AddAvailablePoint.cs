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
    private PlayerStatusUI playerStatusUI;// For Player Status UI Update
  
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
        playerStatusUI = FindObjectOfType<PlayerStatusUI>().GetComponent<PlayerStatusUI>();
        playerStatus.SetDamageAmount(2);//Add 5 damage to player Damage
        playerStatusUI.UpdateDamage();// Update UI
        AddOnePoint();//Check button

    }
    public void AddPointToHealth()
    {
        playerStatusUI = FindObjectOfType<PlayerStatusUI>().GetComponent<PlayerStatusUI>();
        playerHealthControl = FindObjectOfType<PlayerControl>().GetComponent<HealthControl>();
        playerStatus.SetMaxHealth(10);
        playerStatusUI.UpdateMaxHealth();
        playerHealthControl.UpdateMaxHealth();//Update (Curren/MaxHealth) in UI Health Bar
        AddOnePoint();
    }
    public void AddPointToBullet()
    {
        playerStatusUI = FindObjectOfType<PlayerStatusUI>().GetComponent<PlayerStatusUI>();
        weaponParent = FindObjectOfType<PlayerControl>().GetComponentInChildren<WeaponParent>();
        playerStatus.SetBullet();
        playerStatusUI.UpdateMaxBullet();
        weaponParent.UpdateMagazine();
        AddTwoPoint();
    }
    private void AddOnePoint()
    {
        playerStatusUI = FindObjectOfType<PlayerStatusUI>().GetComponent<PlayerStatusUI>();
        playerStatus.SetAvailablePoint(-1);
        playerStatusUI.UpdateAvailablePoint();
        CheckAvailablePoint();
    }
    private void AddTwoPoint()
    {
        playerStatusUI = FindObjectOfType<PlayerStatusUI>().GetComponent<PlayerStatusUI>();
        playerStatus.SetAvailablePoint(-2);
        playerStatusUI.UpdateAvailablePoint();
        CheckAvailablePoint();
    }
}
