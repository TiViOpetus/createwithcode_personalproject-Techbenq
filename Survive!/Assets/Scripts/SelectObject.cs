using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectObject : MonoBehaviour
{
    public Material simpleNatureMat;
    public float changeSpeed;

    public static Interactable current;
    private Material currentMat;

    private Color ogColor;

    private bool brighter = true;

    private int maxBrightness;
    private void OnTriggerStay(Collider other)
    {
        if (current == null)
        {
            current = other.GetComponent<Interactable>();
            currentMat = other.gameObject.GetComponent<MeshRenderer>().material;
            ogColor = currentMat.color;

            if (currentMat == simpleNatureMat) maxBrightness = 6;
            else maxBrightness = 1;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(currentMat != null)
            currentMat.color = ogColor;

        current = null;
        currentMat = null;
    }

    private void Update()
    {
        if(current != null)
        {
            //Make object go brighter then darker 
            if (brighter)
            {
                currentMat.color = Color.Lerp(currentMat.color, Color.white * maxBrightness, changeSpeed * Time.deltaTime);
                if (currentMat.color.b >= 0.8f * maxBrightness ) brighter = false;
            }
            else
            {
                currentMat.color = Color.Lerp(currentMat.color, Color.black, changeSpeed * Time.deltaTime);
                if (currentMat.color.b <= 0.2) brighter = true;
            }
        }
    }
}
