using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassSway : MonoBehaviour
{
    [SerializeField] private Transform frontLayer;  // lớp trước
    [SerializeField] private Transform backLayer;   // lớp sau
    [SerializeField] private float rotateAngle = 15f;
    [SerializeField] private float duration = 0.5f; // thời gian xoay qua trái/phải
    [SerializeField] private float offset = 0.2f;   // độ trễ giữa hai lớp

    private void Start()
    {
        StartCoroutine(Sway(frontLayer, 0f));       // không trễ
        StartCoroutine(Sway(backLayer, offset));    // trễ một chút
    }

    private IEnumerator Sway(Transform layer, float delay)
    {
        yield return new WaitForSeconds(delay);

        while (true)
        {
            yield return RotateTo(layer, -rotateAngle, duration);  // xoay trái
            yield return RotateTo(layer, rotateAngle, duration * 2); // xoay phải
            yield return RotateTo(layer, 0, duration);             // trở lại giữa
        }
    }

    private IEnumerator RotateTo(Transform target, float targetZ, float time)
    {
        float elapsed = 0f;
        Quaternion startRotation = target.localRotation;
        Quaternion endRotation = Quaternion.Euler(0f, 0f, targetZ);

        while (elapsed < time)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / time;
            target.localRotation = Quaternion.Lerp(startRotation, endRotation, t);
            yield return null;
        }

        target.localRotation = endRotation;
    }
}
