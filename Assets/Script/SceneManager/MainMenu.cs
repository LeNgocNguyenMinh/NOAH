using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField]private RectTransform newGameConfirmPanel;
    [SerializeField]private RectTransform quitGameConfirmPanel;
    [SerializeField]private RectTransform settingPanel;
    [SerializeField]private GameObject otherPanel;
    public GameObject OptionUI;
    public GameObject LoadingBar;
    public Slider progressBar;
    public TMP_Text progressText;
    void Start()
    {
        if (OptionUI && LoadingBar != null)
        {
            OptionUI.SetActive(true);
            LoadingBar.SetActive(false);
        }        
    }
    public void NewGameAskPanelShow()
    {
        otherPanel.SetActive(false);
        newGameConfirmPanel.gameObject.SetActive(true);
        newGameConfirmPanel.DOScaleX(1f, 0.5f).SetEase(Ease.OutQuad).SetUpdate(true);
    }
    public void NewGameAskPanelOff()
    {
        newGameConfirmPanel.DOScaleX(0f, 0.5f).SetEase(Ease.OutQuad).SetUpdate(true).OnComplete(() =>
        {
            otherPanel.SetActive(true);
            newGameConfirmPanel.gameObject.SetActive(false);
        });
    }
    //Quit ask panel
    public void QuitAskPanelShow()
    {
        otherPanel.SetActive(false);
        quitGameConfirmPanel.gameObject.SetActive(true);
        quitGameConfirmPanel.DOScaleX(1f, 0.5f).SetEase(Ease.OutQuad).SetUpdate(true);
    }
    public void QuitGameAskPanelOff()
    {
        quitGameConfirmPanel.DOScaleX(0f, 0.5f).SetEase(Ease.OutQuad).SetUpdate(true).OnComplete(() =>
        {
            otherPanel.SetActive(true);
            quitGameConfirmPanel.gameObject.SetActive(false);
        });
    }
    public void StartNewGame()
    {
    }
    public void LoadGameSave()
    {
        StartCoroutine(LoadGameAsync("Level1"));
        SceneManager.LoadScene("Level1");
        SaveController.Instance.LoadSave();
    }
    private IEnumerator LoadGameAsync(string sceneName)
    {
        // Bật màn hình loading
        OnLoading();
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        asyncLoad.allowSceneActivation = false; // Chưa cho phép scene chuyển ngay lập tức
        yield return StartCoroutine(LoadData());
        asyncLoad.allowSceneActivation = true;
    }
    
    private IEnumerator LoadData()
    {
        // Đợi 0.5 giây để tránh lag nếu cần
        yield return new WaitForSeconds(0.5f);
        
        // Gọi hàm load dữ liệu từ SaveController
        SaveController.Instance.LoadSave();

        // Đợi 1 giây để dữ liệu được xử lý (tùy vào hệ thống)
        yield return new WaitForSeconds(1f);
    }
/*     private void UpdateProgressText()
    {
        if (progressText != null)
        {
            float currentProgress = progressBar.value; // Get current value of the slider
            progressText.text = $"Loading: {Mathf.RoundToInt(currentProgress * 100)}%";
        }
    } */
    
    public void SettingPanelShow()
    {
        otherPanel.SetActive(false);
        settingPanel.gameObject.SetActive(true);
        settingPanel.DOScaleX(1f, 0.5f).SetEase(Ease.OutQuad).SetUpdate(true);
    }
    public void SettingPanelOff()
    {
        settingPanel.DOScaleX(0f, 0.5f).SetEase(Ease.OutQuad).SetUpdate(true).OnComplete(() =>
        {
            otherPanel.SetActive(true);
            settingPanel.gameObject.SetActive(false);
        });
    }
    public void QuitGame()
    {
        Application.Quit();
    }

    public void OnLoading()
    {
        OptionUI.SetActive(false);
        LoadingBar.SetActive(true);
    }
   
}
