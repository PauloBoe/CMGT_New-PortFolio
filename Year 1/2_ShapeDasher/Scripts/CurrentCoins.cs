using UnityEngine;
using UnityEngine.UI;

public class CurrentCoins : MonoBehaviour
{
    private Text currentCoins;
    void Start()
    {
        //Dit laad de text zien op de gameover scherm
        currentCoins = GetComponent<Text>();
        currentCoins.text = "Coins: " + MenuManager.coins.ToString(); // wordt de coinnumber tekst bijgewerkt
    }
}