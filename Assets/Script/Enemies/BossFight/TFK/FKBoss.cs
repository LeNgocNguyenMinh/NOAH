using UnityEngine;

public class FKBoss : MonoBehaviour
{
    [field: Header("General attribute")]
    [field: SerializeField]public BossStatus BossStatus { get; set; }
    [field: SerializeField]public Animator FKBossAnimator { get; set; }
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
    [field: SerializeField]public BoxCollider2D ATK1RHBox { get; set; }
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
    public FKBossStateMachine StateMachine { get; private set;}
    public FKBossRestState RestState { get; private set; }
    public FKBossAwakeState AwakeState { get; private set; }
    public FKBossDeadState DeadState { get; private set; }
    public FKBossIdleState IdleState { get; private set; }
    public FKBossATK1ReadyState ATK1ReadyState { get; private set; }
    public FKBossATK1IdleState ATK1IdleState { get; private set; }
    public FKBossATK1AttackState ATK1AttackState { get; private set; }
    public FKBossATK1EndState ATK1EndState { get; private set; }
    public FKBossATK2ReadyState ATK2ReadyState { get; private set; }
    public FKBossATK2IdleState ATK2IdleState { get; private set; }
    public FKBossATK2EndState ATK2EndState { get; private set; }
    public bool BossIsAwake { get; set; }
    [field: SerializeField]public GameObject HealthBarCV{ get; set; }

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
        StateMachine = new FKBossStateMachine();
        RestState = new FKBossRestState(this, StateMachine);
        AwakeState = new FKBossAwakeState(this, StateMachine);
        DeadState = new FKBossDeadState(this, StateMachine);
        IdleState = new FKBossIdleState(this, StateMachine);
        ATK1ReadyState = new FKBossATK1ReadyState(this, StateMachine);
        ATK1IdleState = new FKBossATK1IdleState(this, StateMachine);  
        ATK1AttackState = new FKBossATK1AttackState(this, StateMachine);  
        ATK1EndState = new FKBossATK1EndState(this, StateMachine);      
        ATK2ReadyState = new FKBossATK2ReadyState(this, StateMachine);
        ATK2IdleState = new FKBossATK2IdleState(this, StateMachine);
        ATK2EndState = new FKBossATK2EndState(this, StateMachine);   
    }
    private void Start()
    {
        RightHandOriginTrans = RightHand.position;
        LeftHandOriginTrans = LeftHand.position;
        AttackCount = 0;
        ATK1RHBox.enabled = false;
        HealthBarCV.SetActive(false);
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
