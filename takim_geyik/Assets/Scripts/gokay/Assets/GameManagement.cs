using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagement : MonoBehaviour
{

    public int turn;

    public static GameManagement Instance { get; private set; }
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


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S)) {
            turn++;
            SkipOneTurn();
        }
    }

    public void SkipOneTurn() {
     
       TimelineManager.Instance.ProgressTime();
       TilemapManager.Instance.ObtainResources();
        if (TimelineManager.Instance.elapsedTurn != 0 && TimelineManager.Instance.elapsedTurn % 7 == 0) {
            CardManager.Instance.AddCursedCard();
        }
         
    }
}
