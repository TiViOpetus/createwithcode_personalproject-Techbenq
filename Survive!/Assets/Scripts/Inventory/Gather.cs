using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gather : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Gatherable"))
        {
            other.GetComponent<Gatherable>().Gather();
        }
    }
}
