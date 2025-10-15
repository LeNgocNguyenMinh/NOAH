using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimBehaviourGeneral : MonoBehaviour
{
    [SerializeField]private GameObject currentObject;
    [SerializeField]private Animator animator;
    public void BreakAnim()
    {
        animator.SetTrigger("Break");
    }
    public void DestroyStone()
    {
        Destroy(currentObject);
    }
}
