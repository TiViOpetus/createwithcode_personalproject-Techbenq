using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampfireScript : MonoBehaviour
{
    public GameObject[] sticks;
    public float burnDelay;

    public float minInner, maxInner, minOuter, maxOuter;
    private Light campfireLight;

    private int maxSticks;
    private int burningSticks = 0;

#region instance 
    public static CampfireScript instance;
    private void Awake()
    {
        instance = this;
    }
    #endregion

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

    //Removes a stick if none left game over
    private void BurnStick()
    {
        burningSticks -= 1;
        if(burningSticks <= 0)
        {
            Debug.Log("Game Over!");
        }
        UpdateCamp();
    }

    //Adds a stick
    public void AddStick()
    {
        if (burningSticks >= maxSticks)
            return;

        burningSticks += 1;
        UpdateCamp();
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

        campfireLight.spotAngle = Mathf.Clamp(maxOuter * procent, minOuter, maxOuter);
        campfireLight.innerSpotAngle = Mathf.Clamp(maxInner * procent, minInner, maxInner);

        if (burningSticks == 0)
        {
            campfireLight.intensity = 0;
        }
    }
}
