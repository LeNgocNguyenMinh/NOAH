using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public BossStateMachine StateMachine { get; set; }
    public enum AnimationTriggerType
    {
        WalkAnimFinish,
        DeadAnimFinish,
        AttackAnimFinish,
        IdleAnimFinish,
        PlayerHurt  
    }
    public void AnimationTriggerEvent(AnimationTriggerType triggerType)
    {
        StateMachine.CurrentBossState.AnimationTriggerEvent(triggerType);
    }
}
