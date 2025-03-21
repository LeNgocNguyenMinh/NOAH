using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening; 
public class ShopInteractive : MonoBehaviour
{
    // Start is called before the first frame update
    private ObjectInteraction objectInteraction;
    private ShopController shopController;
    private UIMouseAndPriority uiMouseAndPriority;
    [SerializeField]private RectTransform panel;
    [SerializeField]private Vector2 hiddenPosition;
    [SerializeField]private Vector2 visiblePosition;
    [SerializeField]private float moveDuration = 0.5f; // Thời gian di chuyển
    private void Update()
    {
        objectInteraction = GetComponent<ObjectInteraction>();
        if(objectInteraction.GetCanInteract())
        {
            uiMouseAndPriority = GameObject.FindObjectOfType<UIMouseAndPriority>().GetComponent<UIMouseAndPriority>();
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
                    if(!uiMouseAndPriority.CanOpenThisUI())return;
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
