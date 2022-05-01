using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public CanvasGroup howToPlay;

    //Changes the seed 
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

    
    //starts the game
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }


    //Quits the game
    public void Quit()
    {
        Application.Quit();
    }


    //shows the tutorial
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


    //Goes to settings
    public void GotoSettings()
    {
        SceneManager.LoadScene(2);
    }
}
