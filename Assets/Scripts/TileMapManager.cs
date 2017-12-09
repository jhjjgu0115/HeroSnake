using System.Collections;
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

    public int width = 0;
    public int height = 0;
    
    public float tileSizeX=0;
    public float tileSizeY=0;

    public List<List<Tile>> tilemap = new List<List<Tile>>();

    public Tile tilePrefab;
    public int offsetX = 0;
    public int offsetY = 0;
    
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

    void Initialized()
    {
        for(int indexX=0;indexX<width;indexX++)
        {
            tilemap.Add(new List<Tile>());
            for (int indexY = 0; indexY < width; indexY++)
            {
                tilemap[indexX].Add(Instantiate(tilePrefab, new Vector2(indexX * tileSizeX +(offsetX*indexX)  , indexY*tileSizeY + offsetY*indexY), new Quaternion(0,0,0,0),transform));
                tilemap[indexX][indexY].SetCoordinate(indexX, indexY);
                //타일 정보 초기화.
                //타일 포지션 초기화 + 오프셋만큼 벌린다.
            }
        }
    }


    // Use this for initialization
    void Start ()
    {
        Initialized();
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
