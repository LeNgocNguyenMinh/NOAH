using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CheckCursorIsOverUI : MonoBehaviour, IPointerEnterHandler,IPointerExitHandler
{
     public void OnPointerEnter(PointerEventData eventData)
    {
        MouseSetting.Instance.SetMouseShouldVisible(true);
        PlayerWeaponParent.Instance.playerCanATK = false;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        MouseSetting.Instance.SetMouseShouldVisible(false);
        PlayerWeaponParent.Instance.playerCanATK = true;
    }

}
