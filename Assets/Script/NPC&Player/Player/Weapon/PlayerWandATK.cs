using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerWandATK : MonoBehaviour
{
    public static PlayerWandATK Instance;
    private Animator wandATKAnimator;
    [SerializeField]private float delayWandATK = 0.5f;
    [SerializeField]private Transform firePoint;
    private GameObject bulletPrefap;
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
    public void CheckWandATK()
    {
        bulletPrefap = PlayerWeaponParent.Instance.playerStatus.currentWeapon.weaponBulletType;
        if(PlayerWeaponParent.Instance.delayWandCount > 0) PlayerWeaponParent.Instance.delayWandCount-=Time.deltaTime;
        if(!PlayerWeaponParent.Instance.playerCanATK)return;
        if(Input.GetMouseButtonDown(0) && PlayerWeaponParent.Instance.delayWandCount <= 0)
        {
            PlayerWeaponParent.Instance.delayWandCount = delayWandATK;//Set up delay time between each shoot
            PlayerWeaponParent.Instance.wandSprite.enabled = true;
            PlayerWeaponParent.Instance.physicATKSprite.enabled = false;
            WandShoot();
        }
    }
    private void WandShoot()
    {
        if(PlayerWeaponParent.Instance.currentBullet<=0)return;
        PlayerSound.Instance.PlayShootSound();//play the shoot sound
        wandATKAnimator = GetComponent<Animator>();
        wandATKAnimator.SetTrigger("Shoot");
        PlayerWeaponParent.Instance.currentBullet--;
        PlayerWeaponParent.Instance.CheckEnergyBarLeft();
        PlayerWeaponParent.Instance.UpdateMagazine();
        Instantiate(bulletPrefap, firePoint.position, firePoint.rotation);
    }
    
}
