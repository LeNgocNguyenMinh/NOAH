using System.Collections.Generic;
using UnityEngine;

public class BossSaveData : MonoBehaviour
{
    public static BossSaveData Instance;
    public bool isAOBossDead;
    public bool isFKBossDead;
    public List<GameObject> bossPrefab;
    public List<BossCurrentStatus> bossPrefList;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public List<BossCurrentStatus> GetAllBossCurrentStatus()
    {
        List<BossCurrentStatus> bossStatus = new List<BossCurrentStatus>();

        BossCurrentStatus aoStatus = new BossCurrentStatus
        {
            bossID = "AO_BOSS",
            isDead = AOBoss.Instance.IsDead
        };
        bossStatus.Add(aoStatus);

        BossCurrentStatus fkStatus = new BossCurrentStatus
        {
            bossID = "FK_BOSS",
            isDead = FKBoss.Instance.IsDead
        };
        bossStatus.Add(fkStatus);

        return bossStatus;
    }
    public void SetBossCurrentStatus(List<BossCurrentStatus> bossCurrentStatus)
    {
        foreach (var status in bossCurrentStatus)
        {
            if (status.bossID == "AO_BOSS")
            {
                isAOBossDead = status.isDead;
            }
            else if (status.bossID == "FK_BOSS")
            {
                isFKBossDead = status.isDead;
            }
        }
        bossPrefList = bossCurrentStatus;
        for(int i = 0; i < bossPrefab.Count; i++)
        {
            Destroy(bossPrefab[i]);
        }
        for(int i = 0; i < bossCurrentStatus.Count; i++)
        {
            if(!bossCurrentStatus[i].isDead)
            {
                bossPrefab.Add(Instantiate(ItemDictionary.Instance.GetBossInfo(bossCurrentStatus[i].bossID).bossPrefab, bossCurrentStatus[i].bossPos, Quaternion.identity));
            }
        }
    }
}
public class BossCurrentStatus
{
    public string bossID;
    public bool isDead;
    public Vector3 bossPos;
}
