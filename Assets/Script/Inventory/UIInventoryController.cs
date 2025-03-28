using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening; 

public class UIInventoryController : MonoBehaviour
{
    private UIInventoryPage uiInventoryPage;
    private UIMouseAndPriority uiMouseAndPriority;
    [SerializeField]private PlayerLoadout playerLoadout;
    [Header("---------InventoryUI Move---------")] 
    [SerializeField]private RectTransform panel;
    [SerializeField]private Vector2 hiddenPosition;
    [SerializeField]private Vector2 visiblePosition;
    [SerializeField]private float moveDuration = 0.5f; // Thời gian di chuyển
    [SerializeField]private PlayerStatus playerStatus;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.I))
        {
            uiInventoryPage = FindObjectOfType<UIInventoryPage>().GetComponent<UIInventoryPage>();
            uiMouseAndPriority = GameObject.FindObjectOfType<UIMouseAndPriority>().GetComponent<UIMouseAndPriority>();
            if(UIInventoryPage.inventoryOpen)
            {
                uiInventoryPage.InventoryClose();
                panel.DOAnchorPos(hiddenPosition, moveDuration).SetEase(Ease.OutQuad).SetUpdate(true).OnComplete(() =>
                {
                    Time.timeScale = 1f;
                });
            }
            else
            {
                if(!uiMouseAndPriority.CanOpenThisUI())return;
                uiInventoryPage.InventoryOpen();
                panel.DOAnchorPos(visiblePosition, moveDuration).SetEase(Ease.OutQuad).SetUpdate(true).OnComplete(() =>
                {
                    Time.timeScale = 0f;
                });
                playerLoadout.CheckClothStatus();
            }
        }
    }  
}
