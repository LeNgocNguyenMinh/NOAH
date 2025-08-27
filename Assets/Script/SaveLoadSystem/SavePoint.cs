using UnityEngine;

public class SavePoint : MonoBehaviour
{
    private Animator animator;
    private PlayerControl playerControl;
    private ObjectInteraction objectInteraction;
    private bool triggerNearAnimation = false;
    private bool triggerFarAnimation = false;
    [SerializeField]private bool isBed = false;
    private void Update()
    {
        objectInteraction = GetComponent<ObjectInteraction>();
        if(objectInteraction.GetCanInteract())
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                if(isBed == true)
                {
                    SaveController.Instance.SaveGameByBed();
                    SaveController.Instance.LoadSave();
                }
                else{
                    SaveController.Instance.SaveGame();
                }
            }
            /* if(!triggerNearAnimation)
            {
                triggerNearAnimation = true;
                triggerFarAnimation = false;
                if(GetComponent<Animator>() != null)
                {
                    animator = GetComponent<Animator>();
                    animator.SetTrigger("isNear");
                }
                
            } */
        }
        /* else
        {
            if(!triggerFarAnimation)
            {
                triggerNearAnimation = false;
                triggerFarAnimation = true;
                if(GetComponent<Animator>() != null)
                {
                    animator = GetComponent<Animator>();
                    animator.SetTrigger("isFar");
                }
            }
        } */
    }
}
