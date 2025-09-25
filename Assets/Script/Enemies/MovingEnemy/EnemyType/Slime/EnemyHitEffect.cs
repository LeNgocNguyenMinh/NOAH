using System.Collections;
using UnityEngine;

public class EnemyHitEffect : MonoBehaviour
{
    [SerializeField] private Material flashMaterial;

    [SerializeField] private float duration;
    [SerializeField] private float splashDistance;
    [SerializeField] private GameObject hitEffectPrefab;
    private SpriteRenderer spriteRenderer;
    private Material originalMaterial;
    private Coroutine flashRoutine;
    
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalMaterial = spriteRenderer.material;
        flashMaterial = new Material(flashMaterial);
    }


    public void Flash(Color color)
    {
        // If the flashRoutine is not null, then it is currently running.
        if (flashRoutine != null)
        {
            // In this case, we should stop it first.
            // Multiple FlashRoutines the same time would cause bugs.
            StopCoroutine(flashRoutine);
        }

        // Start the Coroutine, and store the reference for it.
        flashRoutine = StartCoroutine(FlashRoutine(color));
    }

    private IEnumerator FlashRoutine(Color color)
    {
        // Swap to the flashMaterial.
        spriteRenderer.material = flashMaterial;

        // Set the desired color for the flash.
        flashMaterial.color = color;

        // Pause the execution of this function for "duration" seconds.
        yield return new WaitForSeconds(duration);

        // After the pause, swap back to the original material.
        spriteRenderer.material = originalMaterial;

        // Set the flashRoutine to null, signaling that it's finished.
        flashRoutine = null;
    }
    public void SplashAfterEffectInit(Vector2 hitPoint, Vector2 direct)
    {
        Vector2 spawnPoint = hitPoint + direct * splashDistance;
        float angle = Mathf.Atan2(direct.y, direct.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.Euler(0f, 0f, angle);
        Instantiate(hitEffectPrefab, spawnPoint, rotation);
    }
}
