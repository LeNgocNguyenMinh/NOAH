using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections.Generic;
using TMPro;

public class TutorialUIManager : MonoBehaviour
{
    public static TutorialUIManager Instance;
    public static bool panelActive = false;
    [SerializeField]private RectTransform tutorialPanel;
    [SerializeField]private List<GameObject> tutorialUIs = new List<GameObject>();
    [SerializeField]private Button nextButton;
    [SerializeField]private TextMeshProUGUI nextButtonText;
    [SerializeField]private Button previousButton;
    private GameObject currentPanel;
    public int currentPanelIndex;
    private void Awake()
    {
        if (Instance == null)
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
        nextButton.onClick.AddListener(NextPanel);
        previousButton.onClick.AddListener(PreviousPanel);
    }
    public void NextPanel()
    {
        currentPanelIndex++;
        previousButton.gameObject.SetActive(true);
        if (currentPanelIndex >= tutorialUIs.Count)
        {
            EndTutorial();
        }
        else{
            currentPanel.SetActive(false);
            currentPanel = tutorialUIs[currentPanelIndex];
            currentPanel.SetActive(true);
        }
    }
    public void PreviousPanel()
    {
        currentPanelIndex--;
        if (currentPanelIndex <=0)
        {
            currentPanelIndex = 0;
            previousButton.gameObject.SetActive(false);
        }
        else{
            previousButton.gameObject.SetActive(true);
        }
        currentPanel.SetActive(false);
        currentPanel = tutorialUIs[currentPanelIndex];
        currentPanel.SetActive(true);
    }
    public void StartTutorial()
    {
        currentPanelIndex = 0;
        currentPanel = tutorialUIs[0];
        tutorialPanel.DOScaleX(1f, 0.5f).SetEase(Ease.OutQuad).SetUpdate(true).OnComplete(() =>
        {
            panelActive = true;
            PreviousPanel();
            Time.timeScale = 0f;
        });
    }
    private void EndTutorial()
    {
        panelActive = false;
        tutorialPanel.DOScaleX(0f, 0.5f).SetEase(Ease.OutQuad).SetUpdate(true);
        Time.timeScale = 1f;
    }
}


