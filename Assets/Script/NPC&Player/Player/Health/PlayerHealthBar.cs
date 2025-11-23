using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
    
    [SerializeField]private Image healthBarFrontImage;
    [SerializeField]private Image healthBarBackImage;
    [SerializeField]private TextMeshProUGUI healthText;
    public void SetMaxHealth(float health)
    {
        healthBarFrontImage.fillAmount = 1f;
        healthBarBackImage.fillAmount = 1f;
    }

    public void SetHealth(float health)
    {
        float target = PlayerHealthControl.Instance.healthCurrentValue / PlayerHealthControl.Instance.healthMaxValue;
        if(healthBarFrontImage.fillAmount > healthBarBackImage.fillAmount)
        {
            healthBarBackImage.fillAmount = healthBarFrontImage.fillAmount;
        }
        healthBarFrontImage.DOFillAmount(target, .1f).SetEase(Ease.Linear).SetUpdate(true);
        healthBarBackImage.DOFillAmount(target, .5f).SetEase(Ease.Linear).SetUpdate(true);
    }
    public void UpdateHealthText()
    {
        healthText.text = $"{(int)PlayerHealthControl.Instance.healthCurrentValue}/{(int)PlayerHealthControl.Instance.healthMaxValue}";
    }
}