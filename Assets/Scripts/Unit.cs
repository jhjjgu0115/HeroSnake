using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Unit : MonoBehaviour
{
    [Header("Coordinate")]
    [SerializeField]
    Coordinate coordinate;
    [SerializeField]
    int hp;
    [SerializeField]
    int movePoint;
    Direction currentDirection;
    Dictionary<Coordinate, int> damageArea = new Dictionary<Coordinate, int>();

    void Initialize()
    { }
    public void ReBirth()
    {

    }
    public bool MoveToPosition(int x,int y)
    {
        Tile movePositionTile = TileMapManager.Instance.GetTile(x, y);
        if(movePositionTile)
        {
            transform.position = movePositionTile.transform.position;
            movePositionTile.unit = this;
            return true;
        }
        else
        {
            Debug.Log("There is NullTile (" + x + "," + y + ") - move");
            return false;
        }
    }
    public bool GiveDamage(int x,int y,int damage)
    {
        Tile attackPositionTile = TileMapManager.Instance.GetTile(x, y);
        if(attackPositionTile)
        {
            if(attackPositionTile.unit)
            {
                attackPositionTile.unit.GetDamage(damage);
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            Debug.Log("There is NullTile (" + x + "," + y + ") - attack");
            return false;
        }

    }
    public void GetDamage(int damage)
    {
        hp -= damage;
        if(hp<=0)
        {
            Dead();
        }
    }
    public void Dead()
    {
        //사망처리
    }

    public void MoveToDirection(Direction direction)
    {
        //일단 입력 방향에 대하여 이동이 가는한지 검사.

        Coordinate nextPosition;
        switch (direction)
        {
            case Direction.Up:
                nextPosition = new Coordinate(0, movePoint);
                break;
            case Direction.Left:
                nextPosition = new Coordinate(-movePoint, 0);
                break;
            case Direction.Down:
                nextPosition = new Coordinate(0, -movePoint);
                break;
            case Direction.Right:
                nextPosition = new Coordinate(movePoint, 0);
                break;
        }
    }



    void Start ()
    {
	}
	void Update ()
    {
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

[System.Serializable]
public struct Coordinate
{
    public int x;
    public int y;
    public Coordinate(int x, int y)
    {
        this.x = x;
        this.y = y;
    }
}