using UnityEngine;
using DG.Tweening;
using TMPro;

public class DamagePopUp : MonoBehaviour
{
    public static DamagePopUp Instance;
    [SerializeField]private TextMeshPro damageNumberText;
    private void Awake()
    {
        Instance = this;
    }
    public void ShowDamage(float damage)
    {
        damageNumberText.text = ((int)damage).ToString();
        transform.DOMoveY(transform.position.y + 1f, 0.5f)
                  .SetEase(Ease.OutCubic) // chuyển động mượt
                  .OnComplete(() => Destroy(gameObject));
    } 
}
