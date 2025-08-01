using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAOShoot : MonoBehaviour
{
    [SerializeField]private Transform style1BAF;
    [SerializeField]private Transform style2BAF;
    [SerializeField]private Transform bossBodyTransform;
    [SerializeField]private Rigidbody2D bossBodyRB;
    [SerializeField]private MovementStyle2 movementStyle2;
    [SerializeField]private BossAODropAfterDead bossDropAfterDead;
    private Animator animator;
    private BossAttackStyle1 bossAttackStyle1;
    private BossAttackStyle2 bossAttackStyle2;
    private BossAttackStyle3 bossAttackStyle3;
    private bool isSummon;
    public bool isAlive;

    public void StartAttack()
    {
        isSummon = false;
        isAlive = true;
        animator = GetComponent<Animator>();
        bossAttackStyle1 = GetComponent<BossAttackStyle1>();
        bossAttackStyle2 = GetComponent<BossAttackStyle2>();
        bossAttackStyle3 = GetComponent<BossAttackStyle3>();
        movementStyle2.enabled = false;
        StartCoroutine(AttackSequence());
    }
    public void BossDeath()
    {
        if(isAlive)
        {
            StopAllCoroutines();
            isAlive = false;
            bossDropAfterDead.DropOEC(bossBodyTransform.position);
            animator.SetTrigger("isDead");
        }
    }
    public void BossSummon()
    {
        isSummon = true;
    }
    private IEnumerator AttackSequence()
    {
        while (true)
        {
            // Kiểu tấn công 1
            movementStyle2.enabled = false;
            animator.SetTrigger("isIdle");
            yield return MoveToPosition(style1BAF);
            animator.SetTrigger("ATK1Begin");
            animator.SetTrigger("ATK1Shoot");
            while(!bossAttackStyle1.CheckATKFinish())
            {
                yield return null;
            }
            animator.SetTrigger("ATK1End");

            // Kiểu tấn công 2
            animator.SetTrigger("isIdle");
            yield return MoveToPosition(style2BAF);
            animator.SetTrigger("ATK2Begin");
            animator.SetTrigger("ATK2Shoot");
            movementStyle2.enabled = true;
            while(!bossAttackStyle2.CheckATKFinish())
            {
                yield return null;
            }
            movementStyle2.enabled = false;
            animator.SetTrigger("ATK2End");

            // Kiểu tấn công 3
            animator.SetTrigger("isIdle");
            yield return MoveToPosition(style1BAF);
            animator.SetTrigger("ATK3Begin");
            StartCoroutine(bossAttackStyle3.HitStraightToPlayer(5));
            while(!bossAttackStyle3.CheckATKFinish())
            {
                yield return null;
            }
            animator.SetTrigger("ATK3End");
        }
    }
    private IEnumerator MoveToPosition(Transform finalPosition)
    {
        float moveSpeed = 10f; // Tốc độ di chuyển

        while (Vector3.Distance(bossBodyTransform.position, finalPosition.position) > 0.5f)
        {
            Vector2 direction = (finalPosition.position - bossBodyTransform.position).normalized;
            bossBodyRB.MovePosition(bossBodyRB.position + direction * moveSpeed * Time.deltaTime);
            yield return null; // Đợi 1 frame
        }
        bossBodyRB.velocity = Vector3.zero;
    }
}
