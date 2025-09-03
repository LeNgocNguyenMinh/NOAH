using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerMoveable 
{
    Rigidbody2D RB{ get; set; }
    float SpeedX { get; set; }
    float SpeedY { get; set; }
    Vector2 MoveDirect { get; set; }
    float WalkSpeed { get; set; }
    float DashSpeed { get; set; }
    float DashCoolDown { get; set; }
    float DashCoolCounter { get; set; }
    bool IsFacingRight { get; set; }
    Vector3 MousePos { get; set; }
    bool CanChangeState { get; set; }
}
