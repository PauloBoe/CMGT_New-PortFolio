using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeClones : MonoBehaviour
{
    //Maak een float aan die voor de pipe clones zorgt
    public float maxTime = 1.5f;
    float timer = 0;
    public GameObject pipes;
    public float maxHeight;
    public float minHeight;


    void Start()
    {
        GameObject newPipes = Instantiate(pipes);
        newPipes.transform.position = (Vector2)transform.position + Vector2.up * Random.Range(maxHeight, minHeight);
        Destroy(newPipes, 5);
    }

    void Update()
    {
        // Als de times groter is dan de max time, kopieer en plaats pipes
        if(timer > maxTime)
        {
            GameObject newPipes = Instantiate(pipes);
            newPipes.transform.position = (Vector2)transform.position + Vector2.up * Random.Range(maxHeight, minHeight);
            Destroy(newPipes, 5);
            timer = 0;
        }

        // De timer altijd blijven updated door Timer.deltaTime;
        timer += Time.deltaTime;
    }
}
