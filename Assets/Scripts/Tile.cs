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

    public E_Ground groundInfo=E_Ground.Ground;
    public List<GameObject> objectContainer = new List<GameObject>();
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
    public void SetGroundInfo(E_Ground groundInfo)
    {
        this.groundInfo = groundInfo;
        spriteRenderer.sprite = tileSprites.Find(i => i.name == groundInfo.ToString());
    }
    public void DebugColorChange(Color32 color)
    {
        spriteRenderer.color = color;
    }
}
