using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGeneration : MonoBehaviour
{
    public int sizeX, sizeZ;
    public float scale;

    public Gradient groundGradient;

    private Mesh mesh;

    private int seed = 1;

    private Vector3[] vertices;
    private int[] triangles;

    private Color[] vertexColors;
    private void Start()
    {
        seed = Random.Range(1, 99999);
        CreateMesh();
        UpdateMesh();
    }

    //Creates vertices and triangles for the mesh
    private void CreateMesh()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        int i = 0;

        vertices = new Vector3[(sizeX + 1) * (sizeZ + 1)];
        for(int x = 0; x <= sizeX; x++)
        {
            for(int z = 0; z <= sizeZ; z++)
            {
                float y;
                if (z == 0 || x == 0 || z == sizeZ || x == sizeX)
                    y = -1f;
                else
                    y = GetHeight(x, z);

                Vector3 vertPos = new Vector3(x, y, z);
                vertices[i] = vertPos;
                i++;
            }
        }
        i = 0;

        triangles = new int[sizeX * sizeZ  * 6];
        for(int z = 0, vert = 0; z < sizeZ; z++)
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
        i = 0;

        vertexColors = new Color[vertices.Length];
        foreach(Vector3 vert in vertices)
        {
            vertexColors[i] = groundGradient.Evaluate(Mathf.Lerp(0, 1, vert.y));
            i++;
        }
    }

    //Updates the mesh
    private void UpdateMesh()
    {
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.colors = vertexColors;
        mesh.RecalculateNormals();
    }

    //Gets height for the vertice using perlin noise map
    private float GetHeight(int x, int z)
    {
        float xCoord = (float)x / sizeX * scale + seed;
        float zCoord = (float)z / sizeZ * scale + seed;

        float calculated = Mathf.PerlinNoise(xCoord, zCoord);

        if (calculated > 0.45f)
        {
            calculated = 1;
        }

        return calculated;
    }
}
