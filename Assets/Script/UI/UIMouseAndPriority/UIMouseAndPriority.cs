using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMouseAndPriority : MonoBehaviour
{
    public bool CanOpenThisUI()//return false if one of the UI is Open
    {
        if(UIInventoryPage.inventoryOpen || PlayerStatusUI.playerStatusUIOpen 
        || PauseMenu.isPaused || ShopController.shopPanelIsOpen || NPCDialogueControl.isDialogueActive)
        {
            return false;
        }
        return true;
    }
}
