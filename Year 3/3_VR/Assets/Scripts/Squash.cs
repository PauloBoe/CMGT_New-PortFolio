using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Squash : MonoBehaviour
{
    int score;
    [SerializeField] private TMP_Text text;
    AudioSource audioS;

    private void Awake()
    {
        audioS = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.CompareTag("Ball"))
        {
            audioS.Play();
            score++;
            text.text = "" + score;
        }
    }

    public void resetScore()
    {
        score = 0;
        text.text = "" + score;
        Debug.Log(score);
    }
}
