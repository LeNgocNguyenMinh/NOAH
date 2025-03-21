using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttackStyle3 : MonoBehaviour
{
    [SerializeField]private GameObject heavySwordPrefab;
    [SerializeField]private float timeBetweenAttack;
    [SerializeField] private float circleRadius = 10f;
    [SerializeField]private int maxSword = 9;
    [SerializeField] private Transform atk3ShootTransform;  
    public bool isFinish = false;

    public IEnumerator HitStraightToPlayer(int numberOfSword)
    {
        isFinish = false;
        while(numberOfSword <=maxSword)
        {
            for (int i = 0; i < numberOfSword; i++)
            {
                float segment = 2 * Mathf.PI*i/numberOfSword;
                float horizontalValue = Mathf.Cos(segment);
                float verticalValue = Mathf.Sin(segment);
                Vector3 dirValue = new Vector3(horizontalValue, verticalValue);
                Vector3 worldPos = atk3ShootTransform.position + dirValue * circleRadius;

                Vector3 direction = worldPos - atk3ShootTransform.position;
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                angle += 90;
                Quaternion rotation = Quaternion.Euler(0, 0, angle);
                HeavySwordControl heavySword = Instantiate(heavySwordPrefab, worldPos, rotation).GetComponent<HeavySwordControl>();
                heavySword.SetTarget(atk3ShootTransform.position);
            }
            numberOfSword +=2;
            yield return new WaitForSeconds(2f);
        }
        isFinish = true;
    }
    public bool CheckATKFinish()
    {
        return isFinish;
    }
}
