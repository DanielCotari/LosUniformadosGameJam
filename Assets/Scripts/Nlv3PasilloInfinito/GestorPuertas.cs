using UnityEngine;

public class GestorPuertas : MonoBehaviour
{
    public AudioClip[] sonidosDisponibles;
    private PuertaRaycast[] todasLasPuertas;

    void Start()
    {
        todasLasPuertas = FindObjectsOfType<PuertaRaycast>();

        // Elegir una puerta silenciosa aleatoria
        int indexSilenciosa = Random.Range(0, todasLasPuertas.Length);

        for (int i = 0; i < todasLasPuertas.Length; i++)
        {
            if (i == indexSilenciosa)
            {
                continue;
            }

            int clipIndex = Random.Range(0, sonidosDisponibles.Length);
            todasLasPuertas[i].AsignarSonido(sonidosDisponibles[clipIndex]);
        }
    }

}
