using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMouseAndPriority : MonoBehaviour
{
    public static UIMouseAndPriority Instance;

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
        if(PauseMenu.isPaused || NPCDialogueControl.isDialogueActive || PauseMenu.isOver)
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
        if(Input.GetKeyDown(KeyCode.C) && !IsInLimitInteractPanel()) 
        {
            UIInventoryController.Instance.StatusUIInteract();
            return;
        } 
        if(Input.GetKeyDown(KeyCode.J) && !IsInLimitInteractPanel())  
        {
            UIInventoryController.Instance.MissionBoardInteract();
            return;
        }  
        if(Input.GetKeyDown(KeyCode.I) && !IsInLimitInteractPanel()) 
        {
            UIInventoryController.Instance.InventoryInteract();
            return;
        } 
        if(Input.GetKeyDown(KeyCode.Escape) && !IsInLimitInteractPanel() && OtherPanelIsActive())
        {
            CloseAllUI();
            return;
        }
        if(Input.GetKeyDown(KeyCode.Escape) && !PauseMenu.isOver)
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
