using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Unit : MonoBehaviour
{
    [Header("Coordinate")]
    [SerializeField]
    int x;
    [SerializeField]
    int y;

    public void Move(int x,int y)
    {
        Tile movePositionTile = TileMapManager.Instance.GetTile(x, y);
        if(movePositionTile)
        {
            Debug.Log("CanMove (" + x + "," + y + ")");
        }
        else
        {
            transform.position = movePositionTile.transform.position;
        }
    }
    public void MoveUp()
    {

    }
    public void MoveDown()
    {

    }
    public void MoveLeft()
    {

    }
    public void MoveRight()
    {

    }



    void Start ()
    {
	}
	void Update ()
    {
        // w
        //asd
	}
}
public partial class Unit : MonoBehaviour
{
}
public partial class Unit : MonoBehaviour
{
}
public partial class Unit : MonoBehaviour
{
}
public partial class Unit : MonoBehaviour
{
}
public partial class Unit : MonoBehaviour
{
}
public partial class Unit : MonoBehaviour
{
}
public partial class Unit : MonoBehaviour
{
}
