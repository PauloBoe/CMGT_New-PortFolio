using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoundLoop : MonoBehaviour
{
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = (Vector2)transform.position + Vector2.left * speed * Time.deltaTime;
    }
}
