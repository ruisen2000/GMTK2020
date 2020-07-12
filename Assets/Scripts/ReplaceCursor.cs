using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplaceCursor : MonoBehaviour
{
    Vector2 mouse;
    int w = 32;
    int h = 32;
    public Texture2D handClosed;
    bool isHandClosed = false;

    private void Start()
    {
        Cursor.visible = true;
    }

    private void Update()
    {
        mouse = new Vector2(Input.mousePosition.x, Screen.height - Input.mousePosition.y);
        if (Input.GetMouseButton(0))
        {
            if (!isHandClosed)
            {
                Cursor.SetCursor(handClosed, Vector2.zero, CursorMode.Auto);
                isHandClosed = true;
            }

        } else {
            if (isHandClosed)
            {
                Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
                isHandClosed = false;
            }
        }
    }
    void OnMouseExit()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }
}
