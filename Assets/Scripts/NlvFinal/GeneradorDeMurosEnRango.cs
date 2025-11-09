using UnityEngine;

public class GeneradorDeMurosEnRango : MonoBehaviour
{
    public GameObject[] prefabsMuros;
    public float intervalo = 2f;
    public float velocidadMuro = 10f;
    public float fuerzaEmpujeJugador = 500f;

    private float tiempoSiguiente;

    private Vector3 puntoA = new Vector3(-36.34f, 25.9f, -143.49f);
    private Vector3 puntoB = new Vector3(-37.71f, 26.09f, -127.74f);
    private float xDestino = -72.73f;

    void Update()
    {
        if (Time.time >= tiempoSiguiente)
        {
            InstanciarMuro();
            tiempoSiguiente = Time.time + intervalo;
        }
    }

    void InstanciarMuro()
    {
        float xMin = Mathf.Min(puntoA.x, puntoB.x);
        float xMax = Mathf.Max(puntoA.x, puntoB.x);
        float zMin = Mathf.Min(puntoA.z, puntoB.z);
        float zMax = Mathf.Max(puntoA.z, puntoB.z);
        float yFijo = 28f;

        float xAleatorio = Random.Range(xMin, xMax);
        float zAleatorio = Random.Range(zMin, zMax);
        Vector3 spawnPos = new Vector3(xAleatorio, yFijo, zAleatorio);

        GameObject prefab = prefabsMuros[Random.Range(0, prefabsMuros.Length)];
        GameObject muro = Instantiate(prefab, spawnPos, Quaternion.identity);

        // Agregamos el script de movimiento y empuje
        MuroEmpujador script = muro.AddComponent<MuroEmpujador>();
        script.xDestino = xDestino;
        script.velocidad = velocidadMuro;
        script.fuerzaEmpuje = fuerzaEmpujeJugador;

        Destroy(muro, 10f);
    }
}
