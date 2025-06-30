using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawner : MonoBehaviour
{
   
    public GameObject Ball;
    public float minSpeed;
    public float maxSpeed;
    public float delay;

    private void Start()
    {
        StartCoroutine(SpawnBall());
    }

    private IEnumerator SpawnBall()
    {

        GameObject newBall = Instantiate(Ball);
        Destroy(newBall, 20);
        newBall.transform.position = transform.position;
        newBall.transform.name = "TennisBallProjectile";

        Debug.Log("spawning");

        Rigidbody rigidbody = newBall.GetComponent<Rigidbody>();
        rigidbody.velocity = -transform.forward * Random.Range(minSpeed,maxSpeed);

        yield return new WaitForSeconds(delay);
        StartCoroutine(SpawnBall());
    }
}
