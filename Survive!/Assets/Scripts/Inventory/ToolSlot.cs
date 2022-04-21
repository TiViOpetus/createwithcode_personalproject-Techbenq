using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolSlot : MonoBehaviour
{
    public MeshFilter meshFilter;
    public MeshRenderer meshRenderer;

    public static ToolSlot instance;
    private void Awake()
    {
        instance = this;
    }

    public void ChangeTool()
    {

    }
    public void RemoveTool()
    {
        
    }
}
