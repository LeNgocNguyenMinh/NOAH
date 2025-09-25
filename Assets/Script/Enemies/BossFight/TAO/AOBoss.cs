using UnityEngine;

public class AOBoss : MonoBehaviour
{
    [field: Header("General attribute")]
    [field: SerializeField]public BossStatus BossStatus { get; set; }
    [field: SerializeField]public Animator AOBossAnimator { get; set; }
    [field: SerializeField]public Transform RightHand { get; set; }
    [field: SerializeField]public Transform LeftHand { get; set; }
    [field: SerializeField]public Rigidbody2D RHandRB { get; set; }
    [field: SerializeField]public Rigidbody2D LHandRB { get; set; }
    [field: SerializeField]public Rigidbody2D HeadRB { get; set; }
    [field: SerializeField]public BoxCollider2D LHandCld { get; set; }
    [field: SerializeField]public BoxCollider2D RHandCld { get; set; }
    [field: SerializeField]public BoxCollider2D HeadCld { get; set; }
    public Vector3 RightHandOriginTrans { get; set; }
    public Vector3 LeftHandOriginTrans { get; set; }
    [field: Header("ATK1 Setting")]
    [field: SerializeField]public int ATK1MaxAtk { get; set; }
    public int AttackCount { get; set; }
    [field: Header("----Right Hand")]
    [field: SerializeField]public GameObject ATK1SitePref { get; set; }
    [field: SerializeField]public float ATK1RHReadyTime { get; set; }
    [field: SerializeField]public float ATK1RHFlySpeed { get; set; }
    [field: SerializeField]public float ATK1RHFallSpeed { get; set; }
    [field: SerializeField]public float ATK1RHStayTime { get; set; }
    public Vector3 ATK1RHDirect;
    [field: Header("----Left Hand")]
    [field: SerializeField]public Transform ATK1LHShootPos { get; set; }
    [field: SerializeField]public GameObject ATK1LHBulletPref { get; set; }
    [field: SerializeField]public float ATK1LHBulletTime { get; set; }
    [field: SerializeField]public float ATK1LHBulletSpeed { get; set; }
    [field: Header("ATK2 Setting")]
    [field: SerializeField]public int ATK2MaxAtk { get; set; }
    [field: SerializeField]public int ATK2AtkDelay { get; set; }
    [field: Header("----Right Hand")]
    [field: SerializeField]public GameObject ATK2RHBulletPref { get; set; }
    [field: SerializeField]public float ATK2RHBulletSpeed { get; set; }
    [field: SerializeField]public int ATK2RHBoundLimit { get; set; }
    [field: SerializeField]public Transform ATK2RHShootPos { get; set; }
    [field: Header("----Left Hand")]
    [field: SerializeField]public GameObject ATK2LHBulletPref { get; set; }
    [field: SerializeField]public float ATK2LHBulletSpeed { get; set; }
    [field: SerializeField]public int ATK2LHBoundLimit { get; set; }
    [field: SerializeField]public Transform ATK2LHShootPos { get; set; }
    public Vector3 ATK1Direct;
    public AOBossStateMachine StateMachine { get; private set;}
    public AOBossRestState RestState { get; private set; }
    public AOBossAwakeState AwakeState { get; private set; }
    public AOBossDeadState DeadState { get; private set; }
    public AOBossIdleState IdleState { get; private set; }
    public AOBossATK1ReadyState ATK1ReadyState { get; private set; }
    public AOBossATK1IdleState ATK1IdleState { get; private set; }
    public AOBossATK1AttackState ATK1AttackState { get; private set; }
    public AOBossATK1EndState ATK1EndState { get; private set; }
    public AOBossATK2ReadyState ATK2ReadyState { get; private set; }
    public AOBossATK2IdleState ATK2IdleState { get; private set; }
    public AOBossATK2EndState ATK2EndState { get; private set; }
    public bool BossIsAwake { get; set; }
    [field: SerializeField]public BossHealthBar HealthBar{ get; set; }

    public enum AnimationTriggerType
    {
        RestAnimFinish,
        AwakeAnimFinish, 
        IdleAnimFinish,
        ATK1ReadyAnimFinish,
        ATK2ReadyAnimFinish,
        ATK2EndAnimFinish,
        DeadAnimFinish,
    }

    private void Awake()
    {
        StateMachine = new AOBossStateMachine();
        RestState = new AOBossRestState(this, StateMachine);
        AwakeState = new AOBossAwakeState(this, StateMachine);
        DeadState = new AOBossDeadState(this, StateMachine);
        IdleState = new AOBossIdleState(this, StateMachine);
        ATK1ReadyState = new AOBossATK1ReadyState(this, StateMachine);
        ATK1IdleState = new AOBossATK1IdleState(this, StateMachine);  
        ATK1AttackState = new AOBossATK1AttackState(this, StateMachine);  
        ATK1EndState = new AOBossATK1EndState(this, StateMachine);      
        ATK2ReadyState = new AOBossATK2ReadyState(this, StateMachine);
        ATK2IdleState = new AOBossATK2IdleState(this, StateMachine);
        ATK2EndState = new AOBossATK2EndState(this, StateMachine);   
    }
    private void Start()
    {
        RightHandOriginTrans = RightHand.position;
        LeftHandOriginTrans = LeftHand.position;
        AttackCount = 0;
        StateMachine.Initialize(RestState);
    }
    private void Update()
    {
        StateMachine.CurrentState.FrameUpdate();
    }
    public void BossAwake()
    {
        StateMachine.ChangeState(AwakeState);
    }
    public void AnimationTriggerEvent(AnimationTriggerType triggerType)
    {
        StateMachine.CurrentState.AnimationTriggerEvent(triggerType);
    }
}
