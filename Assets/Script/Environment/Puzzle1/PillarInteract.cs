using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillarInteract : MonoBehaviour
{
    [SerializeField]private string text;
    [SerializeField]private LightOnTopInteractive lightOnTopInteractive;
    [SerializeField]private Puzzle01Interact puzzle01Interact;
    public void ResetEnterPass()
    {
        puzzle01Interact.AddToPassWord("");
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.CompareTag("Bullet"))
        {
            puzzle01Interact.AddToPassWord(text);
            lightOnTopInteractive.TurnOnLight();
        }
    }
}