using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;
using System;

public class PauseMenu : MonoBehaviour
{
    public static PauseMenu Instance;
    [SerializeField]private RectTransform pauseMenuPanel;
    [SerializeField]private Vector2 hiddenPosition;
    [SerializeField]private Vector2 visiblePosition;  
    [SerializeField]private float moveDuration = 0.5f;  
    [SerializeField]private RectTransform settingPanel;
    [SerializeField]private Image pauseImage;
    [SerializeField]private Sprite pauseSprite;
    [SerializeField]private Sprite gameOverSprite;
    [SerializeField]private RectTransform quitAskPanel;
    [SerializeField]private RectTransform mainMenuAskPanel;
    [SerializeField]private GameObject otherPanel;
    [SerializeField]private Image resumeButtonImage;
    [SerializeField]private Sprite respawnSprite;
    [SerializeField]private Sprite resumeSprite;
    [SerializeField]private SettingBtnFunction settingBtnFunction;
    public static bool isPaused = false;
    public static bool isOver = false;
    [SerializeField]private GraphicManager graphicManager;
    [Header("Buttons")]
    [SerializeField]private Button resumeBtn;
    [SerializeField]private Button settingBtn;
    [SerializeField]private Button settingCancelBtn;
    [SerializeField]private Button settingApplyBtn;
    [SerializeField]private Button mainMenuBtn;
    [SerializeField]private Button mainMenuConfirmBtn;
    [SerializeField]private Button mainMenuCancelBtn;
    [SerializeField]private Button quitBtn;
    [SerializeField]private Button quitConfirmBtn;
    [SerializeField]private Button quitCancelBtn;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void OnEnable()
    {
        resumeBtn.onClick.AddListener(PauseMenuPanelOff);
        settingBtn.onClick.AddListener(SettingPanelShow);
        settingApplyBtn.onClick.AddListener(graphicManager.ApplyGraphics);
        settingCancelBtn.onClick.AddListener(SettingPanelOff);
        mainMenuBtn.onClick.AddListener(MainMenuAskPanelShow);
        mainMenuConfirmBtn.onClick.AddListener(MoveToMainMenu);
        mainMenuCancelBtn.onClick.AddListener(MainMenuAskPanelOff);
        quitBtn.onClick.AddListener(QuitAskPanelShow);
        quitConfirmBtn.onClick.AddListener(QuitGame);
        quitCancelBtn.onClick.AddListener(QuitAskPanelOff);
    }
    // Update is called once per frame
    //Open pause menu, for pause game or game over
    public void PauseMenuPanelShow(bool isDead = false)
    {
        if(isDead)
        {
            isOver = true;
            pauseImage.sprite = gameOverSprite;
            resumeButtonImage.sprite = respawnSprite;
        }
        else{
            pauseImage.sprite = pauseSprite;
            resumeButtonImage.sprite = resumeSprite;
        }
        Time.timeScale = 0f;
        isPaused = true;
        pauseMenuPanel.DOKill();
        pauseMenuPanel.DOAnchorPos(visiblePosition, moveDuration).SetEase(Ease.OutQuad).SetUpdate(true);
    }
    //Resume button function, if player dead then respawn if player choose continue
    public void PauseMenuPanelOff()
    {
        pauseMenuPanel.DOKill();
        pauseMenuPanel.DOAnchorPos(hiddenPosition, moveDuration).SetEase(Ease.OutQuad).SetUpdate(true).OnComplete(() =>
        {
            if(isOver)
            {
                SpawnControl.Instance.RespawnAfterDead();
                isOver = false;
            }
            isPaused = false;
            Time.timeScale = 1f;
        });
    }
    private void MainMenuAskPanelShow()
    {
        otherPanel.SetActive(false);
        mainMenuAskPanel.DOScaleX(1f, 0.5f).SetEase(Ease.OutQuad).SetUpdate(true);
    }
    private void MainMenuAskPanelOff()
    {
        mainMenuAskPanel.DOScaleX(0f, 0.5f).SetEase(Ease.OutQuad).SetUpdate(true).OnComplete(() =>
        {
            otherPanel.SetActive(true);
        }); 
    }
    private void QuitAskPanelShow()
    {
        otherPanel.SetActive(false);
        quitAskPanel.DOScaleX(1f, 0.5f).SetEase(Ease.OutQuad).SetUpdate(true);
    }
    private void QuitAskPanelOff()
    {
        quitAskPanel.DOScaleX(0f, 0.5f).SetEase(Ease.OutQuad).SetUpdate(true).OnComplete(() =>
        {
            otherPanel.SetActive(true);
        }); 
    }
    private void SettingPanelShow()
    {
        otherPanel.SetActive(false);
        settingBtnFunction.ActiveGameSettingPanel();
        settingPanel.DOScaleX(1f, 0.5f).SetEase(Ease.OutQuad).SetUpdate(true);
    }
    private void SettingPanelOff()
    {
        settingPanel.DOScaleX(0f, 0.5f).SetEase(Ease.OutQuad).SetUpdate(true).OnComplete(() =>
        {
            otherPanel.SetActive(true);
        });
    }
    public void MoveToMainMenu()
    {
        isPaused = false;
        StartCoroutine(ToMainMenuCoroutine());
    }
    public void QuitGame()
    {
        isPaused = false;
        Application.Quit();
    }
    IEnumerator ToMainMenuCoroutine()
    {
        Time.timeScale = 1f;
        SceneTransition.Instance.SceneOut();
        yield return new WaitForSeconds(2f);
        AsyncOperation operation = SceneManager.LoadSceneAsync("MainMenu");
        operation.allowSceneActivation = false;

        // Chờ scene load xong
        while (!operation.isDone)
        {
            if (operation.progress >= 0.9f)
            {
                SceneManager.sceneLoaded += OnSceneLoaded;
                operation.allowSceneActivation = true;
            }
            yield return null;
        }
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        SceneTransition.Instance.SceneIn();    
    }
}
