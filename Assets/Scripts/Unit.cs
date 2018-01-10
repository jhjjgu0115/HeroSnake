using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public partial class Unit : MonoBehaviour
{
    public void Initialze()
    {

    }

    public int hp;
    public Dictionary<E_ActionList, Skill> skillDict = new Dictionary<E_ActionList, Skill>();

    [SerializeField]
    public Coordinate coordinate;
    public Coordinate directionToCoordinate
    {
        get
        {
            switch (direction)
            {
                case E_Direction.Up:
                    return new Coordinate(0, 1);
                case E_Direction.Left:
                    return new Coordinate(-1, 0);
                case E_Direction.Down:
                    return new Coordinate(0, -1);
                case E_Direction.Right:
                    return new Coordinate(1, 0);
                default:
                    return new Coordinate(0, 0);
            }
        }
    }
    public E_Direction direction;
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
    public event CoordinateEvent BeforeMoveEvent;
    public event CoordinateEvent AfterMoveEvent;

    public void Birth()
    { }
    public void Rebirth()
    {

    }
    public void Dead()
    {
        TileMapManager.GetTile(coordinate).unit = null;
        
        Destroy(gameObject);
    }
    public event UnitEvent BirthEvent;
    public event UnitEvent RebirthEvent;
    public event UnitEvent DeadEvent;

    public void Act(E_ActionList action)
    {
        if(skillDict.ContainsKey(action))
        {
            skillDict[action].Cast(this);
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

    private void Start()
    {
        foreach(Skill skill in GetComponentsInChildren<Skill>())
        {
            skillDict.Add(skill.skillType, skill);
        }
    }
}