using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatTextEffect : MonoBehaviour
{

    public float amplitude = 10f;       // Biên độ dao động (độ cao trôi nổi)
    public float frequency = 2f;        // Tần số dao động (tốc độ trôi)
    public float phaseOffset = 0f;      // Độ trễ pha

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.localPosition;
    }

    void Update()
    {
        float yOffset = Mathf.Sin(Time.time * frequency + phaseOffset) * amplitude;
        transform.localPosition = startPos + new Vector3(0, yOffset, 0);
    }
}
