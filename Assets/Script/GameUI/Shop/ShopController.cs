using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;

public class ShopController : MonoBehaviour
{   
    public static ShopController Instance;
    public bool shopPanelIsOpen = false;
    [SerializeField]private List<ShopItemSlot> listOfItemSlot = new List<ShopItemSlot>();
    [SerializeField]private List<Item> listItemForShop = new List<Item>();
    [SerializeField]private TMP_Text playerCoin;
    [SerializeField]private PlayerStatus playerStatus;
    [Header("---------Panel Animation---------")]
    [SerializeField]private RectTransform panel;
    [SerializeField]private Vector2 hiddenPosition;
    [SerializeField]private Vector2 visiblePosition;
    [SerializeField]private float moveDuration = 0.5f; // Thời gian di chuyển
    public bool canOpenShop = false;
    private void Awake() 
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void ShopUIInteract()
    {
        if(shopPanelIsOpen)
        {
            ShopUIClose();
        }
        else 
        {
            ShopUIOpen();
        }
    }
    public void ShopUIOpen()
    {
        if(UIMouseAndPriority.Instance.OtherPanelIsActive())return;
        if(!canOpenShop)
        {
            NotifPopUp.Instance.ShowNotification("Shop open at 10 A.M and close at 10 P.M!!");
            return;
        }
        UpdateWhenOpen();
        shopPanelIsOpen = true;
        panel.DOAnchorPos(visiblePosition, moveDuration).SetEase(Ease.OutQuad).SetUpdate(true).OnComplete(() =>
        {
            Time.timeScale = 0f;
        });
    }
    public void ShopUIClose()
    {
        shopPanelIsOpen = false;
        panel.DOAnchorPos(hiddenPosition, moveDuration).SetEase(Ease.OutQuad).SetUpdate(true).OnComplete(() =>
        {
            Time.timeScale = 1f;
        });
    }
    public void UpdateWhenOpen()
    {
        playerCoin.text = playerStatus.playerCoin.ToString();
    }
    public void CoinTextUpdateAfterBuy(int newValue)
    {
        playerStatus.AddCoin(-newValue);
        playerCoin.text = playerStatus.playerCoin.ToString();
    }
    public void AddItemToShop()
    {
        int randomStartIndex = Random.Range(0, listItemForShop.Count - 4);
        for(int i = 0; i < listOfItemSlot.Count; i++)
        {
            listOfItemSlot[i].SetItem(listItemForShop[randomStartIndex]);
            listOfItemSlot[i].SetNumberOfItem(5);
            randomStartIndex ++;
        }
    }
    public List<ShopSaveData> GetListItemInShop()
    {
        List<ShopSaveData> shopData = new List<ShopSaveData>();
        for(int i = 0; i < listOfItemSlot.Count; i++)
        {
            shopData.Add(new ShopSaveData{itemID = listOfItemSlot[i].GetItemID(), itemLeftNumber = listOfItemSlot[i].GetLeftNumber()});
        }
        return shopData;
    }
    public void SetListItemInShop(List<ShopSaveData> shopData)
    {
        for(int i = 0; i < shopData.Count; i++)
        {
            listOfItemSlot[i].SetItem(ItemDictionary.Instance.GetItemInfo(shopData[i].itemID));
            listOfItemSlot[i].SetNumberOfItem(shopData[i].itemLeftNumber);
        }
    }
}
[System.Serializable]
public class ShopSaveData 
{
    public string itemID;
    public int itemLeftNumber;
}
