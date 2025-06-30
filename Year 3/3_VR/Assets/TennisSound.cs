using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TennisSound : MonoBehaviour
{
    private AudioSource audioS;
    private void Awake()
    {
        audioS = GetComponent<AudioSource>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            audioS.Play();
        }
    }
}
