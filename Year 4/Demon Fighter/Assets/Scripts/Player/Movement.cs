//Auteur: Paulo Boe
//User Story 1: Controle over karakter

using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] CharacterController characterController;
    [SerializeField] LayerMask groundMask;
    [SerializeField] float speed = 11f;
    [SerializeField] float gravity = -30f; // -9.81
    [SerializeField] float jumpHeight = 3.5f;

    private bool isGrounded;
    private bool isJumping;

    private Vector2 horizontalInput;
    private Vector3 verticalVelocity = Vector3.zero;

    private void Update()
    {
        isGrounded = Physics.CheckSphere(transform.position, 0.15f, groundMask);
        if (isGrounded)
        {
            verticalVelocity.y = 0f;
        }
        Vector3 horizontalVelocity = (transform.right * horizontalInput.x + transform.forward * horizontalInput.y) * speed;
        characterController.Move(horizontalVelocity * Time.deltaTime);

        if (isJumping)
        {
            if (isGrounded)
            {
                verticalVelocity.y = Mathf.Sqrt(-2f * jumpHeight * gravity);
            }
            isJumping = false;
        }

        verticalVelocity.y += gravity * Time.deltaTime;
        characterController.Move(verticalVelocity * Time.deltaTime);
    }
    public void ReceiveInput(Vector2 _horizontalInput)
    {
        horizontalInput = _horizontalInput;
    }

    public void OnJumpPressed()
    {
        isJumping = true;
    }

    public void OnKlok()
    {
        print("urmom");
    }
}