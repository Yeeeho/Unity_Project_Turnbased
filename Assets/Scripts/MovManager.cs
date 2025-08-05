using System.Collections;
using System.Net;
using Unity.VisualScripting;
using UnityEngine;

public class MovManager : MonoBehaviour
{
    public static MovManager Instance;
    public float moveSpeed = 5.0f;
    private Vector3 targetPos;
    private void Awake()
    {
        Instance = this;
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
                unit.transform.position = targetPos;
            }
        }
    }

}
