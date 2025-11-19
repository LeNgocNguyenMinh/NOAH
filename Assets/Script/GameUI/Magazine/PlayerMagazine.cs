using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class PlayerMagazine : MonoBehaviour
{
    public static PlayerMagazine Instance;
    [SerializeField]private Image bulletEnergyFront;
    [SerializeField]private Image bulletEnergyBack;
    [SerializeField]private Image hitEnergyFront;
    [SerializeField]private Image hitEnergyBack;
    [SerializeField]private TextMeshProUGUI text;
    private float energyMainTime = 0.3f;
    private float energySlowerTime = 1f;
    private float hitMainTime = 0.1f;
    private float hitSlowerTime = 0.3f;
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
    public void CheckEnergyBarLeft()
    {
        float target = PlayerWeaponParent.Instance.GetCurrentBullet() / (float)PlayerWeaponParent.Instance.GetMagazine();
        if(bulletEnergyFront.fillAmount > bulletEnergyBack.fillAmount)
        {
            bulletEnergyBack.fillAmount = bulletEnergyFront.fillAmount;
        }
        bulletEnergyFront.DOFillAmount(target, energyMainTime).SetEase(Ease.Linear);
        bulletEnergyBack.DOFillAmount(target, energySlowerTime).SetEase(Ease.Linear);
    }
    public void CheckEnergyBarRight()
    {
        float target = PlayerWeaponParent.Instance.GetCurrentHitCount() / (float)PlayerWeaponParent.Instance.GetRequireHit();
        if(hitEnergyFront.fillAmount > hitEnergyBack.fillAmount)
        {
            hitEnergyBack.fillAmount = hitEnergyFront.fillAmount;
        }
        hitEnergyFront.DOFillAmount(target, hitMainTime).SetEase(Ease.Linear);
        hitEnergyBack.DOFillAmount(target, hitSlowerTime).SetEase(Ease.Linear);
        if(PlayerWeaponParent.Instance.GetCurrentHitCount() >= PlayerWeaponParent.Instance.GetRequireHit())
        {
            if(PlayerWeaponParent.Instance.GetCurrentBullet() < PlayerWeaponParent.Instance.GetMagazine())
            {
                PlayerWeaponParent.Instance.AddCurrentBullet();
                CheckEnergyBarLeft();
                PlayerWeaponParent.Instance.UpdateMagazine();
            }
            PlayerWeaponParent.Instance.SetCurrentHitCount(0); // Reset hit count after charging energy
            CheckEnergyBarRight();
        }
    }
    public void HitCountIncrease()
    {
        PlayerWeaponParent.Instance.AddCurrentHitCount();
        CheckEnergyBarRight();
    }
    public void UpdateMagazineText()
    {
        text.text = $"{PlayerWeaponParent.Instance.GetCurrentBullet()}/{PlayerWeaponParent.Instance.GetMagazine()}";
    }
}
