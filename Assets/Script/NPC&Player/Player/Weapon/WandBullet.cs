    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class WandBullet : MonoBehaviour
    {
        [SerializeField]private PlayerStatus playerStatus;
        [SerializeField]private GameObject bulletHitParticle;
        private float maxDistance = 15f;//Max distance bullet go before disappear
        private Vector3 startPosition;//Bullet Spawn point
        private float damageAmount;//Damage each Bullet
        private Projectile projectile;
        private Vector3 target;
        private Vector3 direction;
        private bool isDestroy = false;

        private void Start()
        {
            projectile = GetComponent<Projectile>();
            startPosition = transform.position;
            target = projectile.target;
        }

        private void FixedUpdate()
        {
            if((Vector3.Distance(startPosition, transform.position)>=maxDistance || (Vector3.Distance(transform.position, target) <= 0.3f)) && !isDestroy)
            {
                BulletDestroy();
            }
        }
        public void BulletHitParticleDestroy()
        {
            Destroy(gameObject);
        }
        public void BulletDestroy()
        {
            isDestroy = true;
            Instantiate(bulletHitParticle, transform.position, Quaternion.identity);
            Destroy(gameObject);
        } 
        private void OnTriggerEnter2D(Collider2D hitInfo)
        {
            damageAmount = (Random.Range(0, 5) == 0) ? 0 : (int)Random.Range(playerStatus.playerCurrentDamage, playerStatus.playerCurrentDamage + playerStatus.playerWeaponDamage);
            if(hitInfo.tag == "Boss")
            {
                BossHurt bossHurt = hitInfo.GetComponent<BossHurt>();
                bossHurt.DamageReceive(damageAmount + playerStatus.playerWeaponDamage);
                BulletDestroy();
            }
            else if (hitInfo.tag == "Enemy")
            {
                EnemyHurt enemy = hitInfo.GetComponent<EnemyHurt>();
                enemy.DamageReceive(damageAmount + playerStatus.playerWeaponDamage, direction);//Enemy hurt
                BulletDestroy();
            }
            else if(hitInfo.tag == "ForeGround" || hitInfo.tag == "NPC")
            {
                BulletDestroy();
            }
        }
    } 
