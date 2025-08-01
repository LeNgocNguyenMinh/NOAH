using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

public class PauseMenu : MonoBehaviour
{
    private SpawnControl spawnControl;
    [SerializeField]private RectTransform pauseMenuPanel;
    [SerializeField]private Vector2 hiddenPosition;
    [SerializeField]private Vector2 visiblePosition;  
    [SerializeField]private float moveDuration = 0.5f;  
    [SerializeField]private RectTransform settingPanel;
    [SerializeField]private Image pauseImage;
    [SerializeField]private Sprite pauseSprite;
    [SerializeField]private Sprite gameOverSprite;
    [SerializeField]private GameObject quitAskPanel;
    [SerializeField]private GameObject mainMenuAskPanel;
    [SerializeField]private GameObject otherPanel;
    [SerializeField]private Image resumeButtonImage;
    [SerializeField]private Sprite respawnSprite;
    [SerializeField]private Sprite resumeSprite;

    public static bool isPaused = false;
    private bool isOver = false;
    private bool panelShow = false;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(isOver)return;
            if(isOver||panelShow)return;
            if(isPaused)
            {
                PauseMenuPanelOff();
            }
            else{
                if(!UIMouseAndPriority.Instance.CanOpenThisUI()) return;
                PauseMenuPanelShow();
            }
        }
    }
    //Open pause menu
    private void PauseMenuPanelShow()
    {
        pauseImage.sprite = pauseSprite;
        resumeButtonImage.sprite = resumeSprite;
        pauseMenuPanel.gameObject.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
        pauseMenuPanel.DOAnchorPos(visiblePosition, moveDuration).SetEase(Ease.OutQuad).SetUpdate(true);
    }
    public void GameOverMenuPanelShow()
    {
        isOver = true;
        pauseImage.sprite = gameOverSprite;
        resumeButtonImage.sprite = respawnSprite;
        pauseMenuPanel.gameObject.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
        pauseMenuPanel.DOAnchorPos(visiblePosition, moveDuration).SetEase(Ease.OutQuad).SetUpdate(true);
    }

    //Resume button function
    public void PauseMenuPanelOff()
    {
        Time.timeScale = 1f;
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
        });
    }
  
    public void SettingPanelShow()
    {
        panelShow = true;
        settingPanel.gameObject.SetActive(true);
        settingPanel.DOScaleX(1f, 0.5f).SetEase(Ease.OutQuad).SetUpdate(true);
    }
    public void SettingPanelOff()
    {
        settingPanel.DOScaleX(0f, 0.5f).SetEase(Ease.OutQuad).SetUpdate(true).OnComplete(() =>
        {
            settingPanel.gameObject.SetActive(false);
            panelShow = false;
        });
    }
    //Ask panel active for main menu button function
    public void MainMenuAskPanelShow()
    {
        panelShow = true;
        otherPanel.SetActive(false);
        mainMenuAskPanel.SetActive(true);
    }
    //Ask panel deactive for main menu button function
    public void MainMenuAskPanelOff()
    {
        panelShow = false;
        otherPanel.SetActive(true);
        mainMenuAskPanel.SetActive(false);
    }
    //To main menu
    public void ToMainMenu()
    {
        PauseMenuPanelOff();
        StartCoroutine(ToMainMenuCoroutine());
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
    //Ask panel active for quit game button function
    public void QuitAskPanelShow()
    {
        panelShow = true;
        otherPanel.SetActive(false);
        quitAskPanel.SetActive(true);
    }
    //Ask panel deactive for quit game button function
    public void QuitAskPanelOff()
    {
        panelShow = false;
        otherPanel.SetActive(true);
        quitAskPanel.SetActive(false);
    }
    //quit game
    public void QuitGame()
    {
        PauseMenuPanelOff();
        Application.Quit();
    }
}
