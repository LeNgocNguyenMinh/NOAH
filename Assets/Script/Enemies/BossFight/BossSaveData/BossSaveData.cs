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
            bossID = "B_01",
            isDead = AOBoss.Instance.IsDead,
            bossPos = new Vector3(-16.73f, 75.6f, 0.0f )
        };
        bossStatus.Add(aoStatus);

        BossCurrentStatus fkStatus = new BossCurrentStatus
        {
            bossID = "B_03",
            isDead = FKBoss.Instance.IsDead,
            bossPos = new Vector3(114.32f, 6.04f, 0.0f)
        };
        bossStatus.Add(fkStatus);

        return bossStatus;
    }
    public void SetBossCurrentStatus(List<BossCurrentStatus> bossCurrentStatus)
    {      
        foreach (BossCurrentStatus status in bossCurrentStatus)
        {
            if (status.bossID == "B_01")
            {
                isAOBossDead = status.isDead;
            }
            else if (status.bossID == "B_03")
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
[System.Serializable]
public class BossCurrentStatus
{
    public string bossID;
    public bool isDead;
    public Vector3 bossPos;
}
