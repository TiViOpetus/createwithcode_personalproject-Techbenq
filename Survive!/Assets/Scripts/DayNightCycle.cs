using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DayNightCycle : MonoBehaviour
{
    public static bool bossSpawn = false;

    public static bool isDay = true;

    public Text dayText;
    public Animator nextDayAnim;

    public Gradient ambient, fog, directional;

    public float rotateSpeed;
    public float dayLength;

    public static int dayNum = 1;
    private bool nextDay = false;

    private float currentTime;
    private float dayProcent;

    private Light directionalLight;
    private void Start()
    {
        directionalLight = GetComponent<Light>();
    }

    //Rotates the Light to make a day night cycle and upon a new day plays animation and makes it harder
    private void Update()
    {
        currentTime += Time.deltaTime * rotateSpeed;
        currentTime %= dayLength;
        dayProcent = currentTime / dayLength;

        if (dayProcent > 0.5f)
        {
            isDay = false;
            nextDay = true;
        }
        else
        {
            if (nextDay)
            {
                dayNum++;
                dayText.text = "Day " + dayNum;
                nextDayAnim.Play("FadeIn");

                if (dayNum >= 3) bossSpawn = true;

                ObjectGeneration.instance.CreateObjects();
                EnemySpawner.maxEnemies += 1;

                nextDay = false;
            }
            isDay = true;
        }

        transform.eulerAngles = new Vector3(360 * dayProcent, 0);
        UpdateLightning();
    }

    //Updates lightning settings
    private void UpdateLightning()
    {
        RenderSettings.ambientLight = ambient.Evaluate(dayProcent);
        RenderSettings.fogColor = fog.Evaluate(dayProcent);
        directionalLight.color = directional.Evaluate(dayProcent);
    }
}
