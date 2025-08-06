using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCurrentClothChange : MonoBehaviour
{
    private ClothAnimationOverrider clothAnimationOverrider;
    public void ChangeCloth(Item newCloth)
    {
        clothAnimationOverrider = GetComponent<ClothAnimationOverrider>();
        clothAnimationOverrider.EquipAnimation(newCloth.itemID);
    }
}
