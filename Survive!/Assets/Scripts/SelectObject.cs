using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectObject : MonoBehaviour
{
    public float changeSpeed;

    public static Interactable current;
    private Material currentMat;

    private Color ogColor;
    private float currentCol;

    private bool brighter = true;

    private void OnTriggerStay(Collider other)
    {
        if (current == null)
        {
            current = other.GetComponent<Interactable>();
            currentMat = other.gameObject.GetComponent<MeshRenderer>().material;
            ogColor = currentMat.color;
            currentCol = ogColor.b;
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
                currentCol = Mathf.Lerp(currentCol, 6, changeSpeed * Time.deltaTime);
                currentMat.color = new Color(currentCol,currentCol,currentCol);
                if (currentMat.color.b >= 5 && currentMat.color.r >= 5) brighter = false;
            }
            else
            {
                currentCol = Mathf.Lerp(currentCol, 0, changeSpeed * Time.deltaTime);
                currentMat.color = new Color(currentCol, currentCol, currentCol);
                if (currentMat.color.b <= 2 && currentMat.color.r <= 2) brighter = true;
            }
        }
    }
}
