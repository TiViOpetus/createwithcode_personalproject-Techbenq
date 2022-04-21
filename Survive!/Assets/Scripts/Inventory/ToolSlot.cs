using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolSlot : MonoBehaviour
{
    public MeshFilter meshFilter;
    public MeshRenderer meshRenderer;
    public bool toolEquipped = false;

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

    public void ChangeTool(Mesh mesh, Material[] materials,float size, bool isTool = false)
    {
        meshFilter.mesh = mesh;
        meshRenderer.materials = materials;
        transform.localScale = new Vector3(size, size, size);

        if(isTool)
            toolEquipped = true;
    }

    public void RemoveTool()
    {
        meshFilter.mesh = null;
        toolEquipped = false;
    }
}
