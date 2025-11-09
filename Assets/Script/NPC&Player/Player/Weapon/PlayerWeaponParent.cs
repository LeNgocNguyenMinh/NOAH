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
    [SerializeField]private Image bulletEnergyFront;
    [SerializeField]private Image bulletEnergyBack;
    [SerializeField]private Image hitEnergyFront;
    [SerializeField]private Image hitEnergyBack;
    private float energyMainTime = 0.3f;
    private float energySlowerTime = 1f;
    private float hitMainTime = 0.1f;
    private float hitSlowerTime = 0.3f;
    [SerializeField]private int requireHit;
    [SerializeField]private TextMeshProUGUI text;
    private int hitCount = 0;//Hit count
    private Vector3 mousePos;
    private int magazine;
    public int currentBullet; 
    public bool playerCanATK = true; // Can player shoot or not, set to false when player is reloading or something else
    private GameObject bulletPrefap;
/*     [Header("----TestBullet----")]
    [SerializeField]private GameObject testBullet;
    [SerializeField]private Vector2 groundDispenseVelocity;
    [SerializeField]private Vector2 verticalDispenseVelocity; */
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
        text.text = $"{currentBullet}/{magazine}";
    }
    
    public void CheckEnergyBarLeft()
    {
        float target = currentBullet / (float)magazine;
        if(bulletEnergyFront.fillAmount > bulletEnergyBack.fillAmount)
        {
            bulletEnergyBack.fillAmount = bulletEnergyFront.fillAmount;
        }
        bulletEnergyFront.DOFillAmount(target, energyMainTime).SetEase(Ease.Linear);
        bulletEnergyBack.DOFillAmount(target, energySlowerTime).SetEase(Ease.Linear);
    }
    public void CheckEnergyBarRight()
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
            if(currentBullet < magazine)
            {
                currentBullet++;
                CheckEnergyBarLeft();
                UpdateMagazine();
            }
            hitCount = 0; // Reset hit count after charging energy
            CheckEnergyBarRight();
        }
    }
    public void HitCountIncrease()
    {
        hitCount++;
        CheckEnergyBarRight();
    }
}

