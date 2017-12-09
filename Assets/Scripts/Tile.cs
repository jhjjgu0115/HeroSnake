using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public void SetCoordinate(int x,int y)
    {
        this.x = x;
        this.y = y;
    }
    SpriteRenderer spriteRenderer;

    public int x=0;
    public int y=0;

    public EGround groundInfo=EGround.ground;
    List<Item> itemList = new List<Item>();
    public List<Item> ItemList
    {
        get
        {
            return itemList;
        }
    }
    public Unit unit;

    void Awake()
    {
    }
    void OnEnable()
    {
    }
    void Start ()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
