using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletParticleManager : MonoBehaviour
{
    public void BulletHitParticleDestroy()
    {
        Destroy(gameObject);
    }
}
