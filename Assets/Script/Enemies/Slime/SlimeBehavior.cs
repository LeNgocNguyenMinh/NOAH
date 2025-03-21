using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeBehavior : MonoBehaviour//-------------for Slime Enemy and every Enemies the same
{
    private EnemyHealthControl enemyHealthControl;
    [SerializeField]private EnemyStatus enemyStatus;
    private Transform player;
    [SerializeField]private GameObject enemySprite;
    private HealthControl playerHealthControl;
    private Rigidbody2D rb;
    [SerializeField]private float speed;
    private float changeDirectionCoolDown=0f;
    [SerializeField]private float maxDistance;
    private DropOnDie dropOnDie;
    private EnemySpawn enemySpawn;
    private Vector2 wayPoint;
    private Animator animator;
    private bool isAlive;   
    private bool isAttack;
    private bool isFollow;
    private Vector2 directionToWaypoint;
    private float distanceToPlayer;

    private void Start()
    {
        enemyHealthControl = GetComponent<EnemyHealthControl>();
        dropOnDie = GetComponent<DropOnDie>();
        rb = GetComponent<Rigidbody2D>();
        isAlive = true;
        animator = GetComponent<Animator>();
        enemySpawn = FindObjectOfType<EnemySpawn>();   
        changeDirectionCoolDown = 0;
    }
    public void Awake()
    {
        player = FindObjectOfType<PlayerControl>().transform;
        playerHealthControl = player.GetComponent<HealthControl>();
    }
    
    public void OnLoadGameRunTime()
    {
        player = FindObjectOfType<PlayerControl>().transform;
    }
    private void FixedUpdate()
    {
        if(player == null) return;
        
        if (enemyHealthControl.GetCurrentHealth()<=0)
        {
            EnemyDead();
        }
        distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);
        
        if(!isAlive)
        {
            rb.velocity = Vector2.zero;
            return;
        }  
        Move();
        Follow();
        Attack();
    }
    public void EnemyDead()
    {
        if(isAlive)
        {
            isAlive = false;
            dropOnDie.DropEXP(transform.position);
            enemySpawn.enemyNumberOnField--;
            animator.SetTrigger("isDead");
        }
    }
    private void Move()
    {
        changeDirectionCoolDown -= Time.deltaTime;          
        if(changeDirectionCoolDown<=0)
        {
            animator.SetBool("isWalk", true);
            changeDirectionCoolDown = Random.Range(3f, 7f);
            SetNewDestination();
            directionToWaypoint = (wayPoint - (Vector2)transform.position).normalized;
            FacingDirection();
            rb.velocity = directionToWaypoint * speed;
        }
    } 
    private void Follow()
    {
        if(2f<distanceToPlayer && distanceToPlayer<=4f)
        {
            animator.SetBool("isWalk", true);
            Vector2 directionToPlayer = (player.position - transform.position).normalized;
            FollowDirection();
            rb.velocity = directionToPlayer * speed; 
        }
    }
    private void Attack()
    {
        if(distanceToPlayer <= 2f && !isAttack)
        {
            isAttack = true;
            FollowDirection();
            rb.velocity = Vector2.zero;
            StartCoroutine(AttackAnimation());
        }
    }

    private IEnumerator AttackAnimation()
    {
        animator.SetTrigger("isAttack");
        yield return new WaitForSeconds(1.5f);
        isAttack = false;
        yield return new WaitForSeconds(2f);
    }
    public void OnPlayerTakeDamage()
    {
        float distanceToTakedamage = Vector2.Distance(transform.position, player.transform.position);
        if(distanceToTakedamage <=2f)
        {
            playerHealthControl.PlayerHurt(enemyStatus.enemyDamage);
        } 
    } 
    private void SetNewDestination()
    {
        wayPoint = new Vector2(Random.Range(-maxDistance, maxDistance), Random.Range(-maxDistance, maxDistance));
    }
    public void Die()
    {
        Destroy(gameObject);
    }
    private void FacingDirection()
    {
        if (wayPoint.x < transform.position.x && enemySprite.transform.localScale.x > 0) {
            enemySprite.transform.localScale = new Vector3(enemySprite.transform.localScale.x *-1, enemySprite.transform.localScale.y, enemySprite.transform.localScale.z);
        } else if (wayPoint.x > transform.position.x && enemySprite.transform.localScale.x < 0) 
        {
            enemySprite.transform.localScale = new Vector3(enemySprite.transform.localScale.x *-1, enemySprite.transform.localScale.y, enemySprite.transform.localScale.z);
        }
    }
    private void FollowDirection()
    {
        if (player.position.x < transform.position.x && enemySprite.transform.localScale.x>0) {
            enemySprite.transform.localScale = new Vector3(enemySprite.transform.localScale.x *-1, enemySprite.transform.localScale.y, enemySprite.transform.localScale.z);
        } else if (player.position.x > transform.position.x && enemySprite.transform.localScale.x<0) 
        {
            enemySprite.transform.localScale = new Vector3(enemySprite.transform.localScale.x *-1, enemySprite.transform.localScale.y, enemySprite.transform.localScale.z);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ForeGround") || collision.gameObject.CompareTag("Slime"))
        {
            wayPoint *=-1;
        }
    }
}
