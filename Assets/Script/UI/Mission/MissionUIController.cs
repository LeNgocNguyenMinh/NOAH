using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening; 

public class MissionUIController : MonoBehaviour
{
    [SerializeField]private RectTransform smallPanel;
    [SerializeField]private Vector2 hiddenPositionSmall;
    [SerializeField]private Vector2 visiblePositionSmall;
    [SerializeField]private float moveDuration = 0.5f; // Thời gian di chuyển
    private bool isMissSmallShown = true;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
           if(isMissSmallShown)
            {
                HideSmallMissionUI();
            }
            else
            {
                ShowSmallMissionUI();
            }
        }
    }
    private void ShowSmallMissionUI()
    {
        smallPanel.DOAnchorPos(visiblePositionSmall, moveDuration).SetEase(Ease.OutQuad).SetUpdate(true).OnComplete(() =>
        {
            isMissSmallShown = true;
        });
    }
    private void HideSmallMissionUI()
    {
        smallPanel.DOAnchorPos(hiddenPositionSmall, moveDuration).SetEase(Ease.OutQuad).SetUpdate(true).OnComplete(() =>
        {
            isMissSmallShown = false;
        });
    }
    public void HideButtonInteract()
    {
        if (isMissSmallShown)
        {
            HideSmallMissionUI();
        }
        else
        {
            ShowSmallMissionUI();
        }
    }
}
