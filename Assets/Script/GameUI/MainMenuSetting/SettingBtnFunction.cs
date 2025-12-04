using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingBtnFunction : MonoBehaviour
{
    [SerializeField]private GameObject gameSettingPanel;
    [SerializeField]private GameObject controlSettingPanel;
    [SerializeField]private Button gameSetBtn;
    [SerializeField]private Button controlSetBtn;
    private void OnEnable()
    {
        gameSetBtn.onClick.AddListener(ActiveGameSettingPanel);
        controlSetBtn.onClick.AddListener(ActiveControlSettingPanel);
    }
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
