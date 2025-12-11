using System.Collections.Generic;
using UnityEngine;

public class BossSaveData : MonoBehaviour
{
    public static BossSaveData Instance;
    public List<GameObject> bossPrefab;
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
            bossPos = new Vector3(-14.79f, 69.27f, 0.0f )
        };
        bossStatus.Add(aoStatus);

        BossCurrentStatus fkStatus = new BossCurrentStatus
        {
            bossID = "B_03",
            isDead = FKBoss.Instance.IsDead,
            bossPos = new Vector3(118, 4.4f, 0.0f)
        };
        bossStatus.Add(fkStatus);

        return bossStatus;
    }
    public void SetBossCurrentStatus(List<BossCurrentStatus> bossCurrentStatus)
    {
        AOBoss.Instance = null;
        FKBoss.Instance = null;
        foreach (GameObject boss in bossPrefab)
        {
            Destroy(boss);
        }
        
        bossPrefab.Clear();
        Debug.Log(bossCurrentStatus.Count);
        for(int i = 0; i < bossCurrentStatus.Count; i++)
        {
            if(!bossCurrentStatus[i].isDead)
            {
                Debug.Log("43434343434");
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
