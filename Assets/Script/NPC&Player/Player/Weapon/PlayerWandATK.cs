using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerWandATK : MonoBehaviour
{
    public static PlayerWandATK Instance;
    [SerializeField]private Animator wandATKAnimator;
    [SerializeField]private float delayWandATK = 0.5f;
    [SerializeField]private Transform firePoint;
    [SerializeField]private float wandBulletSpeed;
    [SerializeField]private float canShootRadius;
    private GameObject bulletPrefap;
    private Vector2 direction;
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
        bulletPrefap = PlayerStatus.Instance.currentWeapon.weaponBulletType;
        if(PlayerWeaponParent.Instance.delayWandCount > 0) PlayerWeaponParent.Instance.delayWandCount-=Time.deltaTime;
        if(!PlayerWeaponParent.Instance.playerCanATK)return;   

        if(Input.GetMouseButtonDown(0) && PlayerWeaponParent.Instance.delayWandCount <= 0 )
        {
            WandShoot();
        }
    }
    private void WandShoot()
    {
        PlayerWeaponParent.Instance.delayWandCount = delayWandATK;//Set up delay time between each shoot
        PlayerWeaponParent.Instance.wandSprite.enabled = true;
        PlayerWeaponParent.Instance.physicATKSprite.enabled = false;
        if(PlayerWeaponParent.Instance.GetCurrentBullet()<=0)
        {
            NotifPopUp.Instance.ShowNotification("No Energy!");
            return;
        }
        SoundControl.Instance.PlayerShootSoundPlay();;//play the shoot sound
        wandATKAnimator.SetTrigger("Shoot");
        PlayerWeaponParent.Instance.SubCurrentBullet();
        PlayerMagazine.Instance.CheckEnergyBarLeft();
        PlayerWeaponParent.Instance.UpdateMagazine();
        direction = (Vector2)(firePoint.position - transform.position).normalized;
        Instantiate(bulletPrefap, firePoint.position, Quaternion.identity).GetComponent<WandBullet>().SetValue(wandBulletSpeed, direction);
    }
    
}
