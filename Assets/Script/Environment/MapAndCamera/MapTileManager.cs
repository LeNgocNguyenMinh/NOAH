using UnityEngine;
using UnityEngine.Tilemaps;

public class MapTileManager : MonoBehaviour
{
    public static MapTileManager Instance;
    [SerializeField]private Tilemap[] mapTiles;
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public Tilemap[] GetTileMapList()
    {
        return mapTiles;
    }
}
