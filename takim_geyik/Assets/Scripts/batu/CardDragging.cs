using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDragging : MonoBehaviour
{
    Vector2 StartPosition;

    bool isDraggable;

    void Start()
    {
        StartPosition = transform.position;
    }
    Vector2 differnce = Vector2.zero;
    private void OnMouseDown()
    {
        if (isDraggable == true)
        {
            differnce = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - (Vector2)transform.position;
        }        
        if(!isDraggable == false)
        {
            GetComponent<>
        }

    }
    private void OnMouseDrag()
    {
        if(isDraggable == true) { 
        transform.position = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - differnce;
        }


    }
    private void OnMouseUp()
    {

        //float desiredDuration = 3f;
        // float elapsedTime=0 ;
        if (isDraggable == true)
        {
            if (transform.position.x != StartPosition.x || transform.position.y != StartPosition.y)
            {

                transform.position = Vector3.Lerp(transform.position, StartPosition, 1);

                // elapsedTime +=Time.deltaTime;
                //float percentage = elapsedTime * desiredDuration;

            }
        }
    }

}
