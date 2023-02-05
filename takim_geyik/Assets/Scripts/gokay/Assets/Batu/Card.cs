using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace CardTypeNameSpace {
    public enum CardType
    {
        BUILDING,
        BONUS,
        ADDINGTILE,
        CURSED,
        HOLY
    }
}

public class Card : MonoBehaviour
{
    Vector2 StartPosition;

    public string cardName;

    public CardTypeNameSpace.CardType cardType;

    public bool isDraggable;

    void Start()
    {
        StartPosition = transform.position;
    }
    Vector2 differnce = Vector2.zero;
    private void OnMouseDown()
    {
        AudioManager.Instance.Play("CardPlay");

        if (isDraggable == true)
        {
            differnce = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - (Vector2)transform.position;
        }


        Color32 cardColor = transform.GetComponent<SpriteRenderer>().color;
        cardColor.a = 60;
        transform.GetComponent<SpriteRenderer>().color = cardColor;



       






    }
    private void OnMouseDrag()
    {
        if (isDraggable == true)
        {
            transform.position = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - differnce;
        }


    }
    private void OnMouseUp()
    {
        Color32 cardColor = transform.GetComponent<SpriteRenderer>().color;
        cardColor.a = 255;
        transform.GetComponent<SpriteRenderer>().color = cardColor;

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

            if (TilemapManager.Instance.getTileBlock(Camera.main.ScreenToWorldPoint(Input.mousePosition)) == true) {

                OnCardInterraction();


            }


        }


     

    }

    public void OnCardInterraction() {
        if (cardType == CardTypeNameSpace.CardType.ADDINGTILE) {
            TilemapManager.Instance.RandomlyAddTile();
        }
        GameManagement.Instance.SkipOneTurn();
        CardManager.Instance.DeleteDeck();

        Destroy(this);
    }

}


