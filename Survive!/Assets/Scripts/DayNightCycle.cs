using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    public Gradient ambient, fog, directional;
    public bool isDay = true;
    public float rotateSpeed;
    public float dayLength;

    private float currentTime;
    private float dayProcent;

    private Light directionalLight;
    private void Start()
    {
        directionalLight = GetComponent<Light>();
    }

    private void Update()
    {
        currentTime += Time.deltaTime * rotateSpeed;
        currentTime %= dayLength;
        dayProcent = currentTime / dayLength;

        if (dayProcent > 0.5f)
            isDay = false;
        else isDay = true;

        transform.eulerAngles = new Vector3(360 * dayProcent, 0);
        UpdateLightning();
    }

    private void UpdateLightning()
    {
        RenderSettings.ambientLight = ambient.Evaluate(dayProcent);
        RenderSettings.fogColor = fog.Evaluate(dayProcent);
        directionalLight.color = directional.Evaluate(dayProcent);
    }
}
