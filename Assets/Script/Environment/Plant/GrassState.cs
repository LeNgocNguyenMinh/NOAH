using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class GrassState : MonoBehaviour
{
    private Animator animator;
    private bool canCut = true;
    private bool canGrow = false;
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
            animator = GetComponent<Animator>();
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
    public void SetCanCut()
    {
        canCut = true;
    }
}
