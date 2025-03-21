using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour
{
    private UIMouseAndPriority uiMouseAndPriority;

    //Normal move
    [SerializeField] private float dashSpeed;
    [SerializeField] private float moveSpeed;
    [SerializeField]private Image dashSkill;
    [SerializeField] private PolygonCollider2D bodyHitCollider;
    private float activeMoveSpeed;
    private float speedX, speedY;
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer mySpriteRender;
    private float dashTimer;
    //Dash move
    private float dashCoolDown = 1f;
    private float dashCoolCounter;
    private bool isAlive;
    private Vector2 moveDirect;
    private void Start()
    {
        uiMouseAndPriority = GameObject.FindObjectOfType<UIMouseAndPriority>().GetComponent<UIMouseAndPriority>();
        Time.timeScale = 1f;
        dashSkill.fillAmount = 0;
        isAlive = true;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        mySpriteRender = GetComponent<SpriteRenderer>();
        activeMoveSpeed = moveSpeed;
    }

    private void Update()
    {
        if (!uiMouseAndPriority.CanOpenThisUI()) return;
        speedX = Input.GetAxisRaw("Horizontal");
        speedY = Input.GetAxisRaw("Vertical");
        moveDirect = new Vector2(speedX, speedY).normalized;
        
        if (!isAlive)
        {
            rb.velocity = Vector2.zero;
            return;
        }

        if (speedX == 0 && speedY == 0)
        {
            animator.SetBool("isWalking", false);
        }
        else
        {
            animator.SetBool("isWalking", true);
        }

        HandleDash();
        rb.velocity = moveDirect * activeMoveSpeed;
    }
    public Vector2 GetMoveDirect()
    {
        return moveDirect;
    }
    private void HandleDash()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (dashCoolCounter <= 0)
            {
                animator.SetBool("isDashing", true);
            }
        }

        if (dashCoolCounter > 0)
        {
            dashCoolCounter -= Time.deltaTime;
            dashSkill.fillAmount = dashCoolCounter / dashCoolDown;
        }
    }

    public void StartDash()
    {
        bodyHitCollider.enabled = false;
        dashCoolCounter = dashCoolDown;
        activeMoveSpeed = dashSpeed;
    }

    public void FinishDash()
    {
        animator.SetBool("isDashing", false);
        bodyHitCollider.enabled = true;
        activeMoveSpeed = moveSpeed;
    }

    public void PlayerDead()
    {
        if (isAlive)
        {
            isAlive = false;
            animator.SetTrigger("isDead");
        }
    }

    public void SetIsAlive(bool newIsAlive)
    {
        isAlive = newIsAlive;
    }
}