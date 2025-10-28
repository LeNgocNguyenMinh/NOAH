using System.Collections;
using UnityEngine;

public class FKBossATK2LHRoute : MonoBehaviour
{
    private Vector3 direct;
    private float routeLength;
    private float disBet;
    private Vector3 pointA;
    private Vector3 currentSpawnPos;
    private float disFromA = 0;
    private float delay;
    [SerializeField]private GameObject carrot;
    private Vector3 sizeChange;
    public void SetValue(Vector3 pointA, float delay, float disBet)
    {
        this.pointA = pointA;
        this.delay = delay;
        this.disBet = disBet;
        sizeChange = new Vector3(0f, 0f, 0f);
        direct = (Player.Instance.transform.position - pointA).normalized;
        routeLength = Vector3.Distance(Player.Instance.transform.position, pointA);
        currentSpawnPos = pointA;
        StartCoroutine(Spawn());
    }
    public IEnumerator Spawn()
    {

        while( disFromA < routeLength)
        {
            GameObject tmp = Instantiate(carrot, currentSpawnPos, Quaternion.identity);
            yield return new WaitForSeconds(delay);
            currentSpawnPos += direct * disBet;
            disFromA = Vector3.Distance(currentSpawnPos, pointA);
        }
        Destroy(gameObject);
    } 
}
