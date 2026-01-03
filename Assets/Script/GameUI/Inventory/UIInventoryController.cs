using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening; 
using UnityEngine.UI;

public class UIInventoryController : MonoBehaviour
{
    public static UIInventoryController Instance;
    [SerializeField]private Animator animator;
    [SerializeField]private Button closeBtn;
    [Header("---------GeneralUI---------")] 
    [SerializeField]private RectTransform ismPanel;
    [Header("---------InventoryUI---------")] 
    [SerializeField]private RectTransform invPanel;
    [SerializeField]private Button invBtn;
    [SerializeField]private Vector2 hiddenPosition;
    [SerializeField]private Vector2 visiblePosition;
    [SerializeField]private float moveDuration = 0.5f; // Thời gian di chuyển
    [SerializeField]private CanvasGroup invCanvasGroup;
    [Header("---------MissionUI---------")] 
    [SerializeField]private RectTransform missionPanel;
    [SerializeField]private Button missionBtn;
    [SerializeField]private CanvasGroup missionCanvasGroup;
    [Header("---------StatusUI---------")]
    [SerializeField]private RectTransform statusPanel;
    [SerializeField]private Button statusBtn;
    [SerializeField]private CanvasGroup statusCanvasGroup;
    public static bool inventoryOpen = false;
    public static bool missionBoardOpen = false;
    public static bool statusOpen = false;
    
    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    } 
    public void MissionBoardInteract()
    {
        if(missionBoardOpen)
        {
            CloseMissionBoard();
        }
        else
        {
            OpenMissionBoard();
        }
    }
    public void InventoryInteract()
    {
        if(inventoryOpen)
        {
            CloseInventory();
        }
        else
        {
            OpenInventory();
        }
    }
    public void StatusUIInteract()
    {
        if(statusOpen)
        {
            CloseStatus();
        }
        else
        {
            OpenStatus();
        }
    }
    public void OpenStatus()
    {
        if(UIMouseAndPriority.Instance.OtherPanelIsActive())return;
        invCanvasGroup.alpha = 0f;
        missionCanvasGroup.alpha = 0f;
        statusCanvasGroup.alpha = 1f;
        statusOpen = true;
        PlayerStatusInfoUI.Instance.UpdateInfo(); 
        statusPanel.SetAsLastSibling();
        animator.updateMode = AnimatorUpdateMode.UnscaledTime;
        animator.SetTrigger("Idle");
        MoveUp();
    }
    public void CloseStatus()
    {
        if(!IsOnTop(statusPanel))
        {
            return;
        }
        animator.SetTrigger("Stop");
        MoveDown();
    }
    public void OpenMissionBoard()
    {
        if(UIMouseAndPriority.Instance.OtherPanelIsActive())return;
        invCanvasGroup.alpha = 0f;
        statusCanvasGroup.alpha = 0f;
        missionCanvasGroup.alpha = 1f;
        missionPanel.SetAsLastSibling();
        missionBoardOpen = true;
        MoveUp();
    }
    public void CloseMissionBoard()
    {
        if(!IsOnTop(missionPanel))
        {
            return;
        }
        MoveDown();
        MissionPageUI.Instance.HideMissionInfo();
    }
    public void OpenInventory()
    {
        if(UIMouseAndPriority.Instance.OtherPanelIsActive())return;
        invCanvasGroup.alpha = 1f;
        missionCanvasGroup.alpha = 0f;
        statusCanvasGroup.alpha = 0f;
        inventoryOpen = true;
        UIInventoryPage.Instance.InventoryUpdateOpen();
        invPanel.SetAsLastSibling();
        animator.updateMode = AnimatorUpdateMode.UnscaledTime;
        animator.SetTrigger("Idle");
        MoveUp();
    }
    public void CloseInventory()
    {
        if(!IsOnTop(invPanel))
        {
            return;
        }
        UIInventoryPage.Instance.CloseDescriptionPanel();
        animator.SetTrigger("Stop");
        MoveDown();
    }
    public void MoveUp()
    {
        ismPanel.DOKill();
        ismPanel.DOAnchorPos(visiblePosition, moveDuration).SetEase(Ease.OutQuad).SetUpdate(true).OnComplete(() =>
        {
            Time.timeScale = 0f;
        });
    }
    public void MoveDown()
    {
        ismPanel.DOKill();
        ismPanel.DOAnchorPos(hiddenPosition, moveDuration).SetEase(Ease.OutQuad).SetUpdate(true).OnComplete(() =>
        {
            missionBoardOpen = false;
            inventoryOpen = false;
            statusOpen = false;
            Time.timeScale = 1f;
        });
    }
    private bool IsOnTop(RectTransform panel)
    {
        return panel.transform.GetSiblingIndex() == ismPanel.childCount - 1;
    }
    private void OnEnable()
    {
        closeBtn.onClick.AddListener(MoveDown);
        missionBtn.onClick.AddListener(OnMissButtonClicked);
        invBtn.onClick.AddListener(OnInvButtonClicked);
        statusBtn.onClick.AddListener(OnStatusButtonClicked);
    }
    private void OnMissButtonClicked()
    {   
        invCanvasGroup.alpha = 0f;
        statusCanvasGroup.alpha = 0f;
        missionCanvasGroup.alpha = 1f;
        missionBoardOpen = true;
        inventoryOpen = false;
        statusOpen = false;
        missionPanel.SetAsLastSibling();
    }
    private void OnInvButtonClicked()
    {
        invCanvasGroup.alpha = 1f;
        statusCanvasGroup.alpha = 0f;
        missionCanvasGroup.alpha = 0f;
        inventoryOpen = true;
        missionBoardOpen = false;
        statusOpen = false;
        invPanel.SetAsLastSibling();
    }
    private void OnStatusButtonClicked()
    {
        statusCanvasGroup.alpha = 1f;
        invCanvasGroup.alpha = 0f;
        missionCanvasGroup.alpha = 0f;
        inventoryOpen = false;
        missionBoardOpen = false;
        statusOpen = true;
        statusPanel.SetAsLastSibling();
    }
}
