using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Settings : MonoBehaviour
{
    public Dropdown resolutions;
    public bool fullScreen = true;

    private int screenIndex;
    private List<Resolution> goodResolutions = new List<Resolution>();
    private void Start()
    {
        SetResolutions();
        DontDestroyOnLoad(this);
    }


    private void SetResolutions()
    {
        int i = 0;
        List<string> options = new List<string>();

        foreach (Resolution res in Screen.resolutions)
        {
            string option = res.width + " x " + res.height;
            if (options.Contains(option)) continue;

            options.Add(option);
            goodResolutions.Add(res);
            if (res.width == Screen.width && res.height == Screen.height)
            {
                screenIndex = i;
            }
            i++;
        }

        resolutions.AddOptions(options);
        resolutions.value = screenIndex;
        resolutions.RefreshShownValue();
    }

    public void ToggleFullscreen(bool toggle)
    {
        fullScreen = toggle;
        Screen.fullScreen = toggle;
    }

    public void SetResolution(int reso)
    {
        Screen.SetResolution(goodResolutions[reso].width, goodResolutions[reso].height, fullScreen);
    }

    public void SetQuality(int quality)
    {
        QualitySettings.SetQualityLevel(quality);
    }

    public void GoBack()
    {
        SceneManager.LoadScene(0);
    }
}
