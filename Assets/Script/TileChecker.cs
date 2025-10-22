using UnityEngine;
using UnityEngine.Tilemaps;

public class TileChecker : MonoBehaviour
{
    public Tilemap[] tilemaps; // Gán trong Inspector hoặc tự động tìm

    void Update()
    {
        Vector3Int cellPos = tilemaps[0].WorldToCell(transform.position);

        foreach (var map in tilemaps)
        {
            TileBase tile = map.GetTile(cellPos);
            if (tile != null)
            {
                Debug.Log($"{map.name}: {tile.name}");
            }
        }
    }
}