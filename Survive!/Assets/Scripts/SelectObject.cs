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

    //Object can only trigger on interactable layers
    //Sets current interactable to one in the collider
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

    //When exits the interactable collider sets all to defaults
    private void OnTriggerExit(Collider other)
    {
        if(currentMat != null)
            currentMat.color = ogColor;

        current = null;
        currentMat = null;
    }

    //Make object go brighter then darker 
    private void Update()
    {
        if(current != null)
        {
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
