using UnityEngine;
using UnityEngine.Tilemaps;

public class TileManager : MonoBehaviour
{
    public static TileManager Instance;

    public Tilemap tilemap;
    public Vector3Int startPos;
    public Vector3 tileOffset = new Vector3(0.5f, 0.5f, 0);

    private void Awake()
    {   
        Instance = this;
        startPos = tilemap.WorldToCell(Vector3.zero);
    }

    public void TileCheck(Vector3 worldPos)
    {
        Vector3Int currentPos = tilemap.WorldToCell(worldPos);
        TileBase tile = tilemap.GetTile(currentPos);

        if (tile is CustomTile customTile)
        {
            Debug.Log($"Tile type:{customTile.tileType} Tile location:{currentPos}");
        }
    }
    public TileBase FindCurrentTile(Vector3 worldPos)
    {
        Vector3Int currentPos = tilemap.WorldToCell(worldPos);
        TileBase tile = tilemap.GetTile(currentPos);
        return tile;
    }
}
