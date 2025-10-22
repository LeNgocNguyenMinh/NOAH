using System.Collections;
using UnityEngine;

public class PlayerEffect : MonoBehaviour
{
    public static PlayerEffect Instance;
    [Header("KnockBack")]
    [SerializeField]private Rigidbody2D rb;
    [SerializeField]private float knockBackForce;
    [SerializeField]private float knockDuration;
    private bool isKnock = false;
    private Coroutine knockBackCoroutine;
    [Header("HitFlash")]
    [SerializeField]private float hitFlashDuration;
    [SerializeField]private SpriteRenderer[] spriteRenderer;
    [SerializeField]private Color flashColor;
    [SerializeField]private Material flashMaterial;
    [SerializeField]private Material defaultMaterial;
    private Coroutine flashCoroutine;
    private void Awake()
    {
        if(Instance==null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void PushBack(Vector2 direct)
    {
        isKnock = true;
        if(knockBackCoroutine!=null)
        {
            StopCoroutine(knockBackCoroutine);
        }
        knockBackCoroutine = StartCoroutine(KnockBackCoroutine(direct));   
    }
    private IEnumerator KnockBackCoroutine(Vector2 direct)
    {
        rb.linearVelocity = direct * knockBackForce;
        Player.Instance.GotHit = true;
        yield return new WaitForSeconds(knockDuration);
        isKnock = false;
        Player.Instance.GotHit = false;
        rb.linearVelocity = Vector2.zero;
        knockBackCoroutine = null;
    }
    public void HitFlash()
    {
        if(flashCoroutine!=null)
        {
            StopCoroutine(flashCoroutine);
        }
        flashCoroutine = StartCoroutine(HitFlashCoroutine());
    }
    private IEnumerator HitFlashCoroutine()
    {
        foreach(var sr in spriteRenderer)
        {
            sr.material = flashMaterial;
            sr.material.color = flashColor;
        }
        yield return new WaitForSeconds(hitFlashDuration);
        foreach(var sr in spriteRenderer)
        {
            sr.material = defaultMaterial;
        }
        flashCoroutine = null;
    }
}
