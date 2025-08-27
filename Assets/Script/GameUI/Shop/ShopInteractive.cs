using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening; 
public class ShopInteractive : MonoBehaviour
{
    // Start is called before the first frame update
    private ObjectInteraction objectInteraction;    
    
    private void Update()
    {
        objectInteraction = GetComponent<ObjectInteraction>();
        if(objectInteraction.GetCanInteract())
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                ShopController.Instance.CheckShopUI();
            }       
        }
    }
}
