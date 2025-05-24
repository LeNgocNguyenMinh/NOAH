using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallBulletControl : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 bulletTarget;
    private Vector3 startPosition;
    private Transform playerTransform;
    private bool changeDirect;
    [SerializeField]private float smallBulletSpeed;
    [SerializeField]private float maxDistanceOfSmallBullet;
    [SerializeField] private BossStatus bossStatus;
    private void Start()
    {
        playerTransform = FindObjectOfType<PlayerControl>().transform;
        startPosition = transform.position;
        rb = GetComponent<Rigidbody2D>();
        changeDirect = false;
        StartCoroutine(ToPlayer());
    }
    private void Update()
    {
        rb.velocity = bulletTarget * smallBulletSpeed;
        if(Vector3.Distance(startPosition, transform.position)>=maxDistanceOfSmallBullet)
        {
            Destroy(gameObject);
        }
    }
    private IEnumerator ToPlayer()
    {
        yield return new WaitForSeconds(0.5f);
        if(!changeDirect)
        {
            changeDirect = true;
            startPosition = transform.position;
            SetTarget((playerTransform.position - transform.position).normalized);
        }
        /* yield return new WaitForSeconds(2f); */
    }
    public void SetTarget(Vector3 target)
    {
        bulletTarget = target;
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.CompareTag("PlayerHitCollider"))
        {
           HealthControl.Instance.PlayerHurt(bossStatus.bossDamage/2);
           Destroy(gameObject);
        }
        if(collider.gameObject.tag == "ForeGround")
        {
            Destroy(gameObject);
        }
    }
}
