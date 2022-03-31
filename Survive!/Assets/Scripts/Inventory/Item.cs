using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Item", menuName ="Create New Item")]
public class Item : ScriptableObject
{
    public Sprite itemLogo;
    public string itemName;
    public int maxStack;

    public virtual bool Use()
    {
        return false;
    }

    public virtual void Activate()
    {

    }

    public virtual void Unactivate()
    {

    }
}
