using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Torch", menuName = "Torch")]
public class Torch : Item
{
    public bool lit;
    public override bool Use()
    {
        if (lit)
        {
            if(SelectObject.current is CampfireScript camp)
            {
                if (camp.Light()) return true;
            }
        }
        return false;
    }

    public void LightUp()
    {
        TorchSlot.instance.torchFire.SetActive(true);
        lit = true;
    }

    public override void Activate()
    {
        lit = false;
        TorchSlot.instance.torch = this;
        TorchSlot.instance.ActivateTorch();
    }

    public override void Unactivate()
    {
        lit = false;
        TorchSlot.instance.DeactivateTorch();
    }
}
