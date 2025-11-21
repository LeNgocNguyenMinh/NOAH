using System.Collections.Generic;
using UnityEngine;

public class BossSaveData : MonoBehaviour
{
    public static BossSaveData Instance;
    public bool aoBossDefeat = false;
    public bool fkBossDefeat = false;

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
        List<BossCurrentStatus> bossStatusList = new List<BossCurrentStatus>();

        BossCurrentStatus aoStatus = new BossCurrentStatus
        {
            bossID = "AO_BOSS",
            isDefeated = aoBossDefeat
        };
        bossStatusList.Add(aoStatus);

        BossCurrentStatus fkStatus = new BossCurrentStatus
        {
            bossID = "FK_BOSS",
            isDefeated = fkBossDefeat
        };
        bossStatusList.Add(fkStatus);

        return bossStatusList;
    }
    public void SetBossCurrentStatus(List<BossCurrentStatus> bossCurrentStatuses)
    {
        foreach (var status in bossCurrentStatuses)
        {
            if (status.bossID == "AO_BOSS")
            {
                aoBossDefeat = status.isDefeated;
            }
            else if (status.bossID == "FK_BOSS")
            {
                fkBossDefeat = status.isDefeated;
            }
        }
    }
    public void SetBossDefeated(string bossID)
    {
        if (bossID == "AO_BOSS")
        {
            aoBossDefeat = true;
        }
        else if (bossID == "FK_BOSS")
        {
            fkBossDefeat = true;
        }
    }
}
public class BossCurrentStatus
{
    public string bossID;
    public bool isDefeated;
}
