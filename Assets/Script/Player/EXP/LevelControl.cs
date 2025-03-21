using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelControl : MonoBehaviour
{
    [SerializeField]private TextMeshProUGUI text;
    private void Start()
    {
        text.text = "1";
    }

    public void UpdateLevelText(int level)
    {
        text.text = $"{level}";
    }
}
