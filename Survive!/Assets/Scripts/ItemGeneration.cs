using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGeneration : MonoBehaviour
{
    public Transform parent;

    public int minAvailableVerts;
    public Prefab[] preFabs;
    public List<Vector3> availableVerts;


    public static ItemGeneration instance;
    private void Awake()
    {
        instance = this;
    }

    public void SetValues(List<Vector3> verts, Prefab[] objects)
    {
        preFabs = objects;
        availableVerts = verts;
        minAvailableVerts = verts.Count;
    }


    //Spawns items if there is need
    public void SpawnItems()
    {
        if (availableVerts.Count >= minAvailableVerts)
        {
            List<Vector3> usedVerts = new List<Vector3>();

            for(int i = availableVerts.Count; i >= minAvailableVerts; i--)
            {
                int random = Random.Range(0, preFabs.Length);
                Prefab randomObj = preFabs[random];
            }
        }
    }
}
