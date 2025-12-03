using UnityEngine;
using UnityEngine.UI;

public class PlayerLoadout : MonoBehaviour
{
    public static PlayerLoadout Instance;
    /* [SerializeField]private Image hatSlotImage;
    [SerializeField]private Image coatSlotImage;
    [SerializeField]private Image hatImage;
    [SerializeField]private Image coatImage; */
    [SerializeField]private Image weaponSlotImage;
    /* [SerializeField]private GameObject unequipHat;
    [SerializeField]private GameObject unequipCoat; */
    [SerializeField]private GameObject unequipWeapon;
    /* private Item currentEquipHat;
    private Item currentEquipCoat; */
    private Item currentEquipWeapon;
    private PlayerCurrentClothChange playerCurrentClothChange;
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
    public void CheckClothStatus()
    {
        /* if(PlayerStatus.Instance.currentHat != null)
        {
            EquipCloth(PlayerStatus.Instance.currentHat);
        }
        else{
            hatSlotImage.sprite = PlayerStatus.Instance.defaultHat.itemSprite; //update image in hat slot 
            RectTransform hatRect = hatSlotImage.rectTransform;
            hatRect.sizeDelta = new Vector2(
            hatSlotImage.sprite.rect.width * 3f,
            hatSlotImage.sprite.rect.height * 3f);
        }
        if(PlayerStatus.Instance.currentCoat != null)
        {
            EquipCloth(PlayerStatus.Instance.currentCoat);
        }
        else
        {
            coatSlotImage.sprite = PlayerStatus.Instance.defaultCoat.itemSprite;
            RectTransform coatRect = coatSlotImage.rectTransform;
            coatRect.sizeDelta = new Vector2(
            coatSlotImage.sprite.rect.width * 3f,
            coatSlotImage.sprite.rect.height * 3f);
        } */
        EquipWeapon(PlayerStatus.Instance.currentWeapon);
    }
    /* public void EquipCloth(Item newCloth)
    {
        playerCurrentClothChange = FindObjectOfType<PlayerCurrentClothChange>().GetComponent<PlayerCurrentClothChange>();
        playerCurrentClothChange.ChangeCloth(newCloth);
        if(newCloth.itemID.Contains("Hat"))//newCloth is hat
        {
            if(currentEquipHat != null  && currentEquipHat != PlayerStatus.Instance.defaultHat)
            {
                UIInventoryPage.Instance.AddItem(currentEquipHat, 1); //add current hat to inventory
            }
            hatSlotImage.sprite = newCloth.itemSprite; //enable image in hat slot
            currentEquipHat = newCloth; //update current hat variable
            hatImage.sprite = newCloth.itemSprite; //update image in head in inventory
            hatSlotImage.sprite = newCloth.itemSprite; //update image in hat slot 
            RectTransform hatRect = hatSlotImage.rectTransform;
            hatRect.sizeDelta = new Vector2(
            hatSlotImage.sprite.rect.width * 3f,
            hatSlotImage.sprite.rect.height * 3f);
            PlayerStatus.Instance.SetCurrentHat(newCloth);//update current hat in player status
            unequipHat.SetActive(true);//enable for unequip hat
        }
        else{
            if(currentEquipCoat != null && currentEquipCoat != PlayerStatus.Instance.defaultCoat)
            {
                UIInventoryPage.Instance.AddItem(currentEquipCoat, 1);
            }
            coatSlotImage.sprite = newCloth.itemSprite;
            currentEquipCoat = newCloth;
            coatImage.sprite = newCloth.itemSprite;
            coatSlotImage.sprite = newCloth.itemSprite;
            RectTransform coatRect = coatSlotImage.rectTransform;
            coatRect.sizeDelta = new Vector2(
            coatSlotImage.sprite.rect.width * 3f,
            coatSlotImage .sprite.rect.height * 3f);
            PlayerStatus.Instance.SetCurrentCoat(newCloth);
            unequipCoat.SetActive(true);
        }
    }
    public void UnequipHat()
    {
        if(!UIInventoryPage.Instance.AddItem(currentEquipHat, 1))
        {
            return;
        }       
        playerCurrentClothChange = FindObjectOfType<PlayerCurrentClothChange>().GetComponent<PlayerCurrentClothChange>();
        playerCurrentClothChange.ChangeCloth(PlayerStatus.Instance.defaultHat);
        hatSlotImage.sprite = PlayerStatus.Instance.defaultHat.itemSprite;
        hatImage.sprite = PlayerStatus.Instance.defaultHat.itemSprite;
        PlayerStatus.Instance.SetCurrentHat(PlayerStatus.Instance.defaultHat);
        currentEquipHat = PlayerStatus.Instance.defaultHat;
        unequipHat.SetActive(false);
    }
    public void UnequipCoat()
    {
        if(!UIInventoryPage.Instance.AddItem(currentEquipCoat, 1))
        {
            return;
        } 
        playerCurrentClothChange = FindObjectOfType<PlayerCurrentClothChange>().GetComponent<PlayerCurrentClothChange>();
        playerCurrentClothChange.ChangeCloth(PlayerStatus.Instance.defaultCoat);;
        coatSlotImage.sprite = PlayerStatus.Instance.defaultCoat.itemSprite;
        coatImage.sprite = PlayerStatus.Instance.defaultCoat.itemSprite;
        PlayerStatus.Instance.SetCurrentCoat(PlayerStatus.Instance.defaultCoat);
        currentEquipCoat = PlayerStatus.Instance.defaultCoat;
        unequipCoat.SetActive(false);
    } */
    public void EquipWeapon(Item newWeapon)
    {
        if(newWeapon != PlayerStatus.Instance.defaultWeapon)
        {
            unequipWeapon.SetActive(true);
        }
        currentEquipWeapon = newWeapon;
        weaponSlotImage.sprite = newWeapon.itemSprite; 
        RectTransform rectTransform = weaponSlotImage.rectTransform;
            rectTransform.sizeDelta = new Vector2(
            weaponSlotImage.sprite.rect.width * 3f,
            weaponSlotImage.sprite.rect.height * 3f);
    }
    public void UnequipWeapon()
    {
        if(!UIInventoryPage.Instance.AddItem(currentEquipWeapon, 1))
        {
            return;
        }
        EquipWeapon(PlayerStatus.Instance.defaultWeapon);
        PlayerWeaponParent.Instance.EquipNewWeapon(PlayerStatus.Instance.defaultWeapon);
        PlayerStatus.Instance.SetCurrentWeapon(PlayerStatus.Instance.defaultWeapon);
        unequipWeapon.SetActive(false);
    }
    public void CheckUnequipButton()
    {
        /* if(currentEquipCoat == null || currentEquipCoat == PlayerStatus.Instance.defaultCoat)
        {
            unequipCoat.SetActive(false);
        }
        else
        {
            unequipCoat.SetActive(true);
        }
        if(currentEquipHat == null || currentEquipHat == PlayerStatus.Instance.defaultHat)
        {
            unequipHat.SetActive(false);
        }
        else
        {
            unequipHat.SetActive(true);
        } */
        if(currentEquipWeapon == null || currentEquipWeapon == PlayerStatus.Instance.defaultWeapon)
        {
            unequipWeapon.SetActive(false);
        }
        else
        {
            unequipWeapon.SetActive(true);
        }
    }
}
