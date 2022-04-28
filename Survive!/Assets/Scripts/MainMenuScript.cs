using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public CanvasGroup howToPlay;

    public void ChangeSeed(string seedText)
    {
        if(seedText.Length > 0)
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

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void ShowHowToPlay()
    {
        if(howToPlay.alpha == 0)
        {
            howToPlay.alpha = 1;
        }
        else
        {
            howToPlay.alpha = 0;
        }
    }
}
