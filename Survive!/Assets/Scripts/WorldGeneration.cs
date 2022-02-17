using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGeneration : MonoBehaviour
{
    public int sizeX, sizeZ;
    public float scale;

    public Transform campfire;
    public GameObject[] prefabs;
    public float[] spawnChances;
    public Transform parent;

    public Gradient groundGradient;
    private Mesh mesh;

    private int seed = 1;

    private Vector3[] vertices;
    private int[] triangles;

    private List<Vector3> availableVerts = new List<Vector3>();

    private Color[] vertexColors;
    private void Start()
    {
        //seed = Random.Range(1, 99999);
        CreateMesh();
        UpdateMesh();
        AddObjects(prefabs, spawnChances);
        Destroy(this);
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
            vertexColors[i] = groundGradient.Evaluate(vert.y);
            i++;
        }
    }

    //Updates the mesh
    private void UpdateMesh()
    {
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.colors = vertexColors;

        gameObject.AddComponent<MeshCollider>();
        mesh.RecalculateNormals();
    }

    //Gets height for the vertice using perlin noise map
    private float GetHeight(int x, int z)
    {
        float xCoord = (float)x / sizeX * scale + seed;
        float zCoord = (float)z / sizeZ * scale + seed;

        float calculated = Mathf.PerlinNoise(xCoord, zCoord);

        if (calculated > 0.5f)
        {
            calculated = 1;
        }

        return calculated;
    }

    //Adds objects to the world using chances
    private void AddObjects(GameObject[] objs, float[] chances)
    {
        //List of vertices where objects can spawn
        foreach (Vector3 vert in vertices)
            if (vert.y >= 1)
                availableVerts.Add(vert);

        //Removes vertices near the campfire
        for (int x = (int)campfire.position.x - 3; x < (int)campfire.position.x + 3; x++)
        {
            for (int z = (int)campfire.position.z - 3; z < (int)campfire.position.z + 3; z++)
            {
                availableVerts.Remove(new Vector3(x, 1, z));
            }
        }


        for(int i = 0; i < objs.Length; i++)
        {
            List<Vector3> usedVerts = new List<Vector3>();

            foreach (Vector3 vert in availableVerts)
            {
                if (vert.y >= 1)
                {
                    float random = Random.Range(0, 100);

                    if (random % 2 == 0)
                    {
                        if (random < chances[i])
                        {
                            Vector3 pos = vert;
                            GameObject temp = Instantiate(objs[i], pos, objs[i].transform.rotation,parent);

                            float size = Random.Range(temp.transform.localScale.x - 0.15f, temp.transform.localScale.x + 0.2f);
                            temp.transform.localScale = new Vector3(size, size, size);
                            temp.transform.Rotate(Vector3.up, Random.Range(0, 360));
                            temp.isStatic = true;

                            usedVerts.Add(vert);
                        }
                    }
                }
            }
            //remove used verts from being used again
            foreach (Vector3 vert in usedVerts)
                availableVerts.Remove(vert);
        }
    }
}
