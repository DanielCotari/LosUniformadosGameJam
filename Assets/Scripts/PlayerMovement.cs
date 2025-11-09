using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    public float walkSpeed = 4f;
    public float runSpeed = 8f;
    public float jumpHeight = 2f;
    public float gravity = -9.81f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip[] footstepClips;   // pasos
    public AudioClip jumpClip;          // sonido al saltar
    public AudioClip landClip;          // sonido al aterrizar
    public float stepInterval = 0.5f;   // tiempo entre pasos caminando
    public float runStepInterval = 0.3f; // tiempo entre pasos corriendo

    private CharacterController controller;
    private Vector3 velocity;
    private bool isGrounded;
    private bool wasGrounded;
    private float stepTimer;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        wasGrounded = isGrounded;
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
            velocity.y = -2f;

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 move = transform.right * x + transform.forward * z;

        bool isMoving = move.magnitude > 0.1f;
        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float currentSpeed = isRunning ? runSpeed : walkSpeed;

        controller.Move(move * currentSpeed * Time.deltaTime);

        // 🎧 Pasos
        if (isGrounded && isMoving)
        {
            stepTimer += Time.deltaTime;
            float interval = isRunning ? runStepInterval : stepInterval;

            if (stepTimer >= interval)
            {
                PlayRandomFootstep();
                stepTimer = 0f;
            }
        }
        else
        {
            stepTimer = 0f;
        }

        // 🎧 Salto
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            if (jumpClip != null)
                audioSource.PlayOneShot(jumpClip);
        }

        // 🎧 Aterrizaje
        if (!wasGrounded && isGrounded)
        {
            if (landClip != null)
                audioSource.PlayOneShot(landClip);
        }

        // Aplicar gravedad
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    void PlayRandomFootstep()
    {
        if (footstepClips.Length == 0 || audioSource == null) return;

        AudioClip clip = footstepClips[Random.Range(0, footstepClips.Length)];
        audioSource.PlayOneShot(clip);
    }
}
