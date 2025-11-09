using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowSlimeATK : MonoBehaviour
{
    [SerializeField]private Slime slime;
    [SerializeField]private Transform sword;
    [SerializeField]private Transform swordSprite;
    [SerializeField]public bool canRotate = true;
    public enum RotateBoolValue
    {
        CanRotate,
        NotRotate
    }

    private void Update()
    {
        if(slime.IsInChaseRange && canRotate)
        {
            Vector3 rotation = Player.Instance.transform.position - sword.position;
            float rotz = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
            sword.rotation = Quaternion.Euler(0, 0, rotz);
            /* wandShadowRotate.transform.rotation = Quaternion.Euler(0, 0, -rotz); */
            
            if(slime.IsFacingRight)
            {
                swordSprite.localScale = new Vector3(1, 1, 1);
            }
            else
            {
                swordSprite.localScale = new Vector3(1, -1, 1);
            }
        }
    }
}
