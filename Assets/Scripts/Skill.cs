using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    public E_ActionList skillType;
    public List<Effect> effectList = new List<Effect>();
    public void Cast(Unit caster)
    {
        foreach(Effect effect in effectList)
        {
            effect.caster = caster;
            effect.Adjust();
        }
    }
}
