using UnityEngine;

public class EscapeByAcceptance : MonoBehaviour
{
    public Transform player;
    public GameObject enemy;
    public GameObject exitDoorPrefab;

    public float waitBeforeCheck = 10f;
    public float requiredStillTime = 3f;
    public float movementThreshold = 0.05f;
    public float alturaOffset = 0.37f;

    private float timeSinceEnemyAppeared = 0f;
    private float stillTimer = 0f;
    private Vector3 lastPlayerPosition;
    private bool doorSpawned = false;

    void Start()
    {
        lastPlayerPosition = player.position;
    }

    void Update()
    {
        if (!enemy.activeInHierarchy) return;

        timeSinceEnemyAppeared += Time.deltaTime;

        if (timeSinceEnemyAppeared >= waitBeforeCheck && !doorSpawned)
        {
            float movement = Vector3.Distance(player.position, lastPlayerPosition);

            if (movement < movementThreshold)
            {
                stillTimer += Time.deltaTime;

                if (stillTimer >= requiredStillTime)
                {
                    SpawnExitDoor();
                }
            }
            else
            {
                stillTimer = 0f;
            }

            lastPlayerPosition = player.position;
        }
    }

    void SpawnExitDoor()
    {
        // Elimina la componente vertical del forward para evitar que la puerta aparezca arriba
        Vector3 forwardFlat = new Vector3(player.forward.x, 0f, player.forward.z).normalized;

        // Calcula la posición detrás del jugador en el plano horizontal
        Vector3 spawnPosition = player.position - forwardFlat * 2f;
        spawnPosition.y = player.position.y + alturaOffset; // Ajusta la altura aquí

        // Instancia la puerta con rotación del prefab
        Instantiate(exitDoorPrefab, spawnPosition, exitDoorPrefab.transform.rotation);

        // Desactiva al enemigo: el tiempo deja de perseguirte
        enemy.SetActive(false);
        doorSpawned = true;

        Debug.Log("Has aceptado el paso del tiempo. La salida ha aparecido.");
    }
}
