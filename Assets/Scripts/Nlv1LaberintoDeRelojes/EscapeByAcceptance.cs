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
        // Dirección horizontal hacia atrás
        Vector3 direccionAtras = -new Vector3(player.forward.x, 0f, player.forward.z).normalized;

        // Posición detrás del jugador
        Vector3 spawnPosition = player.position + direccionAtras * 0.4f; 

        // Ajuste de altura
        spawnPosition.y = player.position.y + alturaOffset;

        // Instancia la puerta
        Instantiate(exitDoorPrefab, spawnPosition, exitDoorPrefab.transform.rotation);

        enemy.SetActive(false);
        doorSpawned = true;

        Debug.Log("Puerta generada justo detrás del jugador.");
    }

}
