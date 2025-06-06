using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MagazineText : MonoBehaviour
{
    public TextMeshProUGUI text;
    public WeaponParent weapon;
    public void UpdateAmmorText(int currentBullet, int magazine)
    {
        text.text = $"{currentBullet}/{magazine}";
    }
}
