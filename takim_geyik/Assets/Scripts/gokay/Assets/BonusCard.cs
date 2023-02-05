using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BonusCard:MonoBehaviour
{
    public ResourceNameSpace.ResourceType bonusResourceType;

    public enum BonusType { 
    ONLY,
    EACHTURN
    }
    public BonusType bonusType;

    Vector2 StartPosition;

    public string cardName;

    public int buffAmount = 1;

    public CardTypeNameSpace.CardType cardType;

    public bool isDraggable;

    TextMeshPro textMesh;

    private void Awake()
    {
        textMesh = transform.GetChild(0).GetComponent<TextMeshPro>();

        if(bonusType == BonusType.EACHTURN) { 
        SetCardResource();
        textMesh.SetText("Use this card to obtain +1 bonus gain on" + bonusResourceType.ToString() + "resources.");
        }

        if (bonusType == BonusType.EACHTURN)
        {    
        textMesh.SetText("Gain from your " + bonusResourceType.ToString()+" tiles x"+buffAmount+ "per once.");
        }




    }

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

           
            TilemapManager.Instance.ShowResourceTiles(bonusResourceType);
           

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

            if (TilemapManager.Instance.getTileBlock(Camera.main.ScreenToWorldPoint(Input.mousePosition)) == true)
            {


                    TilemapManager.Instance.HideResourceTiles(bonusResourceType);
                

                OnCardInterraction();


            }


        }




    }

    public void SetCardResource()
    {


        int resourceIndex = Random.Range(0, 4);
        if (resourceIndex == 0)
        {
            bonusResourceType = ResourceNameSpace.ResourceType.LIGHT;
        }
        else if (resourceIndex == 1)
        {
            bonusResourceType = ResourceNameSpace.ResourceType.MAGIC;
        }
        else if (resourceIndex == 2)
        {

            bonusResourceType = ResourceNameSpace.ResourceType.WATER;
        }

        else if (resourceIndex == 3)
        {
            bonusResourceType = ResourceNameSpace.ResourceType.WORSHIPPER;

        }




    }

    public void OnCardInterraction()
    {
        if (bonusType == BonusType.EACHTURN)
        {
            ResourceManagement.Instance.IncreaseResourceMultiplier(bonusResourceType, buffAmount);
        }
        else if (bonusType == BonusType.ONLY) {
            ResourceManagement.Instance.AddResourceTotal(bonusResourceType,buffAmount*TilemapManager.Instance.GetTotalAmountOfResourceTiles(bonusResourceType));
        }

        GameManagement.Instance.SkipOneTurn();
        CardManager.Instance.DeleteDeck();

        Destroy(this);
    }

}
