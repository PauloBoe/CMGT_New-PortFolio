using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public int player1score1;
    public int player1score2;
    public int player1score3;
    public int player1score4;
    public int player1score20;
    public int player1score;

    [SerializeField] private GameObject schijfPrefab;
    SchijfMovement activeSchijf = null;

    public int gamemode = 2;

    int maxPucks = 30;
    int pucksLeft = 30;

    public Canvas suckCanvas;
    public TMP_Text scoreText;
    public TMP_Text puckleftText;
    public GameObject backtoMenu;
    public GameObject averageHard;

    private void Start()
    {
        gamemode = PlayerPrefs.GetInt("gamemode");
    }

    void Update()
    {
        puckleftText.text = "Sjoel remaining: " + pucksLeft;
        scoreText.text = "Score: " + player1score;

        if (gamemode == 3)
        {
            averageHard.SetActive(true);
        }

        if (activeSchijf == null)
        {
            if (pucksLeft < 1)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                backtoMenu.SetActive(true);
            }
            if (gamemode == 1 && pucksLeft > 0)
            {
                activeSchijf = Instantiate(schijfPrefab, new Vector3(34.1064072f, 2.74999948f, 27.2299995f), Quaternion.identity).GetComponent<SchijfMovement>();
                pucksLeft--;
            }
            else if (gamemode == 2 && pucksLeft > 0)
            {
                activeSchijf = Instantiate(schijfPrefab, new Vector3(-11.5878658f, 2.74999948f, 31.3506908f), Quaternion.identity).GetComponent<SchijfMovement>();
                pucksLeft--;
            }
            else if (gamemode == 3 && pucksLeft > 0)
            {
                activeSchijf = Instantiate(schijfPrefab, new Vector3(9.94957638f, 2.74999995f, -22.6706333f), Quaternion.identity).GetComponent<SchijfMovement>();
                pucksLeft--;
            }
        }


        // Check if the player scored 1 point in all holes for bonus points
        if (player1score1 > 0 && player1score2 > 0 && player1score3 > 0 && player1score4 > 0)
        {
            player1score1--;
            player1score2--;
            player1score3--;
            player1score4--;
            player1score20++;
        }
        // Calculate the scores
        player1score = (player1score1) + (player1score2 * 2) + (player1score3 * 3) + (player1score4 * 4) + (player1score20 * 20);

       Debug.Log(player1score);
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void YouSuck()
    {
        suckCanvas.enabled = true;
        Invoke("StopSuck", 2f);
    }

    public void StopSuck()
    {
        suckCanvas.enabled = false;
    }
}

