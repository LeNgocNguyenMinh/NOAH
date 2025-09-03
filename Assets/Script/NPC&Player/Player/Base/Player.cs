using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour, IPlayerMoveable
{
    public Vector3 MousePos { get; set; }
    public bool CanChangeState { get; set; }
    public Animator PlayerAnimator { get; set; }
    [SerializeField] private Transform SpriteTransform;
    public PlayerStateMachine StateMachine { get; private set;}
    public PlayerIdleState IdleState { get; private set; }
    public PlayerWalkState WalkState { get; private set; }
    public PlayerDeadState DeadState { get; private set; }
    public PlayerDashState DashState { get; private set; }
    public Rigidbody2D RB { get; set; }
    [field: SerializeField]public float WalkSpeed { get; set; } 
    [field: SerializeField]public float DashSpeed { get; set; }
    public bool IsFacingRight { get; set; }
    public float SpeedX { get; set; }
    public float SpeedY { get; set; }
    public Vector2 MoveDirect { get; set; }
    [field: SerializeField]public float DashCoolDown { get; set; } = 0f;
    public float DashCoolCounter { get; set; } = 0f;

    public enum AnimationTriggerType
    {
        WalkAnimFinish,
        DeadAnimFinish,
        IdleAnimFinish,
        DashAnimFinish,
    }

    private void Awake()
    {
        StateMachine = new PlayerStateMachine();
        IdleState = new PlayerIdleState(this, StateMachine);
        WalkState = new PlayerWalkState(this, StateMachine);
        DeadState = new PlayerDeadState(this, StateMachine);
        DashState = new PlayerDashState(this, StateMachine);
    }
    private void Start()
    {
        RB = GetComponent<Rigidbody2D>();
        PlayerAnimator = GetComponent<Animator>();
        StateMachine.Initialize(IdleState);
    }
    private void Update()
    {
        StateMachine.CurrentState.FrameUpdate();
    }
    public void CheckDashButton()
    {
        if(Input.GetKeyDown(KeyCode.LeftShift) && DashCoolCounter <= 0f)
        {
            StateMachine.ChangeState(DashState);
        }
    } 
    public void CheckDashCoolDown()
    {
        if(DashCoolCounter > 0f)
        {
            DashCoolCounter -= Time.deltaTime;
        }
        else
        {
            DashCoolCounter = 0f;
        }
    }
    public void PlayerDead()
    {
        Debug.Log("Player Dead:--- "  + StateMachine.CurrentState.GetType().Name);
        if(StateMachine.CurrentState.GetType().Name != "PlayerDeadState") { 
            StateMachine.ChangeState(DeadState);
        }
        
    }
    public Vector2 GetMoveDirect()
    {
        SpeedX = Input.GetAxisRaw("Horizontal");
        SpeedY = Input.GetAxisRaw("Vertical");
        MoveDirect = new Vector2(SpeedX, SpeedY).normalized;
        return MoveDirect;
    }
    public void FacingDirection()
    {
        MousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (MousePos.x < SpriteTransform.position.x && SpriteTransform.localScale.x > 0) {
            SpriteTransform.localScale = new Vector3(SpriteTransform.localScale.x *-1, SpriteTransform.localScale.y, SpriteTransform.localScale.z);
        } else if (MousePos.x > SpriteTransform.position.x && SpriteTransform.localScale.x < 0) 
        {
            SpriteTransform.localScale = new Vector3(SpriteTransform.localScale.x *-1, SpriteTransform.localScale.y, SpriteTransform.localScale.z);
        }
    }
    public void AnimationTriggerEvent(AnimationTriggerType triggerType)
    {
        StateMachine.CurrentState.AnimationTriggerEvent(triggerType);
    }
}
