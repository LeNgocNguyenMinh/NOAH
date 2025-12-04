using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;


public class PlayerWeaponParent : MonoBehaviour
{
    [Header("----General----")]
    public static PlayerWeaponParent Instance;
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
    public bool playerCanATK = true; 
    private Item currentWeapon;

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
        magazine = PlayerStatus.Instance.playerBullet;
        currentBullet = magazine;
        UpdateMagazine();
    }
    void Update()
    {
        if(UIMouseAndPriority.Instance.OtherPanelIsActive() || UIMouseAndPriority.Instance.IsInLimitInteractPanel()) return;
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
    public void EquipNewWeapon(Item newWeapon) // Trigger this function when new Weapon equip
    {
        currentWeapon = newWeapon;
        wandSprite.sprite = newWeapon.itemSprite;
    }
    public void UpdateMagazine()//Trigger when player Spend point in magazine
    {
        magazine = PlayerStatus.Instance.playerBullet;
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
        magazine = PlayerStatus.Instance.playerBullet;
        return magazine;
    }
    public int GetRequireHit()
    {
        return requireHit;
    }
    public Item GetCurrentWeapon() // Trigger this function when new Weapon equip
    {
        return currentWeapon;
    }
}

