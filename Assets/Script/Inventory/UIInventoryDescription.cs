using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIInventoryDescription : MonoBehaviour
{
    [SerializeField]private PlayerStatus playerStatus;
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
    public void ItemShowInformation(Item item)
    {
        itemInDescription = item;
        if(item.itemSprite != null)
        {
            if(item.itemID.Contains("WP"))//Mean only weapon has upgrade function available
            {
                weaponLevelBox.SetActive(true);
                requireForUpgrade.SetActive(true);
                upgradeButton.SetActive(true);
                weaponLevelText.text = "Level " + item.weaponLevel;
                requireForUpgradeText.text = playerStatus.playerCoin + "/" + item.materialNeedToUpgrade;
                this.itemFunction.text = "Damage + " + item.weaponDamage;
            }
            else if(item.itemID.Contains("HP"))//else then show the item information
            {
                weaponLevelBox.SetActive(false);
                requireForUpgrade.SetActive(false);
                upgradeButton.SetActive(false);
                this.itemFunction.text = "Health recover + " + item.healthRecover;
            }
            else
            {
                weaponLevelBox.SetActive(false);
                requireForUpgrade.SetActive(false);
                upgradeButton.SetActive(false);
            }
            
            itemImageBox.enabled = true;
            itemImageBox.sprite = item.itemSprite;
            itemImageBox.SetNativeSize();
            itemNameBox.text = item.itemName +"";
            itemDescriptionBox.text = item.itemDescription + ""; 
        }
        else{
            ItemHideInformation();
        } 
    }
    public void UpgradeThisWeapon()
    {
        if(itemInDescription.materialNeedToUpgrade <= playerStatus.playerCoin)
        {
            playerStatus.AddCoin(-itemInDescription.materialNeedToUpgrade);//Remove the coin player own
            itemInDescription.SetWeaponLevel(); //Add level weapon by 1 
            this.itemFunction.text = "Damage + " + itemInDescription.weaponDamage;//Update the damage
            requireForUpgradeText.text = itemInDescription.materialNeedToUpgrade + "/" + playerStatus.playerCoin; //Update the requirement for upgrade
            weaponLevelText.text = "Level " + itemInDescription.weaponLevel;// Update the level text
            PopUp.Instance.ShowNotification("Update " + itemInDescription.itemName + " succes to level " + itemInDescription.weaponLevel);
        }
        else{
            PopUp.Instance.ShowNotification("Not enough material!!");
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
