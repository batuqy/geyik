using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[System.Serializable]
public class TileBlock
{

    public bool isSelected;
    public bool isBuilded;

    public Vector3Int tilePos;
    public TileBase tileBase;

    public TileBlock leftBlock;
    public TileBlock rightBlock;
    public TileBlock upBlock;
    public TileBlock downBlock;

    public TileBlockNameSpace.TileBlockType TileBlockType;

    public ResourceNameSpace.ResourceType tileResourceType;

    public Tilemap attachedTilemap;

    public Tile tile;

    public Color32 tileColor;

    public TileBlock(Vector3Int tilePos, TileBase tileBase, Tile tile, Tilemap attachedTileMap, TileBlockNameSpace.TileBlockType tileBlockType)
    {   
        this.attachedTilemap = attachedTileMap;
        this.tileBase = tileBase;
        this.tilePos = tilePos;
        this.tileColor = tile.color;
        this.TileBlockType = tileBlockType;

        if (tileBlockType == TileBlockNameSpace.TileBlockType.FRIENDLY)
        {
            SetResource();
        }
        
    }

    void Select()
    {
        isSelected = true;
    }

    void OnHover()
    {
        tile.flags = TileFlags.None;
        tileColor.a = 60;
    }

    void UnHover()
    {
        tile.flags = TileFlags.None;
        tileColor.a = 255;
    }

    public void SetNeighbour(TileBlock block)
    {
        if (block.tilePos == (tilePos + Vector3.right))
        {   
                rightBlock = block;
               //Debug.Log("RIGHT");
            
        }

        else if (block.tilePos == (tilePos + Vector3.left))
        {
        
                leftBlock = block;
              //Debug.Log("LEFT");         
        }

        else if (block.tilePos == (tilePos + Vector3.up))
        {
         
                upBlock = block;
               //Debug.Log("UP");
            
        }

        else if (block.tilePos == (tilePos + Vector3.down))
        {
          
                downBlock = block;
               //Debug.Log("DOWN");
          
        }
    }

    public void AddRightTile(TileBase t) {
        if(rightBlock == null) { 
        attachedTilemap.SetTile(tilePos + Vector3Int.right,t);
        }

    }
    public void AddLeftTile(TileBase t)
    {
        if (leftBlock == null)
        {
            attachedTilemap.SetTile(tilePos + Vector3Int.left, t);
        }

    }

    public void AddUpTile(TileBase t)
    {
        if (upBlock == null)
        {
            attachedTilemap.SetTile(tilePos + Vector3Int.up, t);
        }
    }
    public void AddDownTile(TileBase t)
    {
        if (downBlock == null)
        {
            attachedTilemap.SetTile(tilePos + Vector3Int.down, t);
        }
    }

    public void ShowPlacableTiles() {
        if (rightBlock != null) {
            attachedTilemap.SetTileFlags(rightBlock.tilePos, TileFlags.None);
            attachedTilemap.SetColor(rightBlock.tilePos,Color.red);
        }
        
        if (leftBlock != null)
        {
            attachedTilemap.SetTileFlags(leftBlock.tilePos, TileFlags.None);
            attachedTilemap.SetColor(leftBlock.tilePos, Color.red);
        }
        
        if (upBlock != null)
        {
            attachedTilemap.SetTileFlags(upBlock.tilePos, TileFlags.None);
            attachedTilemap.SetColor(upBlock.tilePos, Color.red);
        }
        
        if (downBlock != null)
        {
            attachedTilemap.SetTileFlags(downBlock.tilePos, TileFlags.None);
            attachedTilemap.SetColor(downBlock.tilePos, Color.red);
        }
      
    }

    public void HidePlacableTiles() 
    {
        if (rightBlock != null)
        {
            attachedTilemap.SetTileFlags(rightBlock.tilePos, TileFlags.None);
            attachedTilemap.SetColor(rightBlock.tilePos, tileColor);

        }

        if (leftBlock != null)
        {
            attachedTilemap.SetTileFlags(leftBlock.tilePos, TileFlags.None);
            attachedTilemap.SetColor(leftBlock.tilePos, tileColor);
        }

        if (upBlock != null)
        {
            attachedTilemap.SetTileFlags(upBlock.tilePos, TileFlags.None);
            attachedTilemap.SetColor(upBlock.tilePos, tileColor);
        }

        if (downBlock != null)
        {
            attachedTilemap.SetTileFlags(downBlock.tilePos, TileFlags.None);
            attachedTilemap.SetColor(downBlock.tilePos, tileColor);
        }

    }



    public void SetResource() {
        int resourceIndex = Random.Range(0, 4);
        if (resourceIndex == 0)
        {
            tileResourceType = ResourceNameSpace.ResourceType.LIGHT;
        }
        else if (resourceIndex == 1)
        {
            tileResourceType = ResourceNameSpace.ResourceType.MAGIC;
        }
        else if (resourceIndex == 2) {

            tileResourceType = ResourceNameSpace.ResourceType.WORSHIPPER;
        }

        else if (resourceIndex == 3)
        {
            tileResourceType = ResourceNameSpace.ResourceType.WATER;

        }



    }




}


