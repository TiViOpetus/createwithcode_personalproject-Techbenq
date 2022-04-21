using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Tool", menuName = "Create New Tool")]
public class Tool : Item
{

    public override void Activate()
    {
        ToolSlot.instance.ChangeTool(itemMesh, itemMaterials, size, true);
    }

    public override void Unactivate()
    {
        base.Unactivate();
        ToolSlot.instance.RemoveTool();
    }
}
