using UnityEngine;

public class SavePoint : MonoBehaviour
{
    [SerializeField]private ObjectInteraction objectInteraction;
    private void Update()
    {
        if(objectInteraction.GetCanInteract())
        {
            if(Input.GetKeyDown(KeyCode.F))
            {
                SaveController.Instance.SaveGame();
            }
            
        }
    }
}
