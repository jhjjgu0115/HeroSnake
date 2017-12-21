using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public partial class Unit : MonoBehaviour
{
    public int myPartyIndex;
    public List<Unit> partyList;
    bool isLeader = false;
    [Serializable]
    public struct DamageInfo
    {
        public List<Coordinate> area;
        public int damage;
        
        public DamageInfo(int damage)
        {
            area = new List<Coordinate>();
            this.damage = damage;
        }
    }
    public List<DamageInfo> damageInfo = new List<DamageInfo>();


    [Header("Coordinate")]
    [SerializeField]
    public Coordinate currentCoordinate;
    public List<Coordinate> traceList = new List<Coordinate>();


    [SerializeField]
    int hp;
    [SerializeField]
    public int movePoint;
    Tile currentTile;
    Direction currentDirection;
    Dictionary<Coordinate, int> damageArea = new Dictionary<Coordinate, int>();

    void Initialize()
    { }
    public void ReBirth()
    {

    }
    public bool MoveToPosition(Coordinate coordinate)
    {
        Tile movePositionTile = TileMapManager.Instance.GetTile(coordinate);

        if(movePositionTile)
        {
            if(movePositionTile.unit==null)
            {
                if (currentTile)
                {
                    currentTile.unit = null;
                }
                transform.position = movePositionTile.transform.position;
                movePositionTile.unit = this;
                AddTrace(currentCoordinate);
                currentCoordinate = coordinate;
                currentTile = movePositionTile;
                return true;
            }
            else
            {
                Debug.Log("Can't Move There Position. Aready Unit exist (" + coordinate.x + "," + coordinate.y + ") - move");
                return false;
            }
            
        }
        else
        {
            Debug.Log("There is NullTile (" + coordinate.x + "," + coordinate.y + ") - move");
            return false;
        }
    }
    public bool MoveToDirection(Direction direction, int _movePoint)
    {
        Coordinate nextPosition;
        switch (direction)
        {
            case Direction.Up:
                nextPosition = new Coordinate(0, 1);
                break;
            case Direction.Left:
                nextPosition = new Coordinate(-1, 0);
                break;
            case Direction.Down:
                nextPosition = new Coordinate(0, -1);
                break;
            case Direction.Right:
                nextPosition = new Coordinate(1, 0);
                break;
            default:
                nextPosition = new Coordinate(0, 0);
                break;
        }
        TileMapManager tm = TileMapManager.Instance;
        for (int point = _movePoint; point > 0; point--)
        {
            Tile t = tm.GetTile(nextPosition * point + currentCoordinate);
            if (t)
            {
                if (t.unit == null)
                {
                    MoveToPosition(nextPosition * point + currentCoordinate);
                    currentDirection = direction;
                    return true;
                }
            }
        }
        return false;

    }
    public Coordinate CurrentDirection
    {
        get
        {
            Coordinate direction;
            switch (currentDirection)
            {
                case Direction.Up:
                    direction = new Coordinate(0, 1);
                    break;
                case Direction.Left:
                    direction = new Coordinate(-1, 0);
                    break;
                case Direction.Down:
                    direction = new Coordinate(0, -1);
                    break;
                case Direction.Right:
                    direction = new Coordinate(1, 0);
                    break;
                default:
                    direction = new Coordinate(0, 0);
                    break;
            }
            return direction;
        }
        
    }
    public Coordinate GetBackPosition
    {
        get
        {
            return currentCoordinate-CurrentDirection;
        }
    }
    
    void AddTrace(Coordinate coordinate)
    {
        traceList.Insert(0, coordinate);
        if(traceList.Count>10)
        {
            Debug.Log(name + " " + coordinate + " " + traceList.Count);
            traceList.RemoveAt(10);
        }
    }

    public void Attack()
    {
        foreach(DamageInfo damage in damageInfo)
        {
            foreach(Coordinate coord in damage.area)
            {
                GiveDamage(coord, damage.damage);
            }
        }
    }
    Coordinate Rotate(Coordinate coord, float degree)
    {
        float sin = Mathf.Sin(degree * Mathf.Deg2Rad);
        float cos = Mathf.Cos(degree * Mathf.Deg2Rad);
        

        int tx = coord.x;
        int ty = coord.y;

        coord.x = (int)Math.Round((cos * tx) - (sin * ty));
        coord.y = (int)Math.Round((sin * tx) + (cos * ty));
        return coord;
    }
    public bool GiveDamage(Coordinate coordinate, int damage)
    {
        Tile attackPositionTile = TileMapManager.Instance.GetTile(coordinate);
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
            Debug.Log("There is NullTile (" + coordinate.x + "," + coordinate.y + ") - attack");
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


    void Start ()
    {
        Debug.Log(Rotate(new Coordinate(-1, 1), -90));
    }

    void Update ()
    {
	}
}
[Serializable]
public struct Coordinate
{
    public int x;
    public int y;
    public Coordinate(int x, int y)
    {
        this.x = x;
        this.y = y;
    }
    public Coordinate(Coordinate coordinate)
    {
        x = coordinate.x;
        y = coordinate.y;
    }

    public static Coordinate operator +(Coordinate a, Coordinate b)
    {
        return new Coordinate(a.x + b.x, a.y + b.y);
    }
    public static Coordinate operator -(Coordinate a, Coordinate b)
    {
        return new Coordinate(a.x - b.x, a.y - b.y);
    }
    public static Coordinate operator -(Coordinate a)
    {
        return new Coordinate(-a.x, -a.y);
    }
    public static Coordinate operator *(Coordinate a, int b)
    {
        return new Coordinate(a.x * b, a.y * b);
    }
    public static Coordinate operator *(int a,Coordinate b)
    {
        return new Coordinate(b.x * a, b.y * a);
    }
    public override string ToString()
    {
        return "("+x+ "," + y+")";
    }
}