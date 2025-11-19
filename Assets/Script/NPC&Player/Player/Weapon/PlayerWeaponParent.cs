using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;


public class PlayerWeaponParent : MonoBehaviour
{
    [Header("----General----")]
    public static PlayerWeaponParent Instance;
    public PlayerStatus playerStatus;
    public SpriteRenderer wandSprite;
    public SpriteRenderer physicATKSprite;
    [SerializeField]private Transform wandRotate;   
    [SerializeField]private Transform wandShadowRotate;
    public float delayWandCount = 0f; // Delay count for wand attack, used to prevent multiple attacks in a short time
    [Header("----EnergyBar----")]
    [SerializeField]private int requireHit;
    private int currentHitCount = 0;//Hit count
    private Vector3 mousePos;
    private int magazine = 0;
    private int currentBullet = 0; 
    public bool playerCanATK = true; // Can player shoot or not, set to false when player is reloading or something else
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
    private void Start()
    {
        //Set start values
        magazine = playerStatus.playerBullet;
        currentBullet = magazine;
        UpdateMagazine();
        if(playerStatus.currentWeapon == null)
        {
            EquipNewWeapon(playerStatus.defaultWeapon);
        }
        wandSprite.sprite = playerStatus.currentWeapon.itemSprite;
        bulletPrefap = playerStatus.currentWeapon.weaponBulletType;
    }
    void Update()
    {
        if(UIMouseAndPriority.Instance.OtherPanelIsActive()) return;
        WeaponRotate();
        PlayerWandATK.Instance.CheckWandATK();  
        PlayerMeleeATK.Instance.CheckMeleeATK();
    }
    public void ActiveWandATK()
    {
        wandSprite.enabled = true;
        physicATKSprite.enabled = false;
    }
    public void ActiveMeleeATK()
    {
        wandSprite.enabled = false;
        physicATKSprite.enabled = true;
    }
    public void WeaponRotate()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 rotation = mousePos - transform.position;
        float rotz = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        wandRotate.transform.rotation = Quaternion.Euler(0, 0, rotz);
        wandShadowRotate.transform.rotation = Quaternion.Euler(0, 0, -rotz);
        
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
        PlayerMagazine.Instance.UpdateMagazineText();
    }
    public int GetCurrentBullet()
    {
        return currentBullet;
    }
    public void SetCurrentBullet(int value)
    {
        currentBullet = value;
    }
    public void SubCurrentBullet()
    {
        currentBullet--;
    }
    public void AddCurrentBullet()
    {
        currentBullet++;
    }
    public int GetCurrentHitCount()
    {
        return currentHitCount;
    }
    public void SetCurrentHitCount(int value)
    {
        currentHitCount = value;
    }
    public void AddCurrentHitCount()
    {
        currentHitCount ++;
    }
    public int GetMagazine()
    {
        magazine = playerStatus.playerBullet;
        return magazine;
    }
    public int GetRequireHit()
    {
        return requireHit;
    }
}

