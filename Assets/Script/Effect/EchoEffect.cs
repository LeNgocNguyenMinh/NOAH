using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EchoEffect : MonoBehaviour
{
    private float timeBtwSpawn;
    public float startTimeBtwSpawn;
    public GameObject echo;
    [SerializeField]private float duration;
    [SerializeField]private Color color;
    private void Start()
    {
        echo.GetComponent<SpriteRenderer>().color = color;
    }
    void Update()
    {
        if(timeBtwSpawn <= 0)
        {
            GameObject clone = Instantiate(echo, transform.position, Quaternion.identity);
            Destroy(clone, duration);
            timeBtwSpawn = startTimeBtwSpawn;
        }
        else
        {
            timeBtwSpawn -= Time.deltaTime;
        }
    }
}
