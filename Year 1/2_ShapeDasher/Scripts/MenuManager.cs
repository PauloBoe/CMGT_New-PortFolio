using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public static int coins = 3;
    public Text coinNumber;

    void Update()
    {
        //Dit is de text dat aangeeft hoeveel Coins je nog hebt
        coinNumber.text = "Coins: " + coins.ToString();
    }

    public void Endless()
    {
        //Dit kijkt als de coins groter is dan 0. Zo ja, dan haal er 1tje vanaf en laad Endless
        if (coins > 0) 
        {
            coins--;
            SceneManager.LoadScene("Endless");
        }
    }

    public void Story()
    {
        //Dit kijkt als de coins groter is dan 0. Zo ja, dan haal er 1tje vanaf en laad Story Mode
        if (coins > 0)
        {
            coins--;
            SceneManager.LoadScene("Level01");
        }
    }

    public void InsertCoins()
    {
        //Laad de scene "Coins"
        SceneManager.LoadScene("Coins");
    } 
    
    public void Quit()
    {
        //Sluit de build
        Application.Quit();
    }
}
