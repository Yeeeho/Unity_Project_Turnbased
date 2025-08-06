using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "Custom Tile", menuName = "Tiles/New Tile")]
public class CustomTile : Tile
{
    public bool isWalkable = true;
    public string tileType = string.Empty;
}
