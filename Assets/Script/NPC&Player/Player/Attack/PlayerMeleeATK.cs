using UnityEngine;

public class PlayerMeleeATK : MonoBehaviour
{
    public static PlayerMeleeATK Instance;
    public Animator animator;
    public PlayerMeleeStateMachine StateMachine { get; private set;}
    public PlayerMelee01State MeleeState01 { get; private set;}
    public PlayerMelee02State MeleeState02 { get; private set;}
    public enum AnimationTriggerType
    {
        ATK1AnimFinish,
        ATK2AnimFinish,
    }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        StateMachine = new PlayerMeleeStateMachine();
        MeleeState01 = new PlayerMelee01State(this, StateMachine);
        MeleeState02 = new PlayerMelee02State(this, StateMachine);
    }
    private void Start()
    {
        StateMachine.Initialize(MeleeState01);
    }

    private void Update()
    {
        StateMachine.CurrentState.FrameUpdate();
    }
    private void FixedUpdate()
    {
        StateMachine.CurrentState.PhysicsUpdate();
    }
    public void AnimationTriggerEvent(AnimationTriggerType triggerType)
    {
        StateMachine.CurrentState.AnimationTriggerEvent(triggerType);
    }
}
