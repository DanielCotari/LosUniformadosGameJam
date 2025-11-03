using UnityEngine;

public class WallVisionBlocker : MonoBehaviour
{
    public float rayDistance = 1.5f;
    public LayerMask wallLayer;
    public float moveSpeed = 5f;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 moveDir = new Vector3(horizontal, 0, vertical).normalized;

        if (moveDir == Vector3.zero) return;

        bool wallDetected = false;

        // Ángulos en abanico: -60°, -30°, 0°, +30°, +60°
        float[] angles = { -60f, -30f, 0f, 30f, 60f };

        foreach (float angle in angles)
        {
            Vector3 rotatedDir = Quaternion.AngleAxis(angle, Vector3.up) * moveDir;
            Ray ray = new Ray(transform.position, rotatedDir);
            Debug.DrawRay(ray.origin, ray.direction * rayDistance, Color.red);

            if (Physics.Raycast(ray, rayDistance, wallLayer))
            {
                wallDetected = true;
                break;
            }
        }

        if (!wallDetected)
        {
            Vector3 newPos = rb.position + moveDir * moveSpeed * Time.deltaTime;
            rb.MovePosition(newPos);
        }
        else
        {
            Debug.Log("Pared detectada en cualquier ángulo: movimiento bloqueado");
        }
    }
}
