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
    private SpawnControl spawnControl;
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

    public static bool isPaused = false;
    public static bool isOver = false;

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
        pauseImage.sprite = pauseSprite;
        resumeButtonImage.sprite = resumeSprite;
        pauseMenuPanel.gameObject.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
        pauseMenuPanel.DOAnchorPos(visiblePosition, moveDuration).SetEase(Ease.OutQuad).SetUpdate(true);
    }
    //Resume button function, if player dead then respawn if player choose continue
    public void PauseMenuPanelOff()
    {
        pauseMenuPanel.DOAnchorPos(hiddenPosition, moveDuration).SetEase(Ease.OutQuad).SetUpdate(true).OnComplete(() =>
        {
            if(isOver)
            {
                spawnControl = FindObjectOfType<SpawnControl>().GetComponent<SpawnControl>();
                spawnControl.RespawnAfterDead();
                isOver = false;
            }
            isPaused = false;
            pauseMenuPanel.gameObject.SetActive(false);
            Time.timeScale = 1f;
        });
    }

    //Ask panel active for main menu button function
    public void ButtonShowFuntion(ButtonEnum btnEnum)
    {
        otherPanel.SetActive(false);
        if(btnEnum.buttonType == ButtonEnum.ButtonType.MainMenuBtn)
        {
            mainMenuAskPanel.gameObject.SetActive(true);
            mainMenuAskPanel.DOScaleX(1f, 0.5f).SetEase(Ease.OutQuad).SetUpdate(true);
        }
        else if(btnEnum.buttonType == ButtonEnum.ButtonType.QuitBtn)
        {
            quitAskPanel.gameObject.SetActive(true);
            quitAskPanel.DOScaleX(1f, 0.5f).SetEase(Ease.OutQuad).SetUpdate(true);
        }
        else if(btnEnum.buttonType == ButtonEnum.ButtonType.SettingBtn)
        {
            settingPanel.gameObject.SetActive(true);
            settingPanel.DOScaleX(1f, 0.5f).SetEase(Ease.OutQuad).SetUpdate(true);
        }
    }
  
    //Ask panel deactive for main menu button function
    public void BackToPauseMenu()
    {
        otherPanel.SetActive(true);
        mainMenuAskPanel.DOScaleX(0f, 0.5f).SetEase(Ease.OutQuad).SetUpdate(true).OnComplete(() =>
        {
            mainMenuAskPanel.gameObject.SetActive(false);
        }); 
        quitAskPanel.DOScaleX(0f, 0.5f).SetEase(Ease.OutQuad).SetUpdate(true).OnComplete(() =>
        {
            quitAskPanel.gameObject.SetActive(false);
        }); 
        settingPanel.DOScaleX(0f, 0.5f).SetEase(Ease.OutQuad).SetUpdate(true).OnComplete(() =>
        {
            settingPanel.gameObject.SetActive(false);
        }); 
    }
    public void MoveTo(ButtonEnum btnEnum)
    {
        PauseMenuPanelOff();
        if(btnEnum.buttonType == ButtonEnum.ButtonType.MainMenuConfirm)
        {
            StartCoroutine(ToMainMenuCoroutine());
        }
        else if(btnEnum.buttonType == ButtonEnum.ButtonType.QuitConfirm)
        {
            Application.Quit();
        }
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
