using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TutorialUIManager : MonoBehaviour
{
    public static TutorialUIManager instance;
    public static bool panelActive = false;
    [SerializeField]private RectTransform tutorialPanel;
    [SerializeField]private CanvasGroup keyBoardCG;
    [SerializeField]private CanvasGroup energyCG;
    [SerializeField]private CanvasGroup bossFightCG;
    [SerializeField]private GameObject KeyBoardGO;
    [SerializeField]private GameObject EnergyGO;
    [SerializeField]private GameObject BossFightGO;
    [SerializeField]private Button toEnergy;
    [SerializeField]private Button toBossFight;
    [SerializeField]private Button backToKeyBoard;
    [SerializeField]private Button endTutorial;
    [SerializeField]private Button backToEnergy;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        StartTutorial();   
    }
    private void OnEnable()
    {
        toEnergy.onClick.AddListener(ShowEnergyTutorial);
        toBossFight.onClick.AddListener(ShowBossFightTutorial);
        backToKeyBoard.onClick.AddListener(ShowKeyBoardTutorial);
        backToEnergy.onClick.AddListener(ShowEnergyTutorial);
        endTutorial.onClick.AddListener(EndTutorial);
    }
    private void ShowEnergyTutorial()
    {
        /* keyBoardCG.alpha = 0;
        keyBoardCG.interactable = false;
        keyBoardCG.blocksRaycasts = false;

        energyCG.alpha = 1;
        energyCG.interactable = true;
        energyCG.blocksRaycasts = true;

        bossFightCG.alpha = 0;
        bossFightCG.interactable = false;
        bossFightCG.blocksRaycasts = false; */
        EnergyGO.SetActive(true);
        KeyBoardGO.SetActive(false);
        BossFightGO.SetActive(false);
    }
    private void ShowBossFightTutorial()
    {
        /* keyBoardCG.alpha = 0;
        keyBoardCG.interactable = false;
        keyBoardCG.blocksRaycasts = false;

        energyCG.alpha = 0;
        energyCG.interactable = false;
        energyCG.blocksRaycasts = false;

        bossFightCG.alpha = 1;
        bossFightCG.interactable = true;
        bossFightCG.blocksRaycasts = true; */
        EnergyGO.SetActive(false);
        KeyBoardGO.SetActive(false);
        BossFightGO.SetActive(true);
    }
    private void ShowKeyBoardTutorial()
    {
        /* keyBoardCG.alpha = 1;
        keyBoardCG.interactable = true;
        keyBoardCG.blocksRaycasts = true;

        energyCG.alpha = 0;
        energyCG.interactable = false;
        energyCG.blocksRaycasts = false;

        bossFightCG.alpha = 0;
        bossFightCG.interactable = false;
        bossFightCG.blocksRaycasts = false; */
        EnergyGO.SetActive(false);
        KeyBoardGO.SetActive(true);
        BossFightGO.SetActive(false);
    }
    public void StartTutorial()
    {
        tutorialPanel.DOScaleX(1f, 0.5f).SetEase(Ease.OutQuad).SetUpdate(true).OnComplete(() =>
        {
            panelActive = true;
            ShowKeyBoardTutorial();
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
