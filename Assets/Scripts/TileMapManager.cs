﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMapManager : MonoBehaviour
{
    static TileMapManager instance;
    public static TileMapManager Instance
    {
        get
        {
            if (!instance)
            {
                instance = FindObjectOfType<TileMapManager>();
                if (!instance)
                {
                    GameObject container = new GameObject();
                    container.name = "TileMapManager";
                    instance = container.AddComponent<TileMapManager>();
                }
            }
            return instance;
        }
    }
    [Header("Total Map Size")]
    public int width = 0;
    public int height = 0;
    [Space]
    public Vector2 tileSize = new Vector2(1,1);
    [Space]
    public Vector2 offset = new Vector2(1, 1);

    public List<List<Tile>> tilemap = new List<List<Tile>>();

    public Tile tilePrefab;
    
    public Tile GetTile(int x,int y)
    {
        if(x>width || y>height)
        {
            Debug.Log("타일맵 인덱스 초과 (" + width + "," + height + ")" + " 검색인덱스 (" + x + "," + y + ")");
            return null;
        }
        else
        {
            return tilemap[x][y];
        }
    }

    void Initialize()
    {
        float pixelPerUnit = tilePrefab.GetComponent<SpriteRenderer>().sprite.pixelsPerUnit;
        Vector2 imageSize = tilePrefab.GetComponent<SpriteRenderer>().sprite.rect.size;
        for (int indexX=0;indexX<width;indexX++)
        {
            tilemap.Add(new List<Tile>());
            for (int indexY = 0; indexY < width; indexY++)
            {
                tilemap[indexX].Add(Instantiate(tilePrefab, new Vector2(indexX * (imageSize.x * tileSize.x + offset.x), indexY * (imageSize.y * tileSize.y + offset.y)), new Quaternion(0, 0, 0, 0), transform));
                tilemap[indexX][indexY].SetCoordinate(indexX, indexY);
                tilemap[indexX][indexY].transform.localScale = tileSize * pixelPerUnit;
            }
        }
    }
    void Start ()
    {
        Initialize();
    }
}