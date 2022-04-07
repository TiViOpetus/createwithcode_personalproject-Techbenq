using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepObjectOnGround : MonoBehaviour
{
    public LayerMask groundMask;

    public Transform meshTransform;
    public MeshRenderer meshRenderer;
    public MeshFilter meshFilter;
    public MeshCollider meshCollider;

    private bool canPlace = true;

    public static KeepObjectOnGround instance;
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        meshTransform = GetComponent<Transform>();
        meshFilter = GetComponent<MeshFilter>();
        meshRenderer = GetComponent<MeshRenderer>();
        meshCollider = GetComponent<MeshCollider>();
    }

    void Update()
    {
        KeepOnGround();
    }

    //Puts objects position on ground
    private void KeepOnGround()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position + Vector3.up, Vector3.down, out hit, 10, groundMask))
        {
            transform.position = hit.point;
        }
    }

    //Sets the prefab on the spot
    public bool SetOnGround(GameObject preFab)
    {
        if (canPlace)
        {
            Instantiate(preFab, transform.position, transform.rotation);
            return true;
        }
        return false;
    }

    public void DisableObject()
    {
        meshFilter.mesh = null;
        meshCollider.enabled = false;
        canPlace = true;
        meshRenderer.material.color = Color.white;
    }

    private void OnTriggerStay(Collider other)
    {
        canPlace = false;
        meshRenderer.material.color = Color.red;
    }
    private void OnTriggerExit(Collider other)
    {
        canPlace = true;
        meshRenderer.material.color = Color.white;
    }
}
