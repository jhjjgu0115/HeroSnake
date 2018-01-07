using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageEffect : Effect
{
    public int damage;

    public override void Adjust()
    {
        foreach (Unit target in targetList)
        {
            if(target!=null)
            {
                target.GetDamage(damage);
            }
        }
    }
}
