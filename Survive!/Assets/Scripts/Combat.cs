using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat : MonoBehaviour
{
    public float strength;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Entity"))
        {
            other.GetComponent<Stats>().TakeDMG(strength);
        }
    }
}
