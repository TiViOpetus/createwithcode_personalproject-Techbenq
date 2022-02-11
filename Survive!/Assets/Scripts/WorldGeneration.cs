using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGeneration : MonoBehaviour
{
    public int sizeX, sizeZ;
    public float scale;

    private Mesh mesh;

    private int seed = 1;

    private Vector3[] vertices;
    private int[] triangles;
    private void Start()
    {
        CreateMesh();
        SetMesh();
    }

    private void CreateMesh()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        vertices = new Vector3[(sizeX + 1) * (sizeZ + 1)];
        for(int x = 0, i = 0; x <= sizeX; x++)
        {
            for(int z = 0; z <= sizeZ; z++)
            {
                Vector3 vertPos = new Vector3(x, GetHeight(x,z), z);
                vertices[i] = vertPos;
                i++;
            }
        }

        triangles = new int[sizeX * sizeZ  * 6];
        for(int z = 0, i = 0, vert = 0; z < sizeZ; z++)
        {
            for(int x = 0; x < sizeX; x++)
            {
                triangles[i] = vert;
                triangles[i + 1] = vert + 1;
                triangles[i + 2] = vert + sizeX + 1;
                triangles[i + 3] = vert + 1;
                triangles[i + 4] = vert + sizeX + 2;
                triangles[i + 5] = vert + sizeX + 1;
                vert++;
                i += 6;
            }
            vert++;
        }
    }

    private void SetMesh()
    {
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
    }
    private float GetHeight(int x, int z)
    {
        float xCoord = (float)x / sizeX * scale + seed;
        float zCoord = (float)z / sizeZ * scale + seed;

        float calculated = Mathf.PerlinNoise(xCoord, zCoord);
        return calculated;
    }
}
