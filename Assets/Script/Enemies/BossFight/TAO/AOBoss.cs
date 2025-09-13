using UnityEngine;

public class AOBoss : MonoBehaviour
{
    [field: Header("General attribute")]
    [field: SerializeField]public Animator AOBossAnimator { get; set; }
    [field: SerializeField]public Transform RightHand { get; set; }
    [field: SerializeField]public Transform LeftHand { get; set; }
    [field: SerializeField]public Rigidbody2D RHandRB { get; set; }
    [field: SerializeField]public Rigidbody2D LHandRB { get; set; }
    public Transform RightHandOriginTrans { get; set; }
    public Transform LeftHandOriginTrans { get; set; }
    [field: Header("ATK1 Setting")]
    [field: SerializeField]public GameObject ATK1SitePref { get; set; }
    [field: SerializeField]public float ATK1ShootTime { get; set; }
    [field: SerializeField]public Transform ATK1RHShootPos { get; set; }
    [field: SerializeField]public GameObject ATK1RHBulletPref { get; set; }
    [field: SerializeField]public float ATK1RHBulletSpeed { get; set; }
    [field: SerializeField]public Transform[] ATK1LHShootPos { get; set; }
    [field: SerializeField]public GameObject ATK1LHBulletPref { get; set; }
    [field: SerializeField]public float ATK1LHBulletTime { get; set; }
    [field: SerializeField]public float ATK1LHBulletSpeed { get; set; }
    public Vector3 ATK1Direct;
    public AOBossStateMachine StateMachine { get; private set;}
    public AOBossRestState RestState { get; private set; }
    public AOBossAwakeState AwakeState { get; private set; }
    public AOBossDeadState DeadState { get; private set; }
    public AOBossIdleState IdleState { get; private set; }
    public AOBossATK1State ATK1State { get; private set; }
    public AOBossATK2State ATK2State { get; private set; }
    public Rigidbody2D RB { get; set; }
    [field: SerializeField]public BossHealthBar HealthBar{ get; set; }

    public enum AnimationTriggerType
    {
        RestAnimFinish,
        DeadAnimFinish,
        AwakeAnimFinish, 
        IdleAnimFinish
    }

    private void Awake()
    {
        StateMachine = new AOBossStateMachine();
        RestState = new AOBossRestState(this, StateMachine);
        AwakeState = new AOBossAwakeState(this, StateMachine);
        DeadState = new AOBossDeadState(this, StateMachine);
        IdleState = new AOBossIdleState(this, StateMachine);
        ATK1State = new AOBossATK1State(this, StateMachine);       
        ATK2State = new AOBossATK2State(this, StateMachine);   
    }
    private void Start()
    {
        RightHandOriginTrans = RightHand;
        LeftHandOriginTrans = LeftHand;
        RB = GetComponent<Rigidbody2D>();
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
