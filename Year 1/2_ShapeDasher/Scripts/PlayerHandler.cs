using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHandler : MonoBehaviour
{
    // Components
    private Rigidbody2D rb2d;
    private SpriteRenderer sr;

    // Configuraties
    public float playerSpeed;
    public float jumpPower;

    // States
    public bool canJump = true;
    public bool jump;

    // Sprites
    public Sprite sadFace;
    public Sprite neutralFace;
    public Sprite happyFace;

    //Koppeling naar scoreCanvas
    public GameObject Canvas;

    //Koppeling naar gameOverScreen
    public GameObject gameOverScreen;

    // Variabele om Input op te slaan
    private float xInput;
    
    private void Start()
    {
        // Vraag components op
        rb2d = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        // Stopt het spel als je op "Esc" drukt
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }

        // Sla de horizontale input op voor de FixedUpdate
        xInput = Input.GetAxis("Horizontal") * playerSpeed;

        // Als de speler de jump knop heeft ingedrukt en de speler mag springen dan verander wat waardes
        if (Input.GetAxis("Jump") > 0 && canJump)
        {
            canJump = false;
            jump = true;
        }
    }

    private void FixedUpdate()
    {
        // Voeg een kracht toe voor de horizontale input
        rb2d.velocity = new Vector2(xInput * Time.fixedDeltaTime, rb2d.velocity.y);

        // Als de speler op de jump knop heeft gedrukt, pas een kracht toe die naar boven werkt en zet jump terug op false.
        if (jump)
        {
            rb2d.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            jump = false;
        }
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Kijk of het GameObject waar dit GameObject mee in aanraking komt een bepaalde tag heeft
        if (collision.CompareTag("Floor"))
        {
            canJump = true;
        }

        if (collision.CompareTag("NeutralTrigger"))
        {
            sr.sprite = neutralFace;
        }
        
        if (collision.CompareTag("HappyTrigger"))
        {
            sr.sprite = happyFace;
        }
        
        if (collision.CompareTag("SadTrigger"))
        {
            sr.sprite = sadFace;
        }
        
        if (collision.CompareTag("NextLevel"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        if (collision.CompareTag("EndlessWall"))
        {
            Time.timeScale = 0;
            Canvas.SetActive(false);
            gameOverScreen.SetActive(true);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Als je de spikes of de witte muur aanraakt gaat de speler naar een gameover scherm. 
        if (collision.gameObject.CompareTag("Spikes"))
        {
            Time.timeScale = 0;
            Canvas.SetActive(false);
            gameOverScreen.SetActive(true);
        }
    }
}