using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MagazineText : MonoBehaviour
{
    public TextMeshProUGUI text;
    public WeaponParent weapon;
    void Start()
    {
        UpdateAmmorText();
    }
    public void UpdateAmmorText()
    {
        text.text = $"{weapon.currentBullet} / {weapon.magazine}";
    }
}
