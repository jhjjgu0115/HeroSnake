using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaSearchEffect : Effect
{
    public Area searchArea;
    public List<Effect> effectList = new List<Effect>();

    public override void Adjust()
    {
        targetList.Clear();
        int angle= caster.directionToCoordinate.GetAngle();
        foreach (Coordinate coord in searchArea.coordList)
        {
            Tile targetTile = TileMapManager.GetTile(coord.Rotate(angle) + caster.coordinate);
            if (targetTile!=null)
            {
                if (targetTile.unit != null)
                {
                    targetList.Add(targetTile.unit);
                }
            }
            
        }
        foreach (Coordinate coord in searchArea.coordList)
        {
            Tile targetTile = TileMapManager.GetTile(coord.Rotate(angle) + caster.coordinate);
            if (targetTile != null)
            {
                targetTile.GetComponent<SpriteRenderer>().color = new Color32(255, 100, 100, 255);
            }
        }
        foreach (Effect effect in effectList)
        {
            effect.targetList = targetList;
            effect.Adjust();
        }

    }

}
