using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
public class NotifPopUp : MonoBehaviour
{
    public static NotifPopUp Instance;

    [SerializeField] private GameObject notificationPrefab;
    [SerializeField] private RectTransform notificationPanel;
    [SerializeField] private float notificationDuration = 2f;
    [SerializeField] private float fadeDuration = 0.5f;
    [SerializeField] private int maxPopups = 3;
    private readonly Queue<GameObject> activePopup = new();

    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void ShowNotification(string newValue)
    {
        GameObject newNotif = Instantiate(notificationPrefab, notificationPanel);
        RectTransform rect = newNotif.GetComponent<RectTransform>();
        newNotif.GetComponentInChildren<TMP_Text>().text = newValue;
        // Gán parent trước khi thay đổi `anchoredPosition` để tránh lỗi
        activePopup.Enqueue(newNotif);
        if(activePopup.Count > maxPopups)
        {
                Destroy(activePopup.Dequeue());
        }
        CanvasGroup canvasGroup = newNotif.GetComponent<CanvasGroup>();
        // Delay 1 giây rồi bắt đầu fade out
        DOVirtual.DelayedCall(1f, () =>
        {
            canvasGroup.DOFade(0f, fadeDuration).SetUpdate(true).OnComplete(() => Destroy(newNotif));
        });
    }
}
