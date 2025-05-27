using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayerLoadout : MonoBehaviour
{
    private ClothAnimationOverrider animationOverrider;
    [SerializeField]private Image hatSlotImage;
    [SerializeField]private Image coatSlotImage;
    [SerializeField]private Image hatImage;
    [SerializeField]private Image coatImage;
    [SerializeField]private PlayerStatus playerStatus;
    [SerializeField]private Image weaponSlotImage;
    [SerializeField]private GameObject unequipHat;
    [SerializeField]private GameObject unequipCoat;
    [SerializeField]private GameObject unequipWeapon;
    private Item currentEquipHat;
    private Item currentEquipCoat;
    private Item currentEquipWeapon;
    private UIInventoryPage uiInventoryPage;
    private WeaponParent weaponParent;
    private PlayerCurrentClothChange playerCurrentClothChange;
    public void Start()
    {
        unequipHat.SetActive(false);
        unequipCoat.SetActive(false);
        unequipWeapon.SetActive(false);
    }
    public void CheckClothStatus()
    {
        if(playerStatus.currentHat != null)
        {
            EquipCloth(playerStatus.currentHat);
        }
        else{
            hatSlotImage.sprite = playerStatus.defaultHat.itemSprite; //update image in hat slot 
            RectTransform hatRect = hatSlotImage.rectTransform;
            hatRect.sizeDelta = new Vector2(
            this.hatSlotImage.sprite.rect.width * 3f,
            this.hatSlotImage.sprite.rect.height * 3f);
        }
        if(playerStatus.currentCoat != null)
        {
            EquipCloth(playerStatus.currentCoat);
        }
        else
        {
            coatSlotImage.sprite = playerStatus.defaultCoat.itemSprite;
            RectTransform coatRect = coatSlotImage.rectTransform;
            coatRect.sizeDelta = new Vector2(
            this.coatSlotImage.sprite.rect.width * 3f,
            this.coatSlotImage .sprite.rect.height * 3f);
        }
        EquipWeapon(playerStatus.currentWeapon);
    }
    public void EquipCloth(Item newCloth)
    {
        playerCurrentClothChange = FindObjectOfType<PlayerCurrentClothChange>().GetComponent<PlayerCurrentClothChange>();
        playerCurrentClothChange.ChangeCloth(newCloth);
        if(newCloth.itemID.Contains("Hat"))//newCloth is hat
        {
            hatSlotImage.sprite = newCloth.itemSprite; //enable image in hat slot
            currentEquipHat = newCloth; //update current hat variable
            hatImage.sprite = newCloth.itemSprite; //update image in head in inventory
            hatSlotImage.sprite = newCloth.itemSprite; //update image in hat slot 
            RectTransform hatRect = hatSlotImage.rectTransform;
            hatRect.sizeDelta = new Vector2(
            this.hatSlotImage.sprite.rect.width * 3f,
            this.hatSlotImage.sprite.rect.height * 3f);
            playerStatus.SetCurrentHat(newCloth);//update current hat in player status
            unequipHat.SetActive(true);//enable for unequip hat
        }
        else{
            coatSlotImage.sprite = newCloth.itemSprite;
            currentEquipCoat = newCloth;
            coatImage.sprite = newCloth.itemSprite;
            coatSlotImage.sprite = newCloth.itemSprite;
            RectTransform coatRect = coatSlotImage.rectTransform;
            coatRect.sizeDelta = new Vector2(
            this.coatSlotImage.sprite.rect.width * 3f,
            this.coatSlotImage .sprite.rect.height * 3f);
            playerStatus.SetCurrentCoat(newCloth);
            unequipCoat.SetActive(true);
        }
    }
    public void UnequipHat()
    {
        uiInventoryPage = FindObjectOfType<UIInventoryPage>().GetComponent<UIInventoryPage>();
        if(!uiInventoryPage.AddItem(currentEquipHat, 1))
        {
            return;
        }       
        playerCurrentClothChange = FindObjectOfType<PlayerCurrentClothChange>().GetComponent<PlayerCurrentClothChange>();
        playerCurrentClothChange.ChangeCloth(playerStatus.defaultHat);
        hatSlotImage.sprite = playerStatus.defaultHat.itemSprite;
        hatImage.sprite = playerStatus.defaultHat.itemSprite;
        playerStatus.SetCurrentHat(null);
        currentEquipHat = null;
        unequipHat.SetActive(false);
    }
    public void UnequipCoat()
    {
        uiInventoryPage = FindObjectOfType<UIInventoryPage>().GetComponent<UIInventoryPage>();
        if(!uiInventoryPage.AddItem(currentEquipCoat, 1))
        {
            return;
        } 
        playerCurrentClothChange = FindObjectOfType<PlayerCurrentClothChange>().GetComponent<PlayerCurrentClothChange>();
        playerCurrentClothChange.ChangeCloth(playerStatus.defaultCoat);;
        coatSlotImage.sprite = playerStatus.defaultCoat.itemSprite;
        coatImage.sprite = playerStatus.defaultCoat.itemSprite;
        playerStatus.SetCurrentCoat(null);
        currentEquipCoat = null;
        unequipCoat.SetActive(false);
    }
    public void EquipWeapon(Item newWeapon)
    {
        if(newWeapon != playerStatus.defaultWeapon)
        {
            unequipWeapon.SetActive(true);
        }
        currentEquipWeapon = newWeapon;
        weaponSlotImage.sprite = newWeapon.itemSprite; 
        RectTransform rectTransform = weaponSlotImage.rectTransform;
            rectTransform.sizeDelta = new Vector2(
            this.weaponSlotImage.sprite.rect.width * 3f,
            this.weaponSlotImage.sprite.rect.height * 3f);
    }
    public void UnequipWeapon()
    {
        uiInventoryPage = FindObjectOfType<UIInventoryPage>().GetComponent<UIInventoryPage>();
        if(!uiInventoryPage.AddItem(currentEquipWeapon, 1))
        {
            return;
        }
        EquipWeapon(playerStatus.defaultWeapon);
        weaponParent = FindObjectOfType<WeaponParent>().GetComponent<WeaponParent>();
        weaponParent.EquipNewWeapon(playerStatus.defaultWeapon);
        playerStatus.SetCurrentWeapon(playerStatus.defaultWeapon);
        unequipWeapon.SetActive(false);
    }
}
