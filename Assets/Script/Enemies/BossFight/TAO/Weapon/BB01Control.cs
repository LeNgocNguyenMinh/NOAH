using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BB01Control : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector3 thisTarget;
    private Vector3 startPosition;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private BossStatus bossStatus;
    [SerializeField] private GameObject smallBulletPrefab;
    private float maxDistanceOfBigBullet;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startPosition = transform.position;
    }
    private void Update()
    {
        float angle = Mathf.Atan2(thisTarget.y, thisTarget.x) * Mathf.Rad2Deg;
        angle += 90f;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
        rb.velocity = thisTarget * bulletSpeed;
        if(Vector3.Distance(startPosition, transform.position)>=maxDistanceOfBigBullet)
        {
            Destroy(gameObject);
        }
    }
    public void SetTarget(Vector3 target)
    {
        thisTarget = target;
    }
    public void SetMaxDistance(float newMaxDistance)
    {
        maxDistanceOfBigBullet = newMaxDistance;
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.CompareTag("PlayerHitCollider"))
        {
           HealthControl.Instance.PlayerHurt(bossStatus.bossDamage);
            Destroy(gameObject);
        }
        if(collider.gameObject.CompareTag("ForeGround"))
        {
            Destroy(gameObject);
        }
    }
}
