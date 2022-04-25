using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Score : MonoBehaviour
{
    //Simple script to add score every second
    //And certain amount if needed

    private Text scoreText;
    private float score;
    private void Start()
    {
        scoreText = GetComponent<Text>();
        InvokeRepeating("AddTimedScore", 1,1);
    }

    public void AddTimedScore()
    {
        AddScore(1);
    }

    public void AddScore(float amount)
    {
        score += amount;
        scoreText.text = "SCORE: " + score;
    }
}
