using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectPlayer : MonoBehaviour
{
    private Transform player;
    [SerializeField]private Transform enemyRenderer;
    [SerializeField]private CRMoveControl crMoveControl;
    private Coroutine shootingCoroutine;
    private Animator crAnimator;
    private void Start()
    {
        crAnimator = GetComponentInParent<Animator>();
    }
    private void Awake()
    {
        player = FindObjectOfType<PlayerControl>().transform;
    }
    private void OnTriggerEnter2D(Collider2D collider)// nếu phát hiện player vào vùng detect thì sau 2s bắn đạn
    {
        if(collider.gameObject.tag=="Player")
        {
            crAnimator.SetBool("isAttack", true);
            shootingCoroutine = StartCoroutine(ShootAtInterval());
        }
    }
    private void OnTriggerStay2D(Collider2D collider)//Xoay enemy đúng hướng vơid player khi player trong vùng detect
    {
        if(collider.gameObject.tag=="Player")
        {
            if(player.position.x > transform.parent.position.x && enemyRenderer.localScale.x < 0)
            {
                enemyRenderer.localScale = new Vector3(enemyRenderer.localScale.x *-1, enemyRenderer.localScale.y, enemyRenderer.localScale.z);
            }
            else if(player.position.x < transform.parent.position.x && enemyRenderer.localScale.x > 0)
            {
                enemyRenderer.localScale = new Vector3(enemyRenderer.localScale.x *-1, enemyRenderer.localScale.y, enemyRenderer.localScale.z);
            }
        }
    }
    private IEnumerator ShootAtInterval()
    {
        while (true)
        {
            float radius = 2.5f; // Bán kính từ player đến các vị trí đạn lan
            int bulletCount = 5; // Số lượng đạn
            crMoveControl.ShootProject(player.position);
            for (int i = 0; i < bulletCount; i++)
            {
                float angle = i * (360f / bulletCount); // Chia đều các góc quanh player
                float angleRad = angle * Mathf.Deg2Rad;
                Vector3 spreadPosition = new Vector3(
                    player.position.x + radius * Mathf.Cos(angleRad),
                    player.position.y + radius * Mathf.Sin(angleRad),
                    player.position.z // 
                );

                crMoveControl.ShootProject(spreadPosition); // Bắn đạn về vị trí lan
            }       
            yield return new WaitForSeconds(2f); // Chờ giây trước khi bắn lần tiếp theo
        }
    }
    private void OnTriggerExit2D(Collider2D collider)//Nếu player ra khởi vùng detect thì dừng việc bắn
    {
        if (collider.gameObject.tag == "Player" && shootingCoroutine != null)
        {
            crAnimator.SetBool("isAttack", false);
            StopCoroutine(shootingCoroutine);
            shootingCoroutine = null; // Đặt về null để sẵn sàng cho lần kích hoạt tiếp theo
        }
    }
}
