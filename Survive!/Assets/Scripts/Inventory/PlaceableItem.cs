using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Placeable Item", menuName = "Create New Placeable Item")]
public class PlaceableItem : Item
{
    public GameObject itemPrefab;
    public float previewSize;
    public override bool Use()
    {
        if (KeepObjectOnGround.instance.SetOnGround(itemPrefab))
        {
            if(InventoryManager.instance.activeSlot.itemAmount <= 1)
            {
                Unactivate();
            }
            return true;
        }

        return false;
    }

    //Changes the mesh if active slot has this item
    public override void Activate()
    {
        base.Activate();
        KeepObjectOnGround.instance.meshTransform.localScale = new Vector3(previewSize, previewSize, previewSize);
        KeepObjectOnGround.instance.meshFilter.mesh = itemMesh;
        KeepObjectOnGround.instance.meshCollider.sharedMesh = itemMesh;
        KeepObjectOnGround.instance.meshCollider.enabled = true;

    }
    public override void Unactivate()
    {
        base.Unactivate();
        KeepObjectOnGround.instance.DisableObject();
    }

}
