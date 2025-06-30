using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //Koppeling naar scoreCanvas
    public GameObject scoreCanvas;

    //Koppeling naar gameOverScreen
    public GameObject gameOverScreen;


    void Start()
    {
        //timescale gebruiken om aan te geven wanneer het spel is afgelopen. 1 is spel loopt, 0 is spel afgelopen
        Time.timeScale = 1;
    }

    public void GameOver()
    {
        Time.timeScale = 0;
        scoreCanvas.SetActive(false);
        gameOverScreen.SetActive(true);
    }

    public void Replay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
