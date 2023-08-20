using UnityEngine;

public class CharacterControllerScript : MonoBehaviour
{
    public CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    public float playerSpeed = 2.0f;
    public float jumpHeight = 1.0f;
    public float gravityValue = -9.81f;

    private Camera playerCamera;
    public float lookSensitivity = 3f;
    private float xRotation = 0f; // rotation de la caméra sur l'axe x (hauteur)

    public Animator animator;

    private void Start()
    {
        // controller = gameObject.AddComponent<CharacterController>();
        playerCamera = GetComponentInChildren<Camera>();
    }

    void Update()
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        // Récupérer la direction de la caméra
        Vector3 move = playerCamera.transform.forward * Input.GetAxis("Vertical") + playerCamera.transform.right * Input.GetAxis("Horizontal");
        move.y = 0f; // Garder le mouvement horizontal
        move.Normalize(); // Normaliser le vecteur de déplacement pour éviter de se déplacer plus rapidement en diagonale

        controller.Move(move * Time.deltaTime * playerSpeed);

        // Changes the height position of the player..
        if (Input.GetButtonDown("Jump") && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);

        // rotation de la caméra

            float mouseX = Input.GetAxisRaw("Mouse X") * lookSensitivity;
            float mouseY = Input.GetAxisRaw("Mouse Y") * lookSensitivity;
            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f); // limiter la rotation sur l'axe x
            playerCamera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            transform.Rotate(Vector3.up * mouseX);
        
    }
}
