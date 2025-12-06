using UnityEngine;
using System.Collections.Generic;

public class WeaponManager : MonoBehaviour
{
    public static WeaponManager Instance;
    [SerializeField] List<WeaponData> weaponList;
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
    public List<WeaponData> GetWeaponData()
    {
        return weaponList;
        
    }
    public void SetWeaponData(List<WeaponData> weaponSaveData)
    {
        //remove all data in weaponList
        weaponList.Clear();
        weaponList = weaponSaveData;
    }
    public void WeaponUpgrade(string weaponID)
    {
        for (int i = 0; i < weaponList.Count; i++)
        {
            if (weaponList[i].weaponID == weaponID)
            {
                weaponList[i].weaponLevel += 1;
                weaponList[i].materialNeedToUpgrade += (int)(weaponList[i].materialNeedToUpgrade * 1.5f);
                weaponList[i].weaponDamage += 10f;
            }
        }
    }
    public WeaponData GetWeaponInfo(string weaponID)
    {
        for (int i = 0; i < weaponList.Count; i++)
        {
            if (weaponList[i].weaponID == weaponID)
            {
                return weaponList[i];
            }
        }
        return null;
    }
}
