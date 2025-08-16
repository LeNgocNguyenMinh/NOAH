using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WandBullet : MonoBehaviour
{
    [SerializeField]private PlayerStatus playerStatus;
    [SerializeField]private GameObject bulletHitParticle;
    [SerializeField]private float bulletSpeed = 20f;//Bullet max Speed
    [SerializeField]private float maxDistance = 3f;//Max distance bullet go before disappear
    [SerializeField]private Color color;
    private Vector3 mousePos;//Bullet Goal
    private Vector3 startPosition;//Bullet Spawn point
    private Rigidbody2D rb;
    private Transform player;
    private Vector3 direction;//Vector way of bullet
    private float damageAmount;//Damage each Bullet

    private void Start()
    {
        startPosition = transform.position;
        rb = GetComponent<Rigidbody2D>();
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        direction = mousePos - player.transform.position;
        rb.velocity = new Vector2(direction.x, direction.y).normalized * bulletSpeed;
    }
    public void Awake()
    {
        player = FindObjectOfType<PlayerControl>().transform;
    }

    private void Update()
    {
        if(Vector3.Distance(startPosition, transform.position)>=maxDistance)
        {
            Destroy(gameObject);
        }
    }
    public void BulletHitParticleDestroy()
    {
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        damageAmount = (Random.Range(0, 5) == 0) ? 0 : (int)Random.Range(playerStatus.playerCurrentDamage, playerStatus.playerCurrentDamage + playerStatus.playerWeaponDamage);
        if(hitInfo.tag == "Boss")
        {
            EnemyReceiveDamage boss = hitInfo.GetComponent<EnemyReceiveDamage>();
            boss.ReceiveDamage(damageAmount + playerStatus.playerWeaponDamage);
            Instantiate(bulletHitParticle, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        else if (hitInfo.tag == "Enemy")
        {
            EnemyHurt enemy = hitInfo.GetComponent<EnemyHurt>();
            enemy.DamageReceive(damageAmount + playerStatus.playerWeaponDamage, direction);//Enemy hurt
            Instantiate(bulletHitParticle, transform.position, Quaternion.identity);
            Destroy(gameObject);//Bullet destroy
        }
        else if(hitInfo.tag == "ForeGround" || hitInfo.tag == "NPC")
        {
            Instantiate(bulletHitParticle, transform.position, Quaternion.identity);
            Destroy(gameObject);//Destroy too because hit solid thing
        }
    }
}
