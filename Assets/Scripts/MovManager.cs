using System.Collections;
using System.Net;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MovManager : MonoBehaviour
{
    public static MovManager Instance;
    public Tilemap tilemap;
    public float moveSpeed = 5.0f;
    private Vector3 targetPos;

    private void Awake()
    {
        Instance = this;
        Vector3Int startPos = tilemap.WorldToCell(Vector3.zero);
    }


    public IEnumerator Move(UnitBase unit)
    {
        bool moved = false;
        while (!moved)
        {
            Vector3 direction = Vector3.zero;

            if (Input.GetKeyDown(KeyCode.RightArrow)) { direction = Vector3.right; moved = true; }
            else if (Input.GetKeyDown(KeyCode.LeftArrow)) { direction = Vector3.left; moved = true; }
            else if (Input.GetKeyDown(KeyCode.UpArrow)) { direction = Vector3.up; moved = true; }
            else if (Input.GetKeyDown(KeyCode.DownArrow)) { direction = Vector3.down; moved = true; }
            else { yield return null; continue; }

            if (direction != Vector3.zero)
            {
                targetPos = unit.transform.position + direction;
                TileManager.Instance.TileCheck(targetPos);
                if (!IsMovable(targetPos))
                {
                    Debug.Log("앞으로 갈 수 없습니다.");
                    targetPos = unit.transform.position;
                }
                unit.transform.position = targetPos;
            }
        }
    }

    public bool IsMovable(Vector3 nextPos)
    {
        TileBase tile = TileManager.Instance.FindCurrentTile(nextPos);
        if (tile is CustomTile customTile)
        {
            if (customTile.isWalkable == true) return true;
            else return false;
        }
        return false;
    }

}
