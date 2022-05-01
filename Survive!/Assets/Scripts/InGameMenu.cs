using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class InGameMenu : MonoBehaviour
{
    public AudioMixer mixer;
    public Text volumeText;
    public Slider volumeSlider;

    private void Start()
    {
        mixer.GetFloat("volume", out float vol);
        volumeSlider.value = vol;

        vol = (vol - -80) / (20 - -80);
        vol *= 100;
        volumeText.text = "Volume: " + (int)vol + "%";
    }


    public void Quit()
    {
        Application.Quit();
    }

    public void ChangeVolume(float level)
    {
        float volProcent = (level - -80) / (20 - -80);
        volProcent *= 100;
        volumeText.text = "Volume: " + (int)volProcent + "%";

        mixer.SetFloat("volume", level);
    }
}
