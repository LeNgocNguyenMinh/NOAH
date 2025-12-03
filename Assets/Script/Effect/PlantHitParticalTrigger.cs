using UnityEngine;

public class PlantHitParticalTrigger : MonoBehaviour
{
    [SerializeField]private ParticleSystem hitPartical;
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Sword") || collider.tag.Contains("bullet"))
        {
            hitPartical.Play();
        }
    }
}
