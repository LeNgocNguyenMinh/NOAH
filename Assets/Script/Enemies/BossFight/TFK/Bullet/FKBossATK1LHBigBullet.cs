using UnityEngine;

public class FKBossATK1LHBigBullet : MonoBehaviour
{
    private Vector3 direct;
    private float wmSpeed;
    private float wmpSpeed;
    [SerializeField]private Animator animator;
    [SerializeField]private Rigidbody2D rb;
    [SerializeField]private GameObject wmPiece;
    [SerializeField]private Transform wmSprite;
    private float wmFlyTime;
    private float wmpFlyDist;
    private bool breaking = false;
    private float damage;
    private bool isShoot = false;
    private float rotateSpeed;
    

    public void SetValue(float wmSpeed,float wmFlyDist, float wmpSpeed, float wmpFlyDist, float damage, float rotateSpeed)
    {
        this.wmSpeed = wmSpeed;
        this.wmpSpeed = wmpSpeed;
        this.wmpFlyDist = wmpFlyDist;
        this.damage = damage;
        this.rotateSpeed = rotateSpeed;
        wmFlyTime = wmFlyDist / wmSpeed;
        breaking = false;
        Shoot();
    }
    public void FixedUpdate()
    {
        if(!isShoot) return;
    
        if(!breaking)
        {
            if(wmFlyTime <= 0f)
            {
                breaking = true;
                animator.SetTrigger("Break");  
            }
            else{
                wmFlyTime -= Time.deltaTime;
                wmSprite.Rotate(0f, 0f, rotateSpeed * Time.deltaTime);
                rb.linearVelocity = direct * wmSpeed ;
            }
        } 
        else{
            rb.linearVelocity = Vector2.zero;
        }

    }
    public void Shoot()
    {
        direct = (Player.Instance.transform.position - transform.position).normalized;
        isShoot = true;
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.CompareTag("PlayerHitCollider") && !breaking)
        {
            breaking = true;
            HealthControl.Instance.PlayerHurt(damage);
            PlayerEffect.Instance.PushBack(direct);
            PlayerEffect.Instance.HitFlash();
            animator.SetTrigger("Break");
        }
    }
    public void WaterMelonSplit()
    {
        for (int i = 0; i < 3; i++)
        {
            float randAngle = Random.Range(0, 360f);
            Vector3 direct = new Vector3(Mathf.Cos(randAngle * Mathf.Deg2Rad), Mathf.Sin(randAngle * Mathf.Deg2Rad), 0).normalized;
            Instantiate(wmPiece, transform.position, Quaternion.identity).GetComponent<FKBossATK1LHSmallBullet>().SetValue(wmpSpeed, wmpFlyDist, damage/3f, direct, rotateSpeed);
        }
    }
    public void DesTroyObject()
    {
        Destroy(gameObject);
    }
}
