using UnityEngine;
using UnityEngine.AI;

public class FloatingEnemy : MonoBehaviour
{
    public Transform player;
    public float floatAmplitude = 0.5f;
    public float floatFrequency = 1f;
    public float moveSpeed = 6f;
    public float alturaFlotante = 1.5f;


    private NavMeshAgent agent;
    private Vector3 basePosition;

    private Quaternion initialRotation;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = moveSpeed;
        agent.acceleration = 12f;
        agent.angularSpeed = 360f;
        agent.updatePosition = false;
        agent.updateRotation = true;

        basePosition = new Vector3(transform.position.x, alturaFlotante, transform.position.z);
        initialRotation = transform.rotation;

        if (player == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
                player = playerObj.transform;
        }
    }


    void Update()
    {
        if (player != null)
        {
            agent.SetDestination(player.position);
        }

        // Movimiento horizontal controlado por NavMeshAgent
        Vector3 nextPos = agent.nextPosition;

        // Movimiento vertical flotante
        float floatY = Mathf.Sin(Time.time * floatFrequency) * floatAmplitude;
        nextPos.y = basePosition.y + floatY;

        // Aplicamos la posición combinada
        transform.position = nextPos;

        // Mantener orientación vertical, solo rotar en Y
        Vector3 currentEuler = initialRotation.eulerAngles;
        transform.rotation = Quaternion.Euler(currentEuler.x, transform.rotation.eulerAngles.y, currentEuler.z);
    }


}
