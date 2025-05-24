using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavySwordControl : MonoBehaviour
{
    [SerializeField]private GameObject smallSwordObject;
    [SerializeField]private float speed;
    [SerializeField] private BossStatus bossStatus;
    private Animator animator;
    private Rigidbody2D rb;
    private Vector3 direction;
    private bool isHit = false;
    private bool split = false;
    private bool startMove = false;
    private void Update()
    {
        if(direction!=null && !isHit && startMove)
        {
            rb = GetComponent<Rigidbody2D>();
            rb.velocity = new Vector2(direction.x, direction.y).normalized * speed; 
        }
    }
    public void SetStartMove()
    {
        startMove = true;
    }
    public void SetTarget(Vector3 target)
    {
        direction = transform.position - target; 
    }   
    private void LastSplitBeforeDestroy()
    {
        if(split)
        {
            for (int i = 0; i < 8; i++)
            {
                float angle = i * 45f; // Góc quay tính bằng độ
                float radian = angle * Mathf.Deg2Rad; // Chuyển sang radian

                Vector3 des = new Vector3(Mathf.Cos(radian), Mathf.Sin(radian));

                SmallSwordControl smallSword = Instantiate(smallSwordObject, transform.position, Quaternion.identity).GetComponent<SmallSwordControl>();
                smallSword.SetTarget(des);
            }
        }
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.CompareTag("PlayerHitCollider"))
        {
            rb = GetComponent<Rigidbody2D>();
            rb.velocity = Vector3.zero;
            HealthControl.Instance.PlayerHurt(bossStatus.bossDamage);
            split = false;
            animator = GetComponent<Animator>();
            animator.SetTrigger("isHit");
        }
        if(collider.gameObject.CompareTag("ForeGround"))
        {
            rb = GetComponent<Rigidbody2D>();
            rb.velocity = Vector3.zero;
            split = true;
            animator = GetComponent<Animator>();
            animator.SetTrigger("isHit");
        }
    }
}
