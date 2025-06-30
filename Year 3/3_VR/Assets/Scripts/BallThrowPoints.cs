using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BallThrowPoints : MonoBehaviour
{

    public int amountOfPoints;
    private int point;
    public TMP_Text text;
    public BallThrowPointsText script;
    private AudioSource audioS;

    private void Awake()
    {
        audioS = GetComponent<AudioSource>();   
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            audioS.Play();
            script.AddScore(amountOfPoints);
            Destroy(other.gameObject, 0.2f);
        }
    }
}
