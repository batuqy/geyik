using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManeger : MonoBehaviour
{
    public GameObject[] cardPrefabs;
    
    public int cardNumber=3;
    
    public void AddCard()
    {
        int carPositionX = 0;
        for (int i = 0; i < cardNumber; i++)
        {
            

            int cardIndex = Random.Range(0, cardPrefabs.Length);
            Instantiate(cardPrefabs[cardIndex], new Vector2(carPositionX, -3), cardPrefabs[cardIndex].transform.rotation);
            if (carPositionX < 0)
            {
                carPositionX = carPositionX * -1;
            }
            else
            {
                carPositionX += 2;
                carPositionX = carPositionX * -1;
            }
        }

    }
        
}
