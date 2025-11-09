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
        if(UIInventoryController.inventoryOpen || UIInventoryController.missionBoardOpen || PlayerStatusUI.playerStatusUIOpen 
        || PauseMenu.isPaused || ShopController.Instance.shopPanelIsOpen || NPCDialogueControl.isDialogueActive)
        {
            return true;
        }
        return false;
    }
}
