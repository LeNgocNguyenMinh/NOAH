using UnityEngine;

    public class WandBullet : MonoBehaviour
    {
        [SerializeField]private GameObject bulletHitParticle;
        [SerializeField]private Rigidbody2D rb;
        private float maxDistance = 15f;//Max distance bullet go before disappear
        private Vector2 startPosition;//Bullet Spawn point
        private float damageAmount;//Damage each Bullet
        /* private Projectile projectile; */
        private Vector2 target;
        private Vector2 direction;
        private bool isDestroy = false;
        private float speed;

        public void SetValue(float speed, Vector2 direction)
        {
            startPosition = transform.position;
            rb.linearVelocity = direction * speed;
            this.speed = speed;
        }

        private void Update()
        {
            if((Vector2.Distance(startPosition, transform.position)>=maxDistance) && !isDestroy && target != null)
            {
                BulletDestroy();
                return;
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
            damageAmount = (Random.Range(0, 5) == 0) ? 0 : (int)Random.Range(PlayerStatus.Instance.playerCurrentDamage, PlayerStatus.Instance.playerCurrentDamage);
            if(hitInfo.tag == "Boss")
            {
                BossHurt bossHurt = hitInfo.GetComponent<BossHurt>();
                bossHurt.DamageReceive(damageAmount);
                BulletDestroy();
            }
            else if (hitInfo.tag == "Enemy")
            {
                EnemyHurt enemy = hitInfo.GetComponent<EnemyHurt>();
                direction = (hitInfo.gameObject.transform.position - Player.Instance.transform.position).normalized;
                enemy.DamageReceive(damageAmount, direction);//Enemy hurt
                BulletDestroy();
            }
            else if(hitInfo.tag == "ForeGround" || hitInfo.tag == "NPC")
            {
                BulletDestroy();
            }
        }
    } 
