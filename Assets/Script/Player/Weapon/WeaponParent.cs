using System;
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


public class WeaponParent : MonoBehaviour
{
    public static WeaponParent Instance;
    [SerializeField]private PlayerStatus playerStatus;
    [SerializeField]private MagazineText magazineText; 
    [SerializeField]private SpriteRenderer wandSprite;
    [SerializeField]private SpriteRenderer physicATKSprite;
    [SerializeField]private GameObject reloadIcon;
    [SerializeField]private Transform firePoint;
    [SerializeField]private Animator physicATKAnimator; 
    [SerializeField]private Animator wandATKAnimator;
    [SerializeField]private float delayWandATK = 0.5f;
    [SerializeField]private float delayPhysicATK = 1f;
    [SerializeField]private PlayerSound playerSound;
    [SerializeField]private Image bulletEnergyFront;
    [SerializeField]private Image bulletEnergyBack;
    [SerializeField]private Image hitEnergyFront;
    [SerializeField]private Image hitEnergyBack;
    [SerializeField]private float energyMainTime;
    [SerializeField]private float energySlowerTime;
    [SerializeField]private float hitMainTime;
    [SerializeField]private float hitSlowerTime;
    [SerializeField]private int requireHit;
    private int hitCount = 0;//Hit count
    private GameObject bulletPrefap;
    private Vector3 mousePos;
    private int magazine;
    private int currentBullet;
    private float delayWandCount = 0f; // Delay count for wand attack, used to prevent multiple attacks in a short time
    private float delayPhysicCount = 0f; // Delay count for physic attack, used to prevent multiple attacks in a short time
    public static bool playerCanShoot = true; // Can player shoot or not, set to false when player is reloading or something else

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
    void Start()
    { 
        magazine = playerStatus.playerBullet;//Get the max magazine
        currentBullet = magazine;
        UpdateMagazine();
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
        CheckEnergyBarLeft();
        /* Reload(); */
        WeaponRotate();
        CheckPhysicATK();
        CheckWandATK();
        
    }
    private void CheckPhysicATK()
    {
        if(delayPhysicCount > 0) delayPhysicCount-=Time.deltaTime;
        if(!playerCanShoot)return;
        if(Input.GetMouseButtonDown(1) && delayPhysicCount <= 0)
        {
            delayPhysicCount = delayPhysicATK;//Set up delay time between each physic attack
            PhysicAtk();
        }
    }
    private void CheckWandATK()
    {
        if(delayWandCount > 0) delayWandCount-=Time.deltaTime;
        if(!playerCanShoot)return;
        if(Input.GetMouseButtonDown(0) && delayWandCount <= 0)
        {
            delayWandCount = delayWandATK;//Set up delay time between each shoot
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
        magazineText.UpdateAmmorText(currentBullet, magazine);
    }
    private void Shoot()
    {
        wandSprite.enabled = true;
        physicATKSprite.enabled = false;
        if(currentBullet<=0)return;
        wandATKAnimator.SetTrigger("Shoot");
        currentBullet--;
        CheckEnergyBarLeft();
        magazineText.UpdateAmmorText(currentBullet, magazine);
        Instantiate(bulletPrefap, firePoint.position, firePoint.rotation);
    }
    private void PhysicAtk()
    {
        wandSprite.enabled = false;
        physicATKSprite.enabled = true;
        physicATKAnimator.SetTrigger("Attack");
    }
    private void CheckEnergyBarLeft()
    {
        float target = currentBullet / (float)magazine;
        if(bulletEnergyFront.fillAmount > bulletEnergyBack.fillAmount)
        {
            bulletEnergyBack.fillAmount = bulletEnergyFront.fillAmount;
        }
        bulletEnergyFront.DOFillAmount(target, energyMainTime).SetEase(Ease.Linear);
        bulletEnergyBack.DOFillAmount(target, energySlowerTime).SetEase(Ease.Linear);
    }
    private void CheckEnergyBarRight()
    {
        float target = hitCount / (float)requireHit;
        if(hitEnergyFront.fillAmount > hitEnergyBack.fillAmount)
        {
            hitEnergyBack.fillAmount = hitEnergyFront.fillAmount;
        }
        hitEnergyFront.DOFillAmount(target, hitMainTime).SetEase(Ease.Linear);
        hitEnergyBack.DOFillAmount(target, hitSlowerTime).SetEase(Ease.Linear);
        if(hitCount >= requireHit)
        {
            EnergyCharge();
            hitCount = 0; // Reset hit count after charging energy
            CheckEnergyBarRight();
        }
    }
    public void PhysicHitAnim()
    {
        physicATKAnimator.SetTrigger("Hit");
    }
    public void EnergyCharge()
    {
        if(currentBullet < magazine)
        {
            currentBullet++;
            CheckEnergyBarLeft();
            magazineText.UpdateAmmorText(currentBullet, magazine);
        }
    }
    public void HitCountIncrease()
    {
        hitCount++;
        CheckEnergyBarRight();
    }
}

