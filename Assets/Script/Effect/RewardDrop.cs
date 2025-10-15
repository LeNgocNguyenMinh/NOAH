using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardDrop : MonoBehaviour
{
    public GameObject coin;
    public GameObject exp;
    public float maxSpeed;
    public float maxHeight;
    public enum TestType {
        Coin,
        EXP,
        Both
    }
    public TestType testType;
    public void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            DropReward();
        }
    }
    public void DropReward()
    {
        int num = 4;
        for(int i = 0; i < num; i++)
        {
            if(testType == TestType.Coin || testType == TestType.Both) 
            {
                
                float xC = (Random.value < 0.5f) ? Random.Range(2f, 3f) : Random.Range(-3f, -2f);
                float yC = (Random.value < 0.5f) ? Random.Range(1f, 2f) : Random.Range(-2f, -1f);
                Vector3 offsetC = new Vector3(xC, yC, 0f);
                Projectile coinPro = Instantiate(coin, transform.position, Quaternion.identity).GetComponent<Projectile>();
                coinPro.InitializeProjectile(transform.position + offsetC, maxSpeed, maxHeight);
            }
            if(testType == TestType.EXP || testType == TestType.Both) 
            {
                float xE = (Random.value < 0.5f) ? Random.Range(2f, 3f) : Random.Range(-3f, -2f);
                float yE = (Random.value < 0.5f) ? Random.Range(1f, 2f) : Random.Range(-2f, -1f);
                Vector3 offsetE = new Vector3(xE, yE, 0f);
                Projectile EXPPro = Instantiate(exp, transform.position, Quaternion.identity).GetComponent<Projectile>();
                EXPPro.InitializeProjectile(transform.position + offsetE, maxSpeed, maxHeight);
            }
        }
    }
}
