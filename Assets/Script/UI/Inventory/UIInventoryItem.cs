using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIInventoryItem : MonoBehaviour, IPointerClickHandler
{
    ////////////////////Item data
    private string itemName;
    public string itemID;
    public int itemQuantity = 0;
    private Sprite itemSprite;
    private string itemDescription;
    public bool isEmpty = true;//Check if slot is empty    
    private Item item;
    private WeaponParent weaponParent;
    private PlayerLoadout playerLoadout;
    [SerializeField]private TMP_Text quantityText;
    [SerializeField]private Image itemImage;
    [SerializeField]private GameObject choicePanel;
    public bool hotBarSlot;
    public GameObject border;
    private bool isSelect;
    private UIInventoryPage uiInventoryPage;
    private UIInventoryDescription uiInventoryDescription;
    private void Start()
    {
        if(!hotBarSlot)
        {
            uiInventoryDescription = FindObjectOfType<UIInventoryDescription>().GetComponent<UIInventoryDescription>();
            playerLoadout = FindObjectOfType<PlayerLoadout>().GetComponent<PlayerLoadout>();
        }
        uiInventoryPage = FindObjectOfType<UIInventoryPage>().GetComponent<UIInventoryPage>();
        weaponParent = FindObjectOfType<WeaponParent>().GetComponent<WeaponParent>();
    }
    private void Awake()
    {
        choicePanel.SetActive(false);
        quantityText.enabled = false;
        itemImage.enabled = false;
        border.SetActive(false);
        isSelect = false;
        isEmpty = true;
    }
    public void AddItem(Item item, int amountOfItem)//Add new Item data to this slot
    {
        this.item = item;
        this.itemName = item.itemName;
        this.itemID = item.itemID;
        this.itemQuantity += amountOfItem;
        this.itemSprite = item.itemSprite;
        this.itemDescription = item.itemDescription;
        this.isEmpty = false;
        
        this.quantityText.enabled = true;
        this.quantityText.text = this.itemQuantity + "";
        
        this.itemImage.enabled = true;
        this.itemImage.sprite = this.itemSprite;
        
        if(itemID.Contains("Cloth"))
        {
            RectTransform rectTransform = this.itemImage.rectTransform;
            rectTransform.sizeDelta = new Vector2(
            this.itemImage.sprite.rect.width * 3f,
            this.itemImage.sprite.rect.height * 3f);
            this.quantityText.enabled = false;
        }
        else if(itemID.Contains("WP"))
        {
            this.itemImage.SetNativeSize();
            this.quantityText.enabled = false;
        }
        else if(itemID.Contains("Gem"))
        {
            RectTransform rectTransform = this.itemImage.rectTransform;
            rectTransform.sizeDelta = new Vector2(
            this.itemImage.sprite.rect.width * 3f,
            this.itemImage.sprite.rect.height * 3f);
            this.quantityText.enabled = false;
        }
        else if(itemID.Contains("Fruit"))
        {
            RectTransform rectTransform = this.itemImage.rectTransform;
            rectTransform.sizeDelta = new Vector2(
            this.itemImage.sprite.rect.width * 3f,
            this.itemImage.sprite.rect.height * 3f);
        }
        else if(itemID.Contains("Stuff"))
        {
            RectTransform rectTransform = this.itemImage.rectTransform;
            rectTransform.sizeDelta = new Vector2(
            this.itemImage.sprite.rect.width * 1f,
            this.itemImage.sprite.rect.height * 1f);
            this.quantityText.enabled = false;
        }
        else{
            RectTransform rectTransform = this.itemImage.rectTransform;
            rectTransform.sizeDelta = new Vector2(
            this.itemImage.sprite.rect.width * 1f,
            this.itemImage.sprite.rect.height * 1f);
        }
    }
    public void DeleteItem()//Delete Item data in this slot
    {
        this.itemName = "";
        this.itemID = "";
        this.itemQuantity = 0;
        this.itemSprite = null;
        this.itemDescription = "";
        this.isEmpty = true;
        this.quantityText.enabled = false;
        this.itemImage.sprite = null;
        this.itemImage.enabled = false;
    }
    public void AddQuantity(int newValue) //Add this slot quantity
    {
        this.itemQuantity += newValue;
        quantityText.text = this.itemQuantity + "";
    }
    public void Equip() // Check the equip is HP item or weapon
    {
        if(isEmpty) return;
        if(itemID.Contains("WP"))//If weapon is equip
        {
            Item usingWeapon = weaponParent.EquipNewWeapon(this.item);//Switch equipped weapon
            if(!hotBarSlot)
            {
                playerLoadout.EquipWeapon(this.item);
                DeleteItem();//Remove this weapon out of slot
                if(usingWeapon!=null )
                {
                    AddItem(usingWeapon, 1);//Add equipped weapon to this slot
                    uiInventoryDescription.ItemShowInformation(this.item);//Show the right information 
                }
                else
                {
                    uiInventoryDescription.ItemHideInformation();
                }
                choicePanel.SetActive(false);//hide the select panel
            }
            else{
                DeleteItem();
                if(usingWeapon!=null )
                {
                    AddItem(usingWeapon, 1);//Add equipped weapon to this slot 
                }
            }
        }
        else if(itemID.Contains("HP"))//If HP item is equip
        {
            if(HealthControl.Instance.HealthRecover(this.item.healthRecover))//this function return: true if health is not full and otherwise
            {
                DeleteOne();//Mean item was used
            }
            else{
                PopUp.Instance.ShowNotification("Health is full.");
            }
        }
        else if(itemID.Contains("Cloth"))
        {
            if(!hotBarSlot)
            {
                playerLoadout.EquipCloth(this.item);
            }
            DeleteItem();
            uiInventoryDescription.ItemHideInformation();
            choicePanel.SetActive(false);//hide the select panel
        }
    }
    public void DeleteOne()//Remove quantity of item
    {
        if(itemID.Contains("WP") || itemID.Contains("Cloth"))
        {
            return;
        }
        this.itemQuantity --;
        quantityText.text = this.itemQuantity + "";
        if(this.itemQuantity == 0)//Mean run out of this item
        {
            DeleteItem();
            if(hotBarSlot)return;
            choicePanel.SetActive(false); //Cause there is no more item to select in this slot
            uiInventoryDescription.ItemHideInformation();
        }
    }
    public void RemoveAll() //Remove all
    {
        if(itemID.Contains("WP")|| itemID.Contains("Cloth"))
        {
            return;
        }
        quantityText.text = "0";
        DeleteItem();
        if(hotBarSlot)return;
        choicePanel.SetActive(false);
        uiInventoryDescription.ItemHideInformation();
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if(hotBarSlot)return;
        if(eventData.button == PointerEventData.InputButton.Left)
        {
            OnLeftClick();
        }
        if(eventData.button == PointerEventData.InputButton.Right)
        {
            OnRightClick();
        }
    }
    public void OnRightClick()//Open or close select action panel 
    {
        if(this.isEmpty) return;
        if(choicePanel.activeSelf)
        {
            border.SetActive(false);
            choicePanel.SetActive(false);
            isSelect = false;
            uiInventoryPage.CloseDescriptionPanel();
        }
        else{
            uiInventoryPage.OnlyClickOneSlot();
            uiInventoryPage.OnlySellectOneSlot();
            uiInventoryDescription.ItemShowInformation(this.item);
            border.SetActive(true);
            choicePanel.SetActive(true);
            isSelect = true;
            uiInventoryPage.OpenDescriptionPanel();
        }
    }
    public void OnLeftClick()//Show this item information 
    {
    
        if(this.isEmpty) return;
        if(isSelect)
        {
            choicePanel.SetActive(false);
            uiInventoryPage.CloseDescriptionPanel();
            border.SetActive(false);
            isSelect = false;
        }
        else{
            uiInventoryPage.OnlyClickOneSlot();
            uiInventoryPage.OnlySellectOneSlot();
            uiInventoryDescription.ItemShowInformation(this.item);
            border.SetActive(true);
            uiInventoryPage.OpenDescriptionPanel();
            isSelect = true;
        }
    }
    ///Get
    public bool GetIsSelect()
    {
        return this.isSelect;
    }
    public string GetItemID()
    {
        return this.itemID;
    }
    public int GetItemQuantity()
    {
        return this.itemQuantity;
    }
    public Item GetItem()
    {
        return this.item;
    }

    ///Set
    public void SetChoicePanel()
    {
        choicePanel.SetActive(false);
    }
    public void SetIsSelect(bool isSelect)
    {
        this.isSelect = isSelect;
    }
    public void SetItem(Item item)
    {
        this.item = item;
    }
}
