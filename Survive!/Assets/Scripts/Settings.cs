using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class Settings : MonoBehaviour
{
    public Dropdown resolutions;
    public Dropdown qualityDropDown;
    public static bool fullScreen;
    public Toggle fullScreenToggle;
    public AudioMixer mixer;
    public Text audioText;

    private int screenIndex;
    private List<Resolution> goodResolutions = new List<Resolution>();
    private void Start()
    {
        fullScreen = Screen.fullScreen;
        fullScreenToggle.isOn = fullScreen;

        qualityDropDown.value = QualitySettings.GetQualityLevel();
        SetResolutions();
    }


    //Sets all supported resolutions to dropdown menu
    //and sets the current one to active
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


    //Toggles fullscreen
    public void ToggleFullscreen(bool toggle)
    {
        fullScreen = toggle;
        Screen.fullScreen = toggle;
    }


    //Sets resolution to one selected
    public void SetResolution(int reso)
    {
        Screen.SetResolution(goodResolutions[reso].width, goodResolutions[reso].height, fullScreen);
    }


    //Sets quality level to one selected
    public void SetQuality(int quality)
    {
        QualitySettings.SetQualityLevel(quality);
    }


    //Changes volume and sets the text to procent amount
    public void ChangeVolume(float level)
    {
        float volProcent = (level - -80) / (20 - -80);
        volProcent *= 100;
        audioText.text = "Volume: " + (int)volProcent + "%";
        mixer.SetFloat("volume", level);
    }


    //Goes back to the main menu
    public void GoBack()
    {
        SceneManager.LoadScene(0);
    }
}
