using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Food", menuName ="Create new food")]
public class EatAble : Item
{
    public float hungerValue;

    public override bool Use()
    {
        base.Use();

        SurvivalNeeds.instance.Eat(hungerValue);
        return true;
    }
}
