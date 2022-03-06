using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampfireScript : Interactable
{
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

    public override void Interact()
    {
        base.Interact();
        if (burningSticks >= maxSticks) return;
        if(InventoryManager.instance.activeSlot.slotItem == stickItem)
        {
            AddStick();
            InventoryManager.instance.activeSlot.RemoveItem(stickItem,1);
        }
    }

    //Removes a stick if none left game over
    private void BurnStick()
    {
        burningSticks -= 1;
        if(burningSticks <= 0)
        {
            CancelInvoke();
            Debug.Log("Game Over!");
        }
        UpdateCamp();
    }

    //Adds a stick
    public bool AddStick()
    {
        if (burningSticks >= maxSticks)
            return false;

        burningSticks += 1;
        UpdateCamp();

        return true;
    }

    //Updates the campfire
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

        if (burningSticks == 0)
            campfireLight.intensity = 0;
    }
}
