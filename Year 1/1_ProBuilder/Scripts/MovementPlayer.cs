using UnityEngine;

public class MovementPlayer : MonoBehaviour
{
    // Dit zijn de variables
    public CharacterController controller; // Dit is de basis om te lopen. Een soort van motor
    public float playerSpeed = 6.0f; // De snelheid van de speler
    public float gravity = -2f; // Dit zorgt er voor dat de speler naar beneden valt als hij in de lucht is

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal"); // De input voor links (A) en rechts (D)
        float vertical = Input.GetAxisRaw("Vertical"); // De input voor lopen naar voren en achteren; W en S
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized; // Slaat de directie op dat de camera naar kijkt. Normalized is om er voor te zorgen dat als je 2 knoppen indrukt dat hij niet 2x zo snel gaat


        // Om te kijken of je in een richting loopt
        if(direction.magnitude >= 0.1f)
        {
            // Veranderd de looprichting naar het vooruitpunt van de camera
             transform.rotation = Quaternion.Euler(0f, Camera.main.transform.localEulerAngles.y, 0f);
        }


        Vector3 move = transform.right * horizontal + transform.forward * vertical; // Dit zet de input om naar een beweging in een richting
        move.y = gravity; // Dit is voor het vallen van de speler in de lucht

        controller.Move(move * playerSpeed * Time.deltaTime); // Dit zorgt er uiteindelijk voor dat de speler daatwerkelijk gaat lopen

        // Ik heb meerdere websites bekeken om de beste manier te vinden maar ik ben uiteindelijk gestopt bij deze 2 video's
        // Zie https://www.youtube.com/watch?v=4HpC--2iowE&t=498s en https://www.youtube.com/watch?v=_QajrabyTJc
        // Andere sites:
        // https://docs.unity3d.com/ScriptReference/CharacterController.Move.html 
        // https://answers.unity.com/questions/1642153/how-to-make-a-3d-character-move.html
        // https://stackoverflow.com/questions/63578140/trouble-with-a-unity-3d-movement-script

        // Deze waren net niet wat ik zocht. En Als laatste kwam ik bij de videos en die waren perfect.

    }
}
