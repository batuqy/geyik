using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManeger : MonoBehaviour
{
    public GameObject[] cardPrefabs;
    int x = 0;

    public int cardNumber=3;
    
    public void AddCard()
    {

        for (int i = 0; i < cardNumber; i++)
        {
            int cardIndex = Random.Range(0, cardPrefabs.Length);
            cardIndex = Random.Range(0, cardPrefabs.Length);
            Instantiate(cardPrefabs[cardIndex], new Vector2(x, -3), cardPrefabs[cardIndex].transform.rotation);
            if (x < 0)
            {
                x = x * -1;
            }
            else
            {
                x += 2;
                x = x * -1;
            }
        }

    }
        
}
