using UnityEngine;
using TMPro;

public class BossStatusController : MonoBehaviour
{
    private int bossLevel;
    private float bossMaxHealth;
    private float bossDamage;
    [SerializeField]private BossStatus bossBaseStatus;
    [SerializeField]private BossHealthControl bossHealthControl;
    [SerializeField]private TextMeshProUGUI levelText;
    public void BossUpdateInfo()
    {
        bossLevel = PlayerStatus.Instance.playerLevel;
        bossMaxHealth = bossBaseStatus.bossMaxHealth * (1 + 0.4f * bossLevel);
        bossDamage = bossBaseStatus.bossBaseDamage + bossLevel;
        levelText.text = bossLevel + "";
        bossHealthControl.SetMaxHealth(bossMaxHealth);
    }
    public float GetBossDamage()
    {
        return bossDamage;
    }
}
