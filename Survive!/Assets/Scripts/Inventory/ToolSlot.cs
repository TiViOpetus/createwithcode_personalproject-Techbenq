using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolSlot : MonoBehaviour
{
    public ToolType currentToolType;
    public int currentToolLevel;

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

    public void ChangeTool(Mesh mesh, Material[] materials,float size, bool isTool = false, Tool tool = null)
    {
        meshFilter.mesh = mesh;
        meshRenderer.materials = materials;
        transform.localScale = new Vector3(size, size, size);

        dmgFromTool = 0;

        if (isTool)
        {
            dmgFromTool = tool.dmg;
            currentToolLevel = tool.toolLevel;

            currentToolType = tool.toolType;
            playerCombat.strength += dmgFromTool;
            toolEquipped = true;
        }
    }

    public void RemoveTool()
    {
        meshFilter.mesh = null;
        toolEquipped = false;
        currentToolLevel = 0;

        playerCombat.strength -= dmgFromTool;
    }
}
