using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Item", menuName ="Create New Item")]
public class Item : ScriptableObject
{
    public Sprite itemLogo;
    public string itemName;
    public int maxStack;

    public float size;
    public Mesh itemMesh;
    public Material[] itemMaterials;
    public virtual bool Use()
    {
        return false;
    }

    //When active slot switches to one with current item this gets called
    //and puts item in hand
    public virtual void Activate()
    {
        ToolSlot.instance.ChangeTool(itemMesh, itemMaterials, size, false);
    }

    //This removes the item when slot switches
    public virtual void Unactivate()
    {
        ToolSlot.instance.RemoveTool();
    }
}
