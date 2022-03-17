using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Score : MonoBehaviour
{
    private Text scoreText;
    private float score;
    private void Start()
    {
        scoreText = GetComponent<Text>();
        Invoke("AddScore", 1);
    }

    public void AddScore(float amount = 1)
    {
        score += amount;
        scoreText.text = "SCORE: " + score;
    }
}
