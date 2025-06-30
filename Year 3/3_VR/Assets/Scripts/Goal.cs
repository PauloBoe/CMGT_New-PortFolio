using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Goal : MonoBehaviour
{
    public TMP_Text text;
    private AudioSource audioS;


    int score = 0;

    private void Awake()
    {
        audioS = GetComponent<AudioSource>();   
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.name == "TennisBallProjectile")
        {
            score++;
            text.text = "" + score;
            audioS.Play();
            Destroy(collision.gameObject);
        }
     }

    public void resetScore()
    {
        score = 0;
        text.text = "" + score;
    }
}
