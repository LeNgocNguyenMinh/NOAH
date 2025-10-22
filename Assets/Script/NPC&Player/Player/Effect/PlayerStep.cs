using UnityEngine.Tilemaps;
using UnityEngine;

public class PlayerStep : MonoBehaviour
{
    private float timeBtwSpawn;
    [SerializeField]private float startTimeBtwSpawn;
    [SerializeField]private float destroyTime;
    [SerializeField]private GameObject stepOnLandEcho;
    [SerializeField]private GameObject stepOnWaterEcho;
    private GameObject echo;
    public Tilemap[] tilemaps; // Gán trong Inspector hoặc tự động tìm

    void Update()
    {
        if(Player.Instance.RB.linearVelocity.sqrMagnitude > 0.5f)
        {
            if(timeBtwSpawn <= 0)
            {
                Vector3Int cellPos = tilemaps[0].WorldToCell(transform.position);
                /* CheckTile(cellPos); */
                IsOnWater(cellPos);
                SpawnEcho();
            }
            else
            {
                timeBtwSpawn -= Time.deltaTime;
            }
        }
    }
    private void CheckTile(Vector3Int cellPos)
    {
        foreach (var map in tilemaps)
        {
            TileBase tile = map.GetTile(cellPos);
            if (tile != null)
            {
                Debug.Log($"{map.name}: {tile.name}");
            }
        }
        
    }
   private void IsOnWater(Vector3Int cellPos)
    {
        foreach (var map in tilemaps)
        {
            TileBase tile = map.GetTile(cellPos);
            if (tile != null && tile.name == "WaterOnLand")
            {
                echo = stepOnWaterEcho;
                return;
            }
        }
        echo = stepOnLandEcho;
    }
    private void SpawnEcho()
    {
        Vector2 dir = Player.Instance.MoveDirect.normalized;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Quaternion rot = Quaternion.Euler(0, 0, angle);
        GameObject clone = Instantiate(echo, transform.position, rot);
        Destroy(clone, destroyTime);
        timeBtwSpawn = startTimeBtwSpawn;
    }
}
