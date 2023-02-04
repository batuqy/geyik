using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDragging : MonoBehaviour
{
    Vector2 StartPosition;
    void Start()
    {
    StartPosition=transform.position;

    }
    Vector2 differnce = Vector2.zero;
    private void OnMouseDown()
    {
        differnce = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) -(Vector2) transform.position;
        
    }
    private void OnMouseDrag()
    {
        transform.position = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - differnce;
       // transform.position = Vector2.Lerp(s);
    }
}
