using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundRecycle : MonoBehaviour
{
    //Boxcollider van de ground hier gebruiken
    BoxCollider2D ground;

    //Een float voor de totale lengte van de ground
    float totalLength;


    void Start()
    {
        //Koppel de naam van de ground collider aan die van Unity
        ground = GetComponent<BoxCollider2D>();
        totalLength = ground.size.x;
    }

    void Update()
    {
        //De transform.position moet even of kleiner zijn dan de totale lengte van de ground
        if (transform.position.x <= -totalLength)
        {
            transform.position = (Vector2)transform.position + Vector2.right * totalLength * 2f;
        }
    }
}
