using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace TileBlockNameSpace
{
    public enum TileBlockType
    {
        HOSTILE,
        FRIENDLY,
        NEUTRAL,
        HOME,
    }
}

public class TilemapManager : MonoBehaviour
{

    public Tilemap groundTilemap;

    public Vector2Int StartingGridSize = new Vector2Int(1, 1);

    public Vector2Int WorldGridSize = new Vector2Int(64, 64);

    public List<TileBlock> tileBlocks = new List<TileBlock>();

    public TileBlock selectedTileBlock;

    public List<Tile> neutralTiles;
    public List<Tile> enemyTiles;
    public List<Tile> friendlyTiles;

    private Color32 originalColor;
    public Color32 tileColor;

    public Vector3Int selectedTilePos;

    public Vector3 mousePos;
    public Vector3Int mousePosToCell;

    public TileBlock homeTile;

    // Start is called before the first frame update
    public static TilemapManager Instance { get; private set; }
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

    void Start()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        mousePosToCell = groundTilemap.WorldToCell(mousePos);
       
        CreateHomeTileGrid();
        UpdateTiles();
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;

        mousePosToCell = groundTilemap.WorldToCell(mousePos);

        OnHoverTile(mousePosToCell);
        SelectTile(mousePosToCell);



        if (Input.GetKeyDown(KeyCode.F))
        {

            RandomlyAddTile();
        }

    }

    void SelectTile(Vector3Int tilePos)
    {
        if (Input.GetMouseButtonDown(0))
        {

            selectedTilePos = tilePos;

            if (groundTilemap.HasTile(selectedTilePos))
            {
                groundTilemap.SetTileFlags(selectedTilePos, TileFlags.None);

                foreach (TileBlock t in tileBlocks)
                {
                    if (t.tilePos == tilePos)
                    {
                        if (selectedTileBlock != null)
                        {
                           // selectedTileBlock.HidePlacableTiles();

                        }
                        selectedTileBlock = t;
                       // selectedTileBlock.ShowPlacableTiles();
                    }
                }

            }

        }

    }

    void OnHoverTile(Vector3Int tilePos)
    {

        if (groundTilemap.HasTile(tilePos))
        {
            if (selectedTilePos != tilePos)
            {
                foreach (TileBlock t in tileBlocks)
                {
                    groundTilemap.SetTileFlags(t.tilePos, TileFlags.None);
                    Color32 unHoverColor = groundTilemap.GetColor(t.tilePos);
                    unHoverColor.a = 255;
                    groundTilemap.SetColor(t.tilePos, unHoverColor);

                }
            }

            groundTilemap.SetTileFlags(tilePos, TileFlags.None);
            Color32 hoverColor = groundTilemap.GetColor(tilePos);
            hoverColor.a = 90;
            groundTilemap.SetColor(tilePos, hoverColor);

            selectedTilePos = tilePos;



        }
        else if (!groundTilemap.HasTile(tilePos))
        {

            foreach (TileBlock t in tileBlocks) {
                groundTilemap.SetTileFlags(t.tilePos, TileFlags.None);
                Color32 unHoverColor = groundTilemap.GetColor(t.tilePos);
                unHoverColor.a = 255;
                groundTilemap.SetColor(t.tilePos, unHoverColor);
            }

        }




    }

    void CreateHomeTileGrid()
    {
        for (int i = 0; i < StartingGridSize.x; i++)
        {
            for (int j = 0; j < StartingGridSize.y; j++)
            {
                Vector3Int currentPos = new Vector3Int(i, j);
                tileBlocks.Add(new TileBlock(currentPos, groundTilemap.GetTile(currentPos), neutralTiles[0], groundTilemap, TileBlockNameSpace.TileBlockType.NEUTRAL));
            }
        }
    }

    void UpdateTiles()
    {
        foreach (TileBlock t in tileBlocks)
        {
            foreach (TileBlock tNeighbour in tileBlocks)
            {
                t.SetNeighbour(tNeighbour);
            }

        }
    }



    /* public void AddRightTile(TileBase t)
     {

         if (selectedTileBlock.rightBlock == null)
         {
             groundTilemap.SetTile(selectedTileBlock.tilePos + Vector3Int.right, t);
             TileBlock addedTileBlock = new TileBlock(selectedTileBlock.tilePos + Vector3Int.right, t, neutralTiles[0], groundTilemap);
             tileBlocks.Add(addedTileBlock);

         }

     }
     public void AddLeftTile(TileBase t)
     {
         if (selectedTileBlock.leftBlock == null)
         {
             groundTilemap.SetTile(selectedTileBlock.tilePos + Vector3Int.left, t);
             TileBlock addedTileBlock = new TileBlock(selectedTileBlock.tilePos + Vector3Int.left, t, neutralTiles[0], groundTilemap);
             tileBlocks.Add(addedTileBlock); 
         }

     }

     public void AddUpTile(TileBase t)
     {
         if (selectedTileBlock.upBlock == null)
         {
             groundTilemap.SetTile(selectedTileBlock.tilePos + Vector3Int.up, t);
             TileBlock addedTileBlock = new TileBlock(selectedTileBlock.tilePos + Vector3Int.up, t, neutralTiles[0], groundTilemap);
             tileBlocks.Add(addedTileBlock);
         }
     }
     public void AddDownTile(TileBase t)
     {
         if (selectedTileBlock.downBlock == null)
         {
             groundTilemap.SetTile(selectedTileBlock.tilePos + Vector3Int.down, t);
             TileBlock addedTileBlock = new TileBlock(selectedTileBlock.tilePos + Vector3Int.down, t, neutralTiles[0], groundTilemap);
             tileBlocks.Add(addedTileBlock);
         }
     }*/

    public void RandomlyAddTile()
    {
        TileBlock randomTile = tileBlocks[Random.Range(0, tileBlocks.Count)];
        TileBlockNameSpace.TileBlockType blockType = TileBlockNameSpace.TileBlockType.NEUTRAL;
        Tile tile = neutralTiles[Random.Range(0, neutralTiles.Count)];
        TileBase tbase = neutralTiles[Random.Range(0, neutralTiles.Count)];

        float typeChance = Random.Range(0.0f, 1.0f);

        if (typeChance <= 0.25f) {
            blockType = TileBlockNameSpace.TileBlockType.FRIENDLY;
            tile = friendlyTiles[Random.Range(0,friendlyTiles.Count)];
            tbase = friendlyTiles[Random.Range(0, friendlyTiles.Count)];
        }

        else if (0.25f < typeChance && typeChance < 0.75f)
        {
            blockType = TileBlockNameSpace.TileBlockType.NEUTRAL;
            tile = neutralTiles[Random.Range(0, neutralTiles.Count)];
            tbase = neutralTiles[Random.Range(0, neutralTiles.Count)];
        }

        else if (typeChance >= 0.75f)
        {
            blockType = TileBlockNameSpace.TileBlockType.HOSTILE;
            tile = enemyTiles[Random.Range(0, enemyTiles.Count)];
            tbase = enemyTiles[Random.Range(0, enemyTiles.Count)];
        }


        if (randomTile.leftBlock == null || randomTile.rightBlock == null || randomTile.upBlock == null || randomTile.rightBlock == null)
        {
            List<Vector3Int> placablePos = new List<Vector3Int>();

            if (randomTile.rightBlock == null) {
                placablePos.Add(randomTile.tilePos + Vector3Int.right);
            }
            if (randomTile.leftBlock == null)
            {
                placablePos.Add(randomTile.tilePos + Vector3Int.left);
            }

            if (randomTile.upBlock == null)
            {
                placablePos.Add(randomTile.tilePos + Vector3Int.up);
            }

            if (randomTile.downBlock == null)
            {
                placablePos.Add(randomTile.tilePos + Vector3Int.down);
            }

            Vector3Int selectedPlacableTile = placablePos[Random.Range(0, placablePos.Count)];

            groundTilemap.SetTile(selectedPlacableTile,tbase);

            TileBlock placedTile = new TileBlock(selectedPlacableTile, tbase, tile, groundTilemap,blockType);
            tileBlocks.Add(placedTile);
            UpdateTiles();
        }


        else {
            Debug.Log("No Placable Tile");
            RandomlyAddTile();
        }



    }

    public void AddSpecificTile()
    {

/*
        TileBlock randomTile = tileBlocks[Random.Range(0, tileBlocks.Count)];

        if (randomTile.leftBlock == null || randomTile.rightBlock == null || randomTile.upBlock == null || randomTile.rightBlock == null)
        {
            List<Vector3Int> placablePos = new List<Vector3Int>();

            if (randomTile.rightBlock == null)
            {
                placablePos.Add(randomTile.tilePos + Vector3Int.right);
            }
            if (randomTile.leftBlock == null)
            {
                placablePos.Add(randomTile.tilePos + Vector3Int.left);
            }

            if (randomTile.upBlock == null)
            {
                placablePos.Add(randomTile.tilePos + Vector3Int.up);
            }

            if (randomTile.downBlock == null)
            {
                placablePos.Add(randomTile.tilePos + Vector3Int.down);
            }
            Vector3Int selectedPlacableTile = placablePos[Random.Range(0, placablePos.Count)];

            groundTilemap.SetTile(selectedPlacableTile, neutralTiles[0]);

            TileBlock placedTile = new TileBlock(selectedPlacableTile, groundTilemap.GetTile(selectedPlacableTile), neutralTiles[0], groundTilemap);
            tileBlocks.Add(placedTile);
            UpdateTiles();
        }


        else
        {
            Debug.Log("No Placable Tile");
            RandomlyAddTile();
        }*/
    }

    public void ObtainResources() {
        foreach (TileBlock t in tileBlocks) {
            if (t.TileBlockType == TileBlockNameSpace.TileBlockType.FRIENDLY) {
                ResourceManagement.Instance.AddResource(t.tileResourceType);
                FadingText.Create(groundTilemap.CellToWorld(t.tilePos),ResourceManagement.Instance.GetIncreaseMultiplier(t.tileResourceType));
            }
        }
    }

    public bool getTileBlock(Vector3 pos) {
        pos.z = 0;
        bool isFound = false;
        foreach (TileBlock t in tileBlocks) {
            if (groundTilemap.WorldToCell(pos) == t.tilePos) {
                Debug.Log("There is a Tile");
                Debug.Log("Mouse Tile Pos" + groundTilemap.WorldToCell(pos));
                Debug.Log("Pos" + pos);
                isFound = true; 
                print(isFound);
            }
        }
        return isFound;
    }

    public void ShowRelatedTiles(TileBlockNameSpace.TileBlockType tileBlockType){

        foreach (TileBlock t in tileBlocks)
        {
            if (tileBlockType == t.TileBlockType)
            {
                groundTilemap.SetColor(t.tilePos,Color.red);
             
            }
            else
            {
                Debug.Log("There is no related tile.");
            }                  
        }
    }

    public void HideRelatedTiles(TileBlockNameSpace.TileBlockType tileBlockType) {
        foreach (TileBlock t in tileBlocks)
        {
            if (tileBlockType == t.TileBlockType)
            {
                groundTilemap.SetColor(t.tilePos, t.tileColor);
            }
            else
            {
                Debug.Log("There is no related tile.");
            }
        }
    }


    public void ShowResourceTiles(ResourceNameSpace.ResourceType resourceType)
    {

        foreach (TileBlock t in tileBlocks)
        {
            if (resourceType == t.tileResourceType)
            {
                print("RELATED TILES" + t.tilePos);
                groundTilemap.SetColor(t.tilePos, Color.red);
            }
            else
            {
                Debug.Log("There is no related tile.");
            }
        }
    }

    public void HideResourceTiles(ResourceNameSpace.ResourceType resourceType)
    {
        foreach (TileBlock t in tileBlocks)
        {
            if (resourceType == t.tileResourceType)
            {
                groundTilemap.SetColor(t.tilePos, t.tileColor);
            }
            else
            {
                Debug.Log("There is no related tile.");
            }
        }
    }

    public int GetTotalAmountOfResourceTiles(ResourceNameSpace.ResourceType resourceType) {
        int amount = 0;
        foreach (TileBlock t in tileBlocks) {
            if (t.tileResourceType == resourceType)
            {
                amount++;   
            }


        
        }

        return amount;
    }














}
