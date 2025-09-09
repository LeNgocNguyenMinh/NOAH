using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AOBoss : MonoBehaviour
{
    public Animator AOBossAnimator { get; set; }
    public AOBossStateMachine StateMachine { get; private set;}
    public AOBossRestState RestState { get; private set; }
    public AOBossAwakeState AwakeState { get; private set; }
    public AOBossDeadState DeadState { get; private set; }
    public AOBossATK1State ATK1State { get; private set; }
    public AOBossATK2State ATK2State { get; private set; }
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
        RestAnimFinish,
        DeadAnimFinish,
        AwakeAnimFinish
    }

    private void Awake()
    {
        StateMachine = new AOBossStateMachine();
        RestState = new AOBossRestState(this, StateMachine);
        AwakeState = new AOBossAwakeState(this, StateMachine);
        DeadState = new AOBossDeadState(this, StateMachine);
        ATK1State = new AOBossATK1State(this, StateMachine);
        ATK2State = new AOBossATK2State(this, StateMachine);   
    }
    private void Start()
    {
        RB = GetComponent<Rigidbody2D>();
        AOBossAnimator = GetComponent<Animator>();
        StateMachine.Initialize(RestState);
    }
    private void Update()
    {
        StateMachine.CurrentState.FrameUpdate();
    }

    public void AnimationTriggerEvent(AnimationTriggerType triggerType)
    {
        StateMachine.CurrentState.AnimationTriggerEvent(triggerType);
    }
}
