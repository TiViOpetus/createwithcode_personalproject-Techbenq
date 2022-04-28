using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuScript : MonoBehaviour
{
    
    public void ChangeSeed(string seedText)
    {
        if(seedText != null)
        {
            int seed = int.Parse(seedText);
            if (seed > 0)
            {
                WorldGenerationV2.seed = seed;
                WorldGenerationV2.randomSeed = false;
                Debug.Log(seed);
            }
            else
            {
                WorldGenerationV2.randomSeed = true;
            }
        }
        else
        {
            WorldGenerationV2.randomSeed = true;
        }
    }
}
