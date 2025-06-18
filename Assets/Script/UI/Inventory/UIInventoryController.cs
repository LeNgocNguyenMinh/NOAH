using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening; 
using UnityEngine.UI;

public class UIInventoryController : MonoBehaviour
{
    private UIInventoryPage uiInventoryPage;
    private PlayerLoadout playerLoadout;
    [Header("---------GeneralUI---------")] 
    [SerializeField]private RectTransform ismPanel;
    [Header("---------InventoryUI---------")] 
    [SerializeField]private RectTransform invPanel;
    [SerializeField]private Button invBtn;
    [SerializeField]private Vector2 hiddenPosition;
    [SerializeField]private Vector2 visiblePosition;
    [SerializeField]private float moveDuration = 0.5f; // Thời gian di chuyển
    [SerializeField]private PlayerStatus playerStatus;
    public static bool inventoryOpen = false;
    [Header("---------MissionUI---------")] 
    [SerializeField]private RectTransform missionPanel;
    [SerializeField]private Button missionBtn;
    
    public static bool missionBoardOpen = false;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.J))
        {
            if(missionBoardOpen)
            {
                MissionBoardClose();
            }
            else
            {
                MissionBoardOpen();
            }
        }
        if(Input.GetKeyDown(KeyCode.I))
        {
            
            if(inventoryOpen)
            {    
                InventoryClose();
            }
            else
            {   
                InventoryOpen();
            }
        }
    }  
    private void MissionBoardOpen()
    {
        if(!UIMouseAndPriority.Instance.CanOpenThisUI())return;
        missionPanel.SetAsLastSibling();
        missionBoardOpen = true;
        MoveUp();
    }
    private void MissionBoardClose()
    {
        if(!IsOnTop(missionPanel))
        {
            return;
        }
        missionBoardOpen = false;
        MoveDown();
    }
    private void InventoryOpen()
    {
        if(!UIMouseAndPriority.Instance.CanOpenThisUI())return;
        inventoryOpen = true;
        uiInventoryPage = GetComponent<UIInventoryPage>();
        uiInventoryPage.InventoryUpdateOpen();
        playerLoadout = GetComponent<PlayerLoadout>();
        playerLoadout.CheckClothStatus();
        invPanel.SetAsLastSibling();
        MoveUp();
    }
    private void InventoryClose()
    {
        if(!IsOnTop(invPanel))
        {
            return;
        }
        inventoryOpen = false;
        uiInventoryPage = GetComponent<UIInventoryPage>();
        uiInventoryPage.InventoryUpdateClose();
        MoveDown();
    }
    private void MoveUp()
    {
        ismPanel.DOAnchorPos(visiblePosition, moveDuration).SetEase(Ease.OutQuad).SetUpdate(true).OnComplete(() =>
        {
            Time.timeScale = 0f;
        });
    }
    private void MoveDown()
    {
        ismPanel.DOAnchorPos(hiddenPosition, moveDuration).SetEase(Ease.OutQuad).SetUpdate(true).OnComplete(() =>
        {
            Time.timeScale = 1f;
        });
    }
    private bool IsOnTop(RectTransform panel)
    {
        return panel.transform.GetSiblingIndex() == ismPanel.childCount - 1;
    }
    private void OnEnable()
    {
        missionBtn.onClick.AddListener(OnMissButtonClicked);
        invBtn.onClick.AddListener(OnInvButtonClicked);
    }
    private void OnMissButtonClicked()
    {   
        missionBoardOpen = true;
        inventoryOpen = false;
        missionPanel.SetAsLastSibling();
    }
    private void OnInvButtonClicked()
    {
        inventoryOpen = true;
        missionBoardOpen = false;
        invPanel.SetAsLastSibling();
    }
}
