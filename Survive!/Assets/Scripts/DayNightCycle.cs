using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    public bool isDay = true;
    public float rotateSpeed;
    public float dayLength;

    private float currentTime;
    private float dayProcent;
    private void Update()
    {
        currentTime += Time.deltaTime * rotateSpeed;
        currentTime %= dayLength;
        dayProcent = currentTime / dayLength;

        if (dayProcent > 0.5f)
            isDay = false;
        else isDay = true;

        transform.eulerAngles = new Vector3(360 * dayProcent, 0);
    }
}
