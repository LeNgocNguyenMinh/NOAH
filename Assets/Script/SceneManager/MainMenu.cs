using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
using Unity.VisualScripting;

public class MainMenu : MonoBehaviour
{
    private string saveLocation;
    private string newGameSaveLocation;
    [SerializeField]private RectTransform newGameConfirmPanel;
    [SerializeField]private RectTransform quitGameConfirmPanel;
    [SerializeField]private RectTransform settingPanel;
    [SerializeField]private GameObject otherPanel;
    public Slider loadingBar;
    public TMP_Text progressText;
    private void Awake()
    {
        saveLocation = Path.Combine(Application.persistentDataPath, "saveData.json");
        newGameSaveLocation = Path.Combine(Application.persistentDataPath, "newGameData.json");   
    }
    void Start()
    {
        Debug.Log("Save location: " + saveLocation);
        if (loadingBar != null)
        {
            loadingBar.value = 0f;
            loadingBar.gameObject.SetActive(false);
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
        OnLoading();
        newGameConfirmPanel.DOScaleX(0f, 0.5f).SetEase(Ease.OutQuad).SetUpdate(true).OnComplete(() =>
        {
            newGameConfirmPanel.gameObject.SetActive(false);
        });
        StartCoroutine(LoadNewGameScene("Level1"));
    }
    private IEnumerator LoadNewGameScene(string sceneName)
    {
        yield return null;
        // Bắt đầu load scene nhưng không active ngay lập tức
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        operation.allowSceneActivation = false;

        // Chờ scene load xong
        while (!operation.isDone)
        {
            float targetProgress = Mathf.Clamp01(operation.progress / 0.9f);
            loadingBar.value = targetProgress ;
            progressText.text = "Loading " + targetProgress * 100 + "%";
            // Khi progress đạt 0.9 có nghĩa là scene đã load xong, chỉ còn chờ active
            if (operation.progress >= 0.9f)
            {
                progressText.text = "Press F to enter game >> ";
                if(Input.GetKeyDown(KeyCode.F))
                {
                    SceneManager.sceneLoaded += OnNewGameSceneLoaded;
                    operation.allowSceneActivation = true;
                }

            }
            yield return null;
        }
    }
    private void OnNewGameSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SceneManager.sceneLoaded -= OnNewGameSceneLoaded;
        SaveController.Instance.LoadNewGame();
    }
    public void LoadGameSave()
    {
        OnLoading();
        if (File.Exists(saveLocation))
        {
            // Đọc nội dung của file save
            string jsonContent = File.ReadAllText(saveLocation);

            // Chuyển nội dung JSON thành đối tượng SaveData
            SaveData saveData = JsonUtility.FromJson<SaveData>(jsonContent);

            // Trả về giá trị saveScene
            StartCoroutine(LoadSceneAsync(saveData.saveScene));
        }
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
            float targetProgress = Mathf.Clamp01(operation.progress / 0.9f);
            loadingBar.value = targetProgress ;
            progressText.text = "Loading " + targetProgress * 100 + "%";
            // Khi progress đạt 0.9 có nghĩa là scene đã load xong, chỉ còn chờ active
            if (operation.progress >= 0.9f)
            {
                progressText.text = "Press F to enter game >> ";
                if(Input.GetKeyDown(KeyCode.F))
                {
                    SceneManager.sceneLoaded += OnSceneLoaded;
                    operation.allowSceneActivation = true;
                }

            }
            yield return null;
        }
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        SaveController.Instance.LoadSave();
    }
    
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
        otherPanel.SetActive(false);
        loadingBar.gameObject.SetActive(true);
    }
   
}
