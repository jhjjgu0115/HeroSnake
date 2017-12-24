using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public partial class Unit : MonoBehaviour
{
    [SerializeField]
    public Coordinate coordinate;
    public Direction direction;
    public int movePoint;
    public bool MoveToPosition(Coordinate coordinate)
    {
        Tile targetTile = TileMapManager.GetTile(coordinate);
        
        if(targetTile!=null)
        {
            if(targetTile.unit==null)
            {
                TileMapManager.GetTile(this.coordinate).unit = null;
                transform.position = targetTile.transform.position;
                targetTile.unit = this;
                this.coordinate = coordinate;
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

    public void Rebirth()
    {

    }
    public void Dead()
    {
        Debug.Log(name + " 쥬금 ");
    }

    public int hp;
    public List<AttackInfo> mainAttackInfoList = new List<AttackInfo>();
    public List<AttackInfo> subAttackInfoList = new List<AttackInfo>();
    public void MainAttack()
    {
        foreach(AttackInfo attackinfo in mainAttackInfoList)
        {
            foreach(Coordinate coord in attackinfo.coordList)
            {
                Coordinate tmpCoord = Rotate(coord, GetAngle(directionToCoordinate));
                GiveDamage(tmpCoord, attackinfo.damage,attackinfo.color);
            }
        }
    }
    public void SubAttack()
    {
        foreach (AttackInfo attackinfo in mainAttackInfoList)
        {
            foreach (Coordinate coord in attackinfo.coordList)
            {
                Coordinate tmpCoord = Rotate(coord, GetAngle(directionToCoordinate));
                GiveDamage(tmpCoord, attackinfo.damage,attackinfo.color);
            }
        }
    }

    public void MainSkill()
    {
    }
    public void SubSkill()
    {

    }
    
    public bool GiveDamage(Coordinate coordinate, int damage,Color32 color)
    {
        Tile targetTile = TileMapManager.GetTile(coordinate+this.coordinate);
        if(targetTile!=null)
        {
            targetTile.DebugColorChange(color);
            if (targetTile.unit!=null)
            {
                targetTile.unit.GetDamage(damage);
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
    
}
public partial class Unit : MonoBehaviour
{
    Coordinate directionToCoordinate
    {
        get
        {
            switch (direction)
            {
                case Direction.Up:
                    return new Coordinate(0, 1);
                case Direction.Left:
                    return new Coordinate(-1, 0);
                case Direction.Down:
                    return new Coordinate(0, -1);
                case Direction.Right:
                    return new Coordinate(1, 0);
                default:
                    return new Coordinate(0, 0);
            }
        }
    }
    [Serializable]
    public class AttackInfo
    {
        public List<Coordinate> coordList = new List<Coordinate>();
        public int damage;
        public Color32 color = new Color32();
    }
    int GetAngle(Coordinate vEnd)
    {
        Coordinate v = vEnd - Coordinate.Zero;

        return (int)(Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg);
    }
    public void Initialze()
    {

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
    /*public bool MoveToDirection(Direction direction, int _movePoint)
    {
        TileMapManager tm = TileMapManager.Instance;
        this.direction = direction;
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
        Coordinate resultPosition = coordinate;
        Coordinate lastPosition = coordinate;

        for (int point = 1; point <=movePoint; point++)
        {
            Tile t = TileMapManager.GetTile(nextPosition * point + coordinate);
            if (t)
            {
                if (t.unit == null)
                {
                    lastPosition = resultPosition;
                    resultPosition = nextPosition * point + coordinate;
                }
                else
                {
                    break;
                }
            }
            else
            {
                break;
            }
        }
        MoveToPosition(resultPosition);
        AddTrace(lastPosition);
        return true;

    }
    public Coordinate coordDirection
    {
        get
        {
            Coordinate direction;
            switch (this.direction)
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
            return coordinate - coordDirection;
        }
    }*/
}