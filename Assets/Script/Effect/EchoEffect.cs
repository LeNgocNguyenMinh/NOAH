using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EchoEffect : MonoBehaviour
{
    private float timeBtwSpawn;
    [SerializeField]private float startTimeBtwSpawn;
    [SerializeField]private GameObject echo;
    [SerializeField]private float duration;
    [SerializeField]private Color color;
    void Update()
    {
        if(timeBtwSpawn <= 0)
        {
            GameObject clone = Instantiate(echo, transform.position, Quaternion.identity);
            clone.GetComponent<SpriteRenderer>().color = color;
            Destroy(clone, duration);
            timeBtwSpawn = startTimeBtwSpawn;
        }
        else
        {
            timeBtwSpawn -= Time.deltaTime;
        }
    }
}
