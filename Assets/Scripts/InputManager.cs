using System.Collections;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;

    public Collider2D curCollider = null;
    private void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
    }
    public IEnumerator SelectCollider()
    {
        bool isSelected = false;
        SpriteRenderer sp = null;
        SpriteRenderer prevSp = null;
        Collider2D prevCollider = null;
        Color originalColor = Color.white;

        while (!isSelected)
        {
            Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mouseWorldPos, Vector2.zero);

            //curCollider is public variable
            curCollider = hit.collider;

            if (curCollider != null)
            {
                sp = curCollider.GetComponent<SpriteRenderer>();

                if (curCollider != prevCollider)
                {
                    originalColor = sp.color;
                }

                Color hoverColor = Color.yellow;
                hoverColor.a = 0.5f;

                sp.color = hoverColor;
                prevCollider = curCollider;
                prevSp = prevCollider.GetComponent<SpriteRenderer>();

                if (Input.GetMouseButtonDown(0))
                {
                    if (hit.collider != null)
                    {
                        Debug.Log($"Selected object:{hit.collider.name}");
                        curCollider = hit.collider;
                        prevSp.color = originalColor;
                        isSelected = true;
                    }
                }
                
            }
            if (prevCollider != null && prevCollider != curCollider)
            {
                prevSp.color = originalColor;
            }
            yield return null;
        }

    }

    
}
