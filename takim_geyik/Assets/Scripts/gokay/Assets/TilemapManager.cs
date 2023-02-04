using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

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

    // Start is called before the first frame update

    private void Awake()
    {

    }

    void Start()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        mousePosToCell = groundTilemap.WorldToCell(mousePos);

        CreateTileGrid();
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
                            selectedTileBlock.HidePlacableTiles();

                        }
                        selectedTileBlock = t;
                        selectedTileBlock.ShowPlacableTiles();
                    }
                }

            }

        }

    }

    //HOVER SISTEMI DUZELTILECEK
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
          
            foreach(TileBlock t in tileBlocks) { 
            groundTilemap.SetTileFlags(t.tilePos, TileFlags.None);
            Color32 unHoverColor = groundTilemap.GetColor(t.tilePos);
            unHoverColor.a = 255;
            groundTilemap.SetColor(t.tilePos, unHoverColor);

            }

        }




    }

    void CreateTileGrid()
    {

        for (int i = 0; i < StartingGridSize.x; i++)
        {

            for (int j = 0; j < StartingGridSize.y; j++)
            {
                Vector3Int currentPos = new Vector3Int(i, j);

                groundTilemap.SetTile(currentPos, neutralTiles[0]);
                tileBlocks.Add(new TileBlock(currentPos, groundTilemap.GetTile(currentPos), neutralTiles[0], groundTilemap));
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



    public void AddRightTile(TileBase t)
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
    }

    public void RandomlyAddTile()
    {


        
        TileBlock randomTile = tileBlocks[Random.Range(0, tileBlocks.Count)];

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

            groundTilemap.SetTile(selectedPlacableTile, neutralTiles[0]);

            TileBlock placedTile = new TileBlock(selectedPlacableTile,groundTilemap.GetTile(selectedPlacableTile),neutralTiles[0],groundTilemap);
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

    }








}
