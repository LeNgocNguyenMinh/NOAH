using UnityEngine;
using DG.Tweening;

public class FKBossATK1RHBullet : MonoBehaviour
{
    private Vector3 direct;
    [SerializeField]private Rigidbody2D rb;
    [SerializeField]private GameObject boomerangRoutePre;
    [SerializeField]private Transform bananaSprite;
    private GameObject boomerangRoutePreInstance;
    private BoomerangRoute boomerangRoute;
    private float rotateSpeed;
    private Vector3 startPoint;
    private Vector3[] controlPoints;
    public void SetValue(float flyTime, float rotateSpeed)
    {
        this.rotateSpeed = rotateSpeed;
        startPoint = transform.position;
        direct = (Player.Instance.transform.position - transform.position).normalized;
        float angle = Mathf.Atan2(direct.y, direct.x) * Mathf.Rad2Deg;
        boomerangRoutePreInstance = Instantiate(boomerangRoutePre, Player.Instance.transform.position, Quaternion.Euler(0f, 0f, angle + 90f));
        boomerangRoute = boomerangRoutePreInstance.GetComponent<BoomerangRoute>();
        
        controlPoints = new Vector3[]
        {
            startPoint, // p1 thực sự bắt đầu
            boomerangRoute.lPoint.position,
            boomerangRoute.mPoint.position,
            boomerangRoute.rPoint.position,
            startPoint
        };
        Destroy(boomerangRoutePreInstance);
        transform.DOPath(controlPoints, flyTime, PathType.CatmullRom, PathMode.Ignore)
            .SetEase(Ease.Linear)
            .OnComplete(() =>
            {
                Destroy(gameObject);
            });
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.CompareTag("PlayerHitCollider"))
        {
            PlayerHealthControl.Instance.PlayerHurt(1f);
            Vector3 hitDirect = (Player.Instance.transform.position - transform.position).normalized;
            PlayerEffect.Instance.PushBack(hitDirect);
            PlayerEffect.Instance.HitFlash();
            Destroy(gameObject);
        }
    }
    private void Update()
    {
        bananaSprite.Rotate(0f, 0f, rotateSpeed * Time.deltaTime);
    }
}
