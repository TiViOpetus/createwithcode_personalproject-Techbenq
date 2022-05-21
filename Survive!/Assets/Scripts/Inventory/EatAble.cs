using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Food", menuName ="Create new food")]
public class EatAble : Item
{
    public bool cookAble;
    public EatAble cookedVersion;
    public float hungerValue;

    public override bool Use()
    {
        base.Use();
        PlayerController.instance.Interact(0);

        SurvivalNeeds.instance.Eat(hungerValue);
        return true;
    }
}
