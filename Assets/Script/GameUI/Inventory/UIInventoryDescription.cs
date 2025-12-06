using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;

public class UIInventoryDescription : MonoBehaviour
{
    public static UIInventoryDescription Instance;
    [SerializeField]private TMP_Text itemNameBox;
    [SerializeField]private TMP_Text itemDescriptionBox;
    [SerializeField]private TMP_Text itemFunction;
    [SerializeField]private Image itemImageBox;
    [SerializeField]private GameObject requireForUpgrade;
    [SerializeField]private TMP_Text requireForUpgradeText;
    [SerializeField]private GameObject weaponLevelBox;
    [SerializeField]private TMP_Text weaponLevelText;
    [SerializeField]private Button wpUpgradeBtn;
    private Item itemInDescription;
    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }
    private void OnEnable()
    {
        wpUpgradeBtn.onClick.AddListener(UpgradeThisWeapon);
    }
    public void ItemShowInformation(Item item)
    {
        itemInDescription = item;
        if(item.itemSprite != null)
        {
            itemImageBox.enabled = true;
            itemImageBox.sprite = item.itemSprite;
            itemImageBox.SetNativeSize();
            itemNameBox.text = item.itemName +"";
            itemDescriptionBox.text = item.itemDescription + ""; 
            if(item.itemID.Contains("WP"))//Mean only weapon has upgrade function available
            {
                WeaponData wpInfo = WeaponManager.Instance.GetWeaponInfo(item.itemID);
                itemFunction.gameObject.SetActive(true);
                weaponLevelBox.SetActive(true);
                requireForUpgrade.SetActive(true);
                wpUpgradeBtn.gameObject.SetActive(true);
                weaponLevelText.text = "Level " + wpInfo.weaponLevel;
                requireForUpgradeText.text = PlayerStatus.Instance.playerCoin + "/" + wpInfo.materialNeedToUpgrade;
                itemFunction.text = "Damage + " + wpInfo.weaponDamage;
                RectTransform rectTransform = itemImageBox.rectTransform;
                rectTransform.sizeDelta = new Vector2(
                itemImageBox.sprite.rect.width * 4f,
                itemImageBox.sprite.rect.height * 4f);
            }
            else if(item.itemID.Contains("HP"))//else then show the item information
            {
                itemFunction.gameObject.SetActive(true);
                weaponLevelBox.SetActive(false);
                requireForUpgrade.SetActive(false);
                wpUpgradeBtn.gameObject.SetActive(false);
                itemFunction.text = "Health recover + " + item.healthRecover;
            }
            else
            {
                weaponLevelBox.SetActive(false);
                requireForUpgrade.SetActive(false);
                wpUpgradeBtn.gameObject.SetActive(false);
                itemFunction.text = null;
                itemFunction.gameObject.SetActive(false);
            }
        }
        else{
            ItemHideInformation();
        } 
    }
    public void UpgradeThisWeapon()
    {
        WeaponData wpData = WeaponManager.Instance.GetWeaponInfo(itemInDescription.itemID);
        if(wpData.materialNeedToUpgrade <= PlayerStatus.Instance.playerCoin)
        {
            PlayerStatus.Instance.AddCoin(-wpData.materialNeedToUpgrade);//Remove the coin player own
            WeaponManager.Instance.WeaponUpgrade(wpData.weaponID); //Add level weapon by 1 
            itemFunction.text = "Damage + " + wpData.weaponDamage;//Update the damage
            requireForUpgradeText.text = PlayerStatus.Instance.playerCoin + "/" + wpData.materialNeedToUpgrade; //Update the requirement for upgrade
            weaponLevelText.text = "Level " + wpData.weaponLevel;// Update the level text
            NotifPopUp.Instance.ShowNotification("Update " + itemInDescription.itemName + " succes to level " + wpData.weaponLevel);
        }
        else{
            NotifPopUp.Instance.ShowNotification("Not enough material!!");
        }
    }
    public void ItemHideInformation()// Hide the information of the item slot
    {
        weaponLevelBox.SetActive(false);
        requireForUpgrade.SetActive(false);
        wpUpgradeBtn.gameObject.SetActive(false);
        itemFunction.text = null;
        itemImageBox.enabled = false;
        itemNameBox.text = null;
        itemDescriptionBox.text = null;
    }
}
