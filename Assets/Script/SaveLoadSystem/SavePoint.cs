using UnityEngine;

public class SavePoint : MonoBehaviour
{
    [SerializeField]private ObjectInteraction objectInteraction;
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
        }
    }
}
