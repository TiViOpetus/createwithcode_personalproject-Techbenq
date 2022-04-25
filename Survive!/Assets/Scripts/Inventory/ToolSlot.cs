using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolSlot : MonoBehaviour
{
    public ToolType currentToolType;
    public MeshFilter meshFilter;
    public MeshRenderer meshRenderer;
    public bool toolEquipped = false;

    public Combat playerCombat;
    private float dmgFromTool = 0;

    public static ToolSlot instance;
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        meshFilter = GetComponent<MeshFilter>();
        meshRenderer = GetComponent<MeshRenderer>();
    }

    public void ChangeTool(Mesh mesh, Material[] materials,float size, bool isTool = false, float dmg = 0, ToolType type = ToolType.Axe)
    {
        meshFilter.mesh = mesh;
        meshRenderer.materials = materials;
        transform.localScale = new Vector3(size, size, size);

        dmgFromTool = dmg;

        if (isTool)
        {
            currentToolType = type;
            playerCombat.strength += dmgFromTool;
            toolEquipped = true;
        }
    }

    public void RemoveTool()
    {
        meshFilter.mesh = null;
        toolEquipped = false;

        playerCombat.strength -= dmgFromTool;
    }
}
