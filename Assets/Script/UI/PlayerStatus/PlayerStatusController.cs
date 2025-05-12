using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening; 

public class PlayerStatusController : MonoBehaviour
{
    private PlayerStatusUI playerStatusUI;
    private UIMouseAndPriority uiMouseAndPriority;
    private AddAvailablePoint addAvailablePoint;
    [Header("---------StatusUI Move---------")] 
    [SerializeField]private RectTransform panel;
    [SerializeField]private GameObject statusPanel;
    [SerializeField]private Vector2 hiddenPosition;
    [SerializeField]private Vector2 visiblePosition;
    [SerializeField]private float moveDuration = 0.5f; // Thời gian di chuyển

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            playerStatusUI = GetComponent<PlayerStatusUI>();
            uiMouseAndPriority = FindObjectOfType<UIMouseAndPriority>().GetComponent<UIMouseAndPriority>();
            if(PlayerStatusUI.playerStatusUIOpen)
            {
                playerStatusUI.ClosePlayerStatus();
                panel.DOAnchorPos(hiddenPosition, moveDuration).SetEase(Ease.OutQuad).SetUpdate(true).OnComplete(() =>
                {
                    Time.timeScale = 1f;
                });
            }
            else{
                if(!uiMouseAndPriority.CanOpenThisUI()) return;
                playerStatusUI.OpenPlayerStatus();
                addAvailablePoint = GetComponent<AddAvailablePoint>();
                addAvailablePoint.CheckAvailablePoint();
                statusPanel.transform.SetAsLastSibling();
                panel.DOAnchorPos(visiblePosition, moveDuration).SetEase(Ease.OutQuad).SetUpdate(true).OnComplete(() =>
                {
                    Time.timeScale = 0f;
                });
            }
        }
    }
}
