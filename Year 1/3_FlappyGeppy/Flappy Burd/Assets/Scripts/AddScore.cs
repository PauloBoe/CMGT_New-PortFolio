using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddScore : MonoBehaviour
{
    // maak score aan en voeg telkens als Burd de collider raakt een punt bij de score op
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Score.score++;
    }
}
