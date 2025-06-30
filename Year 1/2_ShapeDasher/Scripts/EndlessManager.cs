using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class EndlessManager : MonoBehaviour
{
    // Instellingen voor score, tijd en het level
    private float scoreMultiplier = 1;
    private float timeMultiplier = 2;
    private float currentHighscore = 0;
    private float currentScore;
    private float currentTime = 4.1f;
    private float wallSpeed = 85f;
    private float decayTime = 75;

    // Onthoud de volgende positition voor elk volgend segment
    private Vector2 nextPosition = new Vector2(0, 0);

    // Alle segmenten die worden gebruikt en de muur
    public GameObject[] segments;
    public Rigidbody2D wallRigidbody2D;
    
    // UI
    public Text scoreText;
    public Text highScoreText;
    public Text scoreTextGameOver;
    public Text highScoreTextGameOver;
    public Text currentCoins;

    // Bool voor het bijhouden of het eerste segment moet worden gespawned
    private bool firstTime = true;
    private bool gameOver;

    private void Start()
    {
        // Dit laad de huidige coins zien als tekst
        currentCoins.text = "Coins: " + MenuManager.coins.ToString();

        // Update de Highscore wanneer de scene wordt geladen
        if (PlayerPrefs.HasKey("HighScore"))
        {
            // Verander de text naar de huidige Highscore
            currentHighscore = PlayerPrefs.GetFloat("HighScore");
            highScoreText.text = "HighScore: " + currentHighscore.ToString();
        }

        else
        {
            // Eerste keer dus zorg ervoor dat Highscore een waarde krijgt en wordt opgeslagen
            PlayerPrefs.SetFloat("HighScore", 0);
            highScoreText.text = "HighScore: " + PlayerPrefs.GetFloat("HighScore").ToString();
        }
    }
    
    private void Update()
    {
        // Update de huidige score en tijd
        currentScore += Time.deltaTime * scoreMultiplier;
        currentTime += Time.deltaTime * timeMultiplier;

        // Kijk of de score hoger is dan de highscore
        if (math.floor(currentScore) > currentHighscore)
        {
            // Als de score hoger is, update de highscore
            currentHighscore = currentScore;
            PlayerPrefs.SetFloat("HighScore", currentHighscore);
        }
        
        // Update de time text en de highscore
        scoreText.text = "Score: " + math.floor(currentScore).ToString();
        highScoreText.text = "HighScore: " + math.floor(currentHighscore).ToString();
        scoreTextGameOver.text = scoreText.text;
        highScoreTextGameOver.text = highScoreText.text; 

        if (currentTime > 5) // Als er 5 seconden voorbij zijn gegaan
        {
            currentTime = 0; // Reset de timer

            // Deze if statement zorgt ervoor dat de positie correct wordt gezet zodat de segmenten op de juiste plekken spawned
            if (firstTime == false)
            {
                nextPosition += new Vector2(25 * 0.32f, 0);
            }
            else
            {
                firstTime = false;
                nextPosition = new Vector2(25 * 0.32f, 0);
            }

            GameObject chosenPrefab = segments[Random.Range(0, segments.Length)]; // Pak een random segment van alle segmenten
            GameObject spawnedPrefab =
                (GameObject) Instantiate(chosenPrefab, nextPosition, Quaternion.identity, transform.parent); // Hier wordt er een GameObject van een prefab gemaakt.
            spawnedPrefab.transform.parent = transform; // Zorgt dat alle segmenten mooi in de hierarchy wordt neergezet
            spawnedPrefab.name = "Segment"; // Geef het segment een naam
            Destroy(spawnedPrefab, decayTime); // Zorgt dat het segment na een tijd verdwijnt
        }
    }

    private void FixedUpdate()
    {
        if (currentTime > 3.99f) 
        {
            wallRigidbody2D.velocity = Vector2.right * (wallSpeed * Time.fixedDeltaTime); // Zorgt dat hij elke FixedUpdate de muur beweegt
        }
    }
}