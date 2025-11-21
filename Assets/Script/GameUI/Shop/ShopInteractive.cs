using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening; 
public class ShopInteractive : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]private ObjectInteraction objectInteraction;    
    
    private void Update()
    {
        if(objectInteraction.GetCanInteract())
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                ShopController.Instance.ShopUIInteract();
            }     
        }
    }
}
