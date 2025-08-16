using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingBtnFunction : MonoBehaviour
{
    [SerializeField]private GameObject gameSettingPanel;
    [SerializeField]private GameObject controlSettingPanel;
    public void ActiveGameSettingPanel()
    {
        gameSettingPanel.SetActive(true);
        controlSettingPanel.SetActive(false);
    }
    public void ActiveControlSettingPanel()
    {
        gameSettingPanel.SetActive(false);
        controlSettingPanel.SetActive(true);
    }
    
}
