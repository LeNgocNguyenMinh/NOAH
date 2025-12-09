using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTransition : MonoBehaviour
{
    public static SceneTransition Instance;
    [SerializeField]private Animator animator;
    public bool inAnim;
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
        inAnim = true;
        animator.SetTrigger("sceneOut");
    }
    public void SceneIn()
    {
        inAnim = true;
        animator.SetTrigger("sceneIn");
    }
    public void SceneTransFinish()
    {
        inAnim = false;
    }
}
