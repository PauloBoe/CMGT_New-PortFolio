using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeletePucks : MonoBehaviour
{
    bool easyModePoint = false;
    [SerializeField] private GameObject puckPre;
    GameManager gm;

    public Canvas youWin;

    private void Awake()
    {
        gm = FindObjectOfType<GameManager>().GetComponent<GameManager>();
    }

    void Update()
    {
        if (easyModePoint && gm.gamemode == 1)
        {
            Instantiate(puckPre, new Vector3(52.1012688f, 2.53999949f, 27.3760986f), Quaternion.identity);
            Debug.Log("Test");
            youWin.enabled = true;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Puck"))
        {
            other.gameObject.tag = "DeadPuck";
            if (gameObject.name == "1Point")
            {
                    gm.player1score1++;
            }
            else if (gameObject.name == "2Point")
            {
                    gm.player1score2++;
            }
            else if (gameObject.name == "3Point")
            {
                    gm.player1score3++;
            }
            else if (gameObject.name == "4Point")
            {
                    gm.player1score4++;
            }
            Destroy(other.gameObject, 1);
            easyModePoint = true;
        }

    }
}
