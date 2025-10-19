using UnityEngine;

public class ChunkEstacion : MonoBehaviour
{
    public Transform jugador;
    public float distanciaActivacion = 20f;

    void Update()
    {
        float distancia = Vector3.Distance(transform.position, jugador.position);
        gameObject.SetActive(distancia < distanciaActivacion);
    }
}
