using UnityEngine;

public class LaberintoSpawner : MonoBehaviour
{
    public GameObject laberintoPrefab;

    void Start()
    {
        Vector3 spawnPosition = new Vector3(-53.7f, -28.6f, -45.6f);
        Instantiate(laberintoPrefab, spawnPosition, Quaternion.identity);

    }
}
