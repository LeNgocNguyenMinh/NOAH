using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FDATK2 : MonoBehaviour
{
    [SerializeField]private LineRenderer atk2Line;
    [SerializeField]private float rotationSpeed;
    [SerializeField]private float moveSpeed;
    [SerializeField]private GameObject head;
    [SerializeField]private GameObject topHead;
    [SerializeField]private GameObject topHand;
    [SerializeField]private GameObject summonCircleList;
    [SerializeField]private FDATK3 fdATK3;
    [SerializeField]private Transform[] spawnPointTransform;
    [SerializeField]private GameObject bombDemonPrefab;
    [SerializeField]private int maxSpawnTime;
    private int spawnCount = 0;
    public bool canSpawnBombDemon = false;
    private Vector3[] positions;
    private Vector3 direction;
    private int atk2LineIndex = 1;
    private bool atk2Ready = false;
    public bool startATK2 = false;
    private bool atk2LineFinish = false;
    private Animator animator;
    void Update()
    {
        if(!startATK2)
        {
            atk2LineIndex = 0;
            return;
        }        
        if(!atk2LineFinish)
        {
            MoveToATK2();
        }
    }
    private void PrepareATK2()
    {
        positions = new Vector3[atk2Line.positionCount];
        atk2Line.GetPositions(positions);
        for (int i = 0; i < positions.Length; i++)
        {
            positions[i].z = 0;
        }
        atk2Ready = true;
    }
    private void MoveToATK2()
    {
        if(!atk2Ready)
        {
            PrepareATK2();
        }
        Vector3 targetPosition = positions[atk2LineIndex];
        direction = targetPosition - head.transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        head.transform.rotation = Quaternion.Slerp(head.transform.rotation, rotation, rotationSpeed * Time.deltaTime);
        head.transform.position = Vector3.MoveTowards(head.transform.position, targetPosition, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(head.transform.position, targetPosition) < 0.01f)
        {
            atk2LineIndex++; 
            if (atk2LineIndex >= positions.Length)
            {
                atk2LineFinish = true;
                topHead.SetActive(true);
                topHand.SetActive(true);
                summonCircleList.SetActive(true);
                animator = GetComponent<Animator>();
                animator.SetTrigger("atk2Start");
            }
        }
    }
    public void DeactiveSummonCircle()
    {
        summonCircleList.SetActive(false);
    }
    public void StartSummonEnemies()
    {
        canSpawnBombDemon = true;
        InvokeRepeating("BombDemonSummon", 0f, 2f);
    }
    public void StartATK3()
    {
        fdATK3.StartSpawn();
    }
    private void BombDemonSummon()
    {
        spawnCount ++;
        if(spawnCount > maxSpawnTime)
        {
            CancelInvoke("BombDemonSummon");
            spawnCount = 0;
            animator.SetTrigger("atk2End");
            startATK2 = false;
            atk2LineFinish = false;
            return;
        }
        for(int i = 0; i < spawnPointTransform.Length; i++)
        {
            Instantiate(bombDemonPrefab, spawnPointTransform[i].position, Quaternion.identity);
        }
    } 
}
