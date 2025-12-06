using UnityEngine;

public class PlayerMeleeATK : MonoBehaviour
{
    public static PlayerMeleeATK Instance;
    [SerializeField]private GameObject hitParticlePrefab;
    private Animator meleeAnimator;
    private float damageAmount = 1;
    [SerializeField] private float delayMelee;
    private int comboCount = 1;
    private bool canComBo = false;
    private bool canAttack = true;
    private bool inMeleeATK = false;
    public float delayMeleeCount = 0f;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void MeleeHitAnim()
    {
        meleeAnimator = GetComponent<Animator>();
        meleeAnimator.SetTrigger("Hit");
    }
    public void CheckMeleeATK()
    {
        if(delayMeleeCount > 0)
        {
            delayMeleeCount -= Time.deltaTime;
        }
        if(!PlayerWeaponParent.Instance.playerCanATK)return;
        if(Input.GetMouseButtonDown(1) && canAttack == true)
        {
            PlayerWeaponParent.Instance.ActiveMeleeATK();
            meleeAnimator = GetComponent<Animator>();
            if(delayMeleeCount <=0)
            {
                comboCount = 1;
            }
            if(comboCount == 1)
            {
                meleeAnimator.SetTrigger("atk1");
                SoundControl.Instance.PlayerMeleeSoundPlay();
                delayMeleeCount = delayMelee;
                comboCount ++;
            }
            else if(comboCount == 2 && delayMeleeCount > 0)
            {
                meleeAnimator.SetTrigger("atk2");
                SoundControl.Instance.PlayerMeleeSoundPlay();
                delayMeleeCount = 0;
                comboCount--;
            }
            
        }

    }
    public void StartMeleeComboCount()
    {
        delayMeleeCount = delayMelee;
    }
    public void SetCanAttackValue(int value)
    {
        if(value == 0)
        {
            canAttack = false;
        }
        else{
            canAttack = true;
        }
    }
}
