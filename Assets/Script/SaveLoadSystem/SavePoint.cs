using UnityEngine;

public class SavePoint : MonoBehaviour
{
    [SerializeField]private ObjectInteraction objectInteraction;
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            if(objectInteraction.GetCanInteract())
            {
                SaveController.Instance.SaveGame();
            }
        }
        
    }
}
