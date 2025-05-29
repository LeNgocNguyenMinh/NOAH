using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTransition : MonoBehaviour
{
    public static SceneTransition Instance;
    private Animator animator;
    private void Awake()
    {
        Instance = this;   
    }
    public void SceneOut()
    {
        animator = GetComponent<Animator>();
        animator.SetTrigger("sceneOut");
    }
    public void SceneIn()
    {
        animator = GetComponent<Animator>();
        animator.SetTrigger("sceneIn");
    }
    public bool SceneTransFinish()
    {
        animator = GetComponent<Animator>();
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        if (stateInfo.length > 0f)
        {
           return false;
        }
        return true;
    }
}
