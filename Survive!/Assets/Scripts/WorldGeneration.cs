using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGeneration : MonoBehaviour
{
    public int sizeX, sizeZ;
    private MeshFilter filter;
    private Mesh mesh;
    private int seed;

    private Vector3[] vertices;
    private int[] triangles;
    private void Start()
    {
        CreateMesh();
    }

    private void CreateMesh()
    {
        mesh = new Mesh();
        filter.mesh = mesh;

        vertices = new Vector3[sizeX * sizeZ];
    }
}
