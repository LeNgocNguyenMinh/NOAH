using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FDATK1 : MonoBehaviour
{
    private Transform playerTransform;
    private Vector3 direction;
    //Head Control so alway toward to player
    public bool startATK1 = false;
    [SerializeField]private GameObject head;
    [SerializeField]private int maxShootTime;
    [SerializeField]private GameObject chargeShot;
    [SerializeField]private Transform chargeShotTransform;
    [SerializeField]private Transform[] smallShotTransform;
    [SerializeField]private FDATK2 fdATK2;
    [SerializeField]private LineRenderer atk1Line;
    [SerializeField]private float rotationSpeed;
    [SerializeField]private float moveSpeed;
    private Vector3[] positions;
    private int atk1LineIndex = 0;
    private bool atk1Ready = false;
    public bool atk1LineFinish = false;
    private Animator animator;

    private void Update()
    {
        //If ATK1 not start yet
        if(!startATK1)
        {
            atk1LineIndex = 0;
            return;
        }
        if(!atk1LineFinish)
        {
            ATK1Move();
            return;
        }
        HeadRotate();
    }
    private void ATK1Prepare()
    {
        positions = new Vector3[atk1Line.positionCount];
        atk1Line.GetPositions(positions);
        for (int i = 0; i < positions.Length; i++)
        {
            positions[i].z = 0;
        }
        head.transform.position = positions[0]; 
        atk1Ready = true;
    }
    private void ATK1Move()
    {
        if(!atk1Ready)
        {
            ATK1Prepare();
        }
        Vector3 targetPosition = positions[atk1LineIndex];
        direction = targetPosition - head.transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        head.transform.rotation = Quaternion.Slerp(head.transform.rotation, rotation, rotationSpeed * Time.deltaTime);
        head.transform.position = Vector3.MoveTowards(head.transform.position, targetPosition, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(head.transform.position, targetPosition) < 0.01f)
        {
            atk1LineIndex++; 

            if (atk1LineIndex >= positions.Length)
            {
                atk1LineFinish = true;
                animator = GetComponent<Animator>();
                animator.SetTrigger("headATK1Start");
            }
        }
    }
    private void HeadRotate()
    {
        playerTransform = FindObjectOfType<PlayerControl>().transform;
        direction = playerTransform.position - head.transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        head.transform.rotation = Quaternion.Slerp(head.transform.rotation, rotation, rotationSpeed * Time.deltaTime); 
    }
    public void ChargeShotInstantiate()
    {
        GameObject shot = Instantiate(chargeShot, chargeShotTransform.position, chargeShotTransform.rotation, chargeShotTransform);
        ChargeShotATK1 chargeShotATK1 = shot.GetComponent<ChargeShotATK1>();
        chargeShotATK1.SetMaxShootTime(maxShootTime);
        chargeShotATK1.SetSmallShotTransform(smallShotTransform);
        chargeShotATK1.StartChargeShotAnimation();
    }
    public void StartMoveToATK2StartPoint()
    {
        startATK1 = false;
        atk1LineFinish = false;
        fdATK2.startATK2 = true;
    }
}
