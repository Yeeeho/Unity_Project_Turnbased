using System.Collections;
using System.Transactions;
using Unity.VisualScripting;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;
    public UnitBase selectedUnit = null;
    public IEnumerator MouseSelect()
    {
        bool isSelected = false;
        while (!isSelected)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(mouseWorldPos, Vector2.zero);

                if (hit.collider != null) 
                {
                    Debug.Log($"Selected object:{hit.collider.name}");
                    selectedUnit = hit.collider.GetComponent<UnitBase>();
                    SpriteRenderer sr = hit.collider.GetComponent<SpriteRenderer>();
                    if (sr != null) { sr.color = Color.green; }
                    isSelected = true;
                }
            }
            yield return null;
        }

    }

    private void Awake()
    {
        Instance = this;
    }
}
