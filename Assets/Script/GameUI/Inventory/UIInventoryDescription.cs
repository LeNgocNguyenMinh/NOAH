using UnityEngine;
using TMPro;
using UnityEngine.UI;

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
    [SerializeField]private GameObject upgradeButton;
    private Item itemInDescription;
    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
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
                itemFunction.gameObject.SetActive(true);
                weaponLevelBox.SetActive(true);
                requireForUpgrade.SetActive(true);
                upgradeButton.SetActive(true);
                weaponLevelText.text = "Level " + item.weaponLevel;
                requireForUpgradeText.text = PlayerStatus.Instance.playerCoin + "/" + item.materialNeedToUpgrade;
                itemFunction.text = "Damage + " + item.weaponDamage;
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
                upgradeButton.SetActive(false);
                itemFunction.text = "Health recover + " + item.healthRecover;
            }
            else
            {
                weaponLevelBox.SetActive(false);
                requireForUpgrade.SetActive(false);
                upgradeButton.SetActive(false);
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
        if(itemInDescription.materialNeedToUpgrade <= PlayerStatus.Instance.playerCoin)
        {
            PlayerStatus.Instance.AddCoin(-itemInDescription.materialNeedToUpgrade);//Remove the coin player own
            itemInDescription.SetWeaponLevel(); //Add level weapon by 1 
            itemFunction.text = "Damage + " + itemInDescription.weaponDamage;//Update the damage
            requireForUpgradeText.text = PlayerStatus.Instance.playerCoin + "/" + itemInDescription.materialNeedToUpgrade; //Update the requirement for upgrade
            weaponLevelText.text = "Level " + itemInDescription.weaponLevel;// Update the level text
            NotifPopUp.Instance.ShowNotification("Update " + itemInDescription.itemName + " succes to level " + itemInDescription.weaponLevel);
        }
        else{
            NotifPopUp.Instance.ShowNotification("Not enough material!!");
        }
    }
    public void ItemHideInformation()// Hide the information of the item slot
    {
        weaponLevelBox.SetActive(false);
        requireForUpgrade.SetActive(false);
        upgradeButton.SetActive(false);
        itemFunction.text = null;
        itemImageBox.enabled = false;
        itemNameBox.text = null;
        itemDescriptionBox.text = null;
    }
}
