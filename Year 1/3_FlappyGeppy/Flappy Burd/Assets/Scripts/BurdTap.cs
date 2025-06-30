using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurdTap : MonoBehaviour
{
    // Public variable maken voor de speed van de Burd
    public float speed;

    // Hier moet ik de Burd aan kunnen spreken door zijn rigidbody component
    Rigidbody2D rbb;

    // Een link maken met GameManager om de GameOver screen te laten zien
    public GameManager GameManager;



    // De rigidbody van de burd in dit
    void Start()
    {
        rbb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            //Calculeer de speed x de x as zodat de Burd omhoog gaat
            rbb.velocity = Vector2.up * speed;
        }
}
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Als Burd iets aanraakt dan is het game over
        GameManager.GameOver();
    }
    
        
    


}
