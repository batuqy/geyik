using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public GameObject[] cardPrefabs;
    
    public int cardNumber=3;
    List<GameObject> addedCard =new List<GameObject>();
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {


            DeleteCard();

        }
    }
    public void DeleteCard()
    {
        if (addedCard.Count > 0)
        {
            foreach (GameObject Card in addedCard)
            {
                Destroy(Card);
            }
        }
    }
    public void AddCard()
    {
        
        if (addedCard.Count>0)
        {
            foreach (GameObject Card in addedCard)
            {
                Destroy(Card);
            }
        }
        
        int carPositionX = 0;
        for (int i = 0; i < cardNumber; i++)
        {
            

            int cardIndex = Random.Range(0, cardPrefabs.Length);
            GameObject Card= Instantiate(cardPrefabs[cardIndex], new Vector2(carPositionX, -3), cardPrefabs[cardIndex].transform.rotation);
            addedCard.Add(Card);

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
