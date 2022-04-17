using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WorldGenerationV2 : MonoBehaviour
{
    public int xSize;
    public int zSize;

    public int seed;

    public int campMinSpace;

    public float scale;

    public Gradient groundGradient;

    public GameObject campfire;
    public Transform player;

    private List<Vector3> availableVerts = new List<Vector3>();

    private Mesh mesh;

    private Color[] vertColors;
    private Vector3[] verts;
    private int[] triangles;
    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        //Create Mesh
        seed = Random.Range(1, 99999);
        CreateVerts();
        GetTriangles();
        SetVertColors();
        UpdateMesh();

        //Create Objects
        SetPlayerAndCampfire();
        ObjectGeneration.instance.availableVerts = availableVerts;
        ObjectGeneration.instance.CreateFirstObjects();
        GetComponent<NavMeshSurface>().BuildNavMesh();

        Destroy(this);
    }


    //Creates positions for the verts
    private void CreateVerts()
    {
        verts = new Vector3[(xSize + 1) * (zSize + 1)];

        for(int i = 0, z = 0; z <= zSize; z++)
        {
            for(int x = 0; x <= xSize; x++)
            {
                float y = GetHeight(x, z);
                verts[i] = new Vector3(x, y, z);
                i++;
            }
        }
    }

    //gets a height for a vert
    private float GetHeight(int x, int z)
    {
        float xCoord = (float)x / xSize * scale + seed;
        float zCoord = (float)z / zSize * scale + seed;

        float perlin = Mathf.PerlinNoise(xCoord, zCoord);

        if (z == 0 || z == zSize) return 0;
        if (x == 0 || x == xSize) return 0;

        if(perlin < 0.36f)
        {
            return perlin;
        }
        else
        {
            availableVerts.Add(new Vector3(x, perlin, z));
            return perlin;
        }
    }

    //Gets triangle points for the mesh
    private void GetTriangles()
    {
        triangles = new int[xSize * zSize * 6];

        for(int z = 0, i = 0, vert = 0; z < zSize; z++)
        {
            for (int x = 0; x < xSize; x++)
            {
                triangles[i] = vert;
                triangles[i + 1] = vert + xSize + 1;
                triangles[i + 2] = vert + 1;
                triangles[i + 3] = vert + 1;
                triangles[i + 4] = vert + xSize + 1;
                triangles[i + 5] = vert + xSize + 2;

                vert++;
                i += 6;
            }
            vert++;
        }
    }

    //Sets Colors to the verts according to the height
    private void SetVertColors() 
    {
        vertColors = new Color[verts.Length];

        for(int i = 0; i < vertColors.Length; i++)
        {
            vertColors[i] = groundGradient.Evaluate(verts[i].y);
        }
    }
    

    //Sets the mesh 
    private void UpdateMesh()
    {
        mesh.vertices = verts;
        mesh.triangles = triangles;
        mesh.colors = vertColors;

        mesh.RecalculateNormals();
        gameObject.AddComponent<MeshCollider>();
    }

    //Sets down campfire and player and removes the vertices near the campfire
    private void SetPlayerAndCampfire()
    {
        Vector3 campSpot = FindCampfireSpot();
        Instantiate(campfire, campSpot, campfire.transform.rotation);
        player.position = campSpot + Vector3.forward * 2;


        List<Vector3> nearCampfire = new List<Vector3>();
        foreach(Vector3 vector in availableVerts)
        {
            bool near = false;

            for(int x = (int)campSpot.x - 3;x < (int)campSpot.x + 4; x++)
            {
                if(vector.x == x)
                {
                    near = true;
                    break;
                }
            }
            if (!near) continue;
            near = false;

            for(int z = (int)campSpot.z - 3; z < (int)campSpot.z + 4; z++)
            {
                if (vector.z == z)
                {
                    near = true;
                    break;
                }
            }
            if (!near) continue;

            nearCampfire.Add(vector);
        }

        foreach (Vector3 vector in nearCampfire) availableVerts.Remove(vector);
    }


    //Finds a suitable spot for the campfire
    private Vector3 FindCampfireSpot()
    {
        for(int z= 0, i = 0; z <= zSize; z++)
        {
            for(int x = 0; x <= xSize; x++)
            {
                bool possible = true;

                //Tests that there are enough verts around the current vert
                if(i + -campMinSpace * xSize < 0)
                {
                    i++;
                    continue;
                }
                if (i + campMinSpace * xSize > verts.Length) return Vector3.zero;


                //if current vert is less than 0.4 then its not good
                if (verts[i].y < 0.4f)
                {
                    i++;
                    continue;
                }


                //Tests every vert to the sides of the current vert
                for(int width = -campMinSpace; width < campMinSpace; width++)
                {
                    if (verts[i + width].y < 0.4)
                    {
                        possible = false;
                        break;
                    }
                }
                if (!possible)
                {
                    i++;
                    continue;
                }

                //Tests every vert up and down of the current vert
                for (int height = -campMinSpace; height < campMinSpace; height++)
                {
                    if(height < 0)
                    {
                        if(verts[i - (height - 1) * xSize].y < 0.4f)
                        {
                            possible = false;
                            break;
                        }
                    }
                    else
                    {
                        if (verts[i + (height - 1) * xSize].y < 0.4f)
                        {
                            possible = false;
                            break;
                        }
                    }
                }
                if (!possible)
                {
                    i++;
                    continue;
                }

                //if current vert passes all tests it is chosen

                return verts[i];
            }
        }

        return Vector3.zero;
    }
}
