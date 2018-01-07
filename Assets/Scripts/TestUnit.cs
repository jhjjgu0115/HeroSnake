using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestUnit : MonoBehaviour
{
    public Coordinate currentPosition;
    public E_Direction currentDirection;

    public void MoveToPosition(Coordinate movePosition)
    {

    }
    public void MoveToDiretion(E_Direction direction, int movePoint)
    {

    }
    
    public void Attack()
    {

    }
    void GiveDamageToPosition(Coordinate position,int damage)
    {
        Tile targetTile = TileMapManager.GetTile(position);
        if(targetTile!=null)
        {
            if(targetTile.unit!=null)
            {
                targetTile.unit.GetDamage(damage);
            }
        }
    }
    public void GetHit(Damage damage)
    {

    }

    public void DigWall()
    {

    }

    public delegate void CoordinateEvent();
    public delegate void DamageDel(Damage damage);
    public event DamageDel BeforeAttackEvent;
    public event DamageDel AfterAttackEvent;

}
