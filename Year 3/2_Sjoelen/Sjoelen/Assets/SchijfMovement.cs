using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SchijfMovement : MonoBehaviour
{
    Rigidbody rb;

    // Object on the disc that the camera has to follow
    GameObject followObject;
    // The rotationspeed of the camera
    float RotationSpeed = 100f;
    // Y rotation to be clamped
    float rotationY;

    // Boolean for if a puck is still moving
    bool puckIsMoving = false;

    [SerializeField] private GameObject canvas;
    [SerializeField] private Image charge;

    bool chargeGoingUp = true;

    GameManager gm;

    void Start()
    {
        // Lock the cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        rb = GetComponent<Rigidbody>();
        gm = FindObjectOfType<GameManager>().GetComponent<GameManager>();


        followObject = GameObject.FindGameObjectWithTag("CameraFollowObject");

        if (gm.gamemode == 1 || gm.gamemode == 2)
        {
            followObject.transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else if (gm.gamemode == 3)
        {
            followObject.transform.eulerAngles = new Vector3(0, 0, 0);
        }
        

        // Reset the fill amount
        charge.fillAmount = 0;
    }

    void Update()
    {
        // Rotate canvas
        canvas.transform.eulerAngles = new Vector3(followObject.transform.eulerAngles.x + 90, followObject.transform.eulerAngles.y + 180, followObject.transform.eulerAngles.z);
        if (Input.GetKey(KeyCode.W) && rb.velocity == new Vector3(0, 0, 0))
        {
            if (chargeGoingUp)
            {
                charge.fillAmount += 0.7f * Time.deltaTime; 
                if (charge.fillAmount >= 1){
                    chargeGoingUp = false;
                }
            }
            else
            {
                charge.fillAmount -= 0.7f * Time.deltaTime;
                if (charge.fillAmount <= 0)
                {
                    chargeGoingUp = true;
                }
            }
        }

        if (Input.GetKeyUp(KeyCode.W) && rb.velocity == new Vector3(0, 0, 0))
        {
            rb.AddForce(-followObject.transform.right * 6f * charge.fillAmount, ForceMode.Impulse);
            Invoke("StartPuckMovement", 0.2f);
            charge.fillAmount = 0;
        }

        // Make a new puck if this puck stopped moving
        if (puckIsMoving && rb.velocity == new Vector3(0, 0, 0))
        {
            ResetPuck();
        }

        // Rotate camera with mouse
        followObject.transform.Rotate(0, (Input.GetAxis("Mouse X") * RotationSpeed * Time.deltaTime), 0, Space.World);
        // Keep the camera focus point on the disc
        followObject.transform.position = transform.position;
    }

    void StartPuckMovement()
    {
        puckIsMoving = true;
    }

    void ResetPuck()
    {
        if (gm.gamemode == 3)
        {
            gm.YouSuck();
        }      
        Destroy(GetComponent<SchijfMovement>());
    }
}