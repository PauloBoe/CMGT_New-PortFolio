using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void EasyButton()
    {
        PlayerPrefs.SetInt("gamemode", 1);
        SceneManager.LoadScene(1);
    }

    public void MediumButton()
    {
        PlayerPrefs.SetInt("gamemode", 2);
        SceneManager.LoadScene(1);
    }

    public void HardButton()
    {
        PlayerPrefs.SetInt("gamemode", 3);
        SceneManager.LoadScene(1);
    }
}
