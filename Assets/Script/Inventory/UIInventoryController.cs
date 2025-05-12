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
    [SerializeField]private GameObject inventoryPanel;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.I))
        {
            uiInventoryPage = GetComponent<UIInventoryPage>();
            uiMouseAndPriority = FindObjectOfType<UIMouseAndPriority>().GetComponent<UIMouseAndPriority>();
            Debug.Log(UIInventoryPage.inventoryOpen);
            if(UIInventoryPage.inventoryOpen)
            {
                Debug.Log("Close inv");
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
                
                inventoryPanel.transform.SetAsLastSibling();
                panel.DOAnchorPos(visiblePosition, moveDuration).SetEase(Ease.OutQuad).SetUpdate(true).OnComplete(() =>
                {
                    Time.timeScale = 0f;
                });
                Debug.Log("Open inv");
                playerLoadout.CheckClothStatus();
            }
        }
    }  
}
