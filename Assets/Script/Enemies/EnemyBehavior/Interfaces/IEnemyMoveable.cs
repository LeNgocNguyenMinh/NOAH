using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyMoveable 
{
    Rigidbody2D RB{ get; set; }
    float WalkSpeed { get; set; }
    float ChaseSpeed { get; set; }
    bool IsFacingRight { get; set; }
    
    void Move(Vector2 direct, float speedValue);
    void Stop();
    void FlipSprite(Vector2 velocity);
}
