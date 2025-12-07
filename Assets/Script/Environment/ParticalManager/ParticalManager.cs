using UnityEngine;

public class ParticalManager : MonoBehaviour
{
    public static ParticalManager Instance;
    public ParticleSystem fireFly;
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
