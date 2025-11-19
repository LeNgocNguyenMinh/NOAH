using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
 
public class ParadoxEffect : MonoBehaviour {
    [SerializeField] private RawImage rawImage;
    [SerializeField] private float scrollSpeed = 0.5f;

    private float uvX = 0f;

    void Start()
    {
        DOTween.To(() => uvX, value =>
        {
            uvX = value;

            // cập nhật uvRect mỗi frame
            Rect rect = rawImage.uvRect;
            rect.x = uvX;
            rawImage.uvRect = rect;

        }, 1f, scrollSpeed)                            // 0 → 1 trong 2 giây
        .SetEase(Ease.Linear)
        .SetLoops(-1, LoopType.Restart);       // đạt 1 → trở về 0 → chạy lại
    }
}