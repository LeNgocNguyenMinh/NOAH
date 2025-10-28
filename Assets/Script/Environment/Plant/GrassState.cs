using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class GrassState : MonoBehaviour
{
    [SerializeField]private Animator animator;
    private bool canCut = true;
    private bool canGrow = false;
    public enum AnimationTrigger
    {
        GrowAnimFinish,
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Sword") || collision.tag.Contains("Bullet"))
        {
            CanCut();
        }
    }
    private void CanCut()
    {
        if(canCut)
        {
            canCut = false;
            animator.SetTrigger("Cut");
            canGrow = true;
        }
        else{
            return;
        }
        StartCoroutine(AnimationCount());
    }
    private IEnumerator AnimationCount()
    {
        yield return new WaitForSeconds(2f);
        if(canGrow)
        {
            animator.SetTrigger("Grow");
            canGrow = false;
        }
    }
    public void AnimationEventTrigger(AnimationTrigger animationTrigger)
    {
        if(animationTrigger == AnimationTrigger.GrowAnimFinish)
        {
            canCut = true;
            animator.SetTrigger("Idle");
        }
    }
}
