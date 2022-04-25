using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat : MonoBehaviour
{
    public float strength;

    //Ontrigger make other take dmg
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Entity") || other.CompareTag("Human"))
        {
            other.GetComponent<Stats>().TakeDMG(strength);
        }
    }
}
