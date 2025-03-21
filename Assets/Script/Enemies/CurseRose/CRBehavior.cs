using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CRBehavior : MonoBehaviour
{
    private EnemyHealthControl enemyHealthControl;
    private bool isAlive;
    [SerializeField]private DetectPlayer detectPlayer;
    private Animator crAnimator;
    private DropOnDie dropOnDie;

    private void Start()
    {
        isAlive = true;
        dropOnDie = GetComponent<DropOnDie>();
        enemyHealthControl = GetComponent<EnemyHealthControl>();
        crAnimator = GetComponentInParent<Animator>();
    }
    private void FixedUpdate()
    {
        if (enemyHealthControl.GetCurrentHealth()<=0)
        {
            EnemyDead();
        }
    }
    private void EnemyDead()
    {
        if(isAlive)
        {
            isAlive = false;
            dropOnDie.DropEXP(transform.position);
            crAnimator.SetTrigger("isDead");
        }
    }
    public void Die()
    {
        Destroy(gameObject);
    }
}
