using UnityEngine;

public class GeneradorDeMuros : MonoBehaviour
{
    public GameObject prefabMuro;
    public Transform[] puntosSpawn; // posiciones donde aparecen los muros
    public Transform objetivoEmpuje; // normalmente el jugador
    public float fuerzaEmpuje = 800f;

    public float intervalo = 2f;
    private float tiempoSiguiente = 0f;

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
        Transform punto = puntosSpawn[Random.Range(0, puntosSpawn.Length)];
        GameObject muro = Instantiate(prefabMuro, punto.position, Quaternion.identity);

        // Escala aleatoria
        float ancho = Random.Range(1f, 3f);
        float alto = Random.Range(1f, 2f);
        muro.transform.localScale = new Vector3(ancho, alto, 1f);

        // Empuje hacia el jugador
        Rigidbody rb = muro.GetComponent<Rigidbody>();
        if (rb != null)
        {
            Vector3 direccion = (objetivoEmpuje.position - punto.position).normalized;
            rb.AddForce(direccion * fuerzaEmpuje, ForceMode.Impulse);
        }

        Destroy(muro, 10f); // evitar acumulación
    }
}
