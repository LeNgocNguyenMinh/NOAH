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
        StartCoroutine(LoadSceneAsync("Level1"));
    }
    private IEnumerator LoadSceneAsync(string sceneName)
    {
        yield return null;
        // Bắt đầu load scene nhưng không active ngay lập tức
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        operation.allowSceneActivation = false;

        // Chờ scene load xong
        while (!operation.isDone)
        {
            // Khi progress đạt 0.9 có nghĩa là scene đã load xong, chỉ còn chờ active
            if (operation.progress >= 0.9f)
            {
                Debug.Log("Scene Loaded. Now Loading Save Data...");
                
                // Gọi hàm load save game tại đây
                
                SceneManager.sceneLoaded += OnSceneLoaded;
                // Sau khi load save xong, active scene
                operation.allowSceneActivation = true;
            }
            yield return null;
        }
    }
     private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Xóa sự kiện sceneLoaded sau khi nó đã được gọi một lần
        SceneManager.sceneLoaded -= OnSceneLoaded;

        // Log thông báo và load dữ liệu save
        Debug.Log("Scene Activated. Now Loading Save Data...");
        SaveController.Instance.LoadSave(); // Gọi hàm load save ở đây sau khi scene đã active
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
