using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CheckCursorIsOverUI : MonoBehaviour, IPointerEnterHandler,IPointerExitHandler
{
    private MouseSetting mouseSetting;
    private void Start()
    {
        mouseSetting = FindObjectOfType<MouseSetting>().GetComponent<MouseSetting>();
    }
     public void OnPointerEnter(PointerEventData eventData)
    {
        mouseSetting.SetMouseShouldVisible(true);
        WeaponParent.playerCanShoot = false;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        mouseSetting.SetMouseShouldVisible(false);
        WeaponParent.playerCanShoot = true;
    }

}
