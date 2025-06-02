using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening; 

public class MissionUIController : MonoBehaviour
{
    [SerializeField]private RectTransform panel;
    [SerializeField]private Vector2 hiddenPosition;
    [SerializeField]private Vector2 visiblePosition;
    [SerializeField]private float moveDuration = 0.5f; // Thời gian di chuyển
    private bool isShown = true;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
           if(isShown)
            {
                HideMissionUI();
            }
            else
            {
                ShowMissionUI();
            }
        }
    }
    private void ShowMissionUI()
    {
        panel.DOAnchorPos(visiblePosition, moveDuration).SetEase(Ease.OutQuad).SetUpdate(true).OnComplete(() =>
        {
            isShown = true;
        });
    }
    private void HideMissionUI()
    {
        panel.DOAnchorPos(hiddenPosition, moveDuration).SetEase(Ease.OutQuad).SetUpdate(true).OnComplete(() =>
        {
            isShown = false;
        });
    }
    public void HideButtonInteract()
    {
        if (isShown)
        {
            HideMissionUI();
        }
        else
        {
            ShowMissionUI();
        }
    }
}
