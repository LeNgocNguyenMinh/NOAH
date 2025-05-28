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
}
