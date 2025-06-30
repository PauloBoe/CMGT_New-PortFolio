using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BallThrowPointsText : MonoBehaviour
{
    private int score;
    private TMP_Text text;

    private void Awake()
    {
        text = GetComponent<TMP_Text>();
    }

    public void AddScore(int amount)
    {
        score += amount;
        text.text = "Score: " + score;
    }

    public void resetScore()
    {
        score = 0;
        text.text = "Score: " + score;
        Debug.Log(score);
    }
}
