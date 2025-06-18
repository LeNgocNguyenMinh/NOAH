using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class DamageNumberInEnemy : MonoBehaviour
{
    [SerializeField]private PlayerStatus playerStatus;
    public TextMeshProUGUI damageNumberText;
    public Gradient fadeNormalDamageGradient;
    public Gradient fadeCriticalDamageGradient;
    private float fadeTime = 1f;
    private RectTransform rectTransform;
    [SerializeField] private float initialFontSize = 36f;
    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }
    public void showDamageNumber(float damageAmount)
    {
        rectTransform.anchoredPosition = new Vector2(Random.Range(-30, 30), 258);// random appear place
        if(damageAmount == 0)//Mean player miss the hit
        {
            damageNumberText.text = "Miss!";
        }
        else{
            damageNumberText.text = "" + damageAmount;
        }
        StartCoroutine(DamageEffect(damageAmount));
    }
    private IEnumerator DamageEffect(float damageAmount)
    {
        damageNumberText.fontSize = initialFontSize;
        float startFade = 0f;
        while(startFade < fadeTime)
        {
            if(PauseMenu.isPaused || UIInventoryController.inventoryOpen)
            {
                yield return null; 
                continue;
            }
            float percentage = startFade/fadeTime;
            if(startFade < (fadeTime * 0.5f))
            {
                damageNumberText.fontSize ++;
            }
            else{
                damageNumberText.fontSize --;
            }
            if(damageAmount < (playerStatus.playerCurrentDamage + playerStatus.playerWeaponDamage)/2 )
            {
                damageNumberText.fontStyle = FontStyles.Normal;
                damageNumberText.color = fadeNormalDamageGradient.Evaluate(percentage);
            }
            else{
                damageNumberText.fontStyle = FontStyles.Bold;
                damageNumberText.color = fadeCriticalDamageGradient.Evaluate(percentage);
            }
            startFade += Time.deltaTime;
            yield return null;
        }
    }

}
