using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTransition : MonoBehaviour
{
    public static SceneTransition Instance;
    [SerializeField]private Animator animator;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }   
    }
    public void SceneOut()
    {
        animator.SetTrigger("sceneOut");
    }
    public void SceneIn()
    {
        animator.SetTrigger("sceneIn");
    }
    public bool SceneTransFinish()
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        if (stateInfo.length > 0f)
        {
           return false;
        }
        return true;
    }
}
