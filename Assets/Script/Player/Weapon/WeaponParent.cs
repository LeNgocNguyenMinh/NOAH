using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WeaponParent : MonoBehaviour
{
    [SerializeField]private PlayerStatus playerStatus;
    [SerializeField]private MagazineText magazineText;
    private Vector3 mousePos;
    [SerializeField]private SpriteRenderer wandSprite;
    [SerializeField]private SpriteRenderer physicATKSprite;
    [SerializeField]private GameObject reloadIcon;
    [SerializeField]private Transform firePoint;
    [SerializeField]private Animator physicATKAnimator; 
    [SerializeField]private Animator wandATKAnimator;
    private GameObject bulletPrefap;
    [SerializeField]private float delayShootTime = 0.5f;
    [SerializeField]private PlayerSound playerSound;
    public int magazine;
    public int currentBullet;
    public static bool playerCanShoot = true; // Can player shoot or not, set to false when player is reloading or something else

    void Start()
    { 
        magazine = playerStatus.playerBullet;//Get the max magazine
        currentBullet = magazine;
        if(playerStatus.currentWeapon == null)
        {
            EquipNewWeapon(playerStatus.defaultWeapon);
        }
        //Start with the current weapon save in player Data
        wandSprite.sprite = playerStatus.currentWeapon.itemSprite;
        bulletPrefap = playerStatus.currentWeapon.weaponBulletType;
    }
    void Update()
    {
        if(!UIMouseAndPriority.Instance.CanOpenThisUI()) return;
        Reload();
        WeaponRotate();

        if(Input.GetMouseButtonDown(1))//Right click to use physic attack
        {
            PhysicAtk();
        }
        if(delayShootTime > 0) delayShootTime-=Time.deltaTime;
        if(!playerCanShoot)return;
        if(Input.GetMouseButtonDown(0) && delayShootTime <= 0)
        {
            delayShootTime = 0.5f;//Set up delay time between each shoot
            playerSound.PlayShootSound();//play the shoot sound
            Shoot();
        }
    }
    private void WeaponRotate()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 rotation = mousePos - transform.position;
        float rotz = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotz);
        
        if(Mathf.Abs(rotz) > 90)
        {
            wandSprite.flipX = true;
            physicATKSprite.flipY = true;
        }else if(Mathf.Abs(rotz) < 90)
        {
            wandSprite.flipX = false;
            physicATKSprite.flipY = false;
        }
    }
    public Item EquipNewWeapon(Item newWeapon) // Trigger this function when new Weapon equip
    {
        Item usingWeapon = playerStatus.currentWeapon;
        playerStatus.SetCurrentWeapon(newWeapon);//Set player current weapon 
        wandSprite.sprite = playerStatus.currentWeapon.itemSprite;//Change weapon sprite
        bulletPrefap = playerStatus.currentWeapon.weaponBulletType;//Change bullet type
        if(usingWeapon == playerStatus.defaultWeapon)
        {
            return null;
        }
        return usingWeapon;
    }
    public void UpdateMagazine()//Trigger when player Spend point in magazine
    {
        magazine = playerStatus.playerBullet;
        magazineText.UpdateAmmorText();
    }
    private void Reload()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(BulletReload());
        }
    }
    private IEnumerator BulletReload()
    {
        reloadIcon.SetActive(true);
        yield return new WaitForSeconds(0.4f);
        reloadIcon.SetActive(false);
        currentBullet = magazine;
        magazineText.UpdateAmmorText();
    }
    private void Shoot()
    {
        wandSprite.enabled = true;
        physicATKSprite.enabled = false;
        if(currentBullet<=0)return;
        wandATKAnimator.SetTrigger("Shoot");
        currentBullet--;
        magazineText.UpdateAmmorText();
        Instantiate(bulletPrefap, firePoint.position, firePoint.rotation);
    }
    private void PhysicAtk()
    {
        wandSprite.enabled = false;
        physicATKSprite.enabled = true;
        physicATKAnimator.SetTrigger("Attack");
    }
}

