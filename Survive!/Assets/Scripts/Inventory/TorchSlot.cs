using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchSlot : MonoBehaviour
{
    public Torch torch;

    public float burnDelay;
    public float burnRate;

    public GameObject torchSlot;
    public GameObject torchFire;

    public static TorchSlot instance;
    private void Awake()
    {
        instance = this;
    }

    public void ActivateTorch()
    {
        SurvivalNeeds.instance.torchActive = true;
        torchSlot.SetActive(true);
    }

    public void DeactivateTorch()
    {
        torch = null;

        SurvivalNeeds.instance.torchActive = false;
        torchSlot.SetActive(false);
        torchFire.SetActive(false);
    }
}
