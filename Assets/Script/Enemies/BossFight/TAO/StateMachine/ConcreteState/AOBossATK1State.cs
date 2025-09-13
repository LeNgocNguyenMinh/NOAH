using System.Collections;
using UnityEngine;

public class AOBossATK1State : AOBossState
{
    private float timer = 0f;
    private Vector3 rhDirect;
    private Vector3 lhDirect;
    private GameObject atk1Site;
    private SpriteRenderer sr;
    private Vector2 size;
    public AOBossATK1State(AOBoss aoBoss, AOBossStateMachine aoBossStateMachine) : base(aoBoss, aoBossStateMachine)
    {
    }
    public override void EnterState()
    {
        base.EnterState();
        timer = aoBoss.ATK1ShootTime;
        /* atk1Site = Instantiate(aoBoss.ATK1SitePref, aoBoss.RightHand.position, Quaternion.identity, aoBoss.RightHand);
        sr = atk1Site.GetComponent<SpriteRenderer>();
        size = sr.size;
        size.x  = 1f;
        sr.size = size; */
        aoBoss.AOBossAnimator.SetTrigger("ATK1");
        aoBoss.StartCoroutine(RHandLoop());
        aoBoss.StartCoroutine(LHandLoop());
    }
    public override void FrameUpdate()
    {
        base.FrameUpdate();
        timer -= Time.deltaTime;
        rhDirect = (Player.Instance.transform.position - aoBoss.RightHand.position).normalized;
        lhDirect = (Player.Instance.transform.position - aoBoss.LeftHand.position).normalized;
       /*  Vector2 size = sr.size;
        if(size.x <= 20f)
        {
            size.x += 2f * Time.deltaTime;
            sr.size = size;
        } */
        float rAngle = Mathf.Atan2(rhDirect.y, rhDirect.x) * Mathf.Rad2Deg;
        aoBoss.RightHand.rotation = Quaternion.Euler(0f, 0f, rAngle - 180f);
        float lAngle = Mathf.Atan2(lhDirect.y, lhDirect.x) * Mathf.Rad2Deg;
        aoBoss.LeftHand.rotation = Quaternion.Euler(0f, 0f, lAngle);
    }
    public void RHandBulletSpawn()
    {
        Instantiate(aoBoss.ATK1RHBulletPref, aoBoss.ATK1RHShootPos.position, Quaternion.identity).GetComponent<AOBossATK1RHBullet>().SetValue(rhDirect, aoBoss.ATK1RHBulletSpeed);
    }
    public void LHandBulletSpawn()
    {
        Instantiate(aoBoss.ATK1LHBulletPref, aoBoss.ATK1LHShootPos[0].position, Quaternion.identity).GetComponent<AOBossATK1LHBullet>().SetValue(aoBoss.ATK1LHBulletSpeed, aoBoss.ATK1LHBulletTime);
        Instantiate(aoBoss.ATK1LHBulletPref, aoBoss.ATK1LHShootPos[1].position, Quaternion.identity).GetComponent<AOBossATK1LHBullet>().SetValue(aoBoss.ATK1LHBulletSpeed, aoBoss.ATK1LHBulletTime);
    }
    public override void ExitState()
    {
        base.ExitState();
        aoBoss.StopAllCoroutines();
    }
    public override void AnimationTriggerEvent(AOBoss.AnimationTriggerType triggerType)
    {
    }
    private IEnumerator RHandLoop()
    {
        yield return new WaitForSeconds(1f); // delay ban đầu
        while (true)
        {
            RHandBulletSpawn();
            yield return new WaitForSeconds(2f); // lặp lại
        }
    }

    private IEnumerator LHandLoop()
    {
        yield return new WaitForSeconds(2f);
        while (true)
        {
            LHandBulletSpawn();
            yield return new WaitForSeconds(2f);
        }
    }
}
