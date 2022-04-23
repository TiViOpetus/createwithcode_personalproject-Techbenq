using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Tool", menuName = "Create New Tool")]
public class Tool : Item
{
    public float dmg;
    public override void Activate()
    {
        ToolSlot.instance.ChangeTool(itemMesh, itemMaterials, size, true, dmg);
    }

    public override void Unactivate()
    {
        base.Unactivate();
    }
}
