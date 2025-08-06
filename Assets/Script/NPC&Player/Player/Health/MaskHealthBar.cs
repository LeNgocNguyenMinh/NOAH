using UnityEngine;
using UnityEngine.UI;
using DG.Tweening; // Đừng quên import DOTween

public class MaskHealthBar : MonoBehaviour
{
    [SerializeField] private RectMask2D mask;
    private float maxRightMask = 235;
    private float initialRightMask;
    private float maxHP;

    private float currentHP;
    private Tween maskTween;

    private void Start()
    {
        initialRightMask = mask.padding.z;
    }

    public void SetMaxHealth(float maxHealth)
    {
        maxHP = maxHealth;
        currentHP = maxHP;
        SetPadding(currentHP);
    }

    public void SetValue(float newHP)
    {
        newHP = Mathf.Clamp(newHP, 0, maxHP);
        currentHP = newHP;

        float targetWidth = currentHP * maxRightMask / maxHP;
        float newRightPadding = maxRightMask + initialRightMask - targetWidth;

        // Nếu đang tween, dừng lại
        if (maskTween != null && maskTween.IsActive())
            maskTween.Kill();

        float startPaddingZ = mask.padding.z;

        // Tween padding.z bằng cách tween giá trị tạm rồi apply thủ công
        maskTween = DOTween.To(
            () => startPaddingZ,
            x => {
                var padding = mask.padding;
                padding.z = x;
                mask.padding = padding;
            },
            newRightPadding,
            0.4f // thời gian tween
        ).SetEase(Ease.OutCubic);
    }

    private void SetPadding(float hp)
    {
        float targetWidth = hp * maxRightMask / maxHP;
        float newRightMax = maxRightMask + initialRightMask - targetWidth;
        var padding = mask.padding;
        padding.z = newRightMax;
        mask.padding = padding;
    }
}