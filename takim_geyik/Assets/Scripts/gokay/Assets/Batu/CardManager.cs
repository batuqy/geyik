using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardManager : MonoBehaviour
{
    public GameObject[] cardPrefabs;

    public GameObject[] cursedCardPrefabs;

    public Sprite onClickSprite;
    public Sprite normalSprite;

    public Button deckButton;

    public int cardNumber = 3;

    public int cursedCardNumber = 0;

    public List<GameObject> pulledDeck = new List<GameObject>();

    public List<GameObject> addedCards = new List<GameObject>();
    public List<GameObject> cursedCards = new List<GameObject>();

    //public List<GameObject> cursedCards = new List<GameObject>();

    public static CardManager Instance { get; private set; }
    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
           
            
        }
    }
    
    
    public void OnDeckInterraction()
    {
        DeletePulledDeck();

        GetRandomCards();

        InstantiatePulledDeck();

        AudioManager.Instance.Play("DeckShuffle");
 
    }

    public void GetRandomCards() 
    {
        if (cursedCards.Count != 0)
        {
            foreach (GameObject cursedCard in cursedCards)
            {
                pulledDeck.Add(cursedCard);
            }

            for (int i = 0; i < (cardNumber - cursedCards.Count); i++) {
                int cardIndex = Random.Range(0, cardPrefabs.Length);
                pulledDeck.Add(cardPrefabs[cardIndex]);
            }

        }

        else if (cursedCards.Count == 0) {
            for (int i = 0; i < cardNumber; i++)
            {
                int cardIndex = Random.Range(0, cardPrefabs.Length);
                pulledDeck.Add(cardPrefabs[cardIndex]);
            }
       
        }

    }

    public void DeleteDeck() {

        if (addedCards.Count != 0)
        {
       
            foreach (GameObject card in addedCards)
            {
                Destroy(card);
            }
            addedCards.Clear();
            
        }

    }


  

    public void InstantiatePulledDeck() {

        float cardPositionX = 0f;
       
        foreach (GameObject pulledCard in pulledDeck) {
            GameObject card = Instantiate(pulledCard, new Vector2(cardPositionX, -3), pulledCard.transform.rotation);

            addedCards.Add(card);

            if (cardPositionX < 0)
            {
                cardPositionX = cardPositionX * -1;
            }
            else
            {
                cardPositionX += 2.15f;
                cardPositionX = cardPositionX * -1;
            }

        }
    }

    public void DeletePulledDeck() {

        if (pulledDeck.Count != 0)
        {
            pulledDeck.Clear();
        }

        if(addedCards.Count != 0) { 
        foreach (GameObject card in addedCards)
        {
         Destroy(card);
         
        }
            addedCards.Clear();
        }

    }

    public void InstantiateCards()
    {


        float cardPositionX = 0f;
        int cardIndex = 0;



        for (int i = 0; i < cardNumber; i++)
        {

            cardIndex = Random.Range(0, cardPrefabs.Length);

            GameObject card = Instantiate(cardPrefabs[cardIndex], new Vector2(cardPositionX, -3), cardPrefabs[cardIndex].transform.rotation);

            addedCards.Add(card);

            if (cardPositionX < 0)
            {
                cardPositionX = cardPositionX * -1;
            }
            else
            {
                cardPositionX += 2.15f;
                cardPositionX = cardPositionX * -1;
            }

        }
    }
 
 
    public void DeleteFromTheCards(GameObject card) {
            addedCards.Remove(card);
    }


    public void HoverButtonSprite(){
        deckButton.image.sprite = onClickSprite;
    }

    public void NormalSprite()
    {
        deckButton.image.sprite = normalSprite;
    }

    public void AddCursedCard() {
        int randomIndex = Random.Range(0, cursedCardPrefabs.Length);
        cursedCards.Add(cursedCardPrefabs[randomIndex]);
    }



}
