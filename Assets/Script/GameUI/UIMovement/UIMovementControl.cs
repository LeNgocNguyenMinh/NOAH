using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening; 

public class UIMovementControl : MonoBehaviour
{
    [SerializeField]private RectTransform panel;
    [SerializeField]private Vector2 hiddenPosition;
    [SerializeField]private Vector2 visiblePosition;
    [SerializeField]private float moveDuration = 0.5f; // Thời gian di chuyển
    public void MoveUp()
    {
        panel.DOAnchorPos(hiddenPosition, moveDuration).SetEase(Ease.OutQuad);
    }
    public void MoveDown()
    {
        panel.DOAnchorPos(visiblePosition, moveDuration).SetEase(Ease.OutQuad);
    }
 
}
