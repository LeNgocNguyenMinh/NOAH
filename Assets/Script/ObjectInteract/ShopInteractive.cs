using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening; 
public class ShopInteractive : MonoBehaviour
{
    // Start is called before the first frame update
    private ObjectInteraction objectInteraction;
    private ShopController shopController;
    [SerializeField]private RectTransform panel;
    [SerializeField]private Vector2 hiddenPosition;
    [SerializeField]private Vector2 visiblePosition;
    [SerializeField]private float moveDuration = 0.5f; // Thời gian di chuyển
    [SerializeField]private PlayerStatus playerStatus;
    public bool canOpenShop = false;
    private void Update()
    {
        objectInteraction = GetComponent<ObjectInteraction>();
        if(objectInteraction.GetCanInteract())
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                if(ShopController.shopPanelIsOpen)
                {
                    ShopController.shopPanelIsOpen = false;
                    panel.DOAnchorPos(hiddenPosition, moveDuration).SetEase(Ease.OutQuad).SetUpdate(true).OnComplete(() =>
                    {
                        Time.timeScale = 1f;
                    });
                }
                else 
                {
                    if(!UIMouseAndPriority.Instance.CanOpenThisUI())return;
                    Debug.Log("Can Open shop: " + canOpenShop);
                    if(!canOpenShop)
                    {
                        NotifPopUp.Instance.ShowNotification("Shop open at 10 A.M and close at 10 P.M!!");
                        return;
                    }
                    shopController = FindObjectOfType<ShopController>().GetComponent<ShopController>();
                    shopController.UpdateWhenOpen();
                    ShopController.shopPanelIsOpen = true;
                    panel.DOAnchorPos(visiblePosition, moveDuration).SetEase(Ease.OutQuad).SetUpdate(true).OnComplete(() =>
                    {
                        Time.timeScale = 0f;
                    });
                }
            }       
        }
    }
}
