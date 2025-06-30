using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// Toevoegen van Unity library UI


public class Score : MonoBehaviour
{
    public static int score;
    int bestScore;
    public Text scoreBoardText;
    public Text scoreBoardBestText;


    void Start()
    {
        //score is altijd 0 als je een nieuwe game begint
        score = 0;
        bestScore = PlayerPrefs.GetInt("bestscore");
    }

    void Update()
    {
        print(score);
        GetComponent<Text>().text = score.ToString();
        scoreBoardText.text = score.ToString();
        scoreBoardBestText.text = bestScore.ToString();

        if (score > bestScore)
        {
            bestScore = score;
            PlayerPrefs.SetInt("bestscore", bestScore);
        }
    }
}
