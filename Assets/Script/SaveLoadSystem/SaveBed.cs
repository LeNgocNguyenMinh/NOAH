using UnityEngine;

public class SaveBed : MonoBehaviour
{
    [SerializeField]private ObjectInteraction objectInteraction;
    private void Update()
    {
        objectInteraction = GetComponent<ObjectInteraction>();
        if(objectInteraction.GetCanInteract())
        {
            if(Input.GetKeyDown(KeyCode.F))
            {
                SaveController.Instance.SaveGameByBed();
                SaveController.Instance.LoadSave();
            }
            
        }
    }
}
