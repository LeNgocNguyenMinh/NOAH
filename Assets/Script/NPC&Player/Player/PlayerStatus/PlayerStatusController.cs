using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening; 
using UnityEngine.UI;

public class PlayerStatusController : MonoBehaviour
{
    private PlayerStatusUI playerStatusUI;
    private AddAvailablePoint addAvailablePoint;
    [Header("---------StatusUI Move---------")] 
    [SerializeField]private RectTransform panel;
    [SerializeField]private GameObject statusPanel;
    [SerializeField]private Vector2 hiddenPosition;
    [SerializeField]private Vector2 visiblePosition;
    [SerializeField]private float moveDuration = 0.5f; // Thời gian di chuyển
    [SerializeField]private Image statusImage; 
    [SerializeField]private Sprite handOpen;
    [SerializeField]private Sprite handClose;
    [SerializeField]private List<RectMask2D> listMask2D;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            playerStatusUI = GetComponent<PlayerStatusUI>();
            if(PlayerStatusUI.playerStatusUIOpen)
            {
                playerStatusUI.ClosePlayerStatus();
                statusImage.sprite = handClose;
                HideInfo();
                panel.DOAnchorPos(hiddenPosition, moveDuration).SetEase(Ease.OutQuad).SetUpdate(true).OnComplete(() =>
                {
                    Time.timeScale = 1f;
                });
            }
            else{
                if(UIMouseAndPriority.Instance.OtherPanelIsActive()) return;
                playerStatusUI.OpenPlayerStatus();
                addAvailablePoint = GetComponent<AddAvailablePoint>();
                addAvailablePoint.CheckAvailablePoint();                
                panel.DOAnchorPos(visiblePosition, moveDuration).SetEase(Ease.OutQuad).SetUpdate(true).OnComplete(() =>
                {
                    statusImage.sprite = handOpen;
                    ShowInfo();
                    Time.timeScale = 0f;
                });
            }
        }
    }
    private void ShowInfo()
    {
        for(int i = 0; i < listMask2D.Count; i++)
        {
            AnimatePaddingLeft(listMask2D[i], 0);
        }
    }
    private void HideInfo()
    {
        for(int i = 0; i < listMask2D.Count; i++)
        {
            AnimatePaddingLeft(listMask2D[i], 192);
        }
    }
    private void AnimatePaddingLeft(RectMask2D mask, float desValue)
    {
        DOTween.To(() => mask.padding.z, z =>
        {
            var padding = mask.padding;
            padding.z = z;
            mask.padding = padding;
        }, desValue, .5f).SetEase(Ease.Linear).SetUpdate(true);
    }
}
