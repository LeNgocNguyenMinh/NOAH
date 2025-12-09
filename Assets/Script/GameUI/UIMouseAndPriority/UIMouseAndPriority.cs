using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMouseAndPriority : MonoBehaviour
{
    public static UIMouseAndPriority Instance;
    public bool canOpenUI = true;
    private void Awake()
    {
        Instance = this;   
    }
    public bool OtherPanelIsActive()//return false if one of the UI is Open
    {
        if(UIInventoryController.inventoryOpen || UIInventoryController.missionBoardOpen
        || UIInventoryController.statusOpen || ShopController.Instance.shopPanelIsOpen)
        {
            return true;
        }
        return false;
    } 
    public bool IsInLimitInteractPanel()
    {
        if(PauseMenu.isPaused || NPCDialogueControl.isDialogueActive || PauseMenu.isOver || TutorialUIManager.panelActive)
        {
            return true;
        }
        return false;
    }
    public void CloseAllUI()
    {
        UIInventoryController.Instance.MoveDown();
        ShopController.Instance.ShopUIClose(); 
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.C) && !IsInLimitInteractPanel() && canOpenUI) 
        {
            UIInventoryController.Instance.StatusUIInteract();
            return;
        } 
        if(Input.GetKeyDown(KeyCode.J) && !IsInLimitInteractPanel() && canOpenUI)  
        {
            UIInventoryController.Instance.MissionBoardInteract();
            return;
        }  
        if(Input.GetKeyDown(KeyCode.I) && !IsInLimitInteractPanel() && canOpenUI) 
        {
            UIInventoryController.Instance.InventoryInteract();
            return;
        } 
        if(Input.GetKeyDown(KeyCode.Escape) && !IsInLimitInteractPanel() && OtherPanelIsActive() && canOpenUI)
        {
            CloseAllUI();
            return;
        }
        if(Input.GetKeyDown(KeyCode.Escape) && TutorialUIManager.panelActive && canOpenUI)
        {
            return;
        }
        if(Input.GetKeyDown(KeyCode.Escape) && !PauseMenu.isOver && canOpenUI)
        {
            if(PauseMenu.isPaused)
            {
                Debug.Log("11111");
                PauseMenu.Instance.PauseMenuPanelOff();
            }
            else
            {
                Debug.Log("22222222");
                PauseMenu.Instance.PauseMenuPanelShow();
            }
        }
    }
    /* public void CloseCurrentPanel()
    {
        if(UIInventoryController.inventoryOpen || UIInventoryController.missionBoardOpen)
        {
            UIInventoryController.Instance.MoveDown();
        }
        if(PlayerStatusUI.playerStatusUIOpen)
        {
            PlayerStatusUI.Instance.ClosePlayerStatusUI();
        }
    } */
}
