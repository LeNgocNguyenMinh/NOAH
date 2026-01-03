using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;


public class MainMenu : MonoBehaviour
{
    private string saveLocation;
    [SerializeField]private RectTransform newGameConfirmPanel;
    [SerializeField]private RectTransform quitGameConfirmPanel;
    [SerializeField]private RectTransform settingPanel;
    [SerializeField]private RectTransform popUpRect;
    [SerializeField]private Vector2 hiddenPos;
    [SerializeField]private Vector2 visiblePos;
    [SerializeField]private GameObject otherPanel;
    [SerializeField]private SettingBtnFunction settingBtnFunction;
    [SerializeField]private GraphicManager graphicManager;
    [Header("Buttons")]
    [Header("MainMenuButtons")]
    [SerializeField]private Button newGameBtn;
    [SerializeField]private Button newGameConfirmBtn;
    [SerializeField]private Button newGameCancelBtn;
    [SerializeField]private Button loadGameBtn;
    [SerializeField]private Button settingBtn;
    [SerializeField]private Button quitGameBtn;
    [SerializeField]private Button quitGameConfirmBtn;
    [SerializeField]private Button quitGameCancelBtn;
    
    [Header("SettingButtons")]
    [SerializeField]private Button settingApplyBtn;
    [SerializeField]private Button settingCancelMenu;
    public TMP_Text progressText;
    private void Awake()
    {
        saveLocation = Path.Combine(Application.persistentDataPath, "saveData.json");
    }
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Debug.Log("Save location: " + saveLocation);      
    }
    private void OnEnable()
    {
        //MainMenu Buttons
        newGameBtn.onClick.AddListener(NewGameBtn);
        newGameConfirmBtn.onClick.AddListener(StartNewGame);
        newGameCancelBtn.onClick.AddListener(NewGameAskPanelOff);
        loadGameBtn.onClick.AddListener(LoadGameBtn);
        settingBtn.onClick.AddListener(SettingBtn);
        quitGameBtn.onClick.AddListener(QuitGameBtn);
        quitGameConfirmBtn.onClick.AddListener(QuitGame);
        quitGameCancelBtn.onClick.AddListener(QuitGameAskPanelOff);
        //Setting Buttons
        settingApplyBtn.onClick.AddListener(graphicManager.ApplyGraphics);
        settingCancelMenu.onClick.AddListener(SettingPanelOff);
    }
    public void NewGameBtn()
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
    public void QuitGameBtn()
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
        SceneTransition.Instance.SceneOut();
        progressText.gameObject.SetActive(true);
        StartCoroutine(LoadNewGameScene("Level1"));
    }
    private IEnumerator LoadNewGameScene(string sceneName)
    {
        yield return null;
        // Start to load but not activate the scene immediately
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        operation.allowSceneActivation = false;

        // wait for the scene to load
        while (!operation.isDone)
        {
            float targetProgress = Mathf.Clamp01(operation.progress / 0.9f);
            progressText.text = "Loading " + targetProgress * 100 + "%";
            // Khi progress đạt 0.9 có nghĩa là scene đã load xong, chỉ còn chờ active
            if (operation.progress >= 0.9f && !SceneTransition.Instance.inAnim) 
            {
                progressText.text = "Press F to enter game >> ";
                if(Input.GetKeyDown(KeyCode.F))
                {
                    progressText.gameObject.SetActive(false);
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
        TutorialUIManager.Instance.StartTutorial();
        SaveController.Instance.LoadNewGame();
        SceneTransition.Instance.SceneIn();
    }
    public void LoadGameBtn()
    {
        if (File.Exists(saveLocation))
        {
            OnLoading();
            SceneTransition.Instance.SceneOut();
            // Trả về giá trị saveScene
            progressText.gameObject.SetActive(true);
            StartCoroutine(LoadSceneAsync("Level1"));
        }
        else{
            NotifPopUp.Instance.ShowNotification("No save file found!");
        }
        /* #if UNITY_WEBGL
        if (!PlayerPrefs.HasKey("SaveData"))
        {
            NotifPopUp.Instance.ShowNotification("No save file found!");
            return;
        }
        #else
        if (!File.Exists(saveLocation))
        {
            NotifPopUp.Instance.ShowNotification("No save file found!");
            return;
        }
        #endif
        OnLoading();
        SceneTransition.Instance.SceneOut();
        progressText.gameObject.SetActive(true);
        StartCoroutine(LoadSceneAsync("Level1"));
        */
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
            progressText.text = "Loading " + targetProgress * 100 + "%";
            // Khi progress đạt 0.9 có nghĩa là scene đã load xong, chỉ còn chờ active
            if (operation.progress >= 0.9f && !SceneTransition.Instance.inAnim)
            {
                progressText.text = "Press F to enter game >> ";
                if(Input.GetKeyDown(KeyCode.F))
                {
                    progressText.gameObject.SetActive(false);
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
        SceneTransition.Instance.SceneIn();    }
    
    public void SettingBtn()
    {
        otherPanel.SetActive(false);
        settingPanel.gameObject.SetActive(true);
        settingBtnFunction.ActiveGameSettingPanel();
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
    }
   
}
