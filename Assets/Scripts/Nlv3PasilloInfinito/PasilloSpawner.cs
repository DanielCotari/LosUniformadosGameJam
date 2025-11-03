using UnityEngine;

public class PasilloSpawner : MonoBehaviour
{
    public GameObject pasilloCompletoPrefab;
    public Transform player;
    public float spawnDistance = 5f;
    public float prefabOffset = 10f;
    public float xOffset = -2f;

    private Transform[] puntosDeSalida;
    private GameObject prefabDerechaInstanciado = null;
    private GameObject prefabIzquierdaInstanciado = null;

    void Start()
    {
        if (player == null)
            player = GameObject.FindWithTag("Player")?.transform;

        Transform derecha = GameObject.Find("ExitPointDerecha")?.transform;
        Transform izquierda = GameObject.Find("ExitPointIzquierda")?.transform;

        puntosDeSalida = new Transform[] { derecha, izquierda };
    }

    void Update()
    {
        if (player == null || puntosDeSalida == null) return;

        foreach (Transform punto in puntosDeSalida)
        {
            if (punto == null) continue;

            if (Vector3.Distance(player.position, punto.position) < spawnDistance)
            {
                string lado = punto.name;

                if (lado == "ExitPointDerecha")
                {
                    if (prefabIzquierdaInstanciado != null)
                    {
                        Destroy(prefabIzquierdaInstanciado);
                        prefabIzquierdaInstanciado = null;
                    }

                    if (prefabDerechaInstanciado == null)
                        prefabDerechaInstanciado = SpawnDesde(punto);
                }
                else if (lado == "ExitPointIzquierda")
                {
                    if (prefabDerechaInstanciado != null)
                    {
                        Destroy(prefabDerechaInstanciado);
                        prefabDerechaInstanciado = null;
                    }

                    if (prefabIzquierdaInstanciado == null)
                        prefabIzquierdaInstanciado = SpawnDesde(punto);
                }
            }
        }
    }

    GameObject SpawnDesde(Transform puntoSalida)
    {
        Vector3 offsetFrontal = puntoSalida.forward * prefabOffset;
        Vector3 offsetLateral = puntoSalida.right * xOffset;

        Vector3 posicionFinal = puntoSalida.position + offsetFrontal + offsetLateral;
        posicionFinal.y = 8.736142f;

        return Instantiate(
            pasilloCompletoPrefab,
            posicionFinal,
            puntoSalida.rotation
        );
    }
}
