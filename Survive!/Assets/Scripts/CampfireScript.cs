using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampfireScript : Interactable
{
    public ParticleSystem fireEffect;

    public float maxRadius;
    public SphereCollider triggerCollider;

    public Item stickItem;
    public GameObject[] sticks;
    public float burnDelay;

    public float minInner, maxInner, minOuter, maxOuter;
    private Light campfireLight;

    private int maxSticks;
    private int burningSticks = 0;

    private void Start()
    {
        campfireLight = GetComponentInChildren<Light>();
        foreach(GameObject stick in sticks)
        {
            if (stick.activeSelf)
                burningSticks += 1;
        }

        maxSticks = sticks.Length;

        UpdateCamp();
        InvokeRepeating("BurnStick", burnDelay * 2, burnDelay);
    }

    //Interacts with campfire removes a stick and adds it to the campfire
    public override void Interact()
    {
        base.Interact();
        if (burningSticks >= maxSticks) return;
            if(InventoryManager.instance.activeSlot.slotItem == stickItem)
            {
                if(AddStick())
                    InventoryManager.instance.activeSlot.RemoveItem(1);
            }

        if(InventoryManager.instance.activeSlot.slotItem is EatAble eatAble)
        {
            if (eatAble.cookAble)
            {
                InventoryManager.instance.RemoveItems(eatAble, 1);
                InventoryManager.instance.AddItem(eatAble.cookedVersion);
            }
        }
    }

    //Removes a stick if none left game over
    private void BurnStick()
    {
        burningSticks -= 1;
        if(burningSticks <= 0)
        {
            CancelInvoke();
            fireEffect.Stop();
        }
        UpdateCamp();
    }

    //Adds a stick
    public bool AddStick()
    {
        if (burningSticks >= maxSticks)
            return false;
        if (burningSticks <= 0) return false;

        burningSticks += 1;
        UpdateCamp();

        fireEffect.Emit(10);

        return true;
    }

    //Updates the campfire aka sets light radius and collider
    private void UpdateCamp()
    {
        for(int i = 0; i < maxSticks; i++)
        {
            if (i < burningSticks)
            {
                sticks[i].SetActive(true);
            }
            else
                sticks[i].SetActive(false);
        }

        float procent = (float)burningSticks / (float)maxSticks;

        campfireLight.spotAngle = Mathf.Clamp(maxOuter * procent * 1.25f, minOuter, maxOuter);
        campfireLight.innerSpotAngle = Mathf.Clamp(maxInner * procent, minInner, maxInner);

        triggerCollider.radius = maxRadius * procent;

        ParticleSystem.MainModule main = fireEffect.main;
        main.startLifetime = 5 * procent;
        
        if (burningSticks == 0)
            campfireLight.intensity = 0;
    }
}
