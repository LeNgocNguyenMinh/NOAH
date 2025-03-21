using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BB02Control : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector3 startPosition;
    [SerializeField]private GameObject smallBulletPrefab;
    private HealthControl playerHealthControl;
    private float maxDistanceOfBigBullet;
    [SerializeField] private BossStatus bossStatus;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startPosition = transform.position;
        playerHealthControl = GameObject.FindObjectOfType<PlayerControl>().GetComponent<HealthControl>();
    }
    private void Update()
    {
        rb.velocity = Vector2.down * 10f;
        if(Vector3.Distance(startPosition, transform.position)>=maxDistanceOfBigBullet)
        {
            LastSplitBeforeDestroy();
        }
    }
    private void LastSplitBeforeDestroy()
    {
        for (int i = 0; i < 8; i++)
        {
            float angle = i * 45f; // Góc quay tính bằng độ
            float radian = angle * Mathf.Deg2Rad; // Chuyển sang radian

            Vector3 direction = new Vector3(Mathf.Cos(radian), Mathf.Sin(radian));

            SmallBulletControl smallBullet = Instantiate(smallBulletPrefab, transform.position, Quaternion.identity).GetComponent<SmallBulletControl>();
            smallBullet.SetTarget(direction);
        }
        Destroy(gameObject);
    }
    public void SetMaxDistance(float newMaxDistance)
    {
        maxDistanceOfBigBullet = newMaxDistance;
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.CompareTag("PlayerHitCollider"))
        {
           playerHealthControl.PlayerHurt(bossStatus.bossDamage);
           Destroy(gameObject);
        }
        if(collider.gameObject.CompareTag("ForeGround"))
        {
            LastSplitBeforeDestroy();
        }
    }
}
