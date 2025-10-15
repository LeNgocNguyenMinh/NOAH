using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStep : MonoBehaviour
{
    private float timeBtwSpawn;
    [SerializeField]private float startTimeBtwSpawn;
    [SerializeField]private float destroyTime;
    [SerializeField]private GameObject echo;
    void Update()
    {
        if(Player.Instance.RB.velocity.sqrMagnitude > 0.5f)
        {
            if(timeBtwSpawn <= 0)
            {
                Vector2 dir = Player.Instance.MoveDirect.normalized;
                float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                Quaternion rot = Quaternion.Euler(0, 0, angle);
                GameObject clone = Instantiate(echo, transform.position, rot);
                Destroy(clone, destroyTime);
                timeBtwSpawn = startTimeBtwSpawn;
            }
            else
            {
                timeBtwSpawn -= Time.deltaTime;
            }
        }
    }
}
