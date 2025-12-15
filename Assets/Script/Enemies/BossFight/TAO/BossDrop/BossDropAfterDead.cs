using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDropAfterDead : MonoBehaviour
{
    [SerializeField]private GameObject weaponDrop;
    [SerializeField]private GameObject gemDrop;
    [SerializeField]private GameObject expPrefab;
    [SerializeField]private GameObject coinPrefab;
    [SerializeField]private float maxSpeed;
    [SerializeField]private float maxHeight;
    [SerializeField]private int numDropExp;
    [SerializeField]private int numDropCoin;
    public void DropOnDead()
    {
        Vector3 gemOffset = new Vector3(2, 0, 2);
        if(gemDrop != null)
        {
            Instantiate(gemDrop, transform.position + gemOffset, Quaternion.identity);
        }
        if(weaponDrop != null)
        {
            Instantiate(weaponDrop, transform.position, Quaternion.identity);
        }
        for (int i =0; i < numDropExp; i++)
        {
            float xE = (Random.value < 0.5f) ? Random.Range(2f, 3f) : Random.Range(-3f, -2f);
            float yE = (Random.value < 0.5f) ? Random.Range(1f, 2f) : Random.Range(-2f, -1f);
            Vector3 offsetE = new Vector3(xE, yE, 0f);
            Projectile EXPPro = Instantiate(expPrefab, transform.position, Quaternion.identity).GetComponent<Projectile>();
            EXPPro.InitializeProjectile(transform.position + offsetE, maxSpeed, maxHeight);
        }
        for (int i =0; i < numDropCoin; i++)
        {
            float xC = (Random.value < 0.5f) ? Random.Range(2f, 3f) : Random.Range(-3f, -2f);
            float yC = (Random.value < 0.5f) ? Random.Range(1f, 2f) : Random.Range(-2f, -1f);
            Vector3 offsetC = new Vector3(xC, yC, 0f);
            Projectile coinPro = Instantiate(coinPrefab, transform.position, Quaternion.identity).GetComponent<Projectile>();
            coinPro.InitializeProjectile(transform.position + offsetC, maxSpeed, maxHeight);
        }
    }
}
