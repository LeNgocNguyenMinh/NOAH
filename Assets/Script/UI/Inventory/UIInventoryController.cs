using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening; 

public class UIInventoryController : MonoBehaviour
{
    private UIInventoryPage uiInventoryPage;
    private PlayerLoadout playerLoadout;
    [Header("---------InventoryUI Move---------")] 
    [SerializeField]private RectTransform panel;
    [SerializeField]private Vector2 hiddenPosition;
    [SerializeField]private Vector2 visiblePosition;
    [SerializeField]private float moveDuration = 0.5f; // Thời gian di chuyển
    [SerializeField]private PlayerStatus playerStatus;
    [SerializeField]private GameObject inventoryPanel;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.I))
        {
            uiInventoryPage = GetComponent<UIInventoryPage>();
            if(UIInventoryPage.inventoryOpen)
            {
                uiInventoryPage.InventoryUpdateClose();
                InventoryClose();
            }
            else
            {
                if(!UIMouseAndPriority.Instance.CanOpenThisUI())return;
                uiInventoryPage.InventoryUpdateOpen();
                playerLoadout = GetComponent<PlayerLoadout>();
                playerLoadout.CheckClothStatus();
                InventoryOpen();
            }
        }
    }  
    private void InventoryOpen()
    {
        panel.DOAnchorPos(visiblePosition, moveDuration).SetEase(Ease.OutQuad).SetUpdate(true).OnComplete(() =>
        {
            Time.timeScale = 0f;
        });
    }
    private void InventoryClose()
    {
        panel.DOAnchorPos(hiddenPosition, moveDuration).SetEase(Ease.OutQuad).SetUpdate(true).OnComplete(() =>
        {
            Time.timeScale = 1f;
        });
    }
}
