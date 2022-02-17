using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    public bool isDay = true;
    public float rotateSpeed;

    private float currentTime = 0;
    private void Update()
    {
        transform.Rotate(Vector3.right * rotateSpeed * Time.deltaTime);
        Debug.Log(transform.rotation.x);
    }
}
