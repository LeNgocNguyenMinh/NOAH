using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallSwordControl : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 swordTarget;
    private Vector3 startPosition;
    private Transform playerTransform;
    private bool changeDirect;
    [SerializeField] private BossStatus bossStatus;
    [SerializeField]private float smallSwordSpeed;
    [SerializeField]private float maxDistanceOfSmallSword;
    private void Start()
    {
        playerTransform = FindObjectOfType<Player>().transform;
        startPosition = transform.position;
        rb = GetComponent<Rigidbody2D>();
        changeDirect = false;
        StartCoroutine(ToPlayer());
    }
    private void Update()
    {
        rb.velocity = swordTarget * smallSwordSpeed;
        if(Vector3.Distance(startPosition, transform.position)>=maxDistanceOfSmallSword)
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
    }
    public void SetTarget(Vector3 target)
    {
        swordTarget = target;
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
