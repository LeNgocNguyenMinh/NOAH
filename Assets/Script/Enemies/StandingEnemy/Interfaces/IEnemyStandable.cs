using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyStandable 
{
    bool IsFacingRight { get; set; }  
    void FlipSprite();
}
