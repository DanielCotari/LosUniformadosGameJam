using TMPro;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class JefeCritico : MonoBehaviour
{
    public GameObject prefabPalabra;
    public GameObject prefabGrito;
    public GameObject[] prefabsMuros;
    public Transform puntoSpawn;
    public Transform jugador;

    public string[] frases = {
        "Eres un inútil",
        "Nunca serás suficiente",
        "Todos te juzgan"
    };

    public float fuerza = 500f;
    public float intervalo = 3f;
    public int cantidad = 3;
    public float separacion = 2f;
    public float desaceleracion = 2f;
    public float probabilidadVuelo = 0.3f;
    public float impulsoVertical = 200f;
    public float fuerzaEmpujeMuros = 800f;

    private float tiempoSiguienteAtaque;
    private float tiempoInicioFase;
    private float tiempoInicioFase2;
    private int faseActual;

    void Start()
    {
      
        faseActual = 1; // ← aseguramos que comience en fase 1
        tiempoInicioFase = Time.time;
        tiempoSiguienteAtaque = Time.time + intervalo;
    }

    void Update()
    {
        if (faseActual == 1 && Time.time - tiempoInicioFase >= 30f)
        {
            CambiarAFase2();
        }

        if (faseActual == 2 && Time.time - tiempoInicioFase2 >= 30f)
        {
            CambiarAFase3();
        }

        if (Time.time >= tiempoSiguienteAtaque)
        {
            if (faseActual == 1)
            {
                LanzarLineaDeCriticas();
            }
            else if (faseActual == 2)
            {
                LanzarGritoYDesaliento();
            }
            else if (faseActual == 3)
            {
                LanzarMurosEmpuje();
            }

            tiempoSiguienteAtaque = Time.time + intervalo;
        }
        VerificarFinal();
    }

    void CambiarAFase2()
    {
        faseActual = 2;
        tiempoInicioFase2 = Time.time;
        Debug.Log("Fase 2 activada: máscara social y gritos de desaliento");
    }

    void CambiarAFase3()
    {
        faseActual = 3;
        Debug.Log("Fase 3 activada: barreras protectoras y empujes");
    }

    void LanzarLineaDeCriticas()
    {
        if (faseActual != 1) return;

        for (int i = 0; i < cantidad; i++)
        {
            string frase = frases[Random.Range(0, frases.Length)];
            Vector3 offset = new Vector3((i - cantidad / 2f) * separacion, 0, 0);
            Vector3 spawnPos = puntoSpawn.position + offset;

            GameObject palabra = Instantiate(prefabPalabra, spawnPos, Quaternion.identity);
            palabra.name = frase;

            TextMeshPro tmp = palabra.GetComponent<TextMeshPro>();
            if (tmp != null) tmp.text = frase;

            Vector3 direccion = (jugador.position - spawnPos).normalized;
            Rigidbody rb = palabra.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddForce(direccion * fuerza, ForceMode.Impulse);
                rb.drag = desaceleracion;
                if (Random.value < probabilidadVuelo)
                    rb.AddForce(Vector3.up * impulsoVertical, ForceMode.Impulse);
            }

            Destroy(palabra, 10f);
        }
    }

    void LanzarGritoYDesaliento()
    {
        if (faseActual != 2) return;

        if (prefabGrito != null)
        {
            Instantiate(prefabGrito, transform.position, Quaternion.identity);
        }

        for (int i = 0; i < cantidad; i++)
        {
            string frase = frases[Random.Range(0, frases.Length)];
            Vector3 offset = new Vector3((i - cantidad / 2f) * separacion, 0, 0);
            Vector3 spawnPos = puntoSpawn.position + offset;

            GameObject palabra = Instantiate(prefabPalabra, spawnPos, Quaternion.identity);
            palabra.name = frase;

            TextMeshPro tmp = palabra.GetComponent<TextMeshPro>();
            if (tmp != null) tmp.text = frase;

            Vector3 direccion = (jugador.position - spawnPos).normalized;
            Rigidbody rb = palabra.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddForce(direccion * fuerza, ForceMode.Impulse);
                rb.drag = desaceleracion;
                if (Random.value < probabilidadVuelo)
                    rb.AddForce(Vector3.up * impulsoVertical, ForceMode.Impulse);
            }

            palabra.AddComponent<Ralentizador>();
            Destroy(palabra, 10f);
        }
    }

    void LanzarMurosEmpuje()
    {
        if (faseActual != 3) return;

        if (prefabsMuros == null || prefabsMuros.Length == 0)
        {
            Debug.LogWarning("No hay prefabs de muros asignados.");
            return;
        }

        float xFijo = -36.34f;
        float yFijo = 25.9f;
        float zMin = -143.49f;
        float zMax = -127.74f;
        float zAleatorio = Random.Range(zMin, zMax);
        Vector3 spawnPos = new Vector3(xFijo, yFijo, zAleatorio);

        GameObject prefab = prefabsMuros[Random.Range(0, prefabsMuros.Length)];
        GameObject muro = Instantiate(prefab, spawnPos, Quaternion.identity);

        Rigidbody rb = muro.GetComponent<Rigidbody>();
        if (rb != null)
        {
            Vector3 direccion = (jugador.position - spawnPos).normalized;
            rb.AddForce(direccion * fuerzaEmpujeMuros, ForceMode.Impulse);
        }

        Destroy(muro, 10f);
    }

    public Transform fin; // ← asigná el GameObject vacío en el Inspector
    public VideoPlayer videoPlayer; // ← asigná el VideoPlayer en el Inspector
    public float distanciaActivacion = 1.5f;

    private bool videoMostrado = false;



    void VerificarFinal()
    {
        if (faseActual != 3 || videoMostrado || fin == null || jugador == null)
            return;

        float distancia = Vector3.Distance(jugador.position, fin.position);
        Debug.Log("Distancia al punto fin: " + distancia);

        if (distancia <= distanciaActivacion)
        {
            videoMostrado = true;
            Debug.Log("¡Jugador llegó al punto final!");
            SceneManager.LoadScene("CinematicaFinal"); // ← Cambia a la escena del video
        }
    }

}
