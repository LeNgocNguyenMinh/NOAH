using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillarInteract : MonoBehaviour
{
    [SerializeField]private string text;
    private LightOnTopInteractive lightOnTopInteractive;
    private Puzzle01Interact puzzle01Interact;
    private void Start()
    {
        puzzle01Interact = FindObjectOfType<Puzzle01Interact>().GetComponent<Puzzle01Interact>();
        lightOnTopInteractive = GetComponentInChildren<LightOnTopInteractive>();
    }
    public void ResetEnterPass()
    {
        puzzle01Interact.AddToPassWord("");
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag.Contains("Bullet"))
        {
            puzzle01Interact.AddToPassWord(text);
            lightOnTopInteractive.LightOnTopActive();
        }
    }
}
