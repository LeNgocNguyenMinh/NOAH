using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FDATK3 : MonoBehaviour
{
    [SerializeField]private GameObject topHead;
    [SerializeField]private GameObject topHand;
    [SerializeField]private GameObject groundATK;
    [SerializeField]private float delayBetweenTentacleSpawn;
    [SerializeField]private float delayBetweenSpawn;
    [SerializeField]private int maxSpawnNumber;
    [SerializeField]private int maxSpawnTime;
    [SerializeField]private FDATK1 fdATK1;
    [SerializeField]private float spawnRadius = 5f; 
    private int spawnTimeCount = 0; 
    private Transform playerTransform;
    private Player playerControl;
    private Vector3 spawnPosition;
    ////////////////
    private Vector3[] positions;
    public bool startATK3;
    private List<Vector3> spawnPoints = new List<Vector3>();
    private Animator animator;
    public void StartSpawn()
    {
        InvokeRepeating("CreateSpawnPoints", 1f, delayBetweenSpawn);
    }
    private void CreateSpawnPoints()
    {
        spawnTimeCount ++;
        if(spawnTimeCount > maxSpawnTime)
        {
            spawnTimeCount = 0;
            animator = GetComponent<Animator>();
            animator.SetTrigger("atk3End");
            CancelInvoke("CreateSpawnPoints");
            return;
        }
        playerControl = FindObjectOfType<Player>().GetComponent<Player>();
        Transform playerTrans = playerControl.transform;
        spawnPoints.Clear();
        spawnPoints.Add(playerTrans.position);
        for (int i = 1; i < 6; i++)
        {
            Vector2 randomPoint = Random.insideUnitCircle * spawnRadius;
            Vector3 spawnPos = playerTrans.position + new Vector3(randomPoint.x, randomPoint.y, 0);
            spawnPoints.Add(spawnPos);
        }
        StartCoroutine(Spawn());
    }
    private IEnumerator Spawn()
    {
        foreach(Vector3 pos in spawnPoints)
        {
            Instantiate(groundATK, pos, Quaternion.identity);
            yield return new WaitForSeconds(delayBetweenTentacleSpawn);
        }
    }
    public void StartATK1()
    {
        fdATK1.startATK1 = true;
    }
    public void DeactiveTopHead()
    {
        topHead.SetActive(false);
    }
    public void DeactiveTopHand()
    {
        topHand.SetActive(false);
    }
}
