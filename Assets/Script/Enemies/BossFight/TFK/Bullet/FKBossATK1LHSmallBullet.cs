using UnityEngine;

public class FKBossATK1LHSmallBullet : MonoBehaviour
{
    private Vector3 direct;
    private float wmpSpeed;
    [SerializeField]private Animator animator;
    [SerializeField]private Rigidbody2D rb;
    private float wmpFlyTime;
    private bool breaking = false;
    private float damage;
    private bool isShoot = false;
    private float rotateSpeed;    

    public void SetValue(float wmpSpeed, float wmpFlyDist, float damage, Vector3 direct, float rotateSpeed)
    {
        this.wmpSpeed = wmpSpeed;
        this.damage = damage;
        this.direct = direct;
        this.rotateSpeed = rotateSpeed;
        breaking = false;
        wmpFlyTime = wmpFlyDist / wmpSpeed;
        Shoot();
    }
    public void Update()
    {
        if(!isShoot) return;
    
        if(!breaking)
        {
            if(wmpFlyTime <= 0f)
            {
                breaking = true;
                wmpFlyTime = 0f;
                animator.SetTrigger("Break");  
                rb.linearVelocity = Vector2.zero;
            }
            else{
                wmpFlyTime -= Time.deltaTime;
                float angle = Mathf.Atan2(direct.y, direct.x) * Mathf.Rad2Deg;
                transform.Rotate(0f, 0f, rotateSpeed * Time.deltaTime);
                rb.linearVelocity = direct * wmpSpeed ;
            }
        } 
        else{
            rb.linearVelocity = Vector2.zero;
        }

    }
    public void Shoot()
    {
        isShoot = true;
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
    }
    public void DesTroyObject()
    {
        Destroy(gameObject);
    }
}
