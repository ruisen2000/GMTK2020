using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turn : MonoBehaviour
{
    public float speed = 5f;

    bool mouseDown = false;
    Vector2 startPos = Vector2.zero;
    float delta = 0;

    private void Update()
    {        
        if (Input.GetMouseButton(0))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (mouseDown || GetComponent<Collider2D>().OverlapPoint(mousePosition))
            {
                Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
                Vector2 oldDirection = startPos;
                startPos = direction;

                if (!mouseDown)
                {
                    oldDirection = direction;
                    mouseDown = true;
                }

                float angle = -Vector2.SignedAngle(direction, oldDirection);
                transform.eulerAngles = new Vector3(0, 0, angle + transform.eulerAngles.z);
                delta += angle;
            }
            
        }
        else
        {
            mouseDown = false;
        }


    } 
}
