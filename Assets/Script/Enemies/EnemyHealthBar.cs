using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class EnemyHealthBar : MonoBehaviour
{
/*     [SerializeField]private Image healthBarFrontImage; */
    [SerializeField]private Image healthBarBackImage;
    [SerializeField]private TextMeshProUGUI healthText;
    private float currentHealth;
    private float maxHealth;
    private Camera mainCamera;
    private void Start()
    {
        mainCamera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
    }
    public void SetMaxHealth(float health)
    {
/*         healthBarFrontImage.fillAmount = 1f; */
        healthBarBackImage.fillAmount = 1f;
        currentHealth = health;
        maxHealth = health;
    }

    public void SetHealth(float health)
    {
        currentHealth = health;
        float target = health / maxHealth;
        /* if(healthBarFrontImage.fillAmount > healthBarBackImage.fillAmount)
        {
            healthBarBackImage.fillAmount = healthBarFrontImage.fillAmount;
        } */
        /* healthBarFrontImage.DOFillAmount(target, .1f).SetEase(Ease.Linear).SetUpdate(true); */
        healthBarBackImage.DOFillAmount(target, .5f).SetEase(Ease.Linear).SetUpdate(true);
    }
    public void UpdateHealthText() //Update Health Text only when something change
    {
        healthText.text = $"{currentHealth} / {maxHealth}";
    }
    private void Update()
    {
        transform.rotation = mainCamera.transform.rotation;
    }
}
