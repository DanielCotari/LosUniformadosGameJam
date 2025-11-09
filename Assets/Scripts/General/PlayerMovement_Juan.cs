using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement_Juan : MonoBehaviour
{
    [Header("Movimiento")]
    public float walkSpeed = 4f;
    public float runSpeed = 8f;
    public float jumpHeight = 2f;
    public float gravity = -9.81f;

    [Header("Detección de suelo")]
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    [Header("Sonidos")]
    public AudioSource audioMovimiento;
    public AudioClip clipCaminar;
    public AudioClip clipCorrer;
    public AudioClip clipSaltar;
    public float intervaloPasos = 0.5f;

    private CharacterController controller;
    private Vector3 velocity;
    private bool isGrounded;
    private float tiempoUltimoPaso = 0f;

    // Ralentización
    private float velocidadOriginalWalk;
    private float velocidadOriginalRun;
    private bool ralentizado = false;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        velocidadOriginalWalk = walkSpeed;
        velocidadOriginalRun = runSpeed;
    }

    void Update()
    {
        // Verifica si está tocando el suelo
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
            velocity.y = -2f;

        // Movimiento básico
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 move = transform.right * x + transform.forward * z;

        // Correr con Shift
        bool estaCorriendo = Input.GetKey(KeyCode.LeftShift);
        float currentSpeed = estaCorriendo ? runSpeed : walkSpeed;
        controller.Move(move * currentSpeed * Time.deltaTime);

        // Sonido de pasos
        if (isGrounded && move.magnitude > 0.1f)
        {
            if (Time.time > tiempoUltimoPaso + intervaloPasos)
            {
                AudioClip clip = estaCorriendo ? clipCorrer : clipCaminar;
                if (audioMovimiento != null && clip != null)
                {
                    audioMovimiento.PlayOneShot(clip);
                    tiempoUltimoPaso = Time.time;
                }
            }
        }

        // Saltar
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            if (audioMovimiento != null && clipSaltar != null)
            {
                audioMovimiento.PlayOneShot(clipSaltar);
            }
        }

        // Aplicar gravedad
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    // Método público para ralentizar al jugador
    public void Ralentizar(float factorReduccion, float duracion)
    {
        if (!ralentizado)
        {
            walkSpeed *= factorReduccion;
            runSpeed *= factorReduccion;
            ralentizado = true;
            Invoke("RestaurarVelocidad", duracion);
        }
    }

    void RestaurarVelocidad()
    {
        walkSpeed = velocidadOriginalWalk;
        runSpeed = velocidadOriginalRun;
        ralentizado = false;
    }
}
