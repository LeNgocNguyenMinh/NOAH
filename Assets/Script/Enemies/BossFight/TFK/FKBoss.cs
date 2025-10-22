using UnityEngine;

public class FKBoss : MonoBehaviour
{
    public static FKBoss Instance { get; private set; }
    [field: Header("General attribute")]
    [field: SerializeField]public BossStatus BossStatus { get; set; }
    [field: SerializeField]public Animator FKBossAnimator { get; set; }
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
    public FKBossATK2AttackState ATK2AttackState { get; private set; }
    public FKBossATK2EndState ATK2EndState { get; private set; }
    public bool BossIsAwake { get; set; }
    [field: SerializeField]public GameObject HealthBarCV{ get; set; }
    [field: Header("ATK1 attribute")]
    [field: SerializeField]public int ATK1AtttackNum { get; set; }
    [field: SerializeField]public float ATK1RtSpeed { get; set; }
    [field: SerializeField]public int ATK1IdleTime { get; set; }
    public int ATK1Count { get; set; }
    [field: Header("ATK1 LH")]
    [field: SerializeField]public float ATK1LHWMFlyDist { get; set; }
    [field: SerializeField]public GameObject WaterMelon { get; set; }
    [field: SerializeField]public float ATK1LHWMSpeed { get; set; }
    [field: SerializeField]public float ATK1LHWMPFlyDist { get; set; }
    [field: SerializeField]public GameObject WaterMelonPiece { get; set; }
    [field: SerializeField]public float ATK1LHWMPSpeed { get; set; }
    [field: SerializeField]public Transform LHThrowPoint { get; set; }

    [field: SerializeField]public float LHATK1Time { get; set; }
    [field: Header("ATK1 RH")]
    [field: SerializeField]public Transform RHThrowPoint { get; set; }
    [field: SerializeField]public GameObject Banana { get; set; }
    [field: SerializeField]public float RHATK1Time { get; set; }
    [field: SerializeField]public float ATK1RHFlyTime { get; set; }
    [field: SerializeField]public float ATK1RhRotateSpeed { get; set; }
    [field: Header("ATK2 attribute")]
    [field: SerializeField]public int ATK2AtttackNum { get; set; }
    [field: SerializeField]public int ATK2IdleTime { get; set; }
    public int ATK2Count { get; set; }

    [field: Header("ATK2 RH")]
    [field: SerializeField]public Transform ATK2RHShootPoint { get; set; }
    [field: SerializeField]public GameObject ATK2RHBullet { get; set; }
    [field: SerializeField]public float ATK2RHBulletMaxSpeed { get; set; }
    [field: SerializeField]public float ATK2RHBulletMaxHeight { get; set; }
    [field: Header("ATK2 LH")]
    [field: SerializeField]public GameObject ATK2LHRoute { get; set; }
    [field: SerializeField]public float ATK2LHSpawnDistance{ get; set; }
    [field: SerializeField]public float ATK2LHSpawdDelay { get; set; }
    public enum AnimationTriggerType
    {
        RestAnimFinish,
        AwakeAnimFinish, 
        IdleAnimFinish,
        ATK1ReadyAnimFinish,
        ATK1IdleAnimFinish,
        ATK1AttackAnimFinish,
        ATK1EndAnimFinish,
        ATK1RHThrow,
        ATK1LHThrow,
        ATK2ReadyAnimFinish,
        ATK2IdleAnimFinish,
        ATK2AttackAnimFinish,
        ATK2EndAnimFinish,
        ATK2RHShoot,
        ATK2LHAttack,
        DeadAnimFinish,
    }

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
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
        ATK2AttackState = new FKBossATK2AttackState(this, StateMachine);
        ATK2EndState = new FKBossATK2EndState(this, StateMachine);   
    }
    private void Start()
    {
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
