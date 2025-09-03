using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeShotATK1 : MonoBehaviour
{
    [SerializeField] private BossStatus bossStatus;
    private Transform playerTransform;
    private Rigidbody2D rb;
    private Vector3 direct;
    private int maxShootTime;
    private bool isHit = false;
    private int shootCount = 0;
    [SerializeField]private float rotationSpeed;
    [SerializeField]private float smallShotSpeed;
    [SerializeField]private float chargeShotSpeed;
    private Transform[] smallShotTrans;
    [SerializeField]private GameObject smallShot;
    private void Update()
    {
        if(direct == null || isHit) return;
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = direct * chargeShotSpeed; 
    }
    public void SetMaxShootTime(int value)
    {
        maxShootTime = value;
    }
    public void SetSmallShotTransform(Transform[] value)
    {
        smallShotTrans = value;
    }
    public void StartChargeShotAnimation()
    {
        Animator animator = GetComponent<Animator>();
        animator.SetTrigger("chargeATK1");
    }
    public void IdleShotStart()
    {
        InvokeRepeating("SmallShot", 1f, 2f);
    }
    public void SmallShot()
    {
        shootCount ++;
        if(shootCount > maxShootTime) 
        {
            shootCount = 0;
            CancelInvoke("SmallShot");
            Animator headAnimator = FindObjectOfType<FDATK1>().GetComponent<Animator>();
            headAnimator.SetTrigger("headATK1End");
            ShootShot();
            return;
        }
        for(int i = 0; i < 4; i++)
        {
            SmallShotATK1 eachShot = Instantiate(smallShot, smallShotTrans[i].position, Quaternion.identity).GetComponent<SmallShotATK1>();
            eachShot.SetShootSpeed(smallShotSpeed);
        }
    }
    public void ShootShot()
    {
        transform.parent = null;
        Animator animator = GetComponent<Animator>();
        animator.SetTrigger("shotShootATK1");
        playerTransform = FindObjectOfType<Player>().transform;
        direct = (playerTransform.position - transform.position).normalized;
        float angle = Mathf.Atan2(direct.y, direct.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
    }
    public void DestroyChargeShot()
    {
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.tag == "PlayerHitCollider" || collider.tag == "ForeGround")
        {
            if(!isHit)
            {
                rb = GetComponent<Rigidbody2D>();
                rb.velocity = Vector3.zero; 

                if(collider.tag == "PlayerHitCollider")
                {
                    HealthControl.Instance.PlayerHurt(bossStatus.bossDamage);
                }
                
                Animator animator = GetComponent<Animator>();
                animator.SetTrigger("shotBreakATK1");
                isHit = true;
            } 
        }
    }
}
