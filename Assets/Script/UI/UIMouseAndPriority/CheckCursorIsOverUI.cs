using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CheckCursorIsOverUI : MonoBehaviour, IPointerEnterHandler,IPointerExitHandler
{
     public void OnPointerEnter(PointerEventData eventData)
    {
        MouseSetting.Instance.SetMouseShouldVisible(true);
        WeaponParent.playerCanShoot = false;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        MouseSetting.Instance.SetMouseShouldVisible(false);
        WeaponParent.playerCanShoot = true;
    }

}
