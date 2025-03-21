using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileCurveMove : MonoBehaviour
{
    private HealthControl playerHealthControl;
    [SerializeField]private EnemyStatus enemyStatus;
    private Transform player;
    private Vector3 target; //Lưu vị trí bắn 
    private GameObject tmp;//Biến tạm
    private float baseSpeed = 10f;//Tốc độ bắn cơ bản
    [SerializeField]private GameObject targetLocationPrefab;//Target Mask 
    private Vector3 startPoint; //Điểm bắt đầu bắn (tương đương với enemy.position)
    private float totalDistance; // Tổng khoảng cách giữa điểm bắt đầu và mục tiêu
    private bool targetLocationSpawned = false;
    private float traveledDistance = 0f;// Tính ra khoảng cách từ nơi bắn đến đích

    [SerializeField] private AnimationCurve heightCurve; // Animation Curve để điều chỉnh chiều cao
    [SerializeField] private AnimationCurve speedCurve; // Animation Curve để điều chỉnh tốc độ (nếu cần)
    
    private void Start()
    {
        startPoint = transform.position;
        totalDistance = Vector3.Distance(startPoint, target);
    }

    public void Awake()
    {
        player = FindObjectOfType<PlayerControl>().transform;
        playerHealthControl = player.GetComponent<HealthControl>();
    }
    private void Update()
    {
        float progress = Mathf.Clamp01(traveledDistance / totalDistance); // Tiến trình từ 0 đến 1
        float curveHeight = heightCurve.Evaluate(progress); // Lấy giá trị độ cao từ Animation Curve
        float adjustedSpeed = baseSpeed * speedCurve.Evaluate(progress); // Tốc độ theo Animation Curve 
        traveledDistance += adjustedSpeed * Time.deltaTime; // Tính ra khoảng đã đi được

        // Tính vị trí hiện tại
        Vector3 nextPosition = Vector3.Lerp(startPoint, target, progress);
        nextPosition.y += curveHeight * 2f; // Điều chỉnh độ cao từ Animation Curve

        Vector3 direction = (nextPosition - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Cập nhật vị trí
        transform.position = nextPosition;
        transform.rotation = Quaternion.Euler(0, 0, angle + 90);

        if(progress >= 0.5f && !targetLocationSpawned)//tạo mask điểm rơi của đạn
        {
            tmp = Instantiate(targetLocationPrefab, target, Quaternion.identity);
            targetLocationSpawned = true;
        }
        if (targetLocationSpawned && progress >= 0.5f)// Phần này là code thu nhỏ điểm rơi
        {
            float scale = Mathf.Lerp(0.7f, 0.1f, (progress - 0.5f) / 0.5f); // Nội suy từ 1 đến 0.4 khi progress từ 0.5 đến 1
            tmp.transform.localScale = new Vector3(scale, scale, scale); // Cập nhật kích thước
        }
        if (progress >= 1f) //đã đến đích
        {
            Destroy(tmp);
            Destroy(gameObject);
        }

    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.tag == "PlayerHitCollider")
        {
            playerHealthControl.PlayerHurt(enemyStatus.enemyDamage);
            Destroy(tmp);
            Destroy(gameObject);
        }
    }
    public void SetTarget(Vector3 target)
    {
        this.target = target;
    }
}
