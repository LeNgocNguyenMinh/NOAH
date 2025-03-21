using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening; 

public class PlayerStatusController : MonoBehaviour
{
    private PlayerStatusUI playerStatusUI;
    private UIMouseAndPriority uiMouseAndPriority;
    public static bool  playerStatusCanOpen = true;
    private AddAvailablePoint addAvailablePoint;
    [Header("---------StatusUI Move---------")] 
    [SerializeField]private RectTransform panel;
    [SerializeField]private Vector2 hiddenPosition;
    [SerializeField]private Vector2 visiblePosition;
    [SerializeField]private float moveDuration = 0.5f; // Thời gian di chuyển

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            playerStatusUI = FindObjectOfType<PlayerStatusUI>().GetComponent<PlayerStatusUI>();
            uiMouseAndPriority = FindObjectOfType<UIMouseAndPriority>().GetComponent<UIMouseAndPriority>();
            if(PlayerStatusUI.playerStatusUIOpen)
            {
                playerStatusUI.ClosePlayerStatus();
                panel.DOAnchorPos(hiddenPosition, moveDuration).SetEase(Ease.OutQuad);
            }
            else{
                if(!uiMouseAndPriority.CanOpenThisUI()) return;
                panel.DOAnchorPos(visiblePosition, moveDuration).SetEase(Ease.OutQuad);
                playerStatusUI.OpenPlayerStatus();
                addAvailablePoint = FindObjectOfType<AddAvailablePoint>().GetComponent<AddAvailablePoint>();
                addAvailablePoint.CheckAvailablePoint();
            }
        }
    }
}
