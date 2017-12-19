using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    static List<Sprite> tileSprites=new List<Sprite>();
    SpriteRenderer spriteRenderer;

    [SerializeField]
    int x=0;
    [SerializeField]
    int y = 0;
    public int X
    {
        get
        {
            return x;
        }
    }
    public int Y
    {
        get
        {
            return y;
        }
    }

    public EGround groundInfo=EGround.Ground;
    public List<GameObject> interactableList = new List<GameObject>();
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
        spriteRenderer = GetComponent<SpriteRenderer>();
        tileSprites.AddRange(Resources.LoadAll<Sprite>("Sprites/Grounds"));
    }
    void OnEnable()
    {
    }
    void Start ()
    {
    }
    public void SetCoordinate(int x, int y)
    {
        this.x = x;
        this.y = y;
    }
    [ContextMenu("SetGround")]
    public void SetGroundInfo(EGround groundInfo)
    {
        this.groundInfo = groundInfo;
        spriteRenderer.sprite = tileSprites.Find(i => i.name == groundInfo.ToString());
    }

}
