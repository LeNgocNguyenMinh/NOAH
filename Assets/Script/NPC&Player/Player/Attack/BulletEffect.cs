using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEffect : MonoBehaviour
{
    public void BulletBreak()
    {
        //Destroy bullet effect
        Destroy(gameObject);
    }
}
