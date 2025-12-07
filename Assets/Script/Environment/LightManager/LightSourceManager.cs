using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightSourceManager : MonoBehaviour
{
    public static LightSourceManager Instance;
    public Light2D globalLightSource;
    public GameObject objectLightSource;
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
