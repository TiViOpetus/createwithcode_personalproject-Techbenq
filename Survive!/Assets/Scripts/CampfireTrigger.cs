using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampfireTrigger : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Human"))
        {
            SurvivalNeeds.withinRadius = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Human"))
        {
            SurvivalNeeds.withinRadius = false;
        }
    }
}
